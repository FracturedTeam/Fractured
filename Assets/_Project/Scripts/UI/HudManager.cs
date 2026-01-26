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
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class HudManager : PersistentSingleton<HudManager>
    {
        [Header("HUD")]
        [SerializeField] private GameObject interactionParent;
        [SerializeField] private InteractionPopUp interactionUI;
        [SerializeField] private InteractionPopUp interactionUI2;
        [SerializeField] private InteractionPopUp specialUI;
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
        [SerializeField] private Image memoryLine;
        
        private EventBinding<InteractEvent> interactEventBinding;
        private EventBinding<MemoryEvent> memoryEventBinding;
        private Tweener interactTween;
        private Tweener memoryTween;
        
        [Header("Dialogue")]
        [SerializeField] private GlassText glassText;
        private DialogueScriptableObject currentDialogue;
        private CountdownTimer textTimer;
        
        [Header("Glass Animation")]
        [SerializeField] private Fragment fragment;
        [SerializeField] private ParticleSystem spawningParticles;  
        
        [SerializeField] private int currentShardsSpawning;
        [SerializeField] private float firstHalfTime = 1.0f;
        [SerializeField] private float secondHalfTime = 0.5f;
        [SerializeField] private Material transitionMaterial;
        
        private ParticleSystem currentParticle;
        private Fragment currentFrag;
        
        private readonly List<ParticleSystem> freeParticles = new List<ParticleSystem>();
        private readonly List<Fragment> freeFragment = new List<Fragment>();
        private Camera mainCamera;

        private void Start() {
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop += ResetText;
            interactionUI.GetGroup.alpha = 0;
            interactionUI2.GetGroup.alpha = 0;
            specialUI.GetGroup.alpha = 0;
            mainCamera = Camera.main;
        }

        private void OnEnable() {
            interactEventBinding = new EventBinding<InteractEvent>(ShowInteraction);
            EventBus<InteractEvent>.Register(interactEventBinding);
            memoryEventBinding = new EventBinding<MemoryEvent>(ShowMemory);
            EventBus<MemoryEvent>.Register(memoryEventBinding);
        }

        private void OnDisable() {
            EventBus<InteractEvent>.Deregister(interactEventBinding);
            EventBus<MemoryEvent>.Deregister(memoryEventBinding);
            
            interactTween?.Kill();
            memoryTween?.Kill();
            
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

        public void ShardSpawn(Glass shard)
        {
            if(freeParticles.Count <= 0)
            {
                var particle =  Instantiate(spawningParticles, mainCamera.transform);
                particle.transform.localPosition = new Vector3(0, 5, 15);
                freeParticles.Add(particle);
            }
            
            if(freeFragment.Count <= 0)
            {
                var frag = Instantiate(fragment);
                freeFragment.Add(frag);
            }
            
            currentParticle = freeParticles[^1];
            shard.visualParticles = currentParticle;
            freeParticles.Remove(currentParticle);
            currentParticle.gameObject.SetActive(true);
            
            
            currentFrag = freeFragment[^1];
            shard.visualShard = currentFrag;
            freeFragment.Remove(currentFrag);
            currentFrag.gameObject.SetActive(true);
            
            StartCoroutine(HideParticles(shard));
        }

        private IEnumerator HideParticles(Glass shard)
        {
            transitionMaterial.DOFloat(1, "_Progression", firstHalfTime);
            yield return new WaitForSeconds(firstHalfTime);
            shard.SetUp3dShard();
            transitionMaterial.DOFloat(2, "_Progression", secondHalfTime);
            yield return new WaitForSeconds(secondHalfTime);
            
            
            shard.visualParticles.gameObject.SetActive(false);
            shard.visualShard.gameObject.SetActive(false);
            
            freeFragment.Add(shard.visualShard);
            freeParticles.Add(shard.visualParticles);
            
            transitionMaterial.SetFloat("_Progression",  0);
        }

        #region InteractionHUD

        private void ShowInteraction(InteractEvent e) {
                interactTween.Kill();

                if (!e.ShowInteraction || e.Interaction == Interaction.None) {
                    interactTween = interactionUI.GetGroup.DOFade(0f, 0.25f);
                    interactionUI2.GetGroup.DOFade(0f, 0.25f);
                    return;
                }
                
                interactionUI.GetInteractionText.text = e.Interaction switch {
                    Interaction.Grab => $"{grab} {e.ObjectName}",
                    Interaction.ObtainShard => $"{obtainShard}",
                    Interaction.EnterMemory => $"{enterMemory}",
                    Interaction.LeaveMemory => $"{leaveMemory}",
                    Interaction.UseDoor  => $"{useDoor} {e.ObjectName}",
                    Interaction.UseKey =>  $"{useKey}",
                    Interaction.UseFragment => $"{useFragment} {e.ObjectName}",
                    Interaction.NeedFragment => $"{needFragment}",
                    Interaction.NeedKey  => $"{needKey}",
                    Interaction.NeedSomethingElse => $"{needSomethingElse}",
                    Interaction.Dialogue => $"{dialogueInteraction}",
                    Interaction.EnterPressurePlate => $"{enterPressurePlate}",
                    Interaction.LeavePressurePlate => $"{leavePressurePlate}",
                    Interaction.PutObjectOnPressurePlate => $"{putObjectPressurePlate}",
                    Interaction.PickObjectOnPressurePlate => $"{pickObjectPressurePlate}",
                    _ => "Not supported"
                };
                

                if (e.Interaction == Interaction.EnterMemory)
                {
                    interactionUI2.GetInteractionText.text = $"{pickObjectPressurePlate}";
                    interactionUI2.GetInteractionImage.sprite = spriteUp;
                }
                interactionUI2.GetGroup.DOFade(e is { Interaction: Interaction.EnterMemory, ShowInteraction: true }  ? 1f : 0f, 0.25f);
                
                interactionUI.GetInteractionImage.sprite = e.Interaction switch
                {
                    Interaction.Grab => spriteNormal,
                    Interaction.PickObjectOnPressurePlate => spriteUp,
                    Interaction.ObtainShard => spriteGlass,
                    Interaction.UseDoor => spriteUseDoor,
                    Interaction.UseKey => spriteKey,
                    Interaction.UseFragment => spriteDown,
                    Interaction.NeedFragment => spriteGlass,
                    Interaction.NeedKey => spriteKey,
                    _ => spriteNormal
                };
                
                interactTween = interactionUI.GetGroup.DOFade(e.ShowInteraction ? 1f : 0f, 0.25f);
            }


            public void EventInteraction(Vector3 position, string text, Sprite logo )
            {
                specialUI.transform.position =  position;
                specialUI.GetInteractionText.text = text;
                specialUI.GetInteractionImage.sprite = logo;
                specialUI.GetGroup.DOFade( 1f , 0.25f);
            }

            public void StopEventInteraction()
            {
                specialUI.GetGroup.DOFade(0f, 0.25f);
            }

            public static void InteractionSetPosition(Vector3 position)
            {
                Instance.interactionParent.transform.position = position;
            }

            private void ShowMemory(MemoryEvent e) {
                memoryTween.Kill();
                
                memoryImage.sprite = e.memory;
                memoryImage.sprite = e.memoryLine;
                
                memoryTween = memoryHUD.DOFade(e.showMemory ? 1f : 0f, 0.25f);
            }

        #endregion
        
    }
}
