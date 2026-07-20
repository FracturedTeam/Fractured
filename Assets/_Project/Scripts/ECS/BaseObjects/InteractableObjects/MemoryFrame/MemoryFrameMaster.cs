using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class MemoryFrameMaster : MonoBehaviour, IInteractable {
        private BaseObject baseObject;

        [Header("Memory Frame")]
        [SerializeField] private CinemachineCamera frameCamera;
        [SerializeField] private Transform[] frameSlots;
        [SerializeField] private MemoryFrame[] frames;
        
        private bool isInitialized;
        private bool isUsingMemoryFrame;

        public bool IsMemoryCompleted { get; private set; }
        
        public bool IsAFrameSelected { get; private set; }
        
        public void Initialize() {
            if (!isInitialized) {
                if (TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[MemoryFrame] Base object not found");

                baseObject.GetObjectType = ObjectType.MemoryFrame;

                for (var i = 0; i < frameSlots.Length; i++) {
                    frames[i].SetCurrentPosition(i);
                    frames[i].Initialize(this);
                }
                
                baseObject.SetInteract(true);
                
                isInitialized = true;
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(IsMemoryCompleted) return;
            
            if (interaction is ObjectInteraction.Contextual) {
                UseMemoryFrame();
            }
            else if (interaction is ObjectInteraction.Validate) {
                DoValidation();
            }
        }

        private void UseMemoryFrame() {
            if(!IsMemoryCompleted) isUsingMemoryFrame = !isUsingMemoryFrame;
            else
            {
                isUsingMemoryFrame = false;
                foreach (var frame in frames)
                    frame.ChangeState(false);
            }

            GameInitializer.Instance.SetShardsOnOff(!isUsingMemoryFrame);
            
            if (isUsingMemoryFrame) {
                frameCamera.Priority = 2;
                PlayerController.Instance.interact.SetIsFocus(true, baseObject);
                PlayerController.Instance.FreezeController(true);

                foreach (var frame in frames) {
                    frame.CanBeInteracted(true);
                }
            }
            else {
                frameCamera.Priority = 0;
                PlayerController.Instance.interact.SetIsFocus(false);
                PlayerController.Instance.FreezeController(false);
                
                foreach (var frame in frames) {
                    frame.CanBeInteracted(false);
                }
            }
        }

        private void DoValidation() {
            Debug.Log("DoValidation");
            
            bool allValid = true;
            foreach (var frame in frames) {
                if(!frame.ValidPosition()) allValid = false;
            }

            if (allValid) {
                CompleteFrames();
            }
        }
        
        private void CompleteFrames() {
            IsMemoryCompleted = true;
            baseObject.SetInteract(false);
            
            UseMemoryFrame();
            
            GameInitializer.Instance.EmptyShards();
            GameInitializer.Instance.ResetGlassInteractable();
            
            Debug.Log("Memory Completed");
        }

        public void DebugCompleteFrame() {
            foreach (var frame in frames) {
                frame.Unlock();
                frame.SetNewPosition(frame.requiredPosition);
            }
            
            CompleteFrames();
        }
        
        public Transform[] GetSlots() {
            return frameSlots;
        }
        
        public MemoryFrame GetFrame(int index) {
            foreach (var frame in frames) {
                if(frame.GetCurrentPosition() == index) return frame;
            }
            return null;
        }
        
        public BaseObject GetBaseObject() {
            return baseObject;
        }
        
        public Vector3 GetCurrentSlotPosition(int index) {
            return frameSlots[index].position;
        }

        public void SetFrameSelected(bool isSelected) {
            IsAFrameSelected = isSelected;
        }
        
        public void Tick(float deltaTime) {
            
        }

        public void Dispose() {
            
        }

        public void CompleteObject() {
            
        }

        public void ResetObject() {
            
        }
    }
}