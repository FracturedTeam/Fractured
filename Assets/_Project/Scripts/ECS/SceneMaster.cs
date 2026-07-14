using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using UnityEngine;

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

        private bool hasSceneBeenValidated;
        
        private bool isSceneValid;
        private bool IsSceneValidated {
            get => isSceneValid;
            set {
                isSceneValid = value;
                if (isSceneValid) {
                    ValidSceneElement();
                }
            }
        }

        private void Start() {
            SetMasterToSceneElement();
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
            }
            
            frame.Unlock();
            GameInitializer.Instance.AddShards(glassShards);
            
            Debug.Log("Scene has been validated");
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
    }
}