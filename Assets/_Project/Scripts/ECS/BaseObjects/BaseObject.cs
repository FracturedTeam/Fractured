using System;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
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
        public GlassText GetTextInteractable { get; private set; }
        public IInteractable GetInteract  { get; set; }
        public TutorialElement  GetTutorialElement { get; set; }
        public ObjectType GetObjectType { get; set; }
        public LockedState GetLockState { get; set; }

        private BlockedAttribute blockedAttribute;
        private SceneElement sceneElement;
        
        [Header("Object Name")]
        public string ObjectName;

        [Header("HUD")] 
        [SerializeField] private Vector2 hudTransformPoint;
        [SerializeField] private Vector2 hudSpecialTransformPoint;
        
        
        private MeshRenderer meshRenderer;
        
        private Collider objectCollider;

        public bool IsInitialized { get; private set; }
        private bool canBeInteractedWith;
        
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
                GetGlassInteract.SetInteractableInBox(data.ObjectOutOfBox);
            }
            
            if(GetObjectType is ObjectType.Moveable)
                transform.position = data.Position;
            
            GetLockState = data.ObjectCompletion;
            
            if (GetLockState is LockedState.Unlocked) {
                CompleteObject();
            }
            
            SetInteract(data.CanBeInteractedWith);
        }
        
        [ContextMenu("Save")]
        public void SaveData() {
            if(data == null || data.Guid != Guid) return;
            
            if(GetObjectType is ObjectType.Moveable)
                data.Position = transform.position;
                    
            data.ObjectCompletion = GetLockState;
            
            data.CanBeInteractedWith = canBeInteractedWith;
            if (GetGlass) data.ObjectOutOfBox = GetGlassInteract.objectOut;
        }
        #endregion
        
        public void Initialize() {
            if(!IsInitialized) {
                if (TryGetComponent(typeof(GlassInteractable), out var g))
                    GetGlassInteract = g as GlassInteractable;
                if(TryGetComponent(typeof(TutorialElement), out var t))
                    GetTutorialElement = t as TutorialElement;
                if(TryGetComponent(typeof(GlassText), out var gt))
                    GetTextInteractable = gt as GlassText;
                
                if(TryGetComponent(out IInteractable interactable)) GetInteract = interactable;
                else SetInteract(false);

                if (TryGetComponent(out BlockedAttribute blocked)) blockedAttribute = blocked;

                if (TryGetComponent(out SceneElement scene)) sceneElement = scene;
                
                if(TryGetComponent(typeof(MeshRenderer), out var m)) meshRenderer = m as MeshRenderer;
                else Debug.LogWarning($"[BaseObject] {gameObject.name} does not contain MeshRenderer component");
        
                if(TryGetComponent(typeof(Collider), out var c)) objectCollider = c as Collider;
                else Debug.LogWarning($"[BaseObject] {nameof(BaseObject)} does not contain Collider component");
        
                gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
            IsInitialized = true;
        
            GetInteract?.Initialize();
            GetGlassInteract?.Initialize();
            GetTextInteractable?.Initialize();
            blockedAttribute?.Initialize();
        }
        
        private void Update() {
            // if (locked && MemoryManager.Instance.IsUnlockedMemory(memoryId)) {
            //     locked = false;
            //     SetInteract(true);
            // }
            
            GetInteract?.Tick(Time.deltaTime);
            GetGlassInteract?.Tick(Time.deltaTime);
        }

        void OnDestroy() {
            GetInteract?.Dispose();
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable interactable = null) {
            if (GetLockState is LockedState.Locked) {
                blockedAttribute.OnInteract(GetInteract);
                return;
            }
            
            GetInteract.OnInteract(interaction, interactable);
        }

        public void OnShardInteract(bool isOn, Glass shard) {  
            GetGlassInteract.OnShardUpdated(isOn, shard);
        }

        public void CompleteObject() {
            if (GetGlass) GetGlassInteract.CompleteObject();
            GetInteract?.CompleteObject();
        }

        public void ResetInteract() {
            GetInteract?.ResetObject();
            if(GetObjectType is not ObjectType.Moveable)
                GetGlassInteract?.ResetObject();
        }
        
        public void SetInteract(bool canInteract) { // TODO appelé très souvent sous certaines conditions
            canBeInteractedWith = GetInteract != null && canInteract;
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

        public bool HasSceneElement() {
            return sceneElement != null;
        }

        public void TriggerSceneElement() {
            sceneElement.CheckValidation();
        }

        public void Trigger(bool on) {
            if (!GetTutorialElement) return;
            if(on) GetTutorialElement.TriggerEventStart();
            else GetTutorialElement.TriggerEventStop();
        }
    }

    [Serializable]
    public class ObjectData {
        [field: SerializeField] public string Guid { get; set; }
        public Vector3 Position;
        public LockedState ObjectCompletion;
        public bool CanBeInteractedWith;
        public bool ObjectOutOfBox;
    }
}

