using _Project.Scripts.GameServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI.Gameplay {
    public class LockWheel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler {
      
        public bool IsSelected { get; private set; }
        public int currentDigit { get; private set; }

        private PadlockVisual master;

        private float currentRotation;
        
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private MeshRenderer meshRenderer;

        private bool OnWheel;
        
        public void Initialize(PadlockVisual _master) {
            master = _master;
        }
        
        public void SetNumber(int number) {
            if(currentDigit == number) return;
            
            currentDigit = number;

            currentRotation = (number + 1) * -36f;
            transform.DOLocalRotate(new Vector3(currentRotation, 0, 0), 0.25f);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            meshRenderer.material = highlightMaterial;
            OnWheel = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(!IsSelected) meshRenderer.material = normalMaterial;
            OnWheel = false;
        }

        public void OnPointerDown(PointerEventData eventData) {
            IsSelected = true;
        }

        public void OnPointerUp(PointerEventData eventData) {
            IsSelected = false;
            
            if(!OnWheel) meshRenderer.material = normalMaterial;
            
            currentRotation = (currentDigit + 1) * -36f;
            transform.DOLocalRotate(new Vector3(currentRotation, 0, 0), 0.25f);
            
            master.UpdateLock();
        }

        public void OnDrag(PointerEventData eventData) {
            RotateWheel(eventData.delta.y);
        }

        private void RotateWheel(float input) {
            currentRotation -= input * 0.25f;
            transform.localRotation = Quaternion.Euler(currentRotation, 0, 0);

            var newDigit = GetCurrentDigit();
            if (newDigit != currentDigit) {
                currentDigit = newDigit;
                GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().lock_Tick);
            }
        }

        private int GetCurrentDigit() {
            const float step = 36f;
            var normalizedAngle = (-currentRotation % 360f + 360f) % 360f;
            var raw = Mathf.RoundToInt(normalizedAngle / step) % 10;
            return (raw - 1 + 10) % 10;
        }

        public void SetHighlight(bool highlight) {
            meshRenderer.material = highlight ? highlightMaterial : normalMaterial;
        }
    }
}