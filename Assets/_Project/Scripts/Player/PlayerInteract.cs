using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.Enums;
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

        private PlayerController player;
        private CountdownTimer usingDoor;
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

            usingDoor = new CountdownTimer(timerToUseDoor);
        }

        private void OnEnable() {
            inputsBrain.OnInteract += Interact;
        }

        private void OnDisable() {
            inputsBrain.OnInteract -= Interact;
        }

        #endregion
        
        private void Interact(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                interactionHold = true;
                return;
            }

            if (ctx.canceled) 
                interactionHold = false;

            if (hasRemoved) {
                hasRemoved = false;
                return;
            }
            
            interactDuration = 0;
            
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
                if (potentialInteraction.GetInteractionType is ObjectType.Door) {
                    if(potentialInteraction.GetComponent<DoorInteractable>().doorType is DoorType.BigDoor && HasObject) {
                        return;
                    }
                }
                potentialInteraction?.OnInteract(ObjectInteraction.Contextual);
                potentialInteraction = null;
            }
            else
                Debug.Log("[PlayerInteract] No object to interact with...");
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

        private void LeaveMemory() {
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

        private void LeavePressurePlate() {
            potentialInteraction?.OnInteract(ObjectInteraction.LeavePressurePlate);
            potentialInteraction = null;
            
            inPressurePlate = false;
            UpdatePossibleInteraction();
            Debug.Log($"[PlayerInteract] Leave Pressure Plate");
        }

        #endregion
        
        public void HandleUpdate(Vector3 playerDir) {
            interactCenterZone.position = transform.position + playerDir * interactZoneSize.z;

            if(interactionHold)
                interactDuration += Time.deltaTime;
            
            if (interactDuration >= holdInteractionNeeded && !HasObject) {
                if (potentialInteraction.GetInteractionType is ObjectType.Memory && potentialInteraction.GetCompletion is not InteractionCompletion.None) {
                    potentialInteraction?.OnInteract(ObjectInteraction.Remove);
                    hasRemoved = true;
                }
                else if (potentialInteraction.GetInteractionType is ObjectType.PressurePlate) {
                    potentialInteraction?.OnInteract(ObjectInteraction.Remove);
                    hasRemoved = true;
                }
                
                interactDuration = 0;
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
                case > 1: // Sort the closer object from the player or the object the player is looking at -> Refaire la formule
                    //Voir aussi si il y a pas moyen de get autrement le base object depuis le raycast
                    var index = 0;
                    var closestDist = 0f;
                    var closestAngle = 0f;
                    for (var i = 0; i < size; i++) {
                        if (!results[index].GetComponent<BaseObject>().CanBeInteractedWith()) continue;
                        
                        var dist = Vector3.Distance(transform.position, results[i].transform.position);
                        var facing = Vector3.Dot((transform.position - interactCenterZone.position).normalized, (results[i].transform.position - transform.position).normalized);

                        if (!(facing < closestAngle)) continue;
                        closestAngle = facing;
                        if (dist > closestDist) continue;
                        closestDist = dist;
                        index = i;
                    }
                    
                    potentialInteraction = results[index].GetComponent<BaseObject>();
                    break;
                default: // No need for logic, just get the only object we detect
                    potentialInteraction = results[0].GetComponent<BaseObject>();
                    break;
            }
            
            if (HasObject) {
                if (potentialInteraction == currentInteraction) potentialInteraction = null;
            }
            
        }

        void SetPlayerInteraction() {
            if (potentialInteraction is null) {
                CanInteract = false;
                return;
            }
            
            UpdatePossibleInteraction();
            
            if (potentialInteraction.CanBeInteractedWith())
                CanInteract = canPlayerInteract && size > 0;
            else
            {
                CanInteract = false;
                return;
            }
            
            if (potentialInteraction != null && UnityEngine.Camera.main)
                HudManager.InteractionSetPosition(
                    UnityEngine.Camera.main.WorldToScreenPoint(potentialInteraction.GetUIPosition()));
        }

        private void UpdatePossibleInteraction() { //Get le type interaction dans le base object -> Get Component est pas opti surtout dans une update
            if (inMemory) {
                interactionType = Interaction.LeaveMemory;
                RaiseInteraction();
                return;
            }

            if (inPressurePlate) {
                interactionType = Interaction.LeavePressurePlate;
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
                        interactionType = key.GetKeyObject(currentInteraction) ? Interaction.UseKey : Interaction.needSomethingElse;
                    }
                    else
                        interactionType = Interaction.needKey;
                    RaiseInteraction();
                    return;
                }
                case ObjectType.Door:
                    interactionType = Interaction.UseDoor;
                    RaiseInteraction();
                    return;
                case ObjectType.Memory when potentialInteraction.GetCompletion is not InteractionCompletion.None: {
                    if (potentialInteraction.GetCompletion is InteractionCompletion.Completed)
                        interactionType = IsInMemory() ? Interaction.LeaveMemory : Interaction.EnterMemory;
                    else if (HasObject) {
                        var key = potentialInteraction.GetComponent<KeyInteractable>();
                        interactionType = key.GetKeyObject(currentInteraction) ? Interaction.UseFragment : Interaction.needSomethingElse;
                    }
                    else
                        interactionType = Interaction.needFragment;
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
                    interactionType = Interaction.dialogue;
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
                default:
                    return;
            }
        }

        #endregion
        
        private void RaiseInteraction() {
            EventBus<InteractEvent>.Raise(new InteractEvent {
                ShowInteraction = canInteract,
                Interaction = interactionType,
                ObjectName = potentialInteraction.ObjectName
            });
        }
        
        public void SetInteract(bool interact) {
            canPlayerInteract = interact;
        }

        public IInteractable GetCurrentInteractable() {
            return currentInteraction.GetInteract;
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

        public void StartUsingDoor() {
            usingDoor.Start();
            usingDoor.Reset(timerToUseDoor);
        }
        
        public bool UsingDoor() {
            return usingDoor.IsRunning;
        }
    }
}