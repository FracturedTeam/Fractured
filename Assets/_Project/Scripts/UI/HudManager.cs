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
        [SerializeField] private Image interactionImage;
        [field:SerializeField] public Transform glassHolder {get; private set;}

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
        [SerializeField] private string dialogueInteraction = "";
        [SerializeField] private string enterPressurePlate = "[E] to interact";
        [SerializeField] private string leavePressurePlate = "[E] to leave";
        [SerializeField] private string putObjectPressurePlate = "[E] to put object on";
        [SerializeField] private string pickObjectPressurePlate = "Hold [E] to pick up";
        
        [Header("Interaction Image")] 
        [SerializeField] private Sprite spriteNormal;
        [SerializeField] private Sprite spriteGlass;
        [SerializeField] private Sprite spriteUseDoor;
        [SerializeField] private Sprite spriteKey;
        [SerializeField] private Sprite spriteUp;
        [SerializeField] private Sprite spriteDown;
        
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
            interactionUI.alpha = 0;
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
        
        public void ResetText() {
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
                    Interaction.EnterPressurePlate => $"{enterPressurePlate}",
                    Interaction.LeavePressurePlate => $"{leavePressurePlate}",
                    Interaction.PutObjectOnPressurePlate => $"{putObjectPressurePlate}",
                    Interaction.PickObjectOnPressurePlate => $"{pickObjectPressurePlate}",
                    _ => "Not supported"
                };
                
                interactionImage.sprite = e.Interaction switch
                {
                    Interaction.Grab => spriteUp,
                    Interaction.ObtainShard => spriteGlass,
                    Interaction.UseDoor => spriteUseDoor,
                    Interaction.UseKey => spriteKey,
                    Interaction.UseFragment => spriteDown,
                    Interaction.needFragment => spriteGlass,
                    Interaction.needKey => spriteKey,
                    _ => spriteNormal
                };
                
                interactTween = interactionUI.DOFade(e.ShowInteraction ? 1f : 0f, 0.25f);
            }

            public static void InteractionSetPosition(Vector3 position)
            {
                Instance.interactionUI.transform.position = new Vector3(position.x, position.y + 2f, 0f);
            }

            void ShowMemory(MemoryEvent e) {
                memoryTween.Kill();
                
                memoryImage.sprite = e.memory;
                
                memoryTween = memoryHUD.DOFade(e.showMemory ? 1f : 0f, 0.25f);
            }

        #endregion
        
    }
}
