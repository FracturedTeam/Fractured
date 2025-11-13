using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
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
        [SerializeField] private Transform objectMoved;
        [SerializeField] private Transform initialPos;
        [SerializeField] private Transform movedPos;
        
        private float lerpValue;
        private float timer;
        private readonly Collider[] results = new Collider[10];

        private bool initialized = false;

        public void Initialize() {

            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(PressurePlate)}");
                
                gameObject.layer = LayerMask.NameToLayer("Walkable");
            }
            
            baseObject?.SetInteract(false);
            initialized = true;
            objectMoved.position = initialPos.position;
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
        }

        public void Tick(float deltaTime) {
            var size = Physics.OverlapBoxNonAlloc(pressurePlateTriggerPos.position, pressurePlateSize, results, transform.rotation, interactLayerMask );
            
            if(size >  0) {
                timer += deltaTime;
            }
            else {
                timer -= deltaTime;
            }
            
            timer = Mathf.Clamp(timer, 0, timeToMoveObject);
            
            objectMoved.position = Vector3.Lerp(initialPos.position, movedPos.position, lerpValue);
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
            Gizmos.DrawWireSphere(initialPos.position, 0.5f);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(movedPos.position, 0.5f);
        }
    }
}