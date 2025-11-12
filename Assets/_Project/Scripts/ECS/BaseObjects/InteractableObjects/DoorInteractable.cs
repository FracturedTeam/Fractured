using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class DoorInteractable : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        
        [Header("Settings")]
        [SerializeField] private DoorType doorType;
        [SerializeField] private Transform exitPoint;
        [SerializeField] private DoorInteractable linkedDoor;
        [SerializeField] private CinemachineCamera cameraToSwitch;

        private bool initialized = false;
        private bool isUnlocked = true;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b))
                    baseObject = b;
                else
                    Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(DoorInteractable)}");
            }
            
            initialized = true;
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(!isUnlocked || !linkedDoor.DoorUnlocked()) return;

            if(!linkedDoor.GetBaseObject().GetCollider().enabled) return;
            
            if (interaction is not ObjectInteraction.Contextual) return;
            PlayerController.Instance.interact.StartUsingDoor();
            PlayerController.Instance.transform.position = linkedDoor.GetExitPoint().position;
            
            if (doorType is not DoorType.Big) return;
            cameraToSwitch.Priority = 0;
            linkedDoor.cameraToSwitch.Priority = 1;
        }

        public void ResetObject() {
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
        
        public void SetUnlocked(bool value) {
            isUnlocked = value;
        }

        private Transform GetExitPoint() {
            return exitPoint;
        }
        
        public bool DoorUnlocked() {
            return isUnlocked;
        }
    }
}