using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class InspectableAttribute : MonoBehaviour, IInteractable {
        private BaseObject baseObject;

        private bool isInitialized;
        private bool isInspecting;

        [SerializeField] private GlassDocumentScriptableObject glassDocument; 
        
        public void Initialize() {
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[MoveableObject] Cannot find {nameof(BaseObject)} in {nameof(MovableAttribute)}");

                baseObject.GetObjectType = ObjectType.Inspectable;
                
                baseObject.SetInteract(true);
                
                isInitialized = true;
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (interaction is ObjectInteraction.Contextual) {
                Inspect();
            }
        }

        private void Inspect() {
            isInspecting = !isInspecting;

            if (isInspecting) {
                PlayerController.Instance.interact.SetIsFocus(true, baseObject);
                PlayerController.Instance.FreezeController(true);
                
                EventBus<DocumentEvent>.Raise(new DocumentEvent{isOn = true, document =glassDocument });
            }
            else {
                PlayerController.Instance.interact.SetIsFocus(false);
                PlayerController.Instance.FreezeController(false);
                
                EventBus<DocumentEvent>.Raise(new DocumentEvent{isOn = false, document =glassDocument });
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