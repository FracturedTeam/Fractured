using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class BlockedAttribute : LockedAttribute {
        [Header("Blocked Attribute")]
        [SerializeField] private int ID;
        [SerializeField] private bool doInteractImmediately;

        public override void OnInteract(IInteractable interactable) {
            var failed = true;
            foreach (var key in PlayerController.Instance.inventory.keys) {
                if (key.ID == ID) {
                    failed = false;
                    
                    baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger?.OnInteractSuccess);
                    baseObject.GetLockState = LockedState.Unlocked;
                    
                    PlayerController.Instance.inventory.OnKeyUsed(key.ID);
                    
                    if (!doInteractImmediately)  return;
                        switch (interactable.GetBaseObject().GetObjectType) {
                            case ObjectType.Collectable or ObjectType.Moveable:
                                interactable.OnInteract(ObjectInteraction.Grab);
                                break;
                            case ObjectType.Usable:
                                interactable.OnInteract(ObjectInteraction.Contextual);
                                break;
                            default:
                                Debug.LogWarning($"[BlockedAttribute] Interactable type {interactable.GetBaseObject().GetObjectType} not supported");
                                break;
                        }
                    
                    break;
                }
            }
            if(failed)
                baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger?.OnInteractFailed);
        }
    }
}