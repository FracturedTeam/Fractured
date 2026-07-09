using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.UI.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Player {
    public class PlayerInventory : MonoBehaviour {
        public List<Item> items;
        public List<Key> keys;

        public void OnKeyPickUp(KeyAttribute key) {
            var newKey = new Key() {
                keySprite = key.keySprite,
                ID = key.ID,
                oneTimeUse = key.oneTimeUse
            };
            keys.Add(newKey);
            
            EventBus<AddKeyEvent>.Raise(new AddKeyEvent{key = newKey});
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
                    EventBus<RemoveKeyEvent>.Raise(new RemoveKeyEvent{key = keys[index]});
                    keys.RemoveAt(index);
                }

                return true;
            }
            
            return false;
        }
        
        public void OnItemPickedUp(CollectableAttribute item) {
            var newItem = new Item {
                itemName = item.GetBaseObject().name,
                Icon = item.itemSprite,
                worldItem = item.GetBaseObject()
            };
            items.Add(newItem);
            
            EventBus<AddItemEvent>.Raise(new AddItemEvent{item = newItem});
        }

        public void OnItemDropped(BaseObject collectable) {
            for (var i = 0; i < items.Capacity; i++) {
                if (items[i].worldItem == collectable) {
                    EventBus<RemoveItemEvent>.Raise(new RemoveItemEvent{item = items[i]});
                    items.RemoveAt(i);
                    break;
                }
            }
            
        }
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
    }
}