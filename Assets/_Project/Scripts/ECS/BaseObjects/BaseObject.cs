using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Structs;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class BaseObject : MonoBehaviour {
        #region Save
        [SerializeField, HideInInspector] private ObjectData data;
        
        public void Bind(ObjectData data) {
            this.data = data;
        }
        
        [ContextMenu("Load")]
        public void Load() {
            if (GetGlass) {
                GetGlassInteract.ObjectOut = data.objectOut;
                if (data.objectOut) GetGlassInteract.SetInteractableInBox(true);
            }
            
            transform.position = data.position;
            GetCompletion = data.completion;
            if (GetCompletion is InteractionCompletion.Completed) CompleteObject();
            
            SetInteract(data.canInteract);
        }
        
        [ContextMenu("Save")]
        public void SaveData() {
            data.position = transform.position;
            data.completion = GetCompletion;
            data.canInteract = canBeInteractedWith;
            if (GetGlass) data.objectOut = GetGlassInteract.ObjectOut;
        }
        #endregion
        
        public bool GetGlass =>  GetGlassInteract != null;
        public GlassInteractable GetGlassInteract { get; private set; }
        public IInteractable GetInteract  { get; set; }
        public ObjectType GetInteractionType { get; set; }
        public InteractionCompletion GetCompletion { get; set; }

        [Header("Object Name")]
        public string ObjectName;
        
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
                if (TryGetComponent(typeof(GlassInteractable), out var g))
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
            initialized = true;
        
            GetInteract?.Initialize();
            GetGlassInteract?.Initialize();
        }
        
        //Collider[] inObjects = new Collider[4];
        private void Update() {
            GetInteract?.Tick(Time.deltaTime);
            GetGlassInteract?.Tick(Time.deltaTime);
            
            /*if (GetCollider() && GetInteractionType is ObjectType.Moveable) {
                var size = Physics.OverlapBoxNonAlloc(transform.position, objectCollider.bounds.extents * 2, inObjects, transform.rotation, gameObject.layer);
                
                if (size > 0) {
                    var dir = (inObjects[0].transform.position - transform.position).normalized;
                    transform.position += new Vector3(dir.x, 0, dir.z) * 3;
                }
            }*/
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

        public void ResetInteract() {
            GetInteract?.ResetObject();
            GetGlassInteract?.ResetObject();
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

        public MeshRenderer GetRendered() {
            return meshRenderer;
        }
    }

    [Serializable]
    public class ObjectData {
        public BaseObject baseObject;
        public Vector3 position;
        public InteractionCompletion completion;
        public bool canInteract;
        public bool objectOut;
    }
}

