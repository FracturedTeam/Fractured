using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class DoorInteractable : MonoBehaviour, IInteractable {
        private BaseObject baseObject;

        [Header("Load Scene")]
        [SerializeField] public SceneSettings sceneToLoad;
        
        private bool isInitialized = false;
        
        private bool hasBeenInteracted = false;
        
        public void Initialize() {
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(DoorInteractable)}");

                baseObject.GetObjectType = ObjectType.Door;
                baseObject.SetInteract(true);
                
                isInitialized = true;
            }
            
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(hasBeenInteracted) return;
            
            if (PlayerController.Instance.interact.HasObject) {
                PlayerController.Instance.interact.triggerFailedDrop = true;
                return;
            }
            
            if (sceneToLoad == null) return;
            hasBeenInteracted = true;
            
            GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().openBigDoorSound, transform.position);
            PlayerController.Instance.interact.TriggerBigDoor(sceneToLoad, transform.position);
        }

        public void Tick(float deltaTime) {
        }

        public void Dispose() {
        }
        
        public void CompleteObject() {
        }

        public void ResetObject() {
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}