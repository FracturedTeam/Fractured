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
    }
    
    public class PlayerInteract : MonoBehaviour {
        private InputsBrain inputsBrain;

        [SerializeField] public Transform interactCenterZone;
        [SerializeField] public Vector3 interactZoneSize;
        [SerializeField] private LayerMask interactLayerMask;
        
        //Pre allocate space for collider (10 will be completely sufficient)
        private readonly Collider[] results = new Collider[10];
        private BaseObject potentialInteraction;
        private BaseObject currentGrabbedObject;
        
        private bool canPlayerInteract = false;
        public bool hasObject { get; private set; }

        private bool canInteract;
        public bool CanInteract {
            get => canInteract;
            private set {
                if(canInteract == value) return;

                canInteract = value;
                EventBus<InteractEvent>.Raise(new InteractEvent {
                    showInteraction = value
                });
            }
        }
        
        public int size { get; private set; }

        private void Awake() {
            if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");

            size = 0;
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
                potentialInteraction?.OnInteract(ObjectInteraction.Contextual);
            else
                Debug.Log("[PlayerInteract] No object to interact with...");
        }

        private void GrabObject() {
            hasObject = true;
            currentGrabbedObject = potentialInteraction;
            currentGrabbedObject?.OnInteract(ObjectInteraction.Grab);
            
            Debug.Log($"[PlayerInteract] Grabbing {potentialInteraction.name}");
        }

        private void DropObject() { //Ici que j'envoie l'info de résoudre le puzzle si je porte un objet
            Debug.Log($"[PlayerInteract] Dropping {currentGrabbedObject.name} on {potentialInteraction.name}");
            
            currentGrabbedObject?.OnInteract(ObjectInteraction.Drop, potentialInteraction.GetInteract);
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
                    for (int i = 0; i < size; i++) {
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
            if(potentialInteraction == null)
                CanInteract = false;
            else if (potentialInteraction.CanBeInteractedWith()) {
                if (potentialInteraction.TryGetComponent(out DropInteractableObject drop))
                    CanInteract = canPlayerInteract && size > 0 && hasObject && drop.GetKeyObject().GetBaseObject() == currentGrabbedObject;
                else CanInteract = canPlayerInteract && size > 0;
            }
            else 
                CanInteract = false;
            
        }
        
        public void SetInteract(bool interact) {
            canPlayerInteract = interact;
        }

        public IInteractable GetCurrentInteractable() {
            return currentGrabbedObject.GetInteract;
        }

        public void SetDropObject() {
            hasObject = false;
            currentGrabbedObject = null;
        }
        
        private bool CanGrab() {
            if(potentialInteraction ==  null) return false;
            
            if(potentialInteraction.TryGetComponent(out MoveableObject moveable))
                return CanInteract && !hasObject && currentGrabbedObject == null && moveable.CanBeGrab();
            
            return false;
        }

        private bool CanDrop() {
            if(potentialInteraction ==  null) return false;
            
            if (potentialInteraction.TryGetComponent(out DropInteractableObject drop))
                return hasObject && currentGrabbedObject != null && drop != null;
            
            return false;
        }

        private bool CanContextualInteract() {
            return CanInteract /*&& !potentialInteraction.GetInteract.CanGrab()*/;
        }
    }
}