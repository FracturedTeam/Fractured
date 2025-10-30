using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects {
    [RequireComponent(typeof(BaseObject))]
    public class InteractableObject : MonoBehaviour, IInteractable {
        
        private BaseObject baseObject;

        protected virtual void Start() {
            if(TryGetComponent(typeof(BaseObject), out var component))
                baseObject = component as BaseObject;
            
            baseObject?.SetInteract(true);
        }

        public void Initialize() {
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (!baseObject) {
                Debug.LogError("[InteractableObject] No base object found");
                return;
            }
        }
        
        public void ResetObject() {
            //Todo implémenter le reset correctement
            Debug.Log("[InteractableObject] Reset object");
        }

        public BaseObject GetBaseObject() {
            throw new System.NotImplementedException();
        }
    }
}