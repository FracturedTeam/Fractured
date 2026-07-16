using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class SimpleInteractionAttribute : MonoBehaviour, IInteractable {
        private BaseObject baseObject;

        private bool isInitialized;
        
        public void Initialize() {
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[SimpleInteractionAttribute] Cannot find {nameof(BaseObject)} in {nameof(SimpleInteractionAttribute)}");

                baseObject.GetObjectType = ObjectType.SimpleInteraction;
                
                baseObject.SetInteract(true);
                
                isInitialized = true;
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null)
        {
           
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