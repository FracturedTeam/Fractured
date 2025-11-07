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
        
        private bool isViewing = false;
        
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
            
            isViewing = true;
            baseObject.SetInteract(false);
            memoryShard.DisplayMemory(memorySprite);
        }

        private void StopMemoryInteraction() {
            isViewing = false;
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
    }
}