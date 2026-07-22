using System;
using System.Collections;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player {

    public struct InteractEvent : IEvent {
        public bool ShowInteraction;
        public Interaction Interaction;
        public string ObjectName;
    }
    
    public class PlayerInteract : MonoBehaviour {
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
        
        private bool canPlayerInteract = false;
        
        [HideInInspector] public bool triggerShard = false;
        [HideInInspector] public bool triggerDoor = false;
        [HideInInspector] public bool triggerFailedDrop = false;

        private PlayerController player;
        private CountdownTimer usingLockedDoor;
        private CountdownTimer usingDoor;
        private CountdownTimer interactCooldown;
        private const float TimerToUseDoor = 0.15f;
        
        private Interaction interactionType;
        private RaycastHit wallInBetween;
        private LayerMask wallLayerMask;
        
        private bool isFocus = false;

        private bool validationInputHold;
        private float validationInputTime;
        
        private bool canInteract;
        public bool CanInteract {
            get => canInteract;
            private set {
                if(canInteract == value) return;
                
                canInteract = value;
                EventBus<InteractEvent>.Raise(new InteractEvent {
                    ShowInteraction = value,
                    Interaction = interactionType
                });
            }
        }
        
        public int Size { get; private set; }

        #region Initialization

        private void Awake() {
            if(TryGetComponent(out PlayerController _player)) player = _player;
            else Debug.LogWarning("[PlayerController] No PlayerController found");
            
            Size = 0;

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
            InputsBrain.Instance.OnInteract -= Interact;
            InputsBrain.Instance.OnSecondaryInteract -= SecondaryInteract;
        }

        #endregion
        
        private void Interact(InputAction.CallbackContext ctx) {
            if(triggerFailedDrop) return;
            
            if(interactCooldown.IsRunning) return;
            
            if(CanGrab())
                GrabObject();
            else if (CanPickup())
                PickUpItem();
            else if(CanDrop())
                DropObject();
            else if (CanContextualInteract()) {
                potentialInteraction?.OnInteract(ObjectInteraction.Contextual);
                // potentialInteraction = null;
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
        }

        public void HoldObject(bool doHold, BaseObject heldObject = null) {
            if (doHold) {
                HasObject = true;
                currentInteraction = heldObject;
            }
            else {
                HasObject = false;
                currentInteraction = null;
            }
        }

        private void DropObject() {
            currentInteraction?.OnInteract(ObjectInteraction.Drop);
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
            
            if (isFocus) return;
            
            HandleInteraction();
            SetPlayerInteraction();
        }
        
        #region UpdateInteraction

        void HandleInteraction() {
            if (!canPlayerInteract) return;

            Size = Physics.OverlapBoxNonAlloc(interactCenterZone.position, interactZoneSize, results,
                Quaternion.identity, interactLayerMask);

            switch (Size) {
                case 0:
                    potentialInteraction = null;
                    return;
                case 1:
                    potentialInteraction = results[0].GetComponent<BaseObject>();
                    break;
                case > 1:
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

                    break;
                default:
                    potentialInteraction = null;
                    break;
            }

            if (!HasObject) {// Check si le joueur possède un objet + Check si un mur est entre le joueur et l'objet
                if (!potentialInteraction) return;

                var boxCollider = potentialInteraction.GetCollider() as BoxCollider;
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
            if (potentialInteraction is null) {
                CanInteract = false;
                return;
            }
            
            UpdatePossibleInteraction();
            
            if (potentialInteraction.CanBeInteractedWith())
                CanInteract = canPlayerInteract && Size > 0;
            else {
                CanInteract = false;
                return;
            }

            if (potentialInteraction && player.cinemachineBrain.OutputCamera)
                HudManager.InteractionSetPosition( potentialInteraction.GetUIPosition());
        }

        private void UpdatePossibleInteraction() { //Get le type interaction dans le base object -> Get Component est pas opti surtout dans une update
            
            if (potentialInteraction == null || interactCooldown.IsRunning) {
                CanInteract = false;
                interactionType = Interaction.None;
                RaiseInteraction();
                return;
            }
            
            switch (potentialInteraction.GetObjectType) {
                case ObjectType.Moveable:
                    interactionType = Interaction.Grab;
                    RaiseInteraction();
                    return;
                case ObjectType.Door:
                    interactionType = Interaction.UseDoor;
                    RaiseInteraction();
                    return;
                case ObjectType.Collectable:
                    interactionType = Interaction.Grab;
                    RaiseInteraction();
                    return;
                case ObjectType.Usable:
                    interactionType = Interaction.Grab;
                    RaiseInteraction();
                    break;
                case ObjectType.Inspectable:
                    interactionType = Interaction.Dialogue;
                    RaiseInteraction();
                    return;
                case ObjectType.MemoryFrame:
                    interactionType = Interaction.Grab;
                    RaiseInteraction();
                    return;
                case ObjectType.SimpleInteraction:
                    interactionType = Interaction.Grab;
                    RaiseInteraction();
                    return;
                case ObjectType.None:
                default:
                    interactionType = Interaction.None;
                    return;
            }
        }

        #endregion
        
        private void RaiseInteraction() {
            EventBus<InteractEvent>.Raise(new InteractEvent {
                ShowInteraction = canInteract,
                Interaction = interactionType,
                ObjectName = potentialInteraction?.ObjectName
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
        }
        
        public void SetDropObject() {
            HasObject = false;
            currentInteraction = null;
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
                return CanInteract && collectable.CanBeGrab();
            
            return false;
        }

        private bool CanDrop() {
            if (potentialInteraction == null || potentialInteraction.GetObjectType is ObjectType.None or ObjectType.SimpleInteraction) return IsCarrying();

            return false;
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
            this.isFocus = isFocus;
            
            if (isFocus)
                potentialInteraction = obj;
        }

        private IEnumerator LoadScene(SceneSettings toLoad, Vector3 position) {
            yield return new WaitForSeconds(player.useDoorClip.length);
            _ = GameSceneLoaderSystem.Instance.LoadGameplaySceneAsync(toLoad);
        }
    }
}