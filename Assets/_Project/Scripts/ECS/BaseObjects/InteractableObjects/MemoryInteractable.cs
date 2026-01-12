using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.UI;
using FMODUnity;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    
    public struct MemoryEvent : IEvent {
        public bool showMemory;
        public Sprite memory;
    }
    
    [RequireComponent(typeof(BaseObject))]
    public class MemoryInteractable : MonoBehaviour,  IInteractable {
        private BaseObject baseObject;
        
        [SerializeField] private int unlockedMemoryId;
        
        [SerializeField] private Sprite memorySprite;
        private KeyInteractable key;
        
        [Header("Sounds")]
        [SerializeField] private EventReference associateMemorySound;
        
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[MemoryInteractable] Cannot find {nameof(BaseObject)} in {nameof(MemoryInteractable)}");
                
                if(TryGetComponent(out KeyInteractable k)) key = k;

                baseObject.GetInteractionType = ObjectType.Memory;
                
                gameObject.layer = LayerMask.NameToLayer("MemoryObject");
            }
            
            initialized = true;
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (key) {
                if(baseObject.GetCompletion is not InteractionCompletion.Completed)
                {
                    if (other == null)
                    {
                        if(baseObject.cantInteractDialogue is not { alreadyInteracted: true, oneTime: true })
                        {
                            HudManager.Instance.SetText(baseObject.cantInteractDialogue.dialogue);
                            baseObject.cantInteractDialogue.alreadyInteracted = true;
                        }
                        return;
                    }
                    
                    if (baseObject.failedDialogue is not { oneTime: true, alreadyInteracted: true }) 
                        return;
                    
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.failedDialogue.alreadyInteracted = true;
                    
                    return;
                }
                    
            }
            
            switch (interaction) {
                case ObjectInteraction.EnterMemory:
                {
                    DisplayMemory();
                    
                }
                    break;
                case ObjectInteraction.LeaveMemory:
                    StopMemoryInteraction();
                    break;
                case ObjectInteraction.Remove:
                        key?.OnInteract(ObjectInteraction.Remove);
                    break;
                default:
                    Debug.LogWarning($"[MemoryInteractable] Unhandled interaction {interaction} on {nameof(MemoryInteractable)}");
                    break;
            }
        }

        public void Tick(float deltaTime) {
        }

        public void CompleteObject() {
        }

        void DisplayMemory() {
            baseObject.SetInteract(false);
            
            /*
            EventBus<MemoryEvent>.Raise(new MemoryEvent {
                showMemory = true,
                memory = memorySprite
            });
            */
            
            //AudioManager.Instance.PlayEnterMemorySound(transform.position);
            EventBus<MemorySound>.Raise(new MemorySound {
                inMemory = true
            });
            AudioManager.Instance.PlayOneShot(associateMemorySound, transform.position);
            MemoryManager.Instance.SetMemory(true, unlockedMemoryId,  memorySprite);
            
            
            Debug.Log($"[MemoryInteractable] Entering memory");
            
            if (baseObject.successDialogue is { oneTime: true, alreadyInteracted: true })
                return;
            
            print("show memory");
            HudManager.Instance.SetText(baseObject.successDialogue.dialogue);
            baseObject.successDialogue.alreadyInteracted = true;
        }

        private void StopMemoryInteraction() {
            baseObject.SetInteract(true);
            /*
            EventBus<MemoryEvent>.Raise(new MemoryEvent {
                showMemory = false,
                memory = null
            });
            */
            EventBus<MemorySound>.Raise(new MemorySound {
                inMemory = false
            });
            MemoryManager.Instance.SetMemory(false);
            HudManager.Instance.ResetText();
            Debug.Log($"[MemoryInteractable] Leaving memory");
        }
        
        public void ResetObject() {
            if(key)
                key.ResetObject();
            Debug.Log($"[MemoryInteractable] Reset {nameof(MemoryInteractable)}");
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}