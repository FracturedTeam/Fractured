using System;
using System.Collections;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Interfaces;
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
        private InputsBrain inputsBrain;

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
        private bool canInteract;
        private bool inMemory = false;
        private bool inPressurePlate = false;
        [HideInInspector] public bool triggerShard = false;
        [HideInInspector] public bool triggerDoor = false;
        [HideInInspector] public bool triggerFailedDrop = false;

        private PlayerController player;
        private CountdownTimer usingLockedDoor;
        private CountdownTimer usingDoor;
        private CountdownTimer interactCooldown;
        private float timerToUseDoor = 0.15f;
        
        private Interaction interactionType;

        private float interactDuration = 0;
        private float holdInteractionNeeded = 0.5f;
        private bool interactionHold = false;
        private bool hasRemoved = false;
        
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
        
        public int size { get; private set; }

        #region Initialization

        private void Awake() {
            if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");

            if(TryGetComponent(out PlayerController _player)) player = _player;
            else Debug.LogWarning("[PlayerController] No PlayerController found");
            
            size = 0;

            usingLockedDoor = new CountdownTimer(timerToUseDoor);
            usingDoor = new CountdownTimer(0.4f);
            interactCooldown = new CountdownTimer(0.5f);
        }

        private void OnEnable() {
            inputsBrain.OnInteract += Interact;
        }

        private void OnDisable() {
            inputsBrain.OnInteract -= Interact;
        }

        #endregion
        
        private void Interact(InputAction.CallbackContext ctx) {
            if(triggerFailedDrop) return;
            
            if (ctx.performed) {
                interactionHold = true;
                return;
            }

            if (ctx.canceled) interactionHold = false;

            if (hasRemoved) {
                hasRemoved = false;
                return;
            }
            
            interactDuration = 0;
            
            if(interactCooldown.IsRunning) return;
            
            if (inMemory) {
                if(memoryInteraction != null) LeaveMemory();
                else Debug.LogError("[PlayerInteract] Current memory interaction is null");
                
                return;
            }

            if (inPressurePlate) {
                if(potentialInteraction != null) LeavePressurePlate();
                return;
            }
            
            if(CanGrab())
                GrabObject();
            else if(CanDrop())
                DropObject();
            else if(IsMemory())
                MemoryInteraction();
            else if (IsPressurePlate())
                PressurePlateInteraction();
            else if (CanContextualInteract()) {
                if(potentialInteraction.GetInteractionType is ObjectType.Shard) triggerShard = true;
                potentialInteraction?.OnInteract(ObjectInteraction.Contextual);
                potentialInteraction = null;
            }
            else
                Debug.Log("[PlayerInteract] No object to interact with...");
            
            interactCooldown.Start();

            if (!inMemory && !inPressurePlate) {
                canInteract = false;
                interactionType = Interaction.None;
                RaiseInteraction();
            }
        }

        #region InteractionMethods

        private void GrabObject() {
            HasObject = true;
            currentInteraction = potentialInteraction;
            currentInteraction?.OnInteract(ObjectInteraction.Grab);
            
            Debug.Log($"[PlayerInteract] Grabbing {potentialInteraction.name}");
        }

        public void SetGrabbedObject(BaseObject interaction) {
            interaction.SetInteract(true);
            HasObject = true;
            currentInteraction = interaction;
            currentInteraction?.OnInteract(ObjectInteraction.Grab);
            
            Debug.Log($"[PlayerInteract] Grabbing {potentialInteraction.name}");
        }

        private void DropObject() {
            Debug.Log($"[PlayerInteract] Dropping {currentInteraction?.name} on {potentialInteraction?.name}");
            
            if(potentialInteraction != null)
                currentInteraction?.OnInteract(ObjectInteraction.Drop, potentialInteraction.GetInteract);
            else
                currentInteraction?.OnInteract(ObjectInteraction.Drop);
        }

        private void MemoryInteraction() {
            memoryInteraction = potentialInteraction;
            memoryInteraction?.OnInteract(ObjectInteraction.EnterMemory);
            inMemory = true;
            
            UpdatePossibleInteraction();
            Debug.Log($"[PlayerInteract] Interact with memory");
        }

        public void LeaveMemory() {
            memoryInteraction?.OnInteract(ObjectInteraction.LeaveMemory);
            memoryInteraction = null;
            
            inMemory = false;
            
            Debug.Log($"[PlayerInteract] Leave memory");
        }
        
        private void PressurePlateInteraction() {
            if (currentInteraction != null) {
                potentialInteraction?.OnInteract(ObjectInteraction.EnterPressurePlate, currentInteraction.GetInteract);
            }
            else {
                potentialInteraction?.OnInteract(ObjectInteraction.EnterPressurePlate);
                inPressurePlate = true;
            }
            
            
            UpdatePossibleInteraction();
            Debug.Log($"[PlayerInteract] Interact with Pressure Plate");
        }

        public void LeavePressurePlate() {
            potentialInteraction?.OnInteract(ObjectInteraction.LeavePressurePlate);
            potentialInteraction = null;
            
            inPressurePlate = false;
            UpdatePossibleInteraction();
            Debug.Log($"[PlayerInteract] Leave Pressure Plate");
        }

        #endregion

        private void HandleInteractRotation(Vector3 playerDir) {
            var newPos = transform.position + player.movement.mesh.forward * interactZoneSize.z;
            interactCenterZone.position = Vector3.Lerp(interactCenterZone.position, newPos, player.movement.playerConfig.rotationSpeed * Time.deltaTime);
        }
        
        public void HandleUpdate(Vector3 playerDir) {
            HandleInteractRotation(playerDir);
            
            if(interactionHold)
                interactDuration += Time.deltaTime;
            
            if (interactDuration >= holdInteractionNeeded && !HasObject) {
                //If causes a null ref (tried to fix it)
                if (potentialInteraction 
                    &&  potentialInteraction.GetInteractionType is ObjectType.Memory 
                        && potentialInteraction.GetCompletion is not InteractionCompletion.None && !IsInMemory() 
                    || (potentialInteraction.GetInteractionType is ObjectType.PressurePlate && !inPressurePlate)) { 
                    potentialInteraction?.OnInteract(ObjectInteraction.Remove);
                    hasRemoved = true;
                }

                interactDuration = 0;
                
                canInteract = false;
                interactionType = Interaction.None;
                RaiseInteraction();
                return;
            }
            
            HandleInteraction();
            SetPlayerInteraction();
        }

        #region UpdateInteraction

        void HandleInteraction() {
            if(!canPlayerInteract) return;
            
            size = Physics.OverlapBoxNonAlloc(interactCenterZone.position, interactZoneSize, results, Quaternion.identity, interactLayerMask);

            switch (size) {
                case 0:
                    potentialInteraction = null;
                    return;
                case > 1:
                    var closestDist = 10f;
                    BaseObject closest = null;
                    foreach (Collider c in results) {
                        if(c == null) continue;
                        if (!c.TryGetComponent(out BaseObject b)) continue;
                        if(!b.CanBeInteractedWith()) continue;
                        
                        var dist = Vector3.Distance(c.transform.position, transform.position);
                        if (dist < closestDist) {
                            closest = b;
                            closestDist = dist;
                        }
                    }
                    potentialInteraction = closest;
                    break;
                default: // No need for logic, just get the only object we detect
                    potentialInteraction = results[0].GetComponent<BaseObject>();
                    break;
            }

            if (!HasObject) 
                return;
            
            if (potentialInteraction == currentInteraction) potentialInteraction = null;

        }

        void SetPlayerInteraction() {
            if (potentialInteraction is null) {
                CanInteract = false;
                return;
            }
            
            UpdatePossibleInteraction();
            
            if (potentialInteraction.CanBeInteractedWith())
                CanInteract = canPlayerInteract && size > 0;
            else {
                CanInteract = false;
                return;
            }

            if (potentialInteraction && player.cinemachineBrain.OutputCamera)
                HudManager.InteractionSetPosition( potentialInteraction.GetUIPosition());
        }

        private void UpdatePossibleInteraction() { //Get le type interaction dans le base object -> Get Component est pas opti surtout dans une update
            if (inMemory) {
                canInteract = true;
                interactionType = Interaction.LeaveMemory;
                RaiseInteraction();
                return;
            }

            if (inPressurePlate) {
                canInteract = true;
                interactionType = Interaction.LeavePressurePlate;
                RaiseInteraction();
                return;
            }


            if (potentialInteraction == null || interactCooldown.IsRunning) {
                canInteract = false;
                interactionType = Interaction.None;
                RaiseInteraction();
                return;
            }
            switch (potentialInteraction.GetInteractionType) {
                case ObjectType.Moveable:
                    interactionType = Interaction.Grab;
                    RaiseInteraction();
                    return;
                case ObjectType.Door when potentialInteraction.GetCompletion is not InteractionCompletion.None: {
                    if (potentialInteraction.GetCompletion is InteractionCompletion.Completed)
                        interactionType = Interaction.UseDoor;
                    else if (HasObject) {
                        var key = potentialInteraction.GetComponent<KeyInteractable>();
                        interactionType = key.GetKeyObject(currentInteraction) ? Interaction.UseKey : Interaction.NeedSomethingElse;
                    }
                    else
                        interactionType = Interaction.NeedKey;
                    RaiseInteraction();
                    return;
                }
                case ObjectType.Door:
                    interactionType = Interaction.UseDoor;
                    RaiseInteraction();
                    return;
                case ObjectType.Memory when potentialInteraction.GetCompletion is not InteractionCompletion.None: {
                    if (potentialInteraction.GetCompletion is InteractionCompletion.Completed)
                    {
                        interactionType = IsInMemory() ? Interaction.LeaveMemory : Interaction.EnterMemory;
                        RaiseInteraction();
                    }
                    else if (HasObject) {
                        var key = potentialInteraction.GetComponent<KeyInteractable>();
                        interactionType = key.GetKeyObject(currentInteraction) ? Interaction.UseFragment : Interaction.NeedSomethingElse;
                    }
                    else
                        interactionType = Interaction.NeedFragment;
                    RaiseInteraction();
                    return;
                }
                case ObjectType.Memory:
                    interactionType = Interaction.EnterMemory;
                    RaiseInteraction();
                    return;
                case ObjectType.Shard:
                    interactionType = Interaction.ObtainShard;
                    RaiseInteraction();
                    return;
                case ObjectType.Dialogue:
                    interactionType = Interaction.Dialogue;
                    RaiseInteraction();
                    return;
                case ObjectType.PressurePlate:
                    if(potentialInteraction.GetCompletion is not InteractionCompletion.Completed && currentInteraction is not null)
                        interactionType = Interaction.PutObjectOnPressurePlate;
                    else if(potentialInteraction.GetCompletion is not InteractionCompletion.Completed && currentInteraction is null)
                        interactionType = IsInPressurePlate() ? Interaction.LeavePressurePlate : Interaction.EnterPressurePlate;
                    else interactionType = Interaction.PickObjectOnPressurePlate;
                    RaiseInteraction();
                    return;
                case ObjectType.None:
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

        public void SetGrabObject(BaseObject grab) {
            HasObject = true;
            currentInteraction = grab;
            currentInteraction?.OnInteract(ObjectInteraction.Grab);
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
            
            if(potentialInteraction.TryGetComponent(out MoveableObject moveable))
                return CanInteract && !HasObject && currentInteraction == null && moveable.CanBeGrab();
            
            return false;
        }

        private bool CanDrop() {
            if (potentialInteraction == null) return HasObject && currentInteraction != null;
            
            if (potentialInteraction.TryGetComponent(out KeyInteractable drop))
                return HasObject && currentInteraction != null && drop != null && potentialInteraction.GetCompletion is InteractionCompletion.NotCompleted;
            
            return false;
        }

        private bool IsMemory() {
            if (potentialInteraction == null) return false;
            
            if (potentialInteraction.TryGetComponent(out MemoryInteractable memory))
                return memory != null && potentialInteraction.GetCompletion is not InteractionCompletion.NotCompleted;
            
            return false;
        }

        private bool IsPressurePlate() {
            if (potentialInteraction == null) return false;
            
            if (potentialInteraction.TryGetComponent(out PressurePlate plate))
                return plate != null && potentialInteraction.GetCompletion is not InteractionCompletion.Completed;
            
            return false;
        }

        private bool CanContextualInteract() {
            return CanInteract;
        }

        public bool IsCarrying() {
            return currentInteraction != null && HasObject;
        }
        
        public bool IsInMemory() {
            return inMemory;
        }
        
        public bool IsInPressurePlate() {
            return inPressurePlate;
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
        
        public bool UsingDoor() {
            return usingDoor.IsRunning;
        }

        public void TriggerBigDoor(SceneSettings toLoad, Vector3 position) {
            triggerDoor = true;
            AudioManager.Instance.PlayOpenBigSound(position);
            StartCoroutine(LoadScene(toLoad, position));
        }

        private IEnumerator LoadScene(SceneSettings toLoad, Vector3 position) {
            yield return new WaitForSeconds(player.useDoorClip.length);
            GameInitializer.Instance.LoadNewLevel(toLoad);
        }
    }
}