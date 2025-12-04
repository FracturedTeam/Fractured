using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects {
    [RequireComponent(typeof(BaseObject))]
    public class PressurePlate : MonoBehaviour , IInteractable {
        private BaseObject baseObject;
        
        [Header("Pressure Plate Settings")]
        [SerializeField] private Transform pressurePlateTriggerPos;
        [SerializeField] private Vector3 pressurePlateSize;
        [SerializeField] private LayerMask interactLayerMask;
        [SerializeField] private float timeToMoveObject;
        
        [Header("Moved Object Settings")]
        [SerializeField] private ObjectMoved[] movedObjects;
        
        private float lerpValue;
        private float timer;
        private bool isActivated;
        private readonly Collider[] results = new Collider[10];

        private bool initialized = false;

        public void Initialize() {

            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(PressurePlate)}");
                
                gameObject.layer = LayerMask.NameToLayer("Walkable");
            }
            
            baseObject?.SetInteract(true);
            initialized = true;
            foreach (var obj in movedObjects) {
                obj.objectMoved.position = obj.initialPos.position;
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
        }

        public void Tick(float deltaTime) {
            if (!baseObject.CanBeInteractedWith()) {
                timer -= deltaTime;
            }
            else {
                var size = Physics.OverlapBoxNonAlloc(pressurePlateTriggerPos.position, pressurePlateSize, results, transform.rotation, interactLayerMask );
                timer += size >  0 ? deltaTime : -deltaTime;
                
                if((size >  0 && !isActivated) || (size <= 0 && isActivated))
                {
                    isActivated = !isActivated;
                    var dia = size > 0 ? baseObject.successDialogue : baseObject.failedDialogue;
                    if (!dia.oneTime || !dia.alreadyInteracted)
                    {
                        HudManager.Instance.SetText(dia.dialogue);
                        if (size > 0) baseObject.successDialogue.alreadyInteracted = true;
                        else baseObject.failedDialogue.alreadyInteracted = true;
                    }
                }
            }
            timer = Mathf.Clamp(timer, 0, timeToMoveObject);

            foreach (var obj in movedObjects) {
                obj.objectMoved.position = Vector3.Lerp(obj.initialPos.position, obj.movedPos.position, lerpValue);
            }
            
            lerpValue = timer / timeToMoveObject;
        }

        public void ResetObject() {
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.cadetBlue;
            Gizmos.DrawWireCube(pressurePlateTriggerPos.position, pressurePlateSize);

            Gizmos.color = Color.red;
            foreach (var obj in movedObjects) {
                Gizmos.DrawWireSphere(obj.initialPos.position, 0.5f);
            }
            
            Gizmos.color = Color.green;
            foreach (var obj in movedObjects) {
                Gizmos.DrawWireSphere(obj.movedPos.position, 0.5f);
            }
        }
    }

    [Serializable]
    public struct ObjectMoved {
        public Transform objectMoved;
        public Transform initialPos;
        public Transform movedPos;
    }
}