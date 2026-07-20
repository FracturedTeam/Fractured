using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class LockedAttribute : MonoBehaviour {
        protected BaseObject baseObject;
        private bool isInitialized;
        
        public virtual void Initialize() {
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[BlockedAttribute] BaseObject not found");

                baseObject.GetLockState = LockedState.Locked;
                
                isInitialized = true;
            }
        }

        public virtual void OnInteract(IInteractable interactable) {
        }

        public void ForceUnlock() {
            baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger?.OnInteractSuccess);
            baseObject.GetLockState = LockedState.Unlocked;
        }
    }
}