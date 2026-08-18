using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class ChapterSelection : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
        [Header("Chapter")]
        [SerializeField] private int chapterIndex;
        
        [Header("Visuals")] 
        [SerializeField] private CanvasGroup hoverGroup;
        [SerializeField] private Material unlockedMat;
        [SerializeField] private Material lockedMat;
        [SerializeField] private Image chapterImage;
        [SerializeField] private GameObject lockedObject;
        
        [Header("Event On Clicked")]
        public UnityEvent onClickPostTimer;
        [SerializeField] private float callbackTime = 0.5f;
        
        [Header("Tween settings")]
        [SerializeField] private Ease easeType;
        [SerializeField] private float tweenTime = 0.25f;
        
        private Tweener tweener;
        
        private bool locked;
        private bool hasClicked = false;
        
        private void Start() {
            //Get if chapter is Unlocked
        }
        
        public void OnPointerDown(PointerEventData eventData) {
            if(hasClicked) return;
            
            if(locked) return;

            StartCoroutine(CallClickPostTimer());
        }
        
        private IEnumerator CallClickPostTimer() { 
            yield return new WaitForSecondsRealtime(callbackTime);
            
            onClickPostTimer?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            tweener = hoverGroup.DOFade(1f, tweenTime).SetEase(easeType).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData) {
            tweener = hoverGroup.DOFade(0f, tweenTime).SetEase(easeType).SetUpdate(true);
        }
        
        private void OnEnable() {
            hoverGroup.alpha = 0;
            hoverGroup.gameObject.SetActive(true);
            chapterImage.material = locked ? lockedMat : unlockedMat;
            lockedObject.SetActive(locked);
        }

        private void OnDisable() {
            tweener?.Kill();
        }
    }
}