using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class DoorInteractable : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        
        [Header("Door Type")]
        [SerializeField] public DoorType doorType;
        
        [Header("Settings")]
        [SerializeField] public Transform exitPoint;
        [SerializeField] public DoorInteractable linkedDoor;
        [SerializeField] public Direction exitDir;

        [Header("Load Scene")]
        [SerializeField] public SceneSettings sceneToLoad;
        
        private KeyInteractable key;
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(DoorInteractable)}");
                
                if(TryGetComponent(out KeyInteractable k)) key = k;
                
                baseObject.GetInteractionType = ObjectType.Door;
            }
            
            initialized = true;
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (key) {
                if(key.GetBaseObject().GetCompletion is not InteractionCompletion.Completed) return;
            }

            if (doorType is DoorType.BigDoor) {
                if (sceneToLoad == null) return;
                    var load = GameSceneLoaderSystem.Instance.LoadSceneAsync(sceneToLoad);
                return;
            }
            
            if (linkedDoor.key) {
                if(linkedDoor.key.GetBaseObject().GetCompletion is not InteractionCompletion.Completed) return;
            }

            if(!linkedDoor.GetBaseObject().GetCollider().enabled) return;
            
            if (interaction is not ObjectInteraction.Contextual) return;
            
            PlayerController.Instance.interact.StartUsingDoor();
            PlayerController.Instance.movement.SetPosition(linkedDoor.exitPoint.position, exitDir);
        }

        public void Tick(float deltaTime) {
        }

        public void ResetObject() {
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
        
        private Transform GetExitPoint() {
            return exitPoint;
        }
    }
}