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
        
        private bool isCorrectPosition;
        
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
            
            Debug.Log($"OnPointerDown {eventData.position}");
        }

        public void OnPointerUp(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            isSelected = false;
            collider.enabled = true;
            
            var closest = GetClosetPosition();
            
            switch (GetCurrentPosition()) {
                case 0:
                    // See if it is closer to position 1 or 2
                    if (closest == 1) {
                        var frame1 = master.GetFrame(1);
                        currentPos = closest;
                        frame1.currentPos = 0;
                    }
                    else if (closest == 2) {
                        var frame1 = master.GetFrame(1);
                        var frame2 = master.GetFrame(2);
                        currentPos = closest;
                        frame1.currentPos = 0;
                        frame2.currentPos = 1;
                    }
                    
                    break;
                case 1:
                    // See if it is closer to position 0 or 2
                    if (closest == 0) {
                        var frame1 = master.GetFrame(0);
                        currentPos = closest;
                        frame1.currentPos = 1;
                    }
                    else if (closest == 2) {
                        var frame2 = master.GetFrame(2);
                        currentPos = closest;
                        frame2.currentPos = 1;
                    }
                    
                    break;
                case 2:
                    // See if it is closer to position 0 or 1
                    if (closest == 0) {
                        var frame0 = master.GetFrame(0);
                        var frame1 = master.GetFrame(1);
                        currentPos = closest;
                        frame0.currentPos = 1;
                        frame1.currentPos = 2;
                    }
                    else if (closest == 1) {
                        var frame1 = master.GetFrame(1);
                        currentPos = closest;
                        frame1.currentPos = 2;
                    }
                    
                    break;
                default:
                    // No options here
                    break;
            }
            
            master.SetPaintingTransform();
            
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
            
            transform.position = GetMousePosition(eventData);
        }

        public bool ValidPosition() {
            if (currentPos == requiredPosition) return true;
            
            return false;
        }

        private int GetClosetPosition() {
            var closest = float.MaxValue;
            var index = 0;
            
            for(var i = 0; i < master.GetSlots().Length ; i++){
                var dist =  Vector3.Distance(master.GetSlots()[i].transform.position, transform.position);
                if (dist < closest) {
                    closest = dist;
                    index = i;
                }
            }
            
            return index;
        }
        
        private Vector3 GetMousePosition(PointerEventData eventData) {
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

            return horizontal * rightDir + vertical * Vector3.up + depth * forwardDir;
        }
    }
}