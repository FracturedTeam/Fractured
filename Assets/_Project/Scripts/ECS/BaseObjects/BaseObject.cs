using System;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Structs;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class BaseObject : MonoBehaviour {
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
        
        [Header("Locked Behind a Memory")]
        [SerializeField] internal bool locked;
        [SerializeField] internal int memoryId;

        [Header("HUD")] 
        [SerializeField] private Transform hudTransformPoint;
        
        private MeshRenderer meshRenderer;
        private Collider objectCollider;
        
        private FMODUnity.StudioEventEmitter emitter;

        private bool initialized = false;
        private bool canBeInteractedWith;

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
            
            if(GetInteractionType is ObjectType.Moveable)
                transform.position = data.position;
            GetCompletion = data.completion;
            
            if (GetCompletion is InteractionCompletion.Completed) {
                if (GetInteractionType is ObjectType.PressurePlate) {
                    var p = GetInteract as PressurePlate;
                    p.objectOnPressurePlate = SaveInstance.Instance.gameData.ObjectDatas[data.objectIndexOnDisplay].baseObject.GetInteract as MoveableObject;
                }
                CompleteObject();
            }
            
            SetInteract(data.canInteract);
        }
        
        [ContextMenu("Save")]
        public void SaveData() {
            if(GetInteractionType is ObjectType.Moveable)
                data.position = transform.position;
                    
            data.completion = GetCompletion;
            if (GetCompletion is InteractionCompletion.Completed && GetInteractionType is ObjectType.PressurePlate) {
                var p = GetInteract as PressurePlate;
                var index = 0;
                for (var i = 0; i < SaveInstance.Instance.gameData.ObjectDatas.Count; i++) {
                    if (p.objectOnPressurePlate.GetBaseObject() == SaveInstance.Instance.gameData.ObjectDatas[i].baseObject) {
                        index = i;
                        break;
                    }
                }

                data.objectIndexOnDisplay = index;
            }
            
            data.canInteract = canBeInteractedWith;
            if (GetGlass) data.objectOut = GetGlassInteract.ObjectOut;
        }
        #endregion
        
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
                
                if(TryGetComponent(out FMODUnity.StudioEventEmitter e))
                    emitter = e;
                
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

        public FMODUnity.StudioEventEmitter GetEmitter() {
            return emitter;
        }

        public Vector3 GetUIPosition() {
            return hudTransformPoint ? hudTransformPoint.position : transform.position;
        }
    }

    [Serializable]
    public class ObjectData {
        public BaseObject baseObject;
        public Vector3 position;
        public InteractionCompletion completion;
        public bool canInteract;
        public bool objectOut;
        public int objectIndexOnDisplay;
    }
}

