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

        public void OnKeyUsed(KeyAttribute key) {
            for (int i = 0; i < keys.Capacity; i++) {
                if (keys[i].ID == key.ID) {
                    if (key.oneTimeUse) {
                        EventBus<RemoveKeyEvent>.Raise(new RemoveKeyEvent{key = keys[i]});
                        keys.RemoveAt(i);
                    }
                }
            }
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