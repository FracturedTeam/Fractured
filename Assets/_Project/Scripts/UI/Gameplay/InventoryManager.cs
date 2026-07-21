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
        [SerializeField] private RectTransform itemHighlight;
        [SerializeField] private Vector3 closePosition;
        [SerializeField] private Vector3 openPosition;
        [SerializeField] private ItemHolder[] itemHolder;
        [SerializeField] private CanvasGroup itemGroup;
        
        [Header("Key Settings")]
        [SerializeField] private RectTransform keyDisplay;
        [SerializeField] private KeyHolder[] keyHolder;
        
        Tweener openInventoryTween;
        
        private EventBinding<ProcessItemEvent> addItemEventBinding;
        private EventBinding<ProcessKeyEvent> addKeyEventBinding;
        private EventBinding<ShowInventoryEvent> showInventoryEventBinding;
        private EventBinding<SelectItemEvent> selectItemEventBinding;

        private ItemHolder selectedItem;
        
        private void Start() {
            foreach (var item in itemHolder) {
                item.SetInventory(this);
            }
        }
        
        private void OnEnable() {
            addItemEventBinding = new EventBinding<ProcessItemEvent>(ProcessItem);
            EventBus<ProcessItemEvent>.Register(addItemEventBinding);
            
            addKeyEventBinding  = new EventBinding<ProcessKeyEvent>(ProcessKey);
            EventBus<ProcessKeyEvent>.Register(addKeyEventBinding);

            showInventoryEventBinding = new EventBinding<ShowInventoryEvent>(ShowInventory);
            EventBus<ShowInventoryEvent>.Register(showInventoryEventBinding);

            selectItemEventBinding = new EventBinding<SelectItemEvent>(SelectItem);
            EventBus<SelectItemEvent>.Register(selectItemEventBinding);
            
            InputsBrain.Instance.OnInventoryOpen += OpenInventory;
            InputsBrain.Instance.OnSecondaryInteract += HoldItemGamepad;
        }

        private void OnDisable() {
            EventBus<ProcessItemEvent>.Deregister(addItemEventBinding);
            EventBus<ProcessKeyEvent>.Deregister(addKeyEventBinding);
            EventBus<ShowInventoryEvent>.Deregister(showInventoryEventBinding);
            EventBus<SelectItemEvent>.Deregister(selectItemEventBinding);
            
            InputsBrain.Instance.OnInventoryOpen -= OpenInventory;
            InputsBrain.Instance.OnSecondaryInteract -= HoldItemGamepad;
            
            openInventoryTween.Kill();
        }

        private void OpenInventory(InputAction.CallbackContext context) {
            OpenInventory();
        }
        
        public void OpenInventory() {
            if(!itemGroup.interactable) return;
            isOpen = !isOpen;
            
            openInventoryTween = itemDisplay.DOAnchorPos3D(isOpen ? openPosition : closePosition, 0.5f, true);
        }

        private void ShowInventory(ShowInventoryEvent evt) {
            itemGroup.DOFade(evt.doShow ? 1f : 0f, 0.5f);
            itemGroup.interactable = evt.doShow;
            itemGroup.blocksRaycasts = evt.doShow;
            itemHighlight.gameObject.SetActive(evt.doShow);
            
            if (!evt.doShow) isOpen = false;
            openInventoryTween = itemDisplay.DOAnchorPos3D(isOpen ? openPosition : closePosition, 0.5f, true);
        }
        
        #region Items

        private void ProcessItem(ProcessItemEvent evt) {
            if(evt.isAddingItem) AddItem(evt.item);
            else RemoveItem(evt.item);
        }
        
        private void AddItem(Item evt) {
            foreach (var item in itemHolder) {
                if (item.gameObject.activeSelf) continue;
                
                item.gameObject.SetActive(true);
                item.SetItem(evt);
                break;
            }
        }

        private void RemoveItem(Item evt) {
            foreach (var item in itemHolder) {
                if (item.worldItem == evt.worldItem) {
                    item.gameObject.SetActive(false);
                    item.ResetItem();
                    break;
                }
            }
        }
        
        private void SelectItem(SelectItemEvent evt) {
            SetHighlight(evt.selectedItem);
        }

        private void SetHighlight(Item wantedItem) {
            selectedItem = GetItem(wantedItem);
            if (selectedItem) {
                itemHighlight.position = selectedItem.transform.position;
            }
            else {
                itemHighlight.gameObject.SetActive(false);
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

        private void HoldItemGamepad(InputAction.CallbackContext context) {
            if(InputsBrain.Instance.IsKeyboardControl) return;
            if (context.performed) {
                if(selectedItem == null) return;
                if(isOpen || selectedItem.isHeld)
                    selectedItem.HeldItem();
            }
        }

        private ItemHolder GetItem(Item wantedItem) {
            foreach (var item in itemHolder) {
                if (item.worldItem == wantedItem.worldItem) {
                    return item;
                }
            }
            
            return null;
        }
        
     #endregion

        #region Key
    
        private void ProcessKey(ProcessKeyEvent evt) {
            if(evt.isAddingKey) AddKey(evt.key);
            else RemoveKey(evt.key);
        }
        
        private void AddKey(Key evt) {
            foreach (var key in keyHolder) {
                if (key.gameObject.activeSelf) continue;
                   
                key.gameObject.SetActive(true);
                key.SetKey(evt);
                break;
            }
        }
    
        private void RemoveKey(Key evt) {
            foreach (var key in keyHolder) {
                if (key.ID == evt.ID) {
                    key.gameObject.SetActive(false);
                    key.ResetKey();
                    break;
                }
            }
        }
    
        #endregion
        
    }

    public struct ProcessItemEvent : IEvent {
        public Item item;
        public bool isAddingItem;
    }
    
    public struct ProcessKeyEvent : IEvent {
        public Key key;
        public bool isAddingKey;
    }

    public struct ShowInventoryEvent : IEvent {
        public bool doShow;
    }

    public struct SelectItemEvent : IEvent {
        public Item selectedItem;
    }
}