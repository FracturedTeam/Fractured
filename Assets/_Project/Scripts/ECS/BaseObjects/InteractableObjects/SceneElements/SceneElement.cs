using System;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class SceneElement : MonoBehaviour {
        public BaseObject baseObject {get; private set;}
        private SceneMaster masterValidation;

        private bool isValidated;
        public bool IsValidated {
            get => isValidated;
            private set {
                isValidated = value;
                if(masterValidation)
                    masterValidation.CheckForValidation();
                else 
                    Debug.LogError($"{gameObject.name} does not have a scene master register");
            }
        }

        public enum ValidationMethod {Position, GlassState, UseState}
        public ValidationMethod validationMethod;
        
        public Collider requestedCollisionArea;
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
            if (onPlayerInteraction != null) {
                onPlayerInteraction.Invoke();
                return;
            }
            
            switch (validationMethod) {
                case ValidationMethod.Position:
                    PositionValidation();
                    break;
                case ValidationMethod.GlassState:
                    GlassValidation();
                    break;
                case ValidationMethod.UseState:
                    UsableValidation();
                    break;
            }
        }

        public void UnValidate() {
            IsValidated = false;
        }
        
        private void PositionValidation() {
            if (requestedCollisionArea.TryGetComponent(out SphereCollider collider)) {
                var distanceToLocation = Vector3.Distance(requestedCollisionArea.transform.position, transform.position);
                IsValidated = distanceToLocation <= collider.radius;
                return;
            }
            
            IsValidated = requestedCollisionArea.bounds.Contains(transform.position);
        }

        private void UsableValidation() {
            var usable = baseObject.GetInteract as UsableAttribute;
            IsValidated = usable?.IsUsed == requestedUseState;
        }
        
        private void GlassValidation() {
            IsValidated = baseObject.GetGlassInteract.IsVisible == requestedVisibility;
        }

        public void SetDebugUseState(bool state) {
            var usable = baseObject.GetInteract as UsableAttribute;
            usable?.SetUseState(state);
        }

        public void SetValidate() {
            isValidated = true;
        }
        
        public void SetSceneMaster(SceneMaster master) {
            masterValidation = master;
        }
    }
}