using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.UI.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player {
    public class PlayerInventory : MonoBehaviour {
        public List<Item> items;
        public List<Key> keys;

        private int itemIndex;
        
        private void Start() {
            EventBus<ShowInventoryEvent>.Raise(new ShowInventoryEvent{doShow = items.Count > 0});
        }

        void OnEnable() {
            InputsBrain.Instance.OnInventorySelect += InventorySelect;
        }

        void OnDisable() {
            InputsBrain.Instance.OnInventorySelect -= InventorySelect;
        }
        
        #region Key

        public void OnKeyPickUp(CollectableAttribute key) {
            var newKey = new Key{
                keySprite = key.itemSprite,
                ID = key.keyID,
                oneTimeUse = key.isOneTimeUse,
                collect = key
            };
            keys.Add(newKey);
            
            EventBus<ProcessKeyEvent>.Raise(new ProcessKeyEvent{key = newKey, isAddingKey = true});
        }

        public void OnKeyUsed(int keyID) {
            if(keys.Count == 0) return;
            
            if (keys.Count == 1) {
                if(IsRequestedKey(0, keyID))
                    return;
            }
            
            for (int i = 0; i < keys.Capacity; i++) {
                if(IsRequestedKey(i, keyID)) break;
            }
        }

        private bool IsRequestedKey(int index, int keyID) {
            if (keys[index].ID == keyID) {
                if (keys[index].oneTimeUse) {
                    EventBus<ProcessKeyEvent>.Raise(new ProcessKeyEvent(){key = keys[index], isAddingKey = false});
                    keys[index].collect.SetHasBeenUse();
                    keys.RemoveAt(index);
                }

                return true;
            }
            
            return false;
        }

        #endregion

        #region Item

        private void InventorySelect(InputAction.CallbackContext ctx) {
            UpdateSelectedItem(ctx.ReadValue<float>());
        }

        private void UpdateSelectedItem(float input) {
            if (items.Count <= 1) {
                itemIndex = 0;
                EventBus<SelectItemEvent>.Raise(new SelectItemEvent{selectedItem = items[itemIndex]});
                return;
            }
            
            if (input > 0) { // Up
                itemIndex++;
                if(itemIndex >= items.Count) itemIndex = 0;
            }
            else if (input < 0) { // Down
                itemIndex--;
                if(itemIndex < 0) itemIndex = items.Count - 1;
            }
            
            EventBus<SelectItemEvent>.Raise(new SelectItemEvent{selectedItem = items[itemIndex]});
        }
        
        public void OnItemPickedUp(CollectableAttribute item) {
            if(items.Count == 0) itemIndex = 0;
            
            var newItem = new Item {
                itemName = item.GetBaseObject().name,
                Icon = item.itemSprite,
                worldItem = item.GetBaseObject()
            };
            items.Add(newItem);
            
            EventBus<ProcessItemEvent>.Raise(new ProcessItemEvent{item = newItem, isAddingItem = true});
            EventBus<ShowInventoryEvent>.Raise(new ShowInventoryEvent{doShow = items.Count > 0});
            EventBus<SelectItemEvent>.Raise(new SelectItemEvent{selectedItem = items[itemIndex]});
        }

        public void OnItemDropped(BaseObject collectable) {
            for (var i = 0; i < items.Capacity; i++) {
                if (items[i].worldItem == collectable) {
                    EventBus<ProcessItemEvent>.Raise(new ProcessItemEvent{item = items[i], isAddingItem = false});
                    items.RemoveAt(i);
                    
                    EventBus<ShowInventoryEvent>.Raise(new ShowInventoryEvent{doShow = items.Count > 0});
                    if (items.Count > 1) UpdateSelectedItem(-1);
                    break;
                }
            }
        }

        #endregion
        
    }
    
    [Serializable]
    public struct Item {
        public string itemName;
        public Sprite Icon;
        public BaseObject worldItem;
    }

    [Serializable]
    public struct Key {
        public string keyName;
        public int ID;
        public Sprite keySprite;
        public bool oneTimeUse;
        public CollectableAttribute collect;
    }
}