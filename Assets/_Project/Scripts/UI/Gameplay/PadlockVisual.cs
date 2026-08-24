using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay {
    public class PadlockVisual : MonoBehaviour {
        public float distance;
        public float targetScreenFraction = 0.35f;
        public float meshHeightAtScaleOne = 1f;

        private EventBinding<PadlockEvent> padlockEvent;

        [SerializeField] private LockWheel wheel0;
        [SerializeField] private LockWheel wheel1;
        [SerializeField] private LockWheel wheel2;
        [SerializeField] private LockWheel wheel3;
        [SerializeField] private GameObject lockVisual;
        
        private PadlockAttribute currentLock;
        private int previousSelected;
        
        private void OnEnable() {
            padlockEvent = new EventBinding<PadlockEvent>(ShowPadlock);
            EventBus<PadlockEvent>.Register(padlockEvent);
            
            wheel0.Initialize(this);
            wheel1.Initialize(this);
            wheel2.Initialize(this);
            wheel3.Initialize(this);
            
            lockVisual.SetActive(false);
        }

        private void OnDisable() {
            EventBus<PadlockEvent>.Deregister(padlockEvent);
        }
        
        private void ShowPadlock(PadlockEvent e) {
            lockVisual.SetActive(e.doShow);
            if(!e.doShow) return;
            
            HudManager.Instance.interact.ShowInteractionPadlock(true);
        
            var camPos = PlayerController.Instance.cinemachineBrain.OutputCamera.transform;
            transform.position = camPos.position + new Vector3(0,1,-0.75f) + camPos.forward * distance;
        
            transform.LookAt(camPos); 
            transform.eulerAngles = new Vector3(transform.eulerAngles.x - 30f, transform.eulerAngles.y, 0);
            
            wheel0.SetNumber(e.firstDigit);
            wheel1.SetNumber(e.secondDigit);
            wheel2.SetNumber(e.thirdDigit);
            wheel3.SetNumber(e.fourthDigit);

            if (previousSelected != e.selectedDigit) {
                UpdateHighlight(false);
                previousSelected = e.selectedDigit;
                UpdateHighlight(true);
            }
            
            currentLock = e.currentLock;
            
            UpdatePadlockScale();
        }

        private void UpdateHighlight(bool doHighlight) {
            switch (previousSelected) {
                case 0:
                    wheel0.SetHighlight(doHighlight);
                    break;
                case 1:
                    wheel1.SetHighlight(doHighlight);
                    break;
                case 2:
                    wheel2.SetHighlight(doHighlight);
                    break;
                case 3:
                    wheel3.SetHighlight(doHighlight);
                    break;
            }
        }
        
        private void UpdatePadlockScale() {
            var cam = PlayerController.Instance.cinemachineBrain.OutputCamera;
         
            var fov = cam.fieldOfView;
            var visibleHeight= 2f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
            var targetWorld= visibleHeight * targetScreenFraction;
            var scale= targetWorld / meshHeightAtScaleOne;

            transform.localScale = Vector3.one * scale;
        }

        public void UpdateLock() {
            currentLock.UpdateLockDigit(wheel0.currentDigit, wheel1.currentDigit, wheel2.currentDigit, wheel3.currentDigit);
        }
    }
}