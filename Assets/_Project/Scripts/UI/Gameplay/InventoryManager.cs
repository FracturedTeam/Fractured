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
        [SerializeField] private CanvasGroup itemGroup;
        
        [Header("Key Settings")]
        [SerializeField] private RectTransform keyDisplay;
        [SerializeField] private KeyHolder[] keyHolder;
        
        [Header("Item Btt")]
        [SerializeField] private RectTransform itemBtt;
        
        [Header("Gamepad Visual")]
        [SerializeField] private GameObject[] gamepadVisuals;
        
        Tweener openInventoryTween;
        Tweener textTween;
        
        private EventBinding<ProcessItemEvent> addItemEventBinding;
        private EventBinding<ProcessKeyEvent> addKeyEventBinding;
        private EventBinding<ShowInventoryEvent> showInventoryEventBinding;
        private EventBinding<SelectItemEvent> selectItemEventBinding;
        private EventBinding<ClearInventoryEvent> clearItemEventBinding;

        private ItemHolder selectedItem;
        
        private bool isGamepadControlled = false;
        
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

            clearItemEventBinding = new EventBinding<ClearInventoryEvent>(ClearInventory);
            EventBus<ClearInventoryEvent>.Register(clearItemEventBinding);
            
            InputsBrain.Instance.OnInventoryOpen += OpenInventory;
            InputsBrain.Instance.OnSecondaryInteract += HoldItemGamepad;
            InputsBrain.Instance.OnGamepadControlled += UpdateGamepadControlled;
        }
        
        private void OnDisable() {
            EventBus<ProcessItemEvent>.Deregister(addItemEventBinding);
            EventBus<ProcessKeyEvent>.Deregister(addKeyEventBinding);
            EventBus<ShowInventoryEvent>.Deregister(showInventoryEventBinding);
            EventBus<SelectItemEvent>.Deregister(selectItemEventBinding);
            EventBus<ClearInventoryEvent>.Deregister(clearItemEventBinding);

            if (InputsBrain.HasInstance) {
                InputsBrain.Instance.OnInventoryOpen -= OpenInventory;
                InputsBrain.Instance.OnSecondaryInteract -= HoldItemGamepad;
                InputsBrain.Instance.OnGamepadControlled -= UpdateGamepadControlled;
            }
            
            openInventoryTween.Kill();
        }

        private void UpdateGamepadControlled(bool isGamepad) {
            isGamepadControlled = isGamepad;
            
            // Update l'UI également
            foreach (var pad in gamepadVisuals) {
                pad.SetActive(isGamepad);
            }
        }
        
        private void OpenInventory(InputAction.CallbackContext context) {
            OpenInventory();
        }
        
        public void OpenInventory() {
            if(!itemGroup.interactable) return;
            isOpen = !isOpen;
            
            itemBtt.rotation = Quaternion.Euler(0, 0, isOpen ? 0 : 180);
            
            openInventoryTween = itemDisplay.DOAnchorPos3D(isOpen ? openPosition : closePosition, 0.5f, true);

            if (selectedItem && isGamepadControlled) {
                selectedItem.itemHighlight.SetActive(true);
                textTween = selectedItem.text.DOFade(isOpen ? 1 : 0, 0.25f);
            }
        }

        // Fonction pour montrer l'inventaire ou non si le joueur possède des items
        private void ShowInventory(ShowInventoryEvent evt) {
            HideInventory(evt.doShow);
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
            textTween?.Kill();
            foreach (var item in itemHolder) {
                item.itemHighlight.SetActive(false);
                item.text.alpha = 0f;
            }
            
            SetHighlight(evt.wantedItem);
        }

        private void SetHighlight(Item wantedItem) {
            selectedItem = GetItem(wantedItem);
            if (selectedItem && isGamepadControlled) {
                selectedItem.itemHighlight.SetActive(true);
                if(isOpen)
                    textTween = selectedItem.text.DOFade(1f, 0.25f);
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
            if(!isGamepadControlled) return;
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

        private void ClearInventory(ClearInventoryEvent evt) {
            foreach (var item in itemHolder) {
                item.gameObject.SetActive(false);
                item.ResetItem();
            }

            foreach (var key in keyHolder) {
                key.gameObject.SetActive(false);
                key.ResetKey();
            }
            
            HideInventory(false);
        }

        private void HideInventory(bool doShow) {
            itemGroup.DOFade(doShow ? 1f : 0f, 0.5f);
            itemGroup.interactable = doShow;
            itemGroup.blocksRaycasts = doShow;
            
            
            if(selectedItem && isGamepadControlled)
                selectedItem.itemHighlight.SetActive(true);

            if (!doShow) {
                isOpen = false;
                textTween?.Kill();
                foreach (var item in itemHolder) {
                    item.text.alpha = 0;
                }
            }
            
            itemBtt.rotation = Quaternion.Euler(0, 0, isOpen? 0 : 180);
            openInventoryTween = itemDisplay.DOAnchorPos3D(isOpen ? openPosition : closePosition, 0.5f, true);
        }
        
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
        public Item wantedItem;
    }

    public struct ClearInventoryEvent : IEvent {
        
    }
}