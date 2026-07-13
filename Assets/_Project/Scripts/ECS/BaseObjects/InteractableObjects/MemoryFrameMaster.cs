using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;

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

        private bool memoryCompleted;
        
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
            if(memoryCompleted) return;
            
            if (interaction is ObjectInteraction.Contextual) {
                UseMemoryFrame();
            }
        }

        private void UseMemoryFrame() {
            isUsingMemoryFrame = !isUsingMemoryFrame;
            
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

        public Vector3 GetCurrentSlotPosition(int index) {
            return frameSlots[index].position;
        }

        private void CompleteFrames() {
            memoryCompleted = true;
            baseObject.SetInteract(false);
            
            UseMemoryFrame();
            
            Debug.Log("Memory Completed");
        }
        
        public Transform[] GetSlots() {
            return frameSlots;
        }

        public void SetPaintingTransform() {
            bool allValid = true;
            foreach (var frame in frames) {
                frame.transform.position = frameSlots[frame.GetCurrentPosition()].position;
                if(!frame.ValidPosition()) allValid = false;
            }

            if (allValid) {
                CompleteFrames();
            }
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