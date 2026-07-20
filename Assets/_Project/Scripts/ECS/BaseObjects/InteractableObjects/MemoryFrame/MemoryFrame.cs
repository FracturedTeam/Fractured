using System;
using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class MemoryFrame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler {
        private MemoryFrameMaster master;
        private Collider collider;
        
        [Header("Painting Settings")]
        [SerializeField]
        internal int requiredPosition;
        [SerializeField] MemoryFrameScriptable data;
        [SerializeField] MeshRenderer paintingMesh;
        [SerializeField] private TMP_Text text;
        
        private int currentPos;
        private bool isUnlocked;
        private bool canBeInteracted;

        private bool isSelected;
        
        private bool isCorrectPosition;

        private float forwardTime;
        private bool mouseOnFrame = false;

        private Tween tween;
        // private Vector3 initialPosition;
        
        public void Initialize(MemoryFrameMaster master) {
            this.master = master;
            
            if(TryGetComponent(out Collider col)) collider = col;
            else throw new ArgumentNullException($"[MemoryFrame] does not have a collider component");
            
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            text?.DOFade(0, 0);
            if (text) text.text = "";
        }

        private void Update() {
            if(!canBeInteracted) return;

            if (isSelected) mouseOnFrame = true;
            
            if (mouseOnFrame) {
                forwardTime += Time.deltaTime * 5f;
            }
            else {
                forwardTime -= Time.deltaTime * 5f;
            }
            
            forwardTime = Mathf.Clamp(forwardTime, 0, 1);
            
            if (!isSelected && !master.IsAFrameSelected) {
                var cam = CinemachineBrain.GetActiveBrain(0).OutputCamera;
                var forwardDir = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
                
                transform.position = master.GetCurrentSlotPosition(currentPos) - (0.5f * forwardTime) * forwardDir;
            }
        }

        // Mouse event
        public void OnPointerDown(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            isSelected = true;
            master.SetFrameSelected(true);
            collider.enabled = false;
            
            Debug.Log($"OnPointerDown {eventData.position}");
        }

        public void OnPointerUp(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            isSelected = false;
            master.SetFrameSelected(false);
            mouseOnFrame = false;
            collider.enabled = true;
            
            // var closest = GetClosetPosition();
            // SetFramePositions(closest);
            
            Debug.Log($"OnPointerUp {eventData.position}");
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if(!canBeInteracted || master.IsAFrameSelected) return;
            
            mouseOnFrame = true;
            text?.DOFade(1, 0.5f);
            
            Debug.Log($"OnPointerEnter {eventData.position}");
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(!canBeInteracted) return;
            
            mouseOnFrame = false;
            text?.DOFade(0, .5f);
            
            Debug.Log($"OnPointerExit {eventData.position}");
        }
        
        public void OnDrag(PointerEventData eventData) {
            if(!isSelected) return;
            
            transform.position = GetMousePosition(eventData);
            var closest = GetClosetPosition();
            
            if (closest != currentPos) {
                SetFramePositions(closest);
            }
        }

        public bool ValidPosition() {
            if (currentPos == requiredPosition && isUnlocked) return true;
            
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

        public void SetNewPosition(int newPos) {
            if(currentPos == newPos) return;

            currentPos = newPos;
            tween = transform.DOMove(master.GetCurrentSlotPosition(newPos), 0.5f);
        }
        
        private void SetFramePositions(int closest) {
            switch (GetCurrentPosition()) {
                case 0:
                    // See if it is closer to position 1 or 2
                    if (closest == 1) {
                        var frame1 = master.GetFrame(1);
                        currentPos = closest;
                        frame1.SetNewPosition(0);
                    }
                    else if (closest == 2) {
                        var frame1 = master.GetFrame(1);
                        var frame2 = master.GetFrame(2);
                        currentPos = closest;
                        frame1.SetNewPosition(0);
                        frame2.SetNewPosition(1);
                    }
                    break;
                case 1:
                    // See if it is closer to position 0 or 2
                    if (closest == 0) {
                        var frame1 = master.GetFrame(0);
                        currentPos = closest;
                        frame1.SetNewPosition(1);
                    }
                    else if (closest == 2) {
                        var frame2 = master.GetFrame(2);
                        currentPos = closest;
                        frame2.SetNewPosition(1);
                    }
                    break;
                case 2:
                    // See if it is closer to position 0 or 1
                    if (closest == 0) {
                        var frame0 = master.GetFrame(0);
                        var frame1 = master.GetFrame(1);
                        currentPos = closest;
                        frame0.SetNewPosition(1);
                        frame1.SetNewPosition(2);
                    }
                    else if (closest == 1) {
                        var frame1 = master.GetFrame(1);
                        currentPos = closest;
                        frame1.SetNewPosition(2);
                    }
                    break;
                default:
                    // No options here
                    break;
            }
        }
        public int GetCurrentPosition() => currentPos;
        public void SetCurrentPosition(int newPos) => currentPos = newPos;

        public void Unlock() {
            isUnlocked = true;
            paintingMesh.material = data.material;
            if (text) text.text = data.infoText;
        }

        public void CanBeInteracted(bool can) {
            canBeInteracted = can;
        }
    }
}