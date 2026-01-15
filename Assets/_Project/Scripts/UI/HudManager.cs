using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.ECS;
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
        [SerializeField] private GameObject interactionParent;
        [SerializeField] private CanvasGroup interactionUI;
        [SerializeField] private TextMeshProUGUI interactionText;
        [SerializeField] private Image interactionImage;
        
        [SerializeField] private CanvasGroup interactionUI2;
        [SerializeField] private TextMeshProUGUI interactionText2;
        [SerializeField] private Image interactionImage2;
        [field:SerializeField] public Transform glassHolder {get; private set;}

        [SerializeField] private ParticleSystem spawningParticles;  
        [SerializeField] private int maxShardsOnScreen = 2;
        
        
        private List<ParticleSystem> freeParticles = new List<ParticleSystem>();
        private List<ParticleSystem> usingParticles = new List<ParticleSystem>();
        
        [SerializeField] private Fragment fragment;
        private List<Fragment> freeFragment = new List<Fragment>();
        private List<Fragment> usingFragment = new List<Fragment>();
        
        [SerializeField] private int currentShardsSpawning;
        [SerializeField] private float spawningTime = 1.5f;
        [SerializeField] private Material transitionMaterial;

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
        private ParticleSystem current;
        private Fragment currentF;

        private void Start() {
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop  += ResetText;
            interactionUI.alpha = 0;
            interactionUI2.alpha = 0;

            //Particles pool
            for (int i = 0; i < maxShardsOnScreen; i++)
            {
                var particles = Instantiate(spawningParticles);
                freeParticles.Add(particles);
                particles.gameObject.SetActive(false);
            }
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

        public Fragment ShardSpawn(Glass shard)
        {
            if(freeParticles.Count <= 0)
            {
                Debug.Log("Max particles on screen exceeded, you can change the max number in the HUD");
                return null;
            }
            
            if(freeFragment.Count <= 0)
            {
                var frag = Instantiate(shard.VisualShard);
                freeFragment.Add(frag);
                frag.gameObject.SetActive(false);
            }
            
            current = freeParticles[^1];
            freeParticles.Remove(current);
            usingParticles.Add(current);
            current.gameObject.SetActive(true);
            current.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(shard.transform.position.x, shard.transform.position.y, 10));
            
            currentF = freeFragment[^1];
            shard.VisualShard = currentF;
            freeFragment.Remove(currentF);
            usingFragment.Add(currentF);
            currentF.gameObject.SetActive(true);
            
            StartCoroutine(HideParticles(shard));
            freeFragment.Remove(currentF);
            currentF.gameObject.SetActive(true);
            
            Invoke("HideParticles", spawningTime);
            return currentF;
        }

        private IEnumerator HideParticles(Glass shard)
        {
            transitionMaterial.DOFloat(1, "_Progression", spawningTime/2);
            yield return new WaitForSeconds(spawningTime/2);
            shard.SetUp3dShard();
            transitionMaterial.DOFloat(2, "_Progression", spawningTime/2);
            yield return new WaitForSeconds(spawningTime/2);
            
            currentF.gameObject.SetActive(false);
            current.gameObject.SetActive(false);
            shard.VisualShard.gameObject.SetActive(false);
            
            usingFragment.Remove(currentF);
            freeFragment.Add(currentF);
            
            usingParticles.Remove(current);
            freeParticles.Add(current);
            transitionMaterial.DOFloat(0, "_Progression", 0);
            currentF.gameObject.SetActive(false);
            current.gameObject.SetActive(false);
        }

        #region InteractionHUD
        
            void ShowInteraction(InteractEvent e) {
                interactTween.Kill();

                if (!e.ShowInteraction) {
                    interactTween = interactionUI.DOFade(0f, 0.25f);
                    interactionUI2.DOFade(0f, 0.25f);
                    return;
                }
                
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

                if (e.Interaction == Interaction.EnterMemory)
                {
                    interactionText2.text = $"{pickObjectPressurePlate}";
                    interactionImage2.sprite = spriteNormal;
                }
                interactionUI2.DOFade(e is { Interaction: Interaction.EnterMemory, ShowInteraction: true }  ? 1f : 0f, 0.25f);
                
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
                Instance.interactionParent.transform.position = new Vector3(position.x, position.y + 2f, 0f);
            }

            void ShowMemory(MemoryEvent e) {
                memoryTween.Kill();
                
                memoryImage.sprite = e.memory;
                
                memoryTween = memoryHUD.DOFade(e.showMemory ? 1f : 0f, 0.25f);
            }

        #endregion
        
    }
}
