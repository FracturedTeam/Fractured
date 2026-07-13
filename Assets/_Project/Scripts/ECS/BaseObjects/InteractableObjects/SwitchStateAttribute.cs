using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class SwitchStateAttribute : MonoBehaviour, IInteractable {
        private BaseObject baseObject;

        private bool isInitialized;
        [SerializeField] private bool stateOn = true;
        
        public void Initialize() {
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[MoveableObject] Cannot find {nameof(BaseObject)} in {nameof(MovableAttribute)}");

                baseObject.GetObjectType = ObjectType.Inspectable;
                
                baseObject.SetInteract(true);
                
                isInitialized = true;
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null)
        {
            stateOn = !stateOn;
            ForceChangeState(stateOn);
        }

        //In case the GDs want to switch value via another object for some reason
        public void ForceChangeState(bool isOn)
        {
            stateOn = isOn;
            baseObject.GetTrigger?.OnFunction(stateOn ? baseObject.GetTrigger.OnSetStateOn : baseObject.GetTrigger.OnSetStateOff);
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