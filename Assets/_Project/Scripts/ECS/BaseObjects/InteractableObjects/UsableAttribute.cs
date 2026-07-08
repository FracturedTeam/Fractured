using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class UsableAttribute : MonoBehaviour, IInteractable {
        
        private BaseObject baseObject;
        
        private bool initialized = false;
        
        private bool isUsed = false;
        
        [Header("Usable Attribute")]
        [SerializeField] private bool OneTimeUse = false;
        [SerializeField] private bool hasObjectInside = false;
        [SerializeField] private GameObject[] objectsInside;

        [SerializeField] private UnityEvent EventOnActivation;
        [SerializeField] private UnityEvent EventOnDeactivation;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[Collectable] Cannot find {nameof(BaseObject)} in {nameof(CollectableAttribute)}");
                
                baseObject.GetObjectType = ObjectType.Usable;
                
                baseObject.SetInteract(true);

                if (hasObjectInside) {
                    OneTimeUse = true;
                    foreach (var obj in objectsInside) {
                        obj.SetActive(false);
                    }
                }
                
            }

            initialized = true;
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (interaction is ObjectInteraction.Contextual) {
                Use();
            }
        }

        private void Use() {
            if(isUsed && OneTimeUse) return;
            
            isUsed = !isUsed;

            if (hasObjectInside && isUsed) {
                foreach (var obj in objectsInside) {
                    obj.SetActive(true);
                }
            }

            if (OneTimeUse) {
                baseObject.SetInteract(false);
            }

            if (isUsed) {
                EventOnActivation.Invoke();
            }
            else {
                EventOnDeactivation.Invoke();
            }
        }
        
        public void Tick(float deltaTime) {
            
        }

        public void Dispose() {
            
        }

        public void CompleteObject() {
            
        }

        public void ResetObject() {
            
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}