using System.Collections;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player {

    public struct InteractEvent : IEvent {
        public bool ShowInteraction;
        public string ObjectName;
        public Vector3 Position;
    }
    
    public class PlayerInteract : MonoBehaviour {
        public enum DropType {
            Heavy,
            Light,
            Inventory
        }
        
        [Header("UI")] 
        [SerializeField] private Vector2 uiOffset;
        
        [Header("Settings")]
        [SerializeField] public Transform objectPos;
        [SerializeField] public Transform interactCenterZone;
        [SerializeField] public Vector3 interactZoneSize;
        [SerializeField] private LayerMask interactLayerMask;

        //Pre allocate space for collider (10 will be completely sufficient)
        private readonly Collider[] results = new Collider[10];
        private BaseObject potentialInteraction;
        private BaseObject currentInteraction;
        private BaseObject memoryInteraction;
        
        public bool HasObject { get; private set; }
        public bool HasItemObject { get; private set; }
        public bool HasDroppedObject { get; private set; }
        public DropType dropType { get; private set; }
        
        private bool canPlayerInteract = false;

        public float pickUpObjectYPos;
        
        [HideInInspector] public bool triggerShard = false;
        [HideInInspector] public bool triggerDoor = false;
        [HideInInspector] public bool triggerFailedDrop = false;

        private PlayerController player;
        private CountdownTimer usingLockedDoor;
        private CountdownTimer usingDoor;
        private CountdownTimer interactCooldown;
        private const float TimerToUseDoor = 0.15f;
        
        private RaycastHit wallInBetween;
        private LayerMask wallLayerMask;
        
        public bool IsFocus { get; private set; }
        public bool IsInMemory { get; private set; }
        public bool CanGlassInteract { get; private set; }
        public bool TriggerPickUpItem;
        
        private bool validationInputHold;
        private float validationInputTime;
        
        private bool canInteract;
        public bool CanInteract {
            get => canInteract;
            private set {
                canInteract = value;

                if (canInteract == false) {
                    EventBus<InteractEvent>.Raise(new InteractEvent {
                        ShowInteraction = false
                    });
                }
                else {
                    RaiseInteraction();
                }
            }
        }
        
        public int Size { get; private set; }

        #region Initialization

        private void Awake() {
            if(TryGetComponent(out PlayerController _player)) player = _player;
            else Debug.LogWarning("[PlayerController] No PlayerController found");
            
            Size = 0;

            CanGlassInteract = true;

            usingLockedDoor = new CountdownTimer(TimerToUseDoor);
            usingDoor = new CountdownTimer(0.4f);
            interactCooldown = new CountdownTimer(0.5f);
            
            wallLayerMask = LayerMask.GetMask("Wall");
        }

        private void OnEnable() {
            InputsBrain.Instance.OnInteract += Interact;
            InputsBrain.Instance.OnSecondaryInteract += SecondaryInteract;
        }

        private void OnDisable() {
            if (InputsBrain.HasInstance) {
                InputsBrain.Instance.OnInteract -= Interact;
                InputsBrain.Instance.OnSecondaryInteract -= SecondaryInteract;
            }
        }

        #endregion
        
        private void Interact(InputAction.CallbackContext ctx) {
            if(ctx.canceled) return;
            
            if(triggerFailedDrop || IsInMemory) return;
            if(interactCooldown.IsRunning) return;
            
            if(CanGrab())
                GrabObject();
            else if (CanPickup())
                PickUpItem();
            else if(CanDrop())
                DropObject();
            else if (CanContextualInteract()) {
                potentialInteraction?.OnInteract(ObjectInteraction.Contextual);
            }
            else
                Debug.Log("[PlayerInteract] No object to interact with...");
            
            interactCooldown.Start();
        }

        private void SecondaryInteract(InputAction.CallbackContext ctx) {
            if (ctx.performed) validationInputHold = true;

            if (ctx.canceled && validationInputHold) {
                validationInputHold = false;
                validationInputTime = 0;
            }
        }
        
        #region InteractionMethods

        private void GrabObject() {
            potentialInteraction.OnInteract(ObjectInteraction.Grab);
        }
        
        private void PickUpItem() {
            potentialInteraction.OnInteract(ObjectInteraction.Grab);
            TriggerPickUpItem = true;
        }

        public void HoldObject(bool doHold, BaseObject heldObject = null) {
            if (doHold) {
                pickUpObjectYPos = transform.position.y;
                HasItemObject = true;
                HasObject = true;
                currentInteraction = heldObject;
                if(currentInteraction.GetInteract is CollectableAttribute move)
                    player.PlayerIK.SetLightObject( move.leftEdge);
                HasDroppedObject = false;
            }
            else {
                pickUpObjectYPos = transform.position.y;
                HasItemObject = false;
                HasObject = false;
                currentInteraction = null;
                PutInInventory();
            }
        }

        private void DropObject() {
            currentInteraction?.OnInteract(ObjectInteraction.Drop);
            if(HasItemObject) HasItemObject = false;
        }
        #endregion

        private void HandleInteractRotation(Vector3 playerDir) {
            var newPos = transform.position + playerDir * interactZoneSize.z;
            interactCenterZone.position = Vector3.Lerp(interactCenterZone.position, newPos, player.GetRotationSpeed() * Time.deltaTime);
        }
        
        public void HandleUpdate(Vector3 playerDir) {
            HandleInteractRotation(playerDir);

            if (validationInputHold) {
                validationInputTime += Time.deltaTime;

                if (validationInputTime >= 1 && potentialInteraction) {
                    if (potentialInteraction.GetObjectType is ObjectType.MemoryFrame) {
                        potentialInteraction.OnInteract(ObjectInteraction.Validate);
                        validationInputHold = false;
                        validationInputTime = 0;
                    }
                }
            }
            
            if (IsFocus || IsInMemory) return;
            
            HandleInteraction();
            SetPlayerInteraction();
        }
        
        #region UpdateInteraction

        void HandleInteraction() {
            if (!canPlayerInteract) return;
            if(Time.frameCount % 4 != 0) return;
            
            Size = Physics.OverlapBoxNonAlloc(interactCenterZone.position, interactZoneSize, results,
                Quaternion.identity, interactLayerMask);

            if (Size == 1) {
                potentialInteraction = results[0].GetComponent<BaseObject>();
            }
            else if (Size > 1) {
                var closestDist = 10f;

                for (var i = 0; i < Size; i++) {
                    if (results[i].TryGetComponent(out BaseObject b)) {
                        if (!b.CanBeInteractedWith()) continue;
                        var dist = Vector3.Distance(b.transform.position, transform.position);

                        if (dist < closestDist) {
                            closestDist = dist;
                            potentialInteraction = b;
                        }
                    }
                }
            }
            else {
                potentialInteraction = null;
                return;
            }

            if (!HasObject) {// Check si le joueur possède un objet + Check si un mur est entre le joueur et l'objet
                if (!potentialInteraction || !potentialInteraction.transform) return;

                var boxCollider = potentialInteraction.GetCollider() as BoxCollider;

                if (boxCollider == null) return;
                
                var dir = (potentialInteraction.transform.TransformPoint(boxCollider.center) - transform.position).normalized;
                var dist = Vector3.Distance(transform.TransformPoint(boxCollider.center), transform.position);
                
                var hasHit = Physics.Raycast(transform.position, dir, out wallInBetween, dist, wallLayerMask);
                if (hasHit && wallInBetween.collider != potentialInteraction.GetCollider() as BoxCollider) {
                    potentialInteraction = null;
                }

                return;
            }
        
            // Si le joueur possède un objet et que son interaction potentielle est la même que la current, alors il reset la potential
            if (potentialInteraction == currentInteraction) potentialInteraction = null;
        }

        void SetPlayerInteraction() {
            CanInteract = canPlayerInteract && potentialInteraction != null && potentialInteraction.CanBeInteractedWith();
        }

        #endregion
        
        private void RaiseInteraction() {
            EventBus<InteractEvent>.Raise(new InteractEvent {
                ShowInteraction = CanInteract,
                ObjectName = potentialInteraction.ObjectName,
                Position = potentialInteraction.GetUIPosition(),
            });
        }
        
        public void SetInteract(bool interact) {
            canPlayerInteract = interact;
        }

        public BaseObject GetCurrentInteractable() {
            return currentInteraction;
        }
        
        public void SetGrabbedObject(BaseObject interaction) {
            HasObject = true;
            currentInteraction = interaction;
            if(currentInteraction.GetInteract is MovableAttribute move)
                player.PlayerIK.SetHoldingState(move.rightEdge, move.leftEdge);
        }
        
        public void SetDropObject(bool heavy) {
            HasObject = false;
            currentInteraction = null;
            dropType = heavy ? DropType.Heavy : DropType.Light;
            HasDroppedObject = true;
        }

        public void PutInInventory() {
            dropType = DropType.Inventory;
            HasDroppedObject = true;
        }
        
        public void ResetDrop() {
            HasDroppedObject = false;
        }
        
        public void SetDropObjectDebug() {
            HasObject = false;
            currentInteraction?.OnInteract(ObjectInteraction.DropNoTimer);
            currentInteraction = null;
        }
        
        private bool CanGrab() {
            if(potentialInteraction == null) return false;

            if (potentialInteraction.TryGetComponent(out MovableAttribute moveable))
                return CanInteract && !HasObject && currentInteraction == null && moveable.CanBeGrab();

            return false;
        }

        private bool CanPickup() {
            if(potentialInteraction == null) return false;
            
            if(potentialInteraction.TryGetComponent(out CollectableAttribute collectable))
                return CanInteract && !HasObject && currentInteraction == null && collectable.CanBeGrab();
            
            return false;
        }

        private bool CanDrop() {
            return IsCarrying();
        }

        private bool CanContextualInteract() {
            return CanInteract && potentialInteraction && potentialInteraction.GetObjectType is not ObjectType.None;
        }

        public bool IsCarrying() {
            return currentInteraction != null && HasObject;
        }

        public void StartUsingLockedDoor() {
            usingLockedDoor.Start();
        }
        
        public bool UsingLockedDoor() {
            return usingLockedDoor.IsRunning;
        }
        public void StartUsingDoor() {
            usingDoor.Start();
        }
        
        public bool IsUsingDoor() {
            return usingDoor.IsRunning;
        }

        public void TriggerBigDoor(SceneSettings toLoad, Vector3 position) {
            triggerDoor = true;
            StartCoroutine(LoadScene(toLoad, position));
        }

        public void SetIsFocus(bool isFocus, BaseObject obj = null) {
            IsFocus = isFocus;
            
            if (isFocus)
                potentialInteraction = obj;
        }

        public void SetGlassInteraction(bool canInteract) {
            CanGlassInteract = canInteract;   
        }
        
        public void SetInMemory(bool inMemory) => IsInMemory = inMemory;

        private IEnumerator LoadScene(SceneSettings toLoad, Vector3 position) {
            yield return new WaitForSeconds(player.useDoorClip.length);
            _ = GameSceneLoaderSystem.Instance.LoadGameplaySceneAsync(toLoad);
        }
    }
}