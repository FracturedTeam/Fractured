using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class KeyInteractable : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        private List<BaseObject> keyObject;
        private List<BaseObject> keyUsed;

        private bool initialized = false;
        protected bool completed = false;
        
        public virtual void Initialize() {
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component))
                    baseObject = component as BaseObject;
                else 
                    Debug.LogError($"[KeyInteractable] {gameObject.name} does not have a BaseObject !");
                
                keyObject = new List<BaseObject>();
                keyUsed = new List<BaseObject>();
            }
            
            initialized = true;
        }

        public virtual void OnInteract(ObjectInteraction interaction = ObjectInteraction.None, IInteractable other = null) {
            if(completed) return;
            
            if (interaction is not ObjectInteraction.Drop) {
                Debug.LogError($"[KeyInteractable] Interaction is not Drop");
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

        private void CheckForResolve(BaseObject key) { //Des chances que cette fonction casse
            if(keyUsed.Contains(key))
                return;
            
            keyUsed.Add(key);
            
            if(keyUsed.Count == keyObject.Count)
                ResolvePuzzle();
        }

        protected virtual void ResolvePuzzle() {
            Debug.Log("[KeyInteractable] Resolve Puzzle");

            completed = true;
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

        public void SetKeyObject(BaseObject key) {
            keyObject.Add(key);
            
            Debug.Log($"[KeyInteractable] Added Key Object {keyObject.Count}");
        }

        public bool Completed() {
            return completed;
        }
    }

}