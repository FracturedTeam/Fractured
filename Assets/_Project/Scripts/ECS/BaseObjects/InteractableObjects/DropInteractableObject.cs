using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class DropInteractableObject : MonoBehaviour, IInteractable {
        
        private BaseObject baseObject;
        private bool isResolveLocation;

        private MoveableObject keyObject;

        public void Initialize() {
            if(TryGetComponent(typeof(BaseObject), out var component))
                baseObject = component as BaseObject;
            else 
                Debug.LogError($"[DropInteractableObject] {gameObject.name} does not have a BaseObject !");
        }

        public void OnInteract(ObjectInteraction interaction = ObjectInteraction.None, IInteractable other = null) {
            if (interaction is not ObjectInteraction.Drop) {
                Debug.LogError($"[DropInteractableObject] Interaction is not Drop");
                return;
            }
            if (other == null) {
                Debug.LogError($"[DropInteractableObject] Other is null !");
                return;
            }

            if (!other.GetBaseObject().TryGetComponent(out MoveableObject moveableObject)) {
                Debug.LogError($"[DropInteractableObject] Cannot get Moveable object from other");
                return;
            }
            
            if (moveableObject == keyObject)
                CheckForResolve();       
        }

        void CheckForResolve() {
            if (isResolveLocation)
                ResolvePuzzle();
            else {
                GetBaseObject().SetInteract(false);
                GetBaseObject().SetCollider(false);
                Debug.Log("[DropInteractableObject] Object drop on start location");
            }
        }

        void ResolvePuzzle() {
            Debug.Log("[DropInteractableObject] Resolve Puzzle");
            
            baseObject.SetInteract(false);
        }

        public void ResetObject() {
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }

        public void SetResolveLocation(bool isResolveLocation) {
            this.isResolveLocation = isResolveLocation;
        }

        public void SetKeyObject(MoveableObject keyObject) {
            this.keyObject = keyObject;
        }

        public void SetInteract(bool canInteract) {
            baseObject.SetInteract(canInteract);
        }

        public MoveableObject GetKeyObject() {
            return keyObject;
        }
    }

}