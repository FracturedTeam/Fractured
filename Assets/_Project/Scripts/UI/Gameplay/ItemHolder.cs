using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay {
    public class ItemHolder : MonoBehaviour {
        [SerializeField] private Image itemImage;
        public bool isHeld {get; private set;}
        
        public BaseObject worldItem {get; private set;}
        private InventoryManager inventoryManager;

        public void SetInventory(InventoryManager inventoryManager) {
            this.inventoryManager = inventoryManager;
        }
        
        public void SetItem(Item item) {
            itemImage.sprite = item.Icon;
            isHeld = false;
            worldItem = item.worldItem;
        }
        
        public void ResetItem() {
            itemImage.sprite = null;
            isHeld = false;
            worldItem = null;
        }

        public void HeldItem() {
            if (isHeld) {
                isHeld = false;
                worldItem.OnInteract(ObjectInteraction.StopHeld);
                return;
            }

            if (PlayerController.Instance.interact.IsCarrying()) {
                if(PlayerController.Instance.interact.GetCurrentInteractable().GetObjectType is ObjectType.Moveable)
                    return;
                
                inventoryManager.StopHoldingObject();
            }
            
            isHeld = true;
            worldItem.OnInteract(ObjectInteraction.Held);
        }
    }
}