using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.ECS {
    public class MemoryFrame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler {
        private MemoryFrameMaster master;
        private Collider collider;
        
        [Header("Position Required")]
        public int requiredPosition;
        
        private int currentPos;
        private bool isUnlocked;
        private bool canBeInteracted;

        private bool isSelected;

        private float zPos;
        
        public void Initialize(MemoryFrameMaster master) {
            this.master = master;
            
            if(TryGetComponent(out Collider col)) collider = col;
            else throw new ArgumentNullException($"[MemoryFrame] does not have a collider component");
            
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
        
        public int GetCurrentPosition() => currentPos;
        public void SetCurrentPosition(int newPos) => currentPos = newPos;
        
        public void Unlock() => isUnlocked = true;

        public void CanBeInteracted(bool can) {
            canBeInteracted = can;
        }

        // Mouse event
        public void OnPointerDown(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            isSelected = true;
            collider.enabled = false;
            
            // Trouver un moyen d'avoir la "profondeur" de l'objet
            zPos = Vector3.Distance(eventData.pointerCurrentRaycast.origin, eventData.pointerCurrentRaycast.worldPosition);
            
            Debug.Log($"OnPointerDown {eventData.position}");
        }

        public void OnPointerUp(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            isSelected = false;
            collider.enabled = true;
            transform.position = master.GetCurrentSlotPosition(currentPos);
            
            Debug.Log($"OnPointerUp {eventData.position}");
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            Debug.Log($"OnPointerEnter {eventData.position}");
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            Debug.Log($"OnPointerExit {eventData.position}");
        }
        
        public void OnDrag(PointerEventData eventData) {
            if(!isSelected) return;

            // Il y a un soucis avec la position Z de l'objets
            
            var cam = CinemachineBrain.GetActiveBrain(0).OutputCamera;
            var screenPosition = cam.WorldToScreenPoint(transform.position);
            
            var newPos = cam.ScreenToWorldPoint(new Vector3(
                screenPosition.x + eventData.delta.x, 
                screenPosition.y + eventData.delta.y, 
                screenPosition.z
                ));
            
            var rightDir = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;
            var forwardDir = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
            
            var horizontal = Vector3.Dot(newPos, rightDir);
            var depth = Vector3.Dot(newPos, forwardDir);
            var vertical = Vector3.Dot(newPos, Vector3.up);
            
            transform.position = horizontal * rightDir + vertical * Vector3.up + depth * forwardDir;
        }
    }
}