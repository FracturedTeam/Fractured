using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

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
        [SerializeField] private EventReference associateMemoryLoop;
        private EventInstance soundInstance;
        
        private bool initialized = false;

        private CountdownTimer displayCountdown = new CountdownTimer(0.5f);
        
        public void Initialize() {
            displayCountdown.OnTimerStop += DelayDisplay;
            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[MemoryInteractable] Cannot find {nameof(BaseObject)} in {nameof(MemoryInteractable)}");
                
                if(TryGetComponent(out KeyInteractable k)) key = k;

                baseObject.GetInteractionType = ObjectType.Memory;
                
                gameObject.layer = LayerMask.NameToLayer("MemoryObject");

                if(!associateMemoryLoop.IsNull)
                    soundInstance = AudioManager.Instance.CreateInstance(associateMemoryLoop);
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

        public void Dispose() {
            soundInstance.stop(STOP_MODE.IMMEDIATE);
            soundInstance.release();
            soundInstance.clearHandle();
        }

        public void CompleteObject() {
        }

        void DisplayMemory() {
            baseObject.SetInteract(false);
            AudioManager.Instance.PlayEnterMemorySound(transform.position);
            displayCountdown.Start();
        }

        private void DelayDisplay() {
            EventBus<MemorySound>.Raise(new MemorySound {
                inMemory = true
            });
            
            soundInstance.getPlaybackState(out var playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                soundInstance.start();
            }
            
            MemoryManager.Instance.SetMemory(true, unlockedMemoryId,  memorySprite);
            Debug.Log($"[MemoryInteractable] Entering memory");
            
            if (baseObject.successDialogue is { oneTime: true, alreadyInteracted: true })
                return;
            
            HudManager.Instance.SetText(baseObject.successDialogue.dialogue);
            baseObject.successDialogue.alreadyInteracted = true;
        }
        
        private void StopMemoryInteraction() {
            baseObject.SetInteract(true);
            
            AudioManager.Instance.PlayLeaveMemorySound(transform.position);
            soundInstance.stop(STOP_MODE.ALLOWFADEOUT);
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