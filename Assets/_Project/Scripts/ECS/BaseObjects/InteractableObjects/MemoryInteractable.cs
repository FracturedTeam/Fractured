using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Systems.EventBus;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    
    public struct MemoryEvent : IEvent {
        public bool showMemory;
        public Sprite memory;
    }
    
    [RequireComponent(typeof(BaseObject))]
    public class MemoryInteractable : MonoBehaviour,  IInteractable {
        private BaseObject baseObject;
        
        [SerializeField] private Sprite memorySprite;
        private KeyInteractable key;
        
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b))
                    baseObject = b;
                else
                    Debug.LogError($"[MemoryInteractable] Cannot find {nameof(BaseObject)} in {nameof(MemoryInteractable)}");
                
                if(TryGetComponent(out KeyInteractable k))
                    key = k;

                //baseObject.Completion = InteractionCompletion.None;
                baseObject.GetType = ObjectType.Memory;
            }
            
            initialized = true;
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (key) {
                if(key.GetBaseObject().Completion is not InteractionCompletion.Completed) return;
            }
            
            switch (interaction) {
                case ObjectInteraction.EnterMemory:
                    DisplayMemory();
                    break;
                case ObjectInteraction.LeaveMemory:
                    StopMemoryInteraction();
                    break;
                default:
                    Debug.LogWarning($"[MemoryInteractable] Unhandled interaction {interaction} on {nameof(MemoryInteractable)}");
                    break;
            }
        }

        void DisplayMemory() {
            baseObject.SetInteract(false);
            EventBus<MemoryEvent>.Raise(new MemoryEvent {
                showMemory = true,
                memory = memorySprite
            });
            Debug.Log($"[MemoryInteractable] Entering memory");
        }

        private void StopMemoryInteraction() {
            baseObject.SetInteract(true);
            EventBus<MemoryEvent>.Raise(new MemoryEvent {
                showMemory = false,
                memory = null
            });
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
    }
}