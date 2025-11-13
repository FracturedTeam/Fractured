using System;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects {
    public class PressurePlate : MonoBehaviour {

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

        private void Start() {
            objectMoved.position = initialPos.position;
        }

        void Update() {
            var size = Physics.OverlapBoxNonAlloc(pressurePlateTriggerPos.position, pressurePlateSize, results, transform.rotation, interactLayerMask );
            
            if(size >  0) {
                timer += Time.deltaTime;
            }
            else {
                timer -= Time.deltaTime;
            }
            
            timer = Mathf.Clamp(timer, 0, timeToMoveObject);
            
            objectMoved.position = Vector3.Lerp(initialPos.position, movedPos.position, lerpValue);
            lerpValue = timer / timeToMoveObject;
        }
        
    }
}