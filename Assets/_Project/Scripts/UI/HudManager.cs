using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class HudManager : PersistentSingleton<HudManager>
    {
        [Header("HUD")]
        [SerializeField] private CanvasGroup interactionUI;
        [SerializeField] private TextMeshProUGUI interactionText;

        [Header("Interaction Texts")] 
        [SerializeField] private string grab = "Pick up";
        [SerializeField] private string obtainShard = "Break the frame";
        [SerializeField] private string enterMemory = "View Memory";
        [SerializeField] private string leaveMemory = "Leave";
        [SerializeField] private string useDoor = "Enter";
        [SerializeField] private string useKey = "Unlock the door";
        [SerializeField] private string useFragment = "Put";
        [SerializeField] private string needFragment = "[E] to interact";
        [SerializeField] private string needKey = "Door locked";
        [SerializeField] private string needSomethingElse = "[E] to interact";
        [SerializeField] private string dialogueInteraction = "...";
        
        [Header("Memory")]
        [SerializeField] private CanvasGroup memoryHUD;
        [SerializeField] private Image memoryImage;
        
        private EventBinding<InteractEvent> interactEventBinding;
        private EventBinding<MemoryEvent> memoryEventBinding;
        private Tweener interactTween;
        private Tweener memoryTween;
        
        [Header("Dialogue")]
        [SerializeField] private GlassText glassText;
        private DialogueScriptableObject currentDialogue;
        private CountdownTimer textTimer;

        private void Start() {
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop  += ResetText;
        }
        
        void OnEnable() {
            interactEventBinding = new EventBinding<InteractEvent>(ShowInteraction);
            EventBus<InteractEvent>.Register(interactEventBinding);
            memoryEventBinding = new EventBinding<MemoryEvent>(ShowMemory);
            EventBus<MemoryEvent>.Register(memoryEventBinding);
            
        }

        void OnDisable() {
            EventBus<InteractEvent>.Deregister(interactEventBinding);
            EventBus<MemoryEvent>.Deregister(memoryEventBinding);
            
            textTimer.OnTimerStop  -= ResetText;
        }

        public void SetText(DialogueScriptableObject newDialogue) {
            if(!glassText || !newDialogue)
                return;
            
            currentDialogue = newDialogue;
            
            glassText.Setup(currentDialogue);
            
            if (currentDialogue.time <= 0)
                return;
            
            textTimer.Reset(currentDialogue.time);
            textTimer.Start();
        }
        
        private void ResetText() {
            if(currentDialogue && currentDialogue.next)
                SetText(currentDialogue.next);
            else
                glassText.Setup(null);
        }

        #region InteractionHUD
        
            void ShowInteraction(InteractEvent e) {
                interactTween.Kill();

                interactionText.text = e.Interaction switch {
                    Interaction.Grab => $"{grab} {e.ObjectName}",
                    Interaction.ObtainShard => $"{obtainShard}",
                    Interaction.EnterMemory => $"{enterMemory}",
                    Interaction.LeaveMemory => $"{leaveMemory}",
                    Interaction.UseDoor  => $"{useDoor} {e.ObjectName}",
                    Interaction.UseKey =>  $"{useKey}",
                    Interaction.UseFragment => $"{useFragment} {e.ObjectName}",
                    Interaction.needFragment => $"{needFragment}",
                    Interaction.needKey  => $"{needKey}",
                    Interaction.needSomethingElse => $"{needSomethingElse}",
                    Interaction.dialogue => $"{dialogueInteraction}",
                    _ => "Not supported"
                };
                
                interactTween = interactionUI.DOFade(e.ShowInteraction ? 1f : 0f, 0.25f);
            }

            void ShowMemory(MemoryEvent e) {
                memoryTween.Kill();
                
                memoryImage.sprite = e.memory;
                
                memoryTween = memoryHUD.DOFade(e.showMemory ? 1f : 0f, 0.25f);
            }

        #endregion
        
    }
}
