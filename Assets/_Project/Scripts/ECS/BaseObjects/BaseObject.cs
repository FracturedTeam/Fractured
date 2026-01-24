using System;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Structs;
using _Project.Scripts.UI;
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
        private Collider objectCollider;

        public bool initialized { get; private set; }
        private bool canBeInteractedWith;
        private bool isOnPressurePlate = false;
        
        #region Save
        [SerializeField, HideInInspector] private ObjectData data;
        
        public void Bind(ObjectData data) {
            this.data = data;
        }
        
        [ContextMenu("Load")]
        public void Load() {
            if (GetGlass) {
                GetGlassInteract.objectOut = data.objectOut;
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
            if (GetGlass) data.objectOut = GetGlassInteract.objectOut;
        }
        #endregion
        
        private void Awake() {
            Initialize();
        }

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

        private void CompleteObject() {
            Debug.Log("[BaseObject] Complete Object");
            GetInteract.CompleteObject();
            
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
        public BaseObject baseObject;
        public Vector3 position;
        public InteractionCompletion completion;
        public bool canInteract;
        public bool objectOut;
        public int objectIndexOnDisplay;
    }
}

