using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.ECS {
    public class SceneMaster : MonoBehaviour {
        [Header("Scene Elements")]
        [SerializeField] private SceneElement[] elements;
        [Space]
        [SerializeField] private bool requiredPlayerPosition;
        [SerializeField] private Vector3 requiredPosition;
        
        private bool isValidPlayerPosition;
        
        [Header("Obtainable Elements")]
        [SerializeField] public MemoryFrame frame;
        [SerializeField] public Glass[] glassShards;
        [SerializeField] private GlassText worldText;

        [Header("Animation Elements")]
        [SerializeField] private Sprite memorySprite;
        [SerializeField] private DialogueScriptableObject dialogue;
        
        private readonly CountdownTimer validationDelay = new(1.25f);
        
        private bool hasSceneBeenValidated;
        private bool isSceneValid;
        
        public bool IsSceneValidated {
            get => isSceneValid;
            private set {
                isSceneValid = value;
                if (isSceneValid) {
                    ValidSceneElement();
                }
            }
        }

        private void Start() {
            SetMasterToSceneElement();
            validationDelay.OnTimerStop += DisplayMemoryOnDelay;
        }

        private void OnDisable() {
            validationDelay.OnTimerStop -= DisplayMemoryOnDelay;
        }

        private void Update() {
            if (requiredPlayerPosition) {
                var dist =  Vector3.Distance(PlayerController.Instance.transform.position, requiredPosition);
                if (isValidPlayerPosition != dist <= 2) {
                    isValidPlayerPosition = dist <= 2;
                    CheckForValidation();
                }
            }
        }

        private void SetMasterToSceneElement() {
            foreach (var element in elements) {
                element.SetSceneMaster(this);
            }
        }

        private void ValidSceneElement() {
            if(hasSceneBeenValidated) return;
            
            hasSceneBeenValidated = true;

            foreach (var element in elements) { // Lock interaction with sceneElement once the scene is valid
                element.baseObject.SetInteract(false);
                element.baseObject.SetGlassInteract(false);
            }
            
            frame.Unlock();
            worldText?.Appear();
            
            // Timer start
            if(!validationDelay.IsRunning)
                validationDelay.Start();
            
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().enterMemorySound);
            
            PlayerController.Instance.FreezeController(true);
            PlayerController.Instance.SetInteraction(false);
            PlayerController.Instance.SetInMemory(true);
        }

        private void DisplayMemoryOnDelay() {
            GameInitializer.Instance.SetMemoryLoop(true);
            MemoryManager.Instance.SetMemory(true, memorySprite, memorySprite);
            
            HudManager.Instance.SetText(dialogue);
            HudManager.InteractionSetPosition(new Vector3(Screen.width -100, Screen.height -100, 0));

            InputsBrain.Instance.OnInteract += LeaveMemory;
        }

        private void LeaveMemory(InputAction.CallbackContext ctx) {
            if(!ctx.performed) return;
            
            InputsBrain.Instance.OnInteract -= LeaveMemory;
            
            PlayerController.Instance.FreezeController(false);
            PlayerController.Instance.SetInteraction(true);
            PlayerController.Instance.SetInMemory(false);
            
            MemoryManager.Instance.SetMemory(false);
            HudManager.Instance.ResetText();
            
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().leaveMemorySound);
            GameInitializer.Instance.SetMemoryLoop(false);
            GameInitializer.Instance.AddShards(glassShards);
            
        }
        
        public void LoadCompleteScene() {
            ValidSceneElement();
            
            foreach (var element in elements) {
                if (element.validationMethod is SceneElement.ValidationMethod.Position) {
                    element.transform.position = element.requestedCollisionArea.transform.position;
                }

                if (element.validationMethod is SceneElement.ValidationMethod.UseState) {
                    element.SetDebugUseState(element.requestedUseState);
                }
                
                element.SetValidate();
            }
        }
        
        public void CheckForValidation() {
            var everyElementIsValid = true;
            
            foreach (var element in elements) {
                if (!element.IsValidated) {
                    everyElementIsValid = false;
                    break;
                }
            }
            
            if(requiredPlayerPosition && !isValidPlayerPosition) everyElementIsValid = false;
            
            IsSceneValidated = everyElementIsValid;
        }
        
        
         #region Save
         [SerializeField, HideInInspector] private SceneMasterSave data;
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
 
         public void Bind(SceneMasterSave data) {
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
             
             if(data.isCompleted)
                 LoadCompleteScene();
         }
         
         [ContextMenu("Save")]
         public void SaveData() {
             if(data == null || data.Guid != Guid) return;

             data.isCompleted = isSceneValid;
         }
         #endregion
    }

    [Serializable]
    public class SceneMasterSave {
        [field: SerializeField] public string Guid { get; set; }
        public bool isCompleted;
    }
}