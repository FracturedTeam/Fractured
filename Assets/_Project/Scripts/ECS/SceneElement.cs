using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class SceneElement : MonoBehaviour {
        public BaseObject baseObject {get; private set;}
        private SceneMaster masterValidation;

        private bool isValidated;
        public bool IsValidated {
            get => isValidated;
            private set {
                isValidated = value;
                masterValidation.CheckForValidation();
            }
        }

        public enum ValidationMethod {Position, GlassState, UseState}
        public ValidationMethod validationMethod;
        
        public Vector3 requestedPosition;
        public bool requestedVisibility;
        public bool requestedUseState;

        private Action onPlayerInteraction;

        public void SetBaseObject(BaseObject baseObject) {
            this.baseObject = baseObject;
        }
        
        private void OnEnable() {
            switch (validationMethod) {
                case ValidationMethod.Position:
                    onPlayerInteraction += PositionValidation;
                    break;
                case ValidationMethod.GlassState:
                    onPlayerInteraction += GlassValidation;
                    break;
                case ValidationMethod.UseState:
                    onPlayerInteraction += UsableValidation;
                    break;
            }
        }
        
        private void OnDisable() {
            switch (validationMethod) {
                case ValidationMethod.Position:
                    onPlayerInteraction -= PositionValidation;
                    break;
                case ValidationMethod.GlassState:
                    onPlayerInteraction -= GlassValidation;
                    break;
                case ValidationMethod.UseState:
                    onPlayerInteraction -= UsableValidation;
                    break;
            }
        }
        
        public void CheckValidation() {
            onPlayerInteraction.Invoke();
        }
        

        private void PositionValidation() {
            var distanceToLocation = Vector3.Distance(requestedPosition, transform.position);
            IsValidated = distanceToLocation <= 4f;
        }

        private void UsableValidation() {
            var usable = baseObject.GetInteract as UsableAttribute;
            IsValidated = usable?.IsUsed == requestedUseState;
        }
        
        private void GlassValidation() {
            IsValidated = baseObject.GetGlassInteract.IsVisible == requestedVisibility;
        }

        public void SetSceneMaster(SceneMaster master) {
            masterValidation = master;
        }
    }
}