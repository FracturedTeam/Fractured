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
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class HudManager : PersistentSingleton<HudManager>
    {
        [Header("HUD")]
        [field:SerializeField] public Transform glassHolder {get; private set;}
        public InteractionHUD interact {get; private set;}
        public MemoryHUD memory {get; private set;}
        
        
        private EventBinding<DocumentEvent> documentEventBinding;
        
        [Header("Dialogue")]
        [SerializeField] private SubtitleText subtitleText;
        private DialogueScriptableObject currentDialogue;
        private CountdownTimer textTimer;
        
        [Header("Glass Animation")]
        [SerializeField] private Fragment fragment;
        [SerializeField] private ParticleSystem spawningParticles;  
        [SerializeField] private GlassDocument glassDocument;
        [SerializeField] private int currentShardsSpawning;
        [SerializeField] private float firstHalfTime = 1.0f;
        [SerializeField] private float secondHalfTime = 0.5f;
        [SerializeField] private Material transitionMaterial;
        
        private ParticleSystem currentParticle;
        private Fragment currentFrag;
        
        private readonly List<ParticleSystem> freeParticles = new List<ParticleSystem>();
        private readonly List<Fragment> freeFragment = new List<Fragment>();

        private void Start() {
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop += ResetText;
            glassDocument.gameObject.SetActive(false);
            
            interact = GetComponent<InteractionHUD>();
            memory = GetComponent<MemoryHUD>();
        }

        private void OnEnable() {
            documentEventBinding = new EventBinding<DocumentEvent>(OpenDocument);
            EventBus<DocumentEvent>.Register(documentEventBinding);
        }

        private void OnDisable() {
            EventBus<DocumentEvent>.Deregister(documentEventBinding);
            textTimer.OnTimerStop  -= ResetText;
        }

        public void OpenDocument(DocumentEvent e)
        {
            glassDocument.gameObject.SetActive(e.isOn);
            glassDocument.SetUp(e.document, e.isOn);
        }

        public void SetText(DialogueScriptableObject newDialogue) {
            if(!subtitleText || !newDialogue)
                return;
            
            currentDialogue = newDialogue;
            
            subtitleText.Setup(currentDialogue);
            
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
    }

    public struct DocumentEvent : IEvent
    {
        public bool isOn;
        public GlassDocumentScriptableObject document;
    }
}
