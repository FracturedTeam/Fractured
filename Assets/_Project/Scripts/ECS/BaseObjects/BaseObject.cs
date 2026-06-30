using System;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Structs;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class BaseObject : MonoBehaviour {
        public bool GetGlass =>  GetGlassInteract != null;
        public GlassInteractable GetGlassInteract { get; private set; }
        public IInteractable GetInteract  { get; set; }
        public TutorialElement  GetTutorialElement { get; set; }
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
        [SerializeField] private Vector2 hudTransformPoint;
        [SerializeField] private Vector2 hudSpecialTransformPoint;
        
        [Header("Tutorial")]
        [SerializeField] internal TutorialTriggerType stopTutorialTriggerType;
        [SerializeField] internal TutorialTriggerType startTutorialTriggerType;
        [SerializeField] internal TutorialElement interactTutorialElement;
        
        private MeshRenderer meshRenderer;
        public MeshFilter meshFilter { get; private set; }
        private Collider objectCollider;

        public bool initialized { get; private set; }
        private bool canBeInteractedWith;
        private bool isOnPressurePlate = false;
        
        #region Save
        [SerializeField, HideInInspector] private ObjectData data;

        [field:SerializeField] public string Guid { get; set; }
        
        private System.Guid _guid;

        public System.Guid guid {
            get {
                if(_guid == System.Guid.Empty && !System.String.IsNullOrEmpty(Guid))
                {
                    _guid = new System.Guid(Guid);
                }

                return _guid;
            }
        }

#if UNITY_EDITOR
        
        [ContextMenu("Generate Unique ID")]
        public void GenerateGuid() {
            _guid = System.Guid.NewGuid();
            Guid = _guid.ToString();
            EditorUtility.SetDirty(this);
        }
#endif

        public void Bind(ObjectData data) {
            this.data = data;
            if (String.IsNullOrEmpty(Guid)) {
                Debug.LogError($"[BaseObject] {gameObject.name} does not have Guid, please generate it");
                return;
            }
            data.Guid = Guid;
        }
        
        [ContextMenu("Load")]
        public void Load() {
            if(data.Guid != Guid) return;
            
            if (GetGlass) {
                GetGlassInteract.objectOut = data.ObjectOutOfBox;
                if (data.ObjectOutOfBox) GetGlassInteract.SetInteractableInBox(true);
            }
            
            if(GetInteractionType is ObjectType.Moveable)
                transform.position = data.Position;
            GetCompletion = data.ObjectCompletion;
            
            if (GetCompletion is InteractionCompletion.Completed) {
                if (GetInteractionType is ObjectType.PressurePlate) {
                    var p = GetInteract as PressurePlate;

                    for (int i = 0; i < GameSceneSettings.Instance.baseObjects.Capacity; i++) {
                        if (GameSceneSettings.Instance.baseObjects[i].Guid == data.ObjectGuidOnPedestal) {
                            p.objectOnPressurePlate = (MoveableObject)GameSceneSettings.Instance.baseObjects[i].GetInteract;
                            break;
                        }
                    }
                }
                CompleteObject();
            }
            
            SetInteract(data.CanBeInteractedWith);
        }
        
        [ContextMenu("Save")]
        public void SaveData() {
            if(data == null || data.Guid != Guid) return;
            
            if(GetInteractionType is ObjectType.Moveable)
                data.Position = transform.position;
                    
            data.ObjectCompletion = GetCompletion;
            
            if (GetCompletion is InteractionCompletion.Completed && GetInteractionType is ObjectType.PressurePlate) {
                var p = GetInteract as PressurePlate;
                data.ObjectGuidOnPedestal = p.objectOnPressurePlate.GetBaseObject().Guid;
            }
            
            data.CanBeInteractedWith = canBeInteractedWith;
            if (GetGlass) data.ObjectOutOfBox = GetGlassInteract.objectOut;
        }
        #endregion
        
        public void Initialize() {
            if(!initialized) {
                if (TryGetComponent(typeof(GlassInteractable), out var g))
                    GetGlassInteract = g as GlassInteractable;
                if(TryGetComponent(typeof(TutorialElement), out var t))
                    GetTutorialElement = t as TutorialElement;
                
                if(TryGetComponent(typeof(IInteractable), out var p))
                    GetInteract = p as IInteractable;
                else SetInteract(false);
                
                if(TryGetComponent(typeof(MeshRenderer), out var m)) meshRenderer = m as MeshRenderer;
                else Debug.LogWarning($"[BaseObject] {gameObject.name} does not contain MeshRenderer component");
                
                if(TryGetComponent(typeof(MeshFilter), out var mf)) meshFilter = mf as MeshFilter;
                else Debug.LogWarning($"[BaseObject] {gameObject.name} does not contain MeshFilter component");
        
                if(TryGetComponent(typeof(Collider), out var c)) objectCollider = c as Collider;
                else Debug.LogWarning($"[BaseObject] {nameof(BaseObject)} does not contain Collider component");
        
                gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
            initialized = true;
        
            GetInteract?.Initialize();
            GetGlassInteract?.Initialize();
            
            if (locked && !MemoryManager.Instance.IsUnlockedMemory(memoryId))
                SetInteract(false);
            
            else if (startTutorialTriggerType == TutorialTriggerType.OnCanBeSeen)
                Trigger(true);
            else if (startTutorialTriggerType == TutorialTriggerType.OnCanBeSeen)
            {
                Trigger(false);
                interactTutorialElement?.TriggerEventStart();
            }
                
        }
        
        //Collider[] inObjects = new Collider[4];
        private void Update() {
            if (locked && MemoryManager.Instance.IsUnlockedMemory(memoryId)) {
                locked = false;
                SetInteract(true);
            }
            
            
            GetInteract?.Tick(Time.deltaTime);
            GetGlassInteract?.Tick(Time.deltaTime);
        }

        void OnDestroy() {
            GetInteract?.Dispose();
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable interactable = null) { 
            GetInteract.OnInteract(interaction, interactable);

            if (stopTutorialTriggerType == TutorialTriggerType.OnInteract)
            {
                Trigger(false);
                interactTutorialElement?.TriggerEventStart();
            }
            if (startTutorialTriggerType == TutorialTriggerType.OnInteract)
                Trigger(true);
        }

        public void OnShardInteract(bool isOn, Glass shard) {  
            GetGlassInteract.OnInteract(isOn, shard);

            if (!isOn) 
                return;

            if (stopTutorialTriggerType == TutorialTriggerType.OnHideReveal)
            {
                Trigger(false);
                interactTutorialElement?.TriggerEventStart();
            }
            if (startTutorialTriggerType == TutorialTriggerType.OnHideReveal)
                Trigger(true);
        }

        public void CompleteObject() {
            if (GetGlass) GetGlassInteract.CompleteObject();
            GetInteract?.CompleteObject();
            
            if (stopTutorialTriggerType == TutorialTriggerType.OnSuccess)
            {
                Trigger(false);
                interactTutorialElement?.TriggerEventStart();
            }
            if (startTutorialTriggerType == TutorialTriggerType.OnSuccess)
                Trigger(true);
        }

        public void ResetInteract() {
            GetInteract?.ResetObject();
            if(GetInteractionType is not ObjectType.Moveable)
                GetGlassInteract?.ResetObject();
        }
        
        public void SetInteract(bool canInteract) { // TODO appelé très souvent sous certaines conditions
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

        public Vector2 GetUIPosition(bool special = false) {
            return PlayerController.Instance.cinemachineBrain.OutputCamera.WorldToScreenPoint(transform.position) + 
                   (special ? new Vector3(hudSpecialTransformPoint.x, hudSpecialTransformPoint.y + 5) : new Vector3(hudTransformPoint.x, hudTransformPoint.y + 5));
        }

        public void Trigger(bool on) {
            if (!GetTutorialElement) return;
            if(on) GetTutorialElement.TriggerEventStart();
            else GetTutorialElement.TriggerEventStop();
        }

        public void SetOnPressurePlate(bool p) => isOnPressurePlate = p;
        public bool IsOnPressurePlate() => isOnPressurePlate;
    }

    [Serializable]
    public class ObjectData {
        [field: SerializeField] public string Guid { get; set; }
        public Vector3 Position;
        public InteractionCompletion ObjectCompletion;
        public bool CanBeInteractedWith;
        public bool ObjectOutOfBox;
        public string ObjectGuidOnPedestal;
    }
}

