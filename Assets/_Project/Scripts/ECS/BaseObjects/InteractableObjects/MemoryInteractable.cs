using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class MemoryInteractable : MonoBehaviour,  IInteractable {
        private BaseObject baseObject;
        
        private bool initialized = false;

        [SerializeField] private Sprite memorySprite;
        [SerializeField] private Glass memoryShard;

        private KeyInteractable key;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b))
                    baseObject = b;
                else
                    Debug.LogError($"[MemoryInteractable] Cannot find {nameof(BaseObject)} in {nameof(MemoryInteractable)}");
                
                if(TryGetComponent(out KeyInteractable k))
                    key = k;
            }
            
            initialized = true;
            
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (key) {
                if(!key.Completed()) return;
            }
            
            switch (interaction) {
                case ObjectInteraction.EnterMemory:
                    DisplayMemory();
                    break;
                case ObjectInteraction.LeaveMemory:
                    StopMemoryInteraction();
                    break;
                default:
                    Debug.LogWarning($"[MemoryInteractable] Unhandled interaction {interaction}");
                    break;
            }
        }

        void DisplayMemory() {
            Debug.Log($"[MemoryInteractable] Displaying memory");
            
            baseObject.SetInteract(false);
            memoryShard.DisplayMemory(memorySprite);
        }

        private void StopMemoryInteraction() {
            baseObject.SetInteract(true);
            memoryShard.LeaveMemory();
            
            Debug.Log($"[MemoryInteractable] Leaving memory");
        }
        
        public void ResetObject() {
            Debug.Log($"[MemoryInteractable] Reset {nameof(MemoryInteractable)}");
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
        
        public KeyInteractable GetKeyInteractable() {
            return key;
        }

        public bool Unlock() {
            return !key || key.Completed();
        }
    }
}