using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class UsableAttribute : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        
        [Header("Usable Attribute")]
        [SerializeField] private bool oneTimeUse = false;
        [SerializeField] private bool hasObjectInside = false;
        [SerializeField] private GameObject[] objectsInside;

        [SerializeField] private UnityEvent eventOnActivation;
        [SerializeField] private UnityEvent eventOnDeactivation;
        
        private bool isInitialized = false;
        public bool IsUsed { get; private set; }
        
        public void Initialize() {
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[Collectable] Cannot find {nameof(BaseObject)} in {nameof(CollectableAttribute)}");
                
                baseObject.GetObjectType = ObjectType.Usable;
                
                baseObject.SetInteract(true);

                if (hasObjectInside) {
                    oneTimeUse = true;
                    foreach (var obj in objectsInside) {
                        obj.SetActive(false);
                    }
                }
            }

            isInitialized = true;
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (interaction is ObjectInteraction.Contextual) {
                Use();
            }
        }

        private void Use() {
            if(IsUsed && oneTimeUse) return;
            IsUsed = !IsUsed;
            
            UpdateState();
        }

        private void UpdateState() {
            baseObject.GetTrigger?.OnFunction(IsUsed ? baseObject.GetTrigger?.OnSetStateOn : baseObject.GetTrigger?.OnSetStateOff);
            
            if(baseObject.HasSceneElement())
                baseObject.TriggerSceneElement();
            
            if (hasObjectInside && IsUsed) {
                foreach (var obj in objectsInside) {
                    obj.SetActive(true);
                }
            }

            if (oneTimeUse) {
                baseObject.SetInteract(false);
            }

            if (IsUsed) {
                eventOnActivation.Invoke();
            }
            else {
                eventOnDeactivation.Invoke();
            }
        }

        public void SetUseState(bool state) {
            IsUsed = state;
            UpdateState();
            baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger.OnInteract);
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