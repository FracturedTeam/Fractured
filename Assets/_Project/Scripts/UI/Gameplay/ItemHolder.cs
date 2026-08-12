using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay {
    public class ItemHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] public GameObject itemHighlight;
        [SerializeField] public GameObject selectedHighlight;
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
            text.text = worldItem.ObjectName;
            itemHighlight.SetActive(false);
        }
        
        public void ResetItem() {
            itemImage.sprite = null;
            isHeld = false;
            worldItem = null;
            itemHighlight.SetActive(false);
        }

        public void HeldItem() {
            if (isHeld) {
                isHeld = false;
                selectedHighlight.SetActive(false);
                worldItem.OnInteract(ObjectInteraction.StopHeld);
                return;
            }

            if (PlayerController.Instance.Interact.IsCarrying()) {
                if(PlayerController.Instance.Interact.GetCurrentInteractable().GetObjectType is ObjectType.Moveable)
                    return;
                
                inventoryManager.StopHoldingObject();
                selectedHighlight.SetActive(false);
            }
            
            isHeld = true;
            selectedHighlight.SetActive(true);
            worldItem.OnInteract(ObjectInteraction.Held);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            text.DOFade(1f, 0.25f);
            itemHighlight.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData) {
            text.DOFade(0f, 0.5f);
            itemHighlight.SetActive(false);
        }
    }
}