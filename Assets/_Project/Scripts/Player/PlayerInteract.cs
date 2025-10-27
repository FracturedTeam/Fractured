using System;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.EventBus;
using UnityEngine;

namespace _Project.Scripts.Player {

    public struct InteractEvent : IEvent {
        public bool showInteraction;
    }
    
    public class PlayerInteract : MonoBehaviour {
        private InputsBrain inputsBrain;

        [SerializeField] public Transform interactCenterZone;
        [SerializeField] public Vector3 interactZoneSize;
        [SerializeField] private LayerMask interactLayerMask;
        
        //Pre allocate space for collider (10 will be completely sufficient)
        private Collider[] results = new Collider[10];
        private InteractableObject potentialInteraction;
        private InteractableObject currentObject;
        
        private bool canPlayerInteract = false;
        public bool hasObject { get; private set; }

        private bool canInteract;
        public bool CanInteract {
            get => canInteract;
            private set {
                if(canInteract == value) return;

                //canInteract = canPlayerInteract && !hasObject && size > 0;
                canInteract = value;
                EventBus<InteractEvent>.Raise(new InteractEvent {
                    showInteraction = value
                });
            }
        }
        
        internal int size = 0;

        private void Awake() {
            if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");
        }

        private void OnEnable() {
            inputsBrain.OnInteract += Interact;
        }

        private void OnDisable() {
            inputsBrain.OnInteract -= Interact;
        }
        
        private void Interact() {
            if(CanGrab())
                GrabObject();
            else if(CanDrop())
                DropObject();
            else if(CanContextualInteract())
                potentialInteraction.OnInteract(ObjectInteraction.Contextual);
            else
                Debug.Log("[PlayerInteract] No object to interact with...");
        }

        private void GrabObject() {
            SetInteract(false);
            hasObject = true;
            currentObject = potentialInteraction;
            potentialInteraction.OnInteract(ObjectInteraction.Grab);
        }

        private void DropObject() {
            SetInteract(true);
            hasObject = false;
            currentObject = null;
            potentialInteraction.OnInteract(ObjectInteraction.Drop);
        }
        
        public void HandleUpdate(Vector3 playerDir) {
            interactCenterZone.position = transform.position + playerDir * interactZoneSize.z;
            
            CanInteract = canPlayerInteract && !hasObject && size > 0;

            HandleInteraction();
        }

        void HandleInteraction() {
            if(!canPlayerInteract || hasObject) return;
            size = Physics.OverlapBoxNonAlloc(interactCenterZone.position, interactZoneSize, results, Quaternion.identity, interactLayerMask);
            
            if(size == 0) return;
            Debug.Log($"[PlayerInteract] Detecting {size} interactable");
            
            if (size > 1) { // Sort the closer object from the player or the object the player is looking at
                
            }
            else { // No need for logic, just get the only object we detect
                potentialInteraction = results[0].GetComponent<InteractableObject>();
            }
        }
        
        public void SetInteract(bool interact) {
            canPlayerInteract = interact;
        }

        private bool CanGrab() {
            return CanInteract && currentObject == null && potentialInteraction.canBeGrabbed;
        }

        private bool CanDrop() {
            return hasObject && currentObject != null;
        }

        private bool CanContextualInteract() {
            return CanInteract && !potentialInteraction.canBeGrabbed;
        }
    }
}