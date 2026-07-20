using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Key = _Project.Scripts.Player.Key;

namespace _Project.Scripts.UI.Gameplay {
    public class InventoryManager : MonoBehaviour {

        private bool isOpen = false;
        
        [Header("Item Display Settings")]
        [SerializeField] private RectTransform itemDisplay;
        [SerializeField] private Vector3 closePosition;
        [SerializeField] private Vector3 openPosition;
        [SerializeField] private ItemHolder[] itemHolder;
        
        [Header("Key Settings")]
        [SerializeField] private RectTransform keyDisplay;
        [SerializeField] private KeyHolder[] keyHolder;
        
        Tweener openInventoryTween;
        
        private EventBinding<AddItemEvent> addItemEventBinding;
        private EventBinding<RemoveItemEvent> removeItemEventBinding;
        private EventBinding<AddKeyEvent> addKeyEventBinding;
        private EventBinding<RemoveKeyEvent> removeKeyEventBinding;

        private void Start() {
            foreach (var item in itemHolder) {
                item.SetInventory(this);
            }
        }
        
        private void OnEnable() {
            addItemEventBinding = new EventBinding<AddItemEvent>(AddItem);
            EventBus<AddItemEvent>.Register(addItemEventBinding);
            removeItemEventBinding = new EventBinding<RemoveItemEvent>(RemoveItem);
            EventBus<RemoveItemEvent>.Register(removeItemEventBinding);
            
            addKeyEventBinding  = new EventBinding<AddKeyEvent>(AddKey);
            EventBus<AddKeyEvent>.Register(addKeyEventBinding);
            removeKeyEventBinding = new EventBinding<RemoveKeyEvent>(RemoveKey);
            EventBus<RemoveKeyEvent>.Register(removeKeyEventBinding);

            InputsBrain.Instance.OnInventoryOpen += OpenInventory;
        }

        private void OnDisable() {
            EventBus<AddItemEvent>.Deregister(addItemEventBinding);
            EventBus<RemoveItemEvent>.Deregister(removeItemEventBinding);
            
            EventBus<AddKeyEvent>.Deregister(addKeyEventBinding);
            EventBus<RemoveKeyEvent>.Deregister(removeKeyEventBinding);
            
            InputsBrain.Instance.OnInventoryOpen -= OpenInventory;
            
            openInventoryTween.Kill();
        }

        private void OpenInventory(InputAction.CallbackContext context) {
            OpenInventory();
        }
        
        public void OpenInventory() {
            isOpen = !isOpen;
            
            openInventoryTween = itemDisplay.DOAnchorPos3D(isOpen ? openPosition : closePosition, 0.5f, true);
        }

        #region Items
        private void AddItem(AddItemEvent evt) {
            foreach (var item in itemHolder) {
                if (item.gameObject.activeSelf) continue;
                
                item.gameObject.SetActive(true);
                item.SetItem(evt.item);
                break;
            }
        }

        private void RemoveItem(RemoveItemEvent evt) {
            foreach (var item in itemHolder) {
                if (item.worldItem == evt.item.worldItem) {
                    item.gameObject.SetActive(false);
                    item.ResetItem();
                    break;
                }
            }
        }

        public void StopHoldingObject() {
            foreach (var item in itemHolder) {
                if (item.isHeld) {
                    item.HeldItem();
                    break;
                }
            }
        }
     #endregion

        private void AddKey(AddKeyEvent evt) {
            foreach (var key in keyHolder) {
                if (key.gameObject.activeSelf) continue;
                
                key.gameObject.SetActive(true);
                key.SetKey(evt.key);
                break;
            }
        }

        private void RemoveKey(RemoveKeyEvent evt) {
            foreach (var key in keyHolder) {
                if (key.ID == evt.key.ID) {
                    key.gameObject.SetActive(false);
                    key.ResetKey();
                    break;
                }
            }
        }
    }

    public struct AddItemEvent : IEvent {
        public Item item;
    }

    public struct RemoveItemEvent : IEvent {
        public Item item;
    }
    
    public struct AddKeyEvent : IEvent {
        public Key key;
    }

    public struct RemoveKeyEvent : IEvent {
        public Key key;
    }
}