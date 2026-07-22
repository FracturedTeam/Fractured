using System;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
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
        private bool gamepadControlled;

        private bool isSelected;
        
        private bool isCorrectPosition;

        private float forwardTime;
        private bool mouseOnFrame = false;
        private bool isAtOriginalPos;

        private Tween tween;
        private Transform cam;
        
        private readonly CountdownTimer gamepadTimer = new(0.25f);
        
        public void Initialize(MemoryFrameMaster master) {
            this.master = master;
            if (text) text.text = "";
            
            if(TryGetComponent(out Collider col)) collider = col;
            else throw new ArgumentNullException($"[MemoryFrame] does not have a collider component");
            
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            text?.DOFade(0, 0);
            cam = master.frameCamera.transform;
        }

        private void Update() {
            if(gamepadControlled) return;
            if(!canBeInteracted && !isAtOriginalPos) return;

            if (isSelected) mouseOnFrame = true;
            
            if (mouseOnFrame) {
                forwardTime += Time.deltaTime * 5f;
            }
            else {
                forwardTime -= Time.deltaTime * 5f;
            }
            
            forwardTime = Mathf.Clamp(forwardTime, 0, 1);
            if (Mathf.Approximately(forwardTime, 0))
                isAtOriginalPos = true;
            
            if (!isSelected && !master.IsAFrameSelected) {
                var forwardDir = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
                transform.position = master.GetCurrentSlotPosition(currentPos) - (0.5f * forwardTime) * forwardDir;
            }
        }

        private void OnDisable() {
            tween.Kill();
        }

        // Mouse event
        public void OnPointerDown(PointerEventData eventData) {
            if(!canBeInteracted || gamepadControlled) return;
            
            isSelected = true;
            master.SetFrameSelected(true);
            collider.enabled = false;
            
            Debug.Log($"OnPointerDown");
        }

        public void OnPointerUp(PointerEventData eventData) {
            if(!canBeInteracted || gamepadControlled) return;
            
            isSelected = false;
            master.SetFrameSelected(false);
            mouseOnFrame = false;
            collider.enabled = true;
            
            // var closest = GetClosetPosition();
            // SetFramePositions(closest);
            
            Debug.Log($"OnPointerUp");
        }

        public void OnGamepadSelect(bool isSelected) {
            ChangeState(isSelected);
            
            this.isSelected = isSelected;
            master.SetFrameSelected(isSelected);
            
            if (isSelected) {
                var forwardDir = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
                tween = transform.DOMove(master.GetCurrentSlotPosition(currentPos) - 0.5f * forwardDir, 0.5f);
            }
            else
                tween = transform.DOMove(master.GetCurrentSlotPosition(currentPos), 0.5f);
        }

        public void OnGamepadMove(Vector2 delta) {
            if(!isSelected || gamepadTimer.IsRunning) return;
            
            var closest = currentPos;
            if (delta.x > 0.15f) {
                closest++;
                if (closest >= master.GetSlots().Length)
                    closest = 0;
                gamepadTimer.Start();
            }
            else if (delta.x < -0.15f) {
                closest--;
                if(closest < 0)
                    closest = master.GetSlots().Length - 1;
                gamepadTimer.Start();
            }
            
            SetFramePositions(closest);
            SetNewPosition(closest);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            if(!canBeInteracted || master.IsAFrameSelected || gamepadControlled) return;

            ChangeState(true);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(!canBeInteracted || gamepadControlled) return;

            ChangeState(false);
        }

        public void ChangeState(bool isHovering)
        {
            mouseOnFrame = isHovering;
            HudManager.Instance.SetMemoryDialogue(data.infoText, GetClosetPosition());
        }

        public void OnDrag(PointerEventData eventData) {
            if(!isSelected || gamepadControlled) return;

            UpdateFramePosition(eventData.delta);
        }

        private void UpdateFramePosition(Vector2 delta) {
            transform.position = GetMousePosition(delta);
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
        
        private Vector3 GetMousePosition(Vector2 delta) {
            var outputCam = CinemachineBrain.GetActiveBrain(0).OutputCamera;
            var screenPosition = outputCam.WorldToScreenPoint(transform.position);
            
            var newPos = outputCam.ScreenToWorldPoint(new Vector3(
                screenPosition.x + delta.x, 
                screenPosition.y + delta.y, 
                screenPosition.z
            ));
            
            var rightDir = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;
            var forwardDir = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
            
            var horizontal = Vector3.Dot(newPos, rightDir);
            var depth = Vector3.Dot(newPos, forwardDir);
            var vertical = Vector3.Dot(newPos, Vector3.up);

            return horizontal * rightDir + vertical * Vector3.up + depth * forwardDir;
        }

        public void SetNewPosition(int newPos) {
            if(currentPos == newPos && !gamepadControlled) return;

            currentPos = newPos;
            if (isSelected) {
                var forwardDir = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
                tween = transform.DOMove(master.GetCurrentSlotPosition(currentPos) - 0.5f * forwardDir, 0.5f);
            }
            else
                tween = transform.DOMove(master.GetCurrentSlotPosition(currentPos), 0.5f);
        }
        
        private void SetFramePositions(int closest) {
            int framesAmount = master.GetSlots().Length;

            Debug.Log("There is : " + framesAmount + " frames");
            
            if (framesAmount == 2) {
                switch (GetCurrentPosition()) {
                case 0: // See if it is closer to position 1 or 2
                    if (closest == 1) {
                        currentPos = closest;
                        
                        var frame1 = master.GetFrameAtPos(1);
                        frame1.SetNewPosition(0);
                    }
                    break;
                
                case 1: // See if it is closer to position 0 or 2
                    if (closest == 0) {
                        currentPos = closest;
                        
                        var frame0 = master.GetFrameAtPos(0);
                        frame0.SetNewPosition(1);
                    }
                    break;
                
                default:
                    // No options here
                    break;
                }
            }
            else if (framesAmount == 3) {
                switch (GetCurrentPosition()) {
                case 0: // See if it is closer to position 1 or 2
                    if (closest == 1) {
                        var frame1 = master.GetFrameAtPos(1);
                        
                        currentPos = closest;
                        
                        frame1.SetNewPosition(0);
                    }
                    else if (closest == 2) {
                        var frame1 = master.GetFrameAtPos(1);
                        var frame2 = master.GetFrameAtPos(2);
                        
                        currentPos = closest;
                        frame1.SetNewPosition(0);
                        frame2.SetNewPosition(1);
                    }
                    break;
                
                case 1: // See if it is closer to position 0 or 2
                    if (closest == 0) {
                        var frame0 = master.GetFrameAtPos(0);
                        
                        currentPos = closest;
                        
                        frame0.SetNewPosition(1);
                    }
                    else if (closest == 2) {
                        var frame2 = master.GetFrameAtPos(2);
                        
                        currentPos = closest;
                        
                        frame2.SetNewPosition(1);
                    }
                    break;
                
                case 2: // See if it is closer to position 0 or 1
                    if (closest == 0) {
                        var frame0 = master.GetFrameAtPos(0);
                        var frame1 = master.GetFrameAtPos(1);
                        
                        currentPos = closest;
                        
                        frame0.SetNewPosition(1);
                        frame1.SetNewPosition(2);
                    }
                    else if (closest == 1) {
                        var frame1 = master.GetFrameAtPos(1);
                        
                        currentPos = closest;
                        
                        frame1.SetNewPosition(2);
                    }
                    break;
                default:
                    // No options here
                    break;
                }
            }
            else if (framesAmount == 4) {
                switch (GetCurrentPosition()) {
                case 0: // See if it is closer to position 1 or 2
                    if (closest == 1) {
                        var frame1 = master.GetFrameAtPos(1);
                        
                        currentPos = closest;
                        
                        frame1.SetNewPosition(0);
                    }
                    else if (closest == 2) {
                        var frame1 = master.GetFrameAtPos(1);
                        var frame2 = master.GetFrameAtPos(2);
                        
                        currentPos = closest;
                        
                        frame1.SetNewPosition(0);
                        frame2.SetNewPosition(1);
                    }
                    else if (closest == 3) {
                        var frame1 = master.GetFrameAtPos(1);
                        var frame2 = master.GetFrameAtPos(2);
                        var frame3 = master.GetFrameAtPos(3);
                        
                        currentPos = closest;
                        
                        frame1.SetNewPosition(0);
                        frame2.SetNewPosition(1);
                        frame3.SetNewPosition(2);
                    }
                    break;
                
                case 1: // See if it is closer to position 0 or 2
                    if (closest == 0) {
                        var frame0 = master.GetFrameAtPos(0);
                        
                        currentPos = closest;
                        
                        frame0.SetNewPosition(1);
                    }
                    else if (closest == 2) {
                        var frame2 = master.GetFrameAtPos(2);
                        
                        currentPos = closest;
                        
                        frame2.SetNewPosition(1);
                    }
                    else if (closest == 3) {
                        var frame2 = master.GetFrameAtPos(2);
                        var frame3 = master.GetFrameAtPos(3);
                        
                        currentPos = closest;
                        
                        frame2.SetNewPosition(1);
                        frame3.SetNewPosition(2);
                    }
                    break;
                
                case 2: // See if it is closer to position 0 or 1
                    if (closest == 0) {
                        var frame0 = master.GetFrameAtPos(0);
                        var frame1 = master.GetFrameAtPos(1);
                        
                        currentPos = closest;
                        
                        frame0.SetNewPosition(1);
                        frame1.SetNewPosition(2);
                    }
                    else if (closest == 1) {
                        var frame1 = master.GetFrameAtPos(1);
                        
                        currentPos = closest;
                        
                        frame1.SetNewPosition(2);
                    }
                    else if (closest == 3) {
                        var frame3 = master.GetFrameAtPos(3);
                        
                        currentPos = closest;
                        
                        frame3.SetNewPosition(2);
                    }
                    break;
                case 3:
                    if (closest == 2) {
                        var frame2 = master.GetFrameAtPos(2);
                        
                        currentPos = closest;
                        
                        frame2.SetNewPosition(3);
                    }
                    else if (closest == 1) {
                        var frame1 = master.GetFrameAtPos(1);
                        var frame2 = master.GetFrameAtPos(2);
                        
                        currentPos = closest;
                        
                        frame1.SetNewPosition(2);
                        frame2.SetNewPosition(3);
                    }
                    else if (closest == 0) {
                        var frame0 = master.GetFrameAtPos(0);
                        var frame1 = master.GetFrameAtPos(1);
                        var frame2 = master.GetFrameAtPos(2);
                        
                        currentPos = closest;
                        
                        frame0.SetNewPosition(1);
                        frame1.SetNewPosition(2);
                        frame2.SetNewPosition(3);
                    }
                    break;
                default:
                    // No options here
                    break;
                }
            }
        }
        public int GetCurrentPosition() => currentPos;
        public void SetCurrentPosition(int newPos) => currentPos = newPos;

        public void Unlock() {
            isUnlocked = true;
            paintingMesh.material = data.material;
            if (text) text.text = data.infoText;
        }

        public void CanBeInteracted(bool can, bool isGamepadControlled) {
            canBeInteracted = can;
            gamepadControlled = isGamepadControlled;
            if(!can)
                tween = transform.DOMove(master.GetCurrentSlotPosition(currentPos), 0.5f);
        }
    }
}