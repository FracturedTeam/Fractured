using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Structs;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class BaseObject : MonoBehaviour {
        [SerializeField] private ObjectData data;
        
        public void Bind(ObjectData data) {
            this.data = data;
        }
        
        [ContextMenu("Load")]
        public void Load() {
            transform.position = data.position;
            GetCompletion = data.completion;
            
            if (GetCompletion is InteractionCompletion.Completed) {
                CompleteObject();
            }
            
            SetInteract(data.canInteract);
        }
        
        [ContextMenu("Save")]
        public void SaveData() {
            data.position = transform.position;
            data.completion = GetCompletion;
            data.canInteract = canBeInteractedWith;
        }
        
        public bool GetGlass =>  GetGlassInteract != null;
        public GlassInteractable GetGlassInteract { get; private set; }
        public IInteractable GetInteract  { get; set; }
        public ObjectType GetInteractionType { get; set; }
        public InteractionCompletion GetCompletion { get; set; }

        [Header("Dialogues")] 
        [SerializeField] internal Dialogue successDialogue;
        [SerializeField] internal Dialogue cantInteractDialogue;
        [SerializeField] internal Dialogue failedDialogue;
        
        private MeshRenderer meshRenderer;
        private Collider objectCollider;

        private bool initialized = false;
        private bool canBeInteractedWith;

        private void Awake() {
            Initialize();
        }

        public void Initialize() {
            if(!initialized) {
                if(TryGetComponent(typeof(GlassInteractable), out var g))
                    GetGlassInteract = g as GlassInteractable;
                if(TryGetComponent(typeof(IInteractable), out var p))
                    GetInteract = p as IInteractable;
                else SetInteract(false);
            
                if(TryGetComponent(typeof(MeshRenderer), out var m)) meshRenderer = m as MeshRenderer;
                else Debug.LogWarning($"[BaseObject] {nameof(BaseObject)} does not contain MeshRenderer component");
        
                if(TryGetComponent(typeof(Collider), out var c)) objectCollider = c as Collider;
                else Debug.LogWarning($"[BaseObject] {nameof(BaseObject)} does not contain Collider component");
        
                gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        
            GetInteract?.Initialize();
            GetGlassInteract?.Initialize();
            initialized = true;
        }

        private void Update() {
            GetInteract?.Tick(Time.deltaTime);
            GetGlassInteract?.Tick(Time.deltaTime);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable interactable = null) { 
            GetInteract.OnInteract(interaction, interactable);
        }

        public void OnShardInteract(bool isOn, Glass shard) {  
            GetGlassInteract.OnInteract(isOn, shard);
        }

        private void CompleteObject() {
            Debug.Log("[BaseObject] Complete Object");
            GetInteract.CompleteObject();
        }

        public void SetInteract(bool canInteract) {
            if(GetInteract != null)
                canBeInteractedWith = canInteract;
        }
        
        public void SetCollider(bool isOn) {
            if (!objectCollider) return;
            objectCollider.enabled = isOn;
        }
        
        public Collider GetCollider() => objectCollider;
        
        public void SetRenderer(bool isOn) {
            if(!meshRenderer) return;
            meshRenderer.enabled = isOn;
        }

        public bool CanBeInteractedWith() {
            return canBeInteractedWith;
        }
    }

    [Serializable]
    public class ObjectData {
        public BaseObject baseObject;
        public Vector3 position;
        public InteractionCompletion completion;
        public bool canInteract;
    }
}

