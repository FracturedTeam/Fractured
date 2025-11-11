using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class MemoryInteractable : MonoBehaviour,  IInteractable {
        private BaseObject baseObject;
        
        private bool initialized = false;

        [SerializeField] private Sprite memorySprite;
        [SerializeField] private Glass memoryShard;
        
        private bool isUnlocked = true;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b))
                    baseObject = b;
                else
                    Debug.LogError($"[MemoryInteractable] Cannot find {nameof(BaseObject)} in {nameof(MemoryInteractable)}");
            }
            
            initialized = true;
            
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(!isUnlocked) return;
            
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

        public void SetUnlocked(bool value) {
            isUnlocked = value;
        }
        
        public bool MemoryUnlocked() {
            return isUnlocked;
        }
    }
}