using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class SceneMaster : MonoBehaviour {
        [Header("Scene Elements")]
        [SerializeField] private SceneElement[] elements;
        
        [Header("Obtainable Elements")]
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

        private void SetMasterToSceneElement() {
            foreach (var element in elements) {
                element.SetSceneMaster(this);
            }
        }

        private void ValidSceneElement() {
            if(hasSceneBeenValidated) return;
            
            hasSceneBeenValidated = true;
            //Faire les trucs
            
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
            
            IsSceneValidated = everyElementIsValid;
        }
    }
}