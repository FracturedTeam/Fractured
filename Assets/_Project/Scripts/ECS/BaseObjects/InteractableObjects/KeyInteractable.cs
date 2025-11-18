using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class KeyInteractable : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        private List<BaseObject> keyObject;
        private List<BaseObject> keyUsed;

        private bool initialized = false;
        
        public virtual void Initialize() {
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component)) baseObject = component as BaseObject;
                else Debug.LogError($"[KeyInteractable] Cannot find {nameof(BaseObject)} in {nameof(KeyInteractable)}");
                
                baseObject.GetCompletion = InteractionCompletion.NotCompleted;
                
                keyObject = new List<BaseObject>();
                keyUsed = new List<BaseObject>();
            }
            
            initialized = true;
        }

        public virtual void OnInteract(ObjectInteraction interaction = ObjectInteraction.None, IInteractable other = null) {
            if (HasOneKey() && interaction is ObjectInteraction.Remove) {
                RemoveObject();
                return;
            }
            
            if(baseObject.GetCompletion is InteractionCompletion.Completed) return;
            
            if (interaction is not ObjectInteraction.Drop) {
                Debug.LogError($"[KeyInteractable] Interaction is not Drop {nameof(KeyInteractable)} | Interaction is {interaction}");
                return;
            }
            if (other == null) {
                Debug.LogError($"[KeyInteractable] Other is null !");
                return;
            }

            if (!other.GetBaseObject()) {
                Debug.LogError($"[KeyInteractable] Cannot get Base Object from other !");
                return;
            }
            
            if (GetKeyObject(other.GetBaseObject()))
                CheckForResolve(other.GetBaseObject());
        }

        public void Tick(float deltaTime) {
        }

        private void CheckForResolve(BaseObject key) { //Des chances que cette fonction casse
            if(keyUsed.Contains(key))
                return;
            
            keyUsed.Add(key);
            
            if(keyUsed.Count == keyObject.Count)
                ResolvePuzzle();
        }

        private void RemoveObject() {
            var index = keyUsed.Count - 1;
            var objectRemoved = keyUsed[index];
            
            keyUsed.RemoveAt(index);
            
            baseObject.GetCompletion = InteractionCompletion.NotCompleted;
            baseObject.SetInteract(true);
            PlayerController.Instance.interact.SetGrabbedObject(objectRemoved);
        }

        protected virtual void ResolvePuzzle() {
            Debug.Log("[KeyInteractable] Resolve Puzzle");

            baseObject.GetCompletion = InteractionCompletion.Completed;
            baseObject.SetInteract(false);
        }

        public virtual void ResetObject() {
            //Voir le reset qu'il y a besoin de faire ici - Si il y a besoin
        }

        public virtual BaseObject GetBaseObject() {
            return baseObject;
        }

        public bool GetKeyObject(BaseObject currentInteraction) {
            foreach (var key in keyObject) {
                if (key == currentInteraction) return true;
            }
            return false;
        }

        public bool HasOneKey() {
            return keyObject.Count > 0;
        }
        
        public void SetKeyObject(BaseObject key) {
            keyObject.Add(key);
        }
    }

}