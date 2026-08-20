using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI.Gameplay;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class HudManager : PersistentSingleton<HudManager>
    {
        [Header("HUD")]
        [field:SerializeField] public Transform glassHolder {get; private set;}
        public InteractionHUD interact {get; private set;}
        public MemoryHUD memory {get; private set;}
        //public PadlockHud padLock {get; private set;}
        
        [Header("Dialogue")]
        [SerializeField] private SubtitleText subtitleText;
        private DialogueScriptableObject currentDialogue;
        private CountdownTimer textTimer;
        
        [Header("Glass Animation")]
        [SerializeField] private Fragment fragment;
        [SerializeField] private ParticleSystem spawningParticles;  
        [SerializeField] private int currentShardsSpawning;
        [SerializeField] private float firstHalfTime = 1.0f;
        [SerializeField] private float secondHalfTime = 0.5f;
        [SerializeField] private Material transitionMaterial;
        
        [Header("UI Color")]
        [SerializeField] private Color act1Color;
        [SerializeField] private Color act2Color;
        [SerializeField] private Color act3Color;
        [SerializeField] private SetUIColor setUIColor;
        
        [Header("Gamepad Visual")]
        [SerializeField] private GameObject gamepadVisual;
        
        private ParticleSystem currentParticle;
        private Fragment currentFrag;
        
        private readonly List<ParticleSystem> freeParticles = new List<ParticleSystem>();
        private readonly List<Fragment> freeFragment = new List<Fragment>();
        
        private bool hasGlass = false;
        private bool isGamepadControlled = false;

        protected override void Awake() {
            base.Awake();
            
            if (FindAnyObjectByType<HudManager>() != this)
               Destroy(gameObject);
                
            
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop += ResetText;
            
            interact = GetComponent<InteractionHUD>();
            memory = GetComponent<MemoryHUD>();
        }

        private void OnEnable() {
            InputsBrain.Instance.OnGamepadControlled += UpdateGamepadControlled;
        }

        private void OnDisable() {
            if(InputsBrain.HasInstance) InputsBrain.Instance.OnGamepadControlled -= UpdateGamepadControlled;
            textTimer.OnTimerStop  -= ResetText;
        }

        private void UpdateGamepadControlled(bool isGamepad) {
            isGamepadControlled = isGamepad;
            gamepadVisual.SetActive(isGamepad && hasGlass);
        }

        public void SetGlass(bool isOn) {
            hasGlass = isOn;
            gamepadVisual.SetActive(isGamepadControlled && hasGlass);
        }

        public void SetText(DialogueScriptableObject newDialogue) {
            if(!subtitleText || !newDialogue)
                return;
            
            currentDialogue = newDialogue;
            
            subtitleText.Setup(currentDialogue);
            GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().avatar_Sight_Sounds, PlayerController.Instance.transform.position);
            
            if (currentDialogue.time <= 0)
                return;
            
            textTimer.Reset(currentDialogue.time);
            textTimer.Start();
        }
        
        public void ResetText() {
            if(currentDialogue && currentDialogue.next)
                SetText(currentDialogue.next);
            else
                subtitleText.Setup(null);
        }

        public void ShardSpawn(Glass shard)
        {
            if(freeParticles.Count <= 0)
            {
                var particle =  Instantiate(spawningParticles, PlayerController.Instance.cinemachineBrain.OutputCamera.transform);
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

        public void UpdateUIColor(int index) {
            switch (index) {
                case 1:
                    setUIColor.SetSpriteColor(act1Color);
                    break;
                case 2:
                    setUIColor.SetSpriteColor(act2Color);
                    break;
                case 3:
                    setUIColor.SetSpriteColor(act3Color);
                    break;
            }
        }
    }

    public struct DocumentEvent : IEvent
    {
        public bool isOn;
        public GlassDocumentScriptableObject document;
    }
}
