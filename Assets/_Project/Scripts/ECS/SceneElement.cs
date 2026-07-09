using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class SceneElement : MonoBehaviour {
        private BaseObject baseObject;
        private SceneMaster masterValidation;

        private bool isValidated;
        public bool IsValidated {
            get => isValidated;
            set {
                isValidated = value;
                masterValidation.CheckForValidation();
            }
        }

        private enum ValidationMethod {Position, GlassState, UseState}
        [SerializeField] private ValidationMethod validationMethod;
        
        private Vector3 requestedPosition;
        private bool requestedVisibility;
        private bool requestedUseState;

        private Action onPlayerInteraction;

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
            isValidated = distanceToLocation <= 2f;
        }

        private void UsableValidation() {
            var usable = baseObject.GetInteract as UsableAttribute;
            isValidated = usable.IsUsed == requestedUseState;
        }
        
        private void GlassValidation() {
            isValidated = baseObject.GetGlassInteract.IsVisible == requestedVisibility;
        }
    }
}