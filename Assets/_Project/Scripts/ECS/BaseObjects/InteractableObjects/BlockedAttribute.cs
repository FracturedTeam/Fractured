using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class BlockedAttribute : MonoBehaviour {
        private BaseObject baseObject;

        private bool isInitialized;
        
        [Header("Blocked Attribute")]
        [SerializeField] private int ID;
        [SerializeField] private bool doInteractImmediately;
        
        public void Initialize() {
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[BlockedAttribute] BaseObject not found");

                baseObject.GetLockState = LockedState.Locked;
                
                isInitialized = true;
            }
        }

        public void OnInteract(IInteractable interactable)
        {
            var failed = true;
            foreach (var key in PlayerController.Instance.inventory.keys) {
                if (key.ID == ID)
                {
                    failed = false;
                    baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger?.OnInteractSuccess);
                    baseObject.GetLockState = LockedState.Unlocked;
                    PlayerController.Instance.inventory.OnKeyUsed(key.ID);
                    if (doInteractImmediately) {
                        switch (interactable.GetBaseObject().GetObjectType) {
                            case ObjectType.Collectable:
                                interactable.OnInteract(ObjectInteraction.Grab);
                                break;
                            case ObjectType.Moveable:
                                interactable.OnInteract(ObjectInteraction.Grab);
                                break;
                            case ObjectType.Usable:
                                interactable.OnInteract(ObjectInteraction.Contextual);
                                break;
                            default:
                                Debug.LogWarning($"[BlockedAttribute] Interactable type {interactable.GetBaseObject().GetObjectType} not supported");
                                break;
                        }
                    }
                    break;
                }
            }
            if(failed)
                baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger?.OnInteractFailed);
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