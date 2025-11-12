using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player {

    public struct InteractEvent : IEvent {
        public bool showInteraction;
        public Interaction interaction;
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
        
        public bool HasObject { get; private set; }
        
        private bool canPlayerInteract = false;
        private bool canInteract;
        private bool inMemory = false;

        private PlayerController player;
        private CountdownTimer usingDoor;
        
        private Interaction interactionType;
        
        public bool CanInteract {
            get => canInteract;
            private set {
                if(canInteract == value) return;
                
                canInteract = value;
                EventBus<InteractEvent>.Raise(new InteractEvent {
                    showInteraction = value,
                    interaction = interactionType
                });
            }
        }
        
        public int size { get; private set; }

        private void Awake() {
            if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");

            if(TryGetComponent(out PlayerController _player)) player = _player;
            else Debug.LogWarning("[PlayerController] No PlayerController found");
            
            size = 0;

            usingDoor = new CountdownTimer(0.5f);
        }

        private void OnEnable() {
            inputsBrain.OnInteract += Interact;
        }

        private void OnDisable() {
            inputsBrain.OnInteract -= Interact;
        }
        
        private void Interact() {
            if (inMemory) {
                if(currentInteraction != null) LeaveMemory();
                else Debug.LogError("[PlayerInteract] Current memory interaction is null");
                
                return;
            }
            
            if(CanGrab())
                GrabObject();
            else if(CanDrop())
                DropObject();
            else if(IsMemory())
                MemoryInteraction();
            else if(CanContextualInteract())
                potentialInteraction?.OnInteract(ObjectInteraction.Contextual);
            else
                Debug.Log("[PlayerInteract] No object to interact with...");
        }

        private void GrabObject() {
            HasObject = true;
            currentInteraction = potentialInteraction;
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
            currentInteraction = potentialInteraction;
            currentInteraction?.OnInteract(ObjectInteraction.EnterMemory);
            inMemory = true;
            
            UpdatePossibleInteraction();
            Debug.Log($"[PlayerInteract] Interact with memory");
        }

        private void LeaveMemory() {
            currentInteraction?.OnInteract(ObjectInteraction.LeaveMemory);
            currentInteraction = null;
            
            inMemory = false;
            
            Debug.Log($"[PlayerInteract] Leave memory");
        }
        
        public void HandleUpdate(Vector3 playerDir) {
            interactCenterZone.position = transform.position + playerDir * interactZoneSize.z;

            CanPlayerInteract();
            HandleInteraction();
        }

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

                        if (!(facing > closestAngle)) continue;
                        closestAngle = facing;
                        if (!(dist < closestDist)) continue;
                        closestDist = dist;
                        index = i;
                    }
                    
                    potentialInteraction = results[index].GetComponent<BaseObject>();
                    break;
                default: // No need for logic, just get the only object we detect
                    potentialInteraction = results[0].GetComponent<BaseObject>();
                    break;
            }
        }

        void CanPlayerInteract() {
            if (potentialInteraction == null) {
                CanInteract = false;
                return;
            }
            
            UpdatePossibleInteraction();
            
            if (potentialInteraction.CanBeInteractedWith()) {
                if (potentialInteraction.TryGetComponent(out KeyInteractable drop) && !drop.Completed()) {
                    if (drop && !HasObject) { //Show something is needed
                        CanInteract = true;
                    }
                    else { //Anticipate possibly showing not good object
                        CanInteract = canPlayerInteract && size > 0 && HasObject && drop.GetKeyObject(currentInteraction);
                    }
                }
                else {
                    CanInteract = canPlayerInteract && size > 0;
                }
            }
            else {
                CanInteract = false;
            }
            
            
        }

        private void UpdatePossibleInteraction() { //Get le type interaction dans le base object -> Get Component est pas opti surtout dans une update
            if (IsInMemory()) {
                interactionType = Interaction.LeaveMemory;
                RaiseInteraction();
                return;
            }
            
            if (potentialInteraction.GetComponent<MoveableObject>()) {
                interactionType = Interaction.Grab;
                RaiseInteraction();
                return;
            }
            
            if (potentialInteraction.TryGetComponent(out DoorInteractable door) && door) {
                if (potentialInteraction.GetComponent<UnlockedDoor>()) {
                    interactionType = door.DoorUnlocked() ? Interaction.UseDoor : Interaction.UseKey;
                    RaiseInteraction();
                    return;
                }

                interactionType = Interaction.UseDoor;
                RaiseInteraction();
                return;
            }
            
            if (potentialInteraction.TryGetComponent(out MemoryInteractable m) && m) {
                if (potentialInteraction.GetComponent<PedestalInteractable>()) {
                    if (m.MemoryUnlocked())
                        interactionType = IsInMemory() ? Interaction.LeaveMemory : Interaction.EnterMemory;
                    else
                        interactionType = Interaction.UseFragment;
                    
                    RaiseInteraction();
                    return;
                }
                
                interactionType = Interaction.EnterMemory;
                RaiseInteraction();
                return;
            }
            
            if (potentialInteraction.GetComponent<ObtainShardInteractable>()) {
                interactionType = Interaction.ObtainShard;
                RaiseInteraction();
            }
        }

        private void RaiseInteraction() {
            EventBus<InteractEvent>.Raise(new InteractEvent {
                showInteraction = canInteract,
                interaction = interactionType
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
            if(potentialInteraction ==  null) return false;
            
            if(potentialInteraction.TryGetComponent(out MoveableObject moveable))
                return CanInteract && !HasObject && currentInteraction == null && moveable.CanBeGrab();
            
            return false;
        }

        private bool CanDrop() {
            if (potentialInteraction == null) return HasObject && currentInteraction != null;
            
            if (potentialInteraction.TryGetComponent(out KeyInteractable drop))
                return HasObject && currentInteraction != null && drop != null && !drop.Completed();
            
            return false;
        }

        private bool IsMemory() {
            if (potentialInteraction == null) return false;
            
            if (potentialInteraction.TryGetComponent(out MemoryInteractable memory))
                return memory != null && memory.MemoryUnlocked();
            
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

        public void StartUsingDoor() {
            usingDoor.Start();
        }
        
        public bool UsingDoor() {
            return usingDoor.IsRunning;
        }
    }
}