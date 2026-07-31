using System.Collections;
using _Project.Scripts.GameServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class ButtonMainMenu : MonoBehaviour{
        [Header("Text Settings")]
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Color whiteColor;
        [SerializeField] private Color blueColor;
        
        [Header("background Settings")]
        [SerializeField] private CanvasGroup hoverGroup;
        
        [Header("Pressed Settings")]
        [SerializeField] private CanvasGroup pressedGroup;
        [SerializeField] private Ease easeType;
        
        [Header("Others")]
        [SerializeField] private float callbackTime = 0.5f;
        [SerializeField] private float tweenTime = 0.25f;
        [SerializeField] private float multiplicator = 1.15f;
        
        [Header("Event On Clicked")]
        public UnityEvent onClickPostTimer;
        
        private EventTrigger button;
        private Vector3 scale;
        private Tweener tweener;
        
        private Image backgroundImg;
        
        private void Awake() {
            scale = transform.localScale;
            if(TryGetComponent(typeof(EventTrigger), out var btn))
                button = (EventTrigger)btn;
            if(TryGetComponent(out Image img))
                backgroundImg = img;
        }

        public void OnHover(bool hovering) {
            if(!button.enabled) hovering = false;
            
            tweener = transform.DOScale(hovering ? scale * multiplicator : scale, tweenTime).SetUpdate(true);
            
            buttonText.color = hovering ? blueColor : whiteColor;
            hoverGroup.DOFade(hovering ? 0.36f : 0f, 0.3f).SetUpdate(true).SetEase(easeType);
            pressedGroup.gameObject.SetActive(false);
        }
        public void OnClicked() {
            tweener = transform.DOScale(scale, tweenTime).SetUpdate(true);
            
            button.enabled = false;
            buttonText.color = blueColor;
            backgroundImg.enabled = false;
            hoverGroup.DOFade(0, 0.15f).SetUpdate(true).SetEase(easeType);
            pressedGroup.gameObject.SetActive(true);
            pressedGroup.DOFade(1, 0.3f).SetUpdate(true).SetEase(easeType);
            
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().uiBttClickedSound);
            
            StartCoroutine(CallClickPostTimer());
        }
        
        private IEnumerator CallClickPostTimer() { 
            yield return new WaitForSecondsRealtime(callbackTime);
            
            onClickPostTimer?.Invoke();
        }

        private void OnEnable() {
            //sometimes the OnHover false of the disable doesn't work, this fixes it 
            tweener = transform.DOScale(scale, 0).SetUpdate(true);
            backgroundImg.enabled = true;
            buttonText.color = whiteColor;
            hoverGroup.alpha = 0f;
            hoverGroup.gameObject.SetActive(true);
            pressedGroup.gameObject.SetActive(false);
            pressedGroup.alpha = 0;
            
            button.enabled = true;
        }

        private void OnDisable() {
            tweener?.Kill();
        }
    }
}