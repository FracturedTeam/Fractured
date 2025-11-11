using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Systems.EventBus;
using UnityEngine;

namespace _Project.Scripts.Player {

    public struct InteractEvent : IEvent {
        public bool showInteraction;
        public bool needKey;
        public bool memory;
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
        
        public bool hasObject { get; private set; }
        
        private bool canPlayerInteract = false;
        private bool playerNeedKey = false;
        private bool canInteract;
        private bool inMemory = false;

        private PlayerController player;
        
        public bool CanInteract {
            get => canInteract;
            private set {
                if(canInteract == value) return;
                
                canInteract = value;
                EventBus<InteractEvent>.Raise(new InteractEvent {
                    showInteraction = value,
                    needKey = playerNeedKey,
                    memory = inMemory
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
            hasObject = true;
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
            SetInteract(false);
            
            player.movement.FreezeController();
            
            Debug.Log($"[PlayerInteract] Interact with memory");
        }

        private void LeaveMemory() {
            currentInteraction?.OnInteract(ObjectInteraction.LeaveMemory);
            currentInteraction = null;
            
            inMemory = false;
            SetInteract(true);
            
            player.movement.UnfreezeController();
            
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
                case > 1: // Sort the closer object from the player or the object the player is looking at
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
                playerNeedKey = false;
                CanInteract = false;
            }
            else if (potentialInteraction.CanBeInteractedWith()) {
                if (potentialInteraction.TryGetComponent(out KeyInteractable drop)) {
                    if (drop && !hasObject) {
                        playerNeedKey = true;
                        CanInteract = true;
                    }
                    else {
                        playerNeedKey = false;
                        CanInteract = canPlayerInteract && size > 0 && hasObject && drop.GetKeyObject(currentInteraction);
                    }
                }
                else {
                    playerNeedKey = false;
                    CanInteract = canPlayerInteract && size > 0;
                }
            }
            else {
                playerNeedKey = false;
                CanInteract = false;
            }
            
        }
        
        public void SetInteract(bool interact) {
            canPlayerInteract = interact;
        }

        public IInteractable GetCurrentInteractable() {
            return currentInteraction.GetInteract;
        }

        public void SetGrabObject(BaseObject grab) {
            hasObject = true;
            currentInteraction = grab;
            currentInteraction?.OnInteract(ObjectInteraction.Grab);
        }
        
        public void SetDropObject() {
            hasObject = false;
            currentInteraction = null;
        }
        
        private bool CanGrab() {
            if(potentialInteraction ==  null) return false;
            
            if(potentialInteraction.TryGetComponent(out MoveableObject moveable))
                return CanInteract && !hasObject && currentInteraction == null && moveable.CanBeGrab();
            
            return false;
        }

        private bool CanDrop() {
            if (potentialInteraction == null) return hasObject && currentInteraction != null;
            
            if (potentialInteraction.TryGetComponent(out KeyInteractable drop))
                return hasObject && currentInteraction != null && drop != null;
            
            return false;
        }

        private bool IsMemory() {
            if (potentialInteraction == null) return false;
            
            if (potentialInteraction.TryGetComponent(out MemoryInteractable memory))
                return memory != null;
            
            return false;
        }

        private bool CanContextualInteract() {
            return CanInteract;
        }
    }
}