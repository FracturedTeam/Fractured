using System.Collections;
using _Project.Scripts.GameServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class ButtonUI : MonoBehaviour {
        [Header("General Settings")]
        [SerializeField] private bool settingsButtons;
        
        [Header("Text Settings")]
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Color whiteColor;
        [SerializeField] private Color blueColor;
        
        [Header("background Settings")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Sprite backgroundNormal;
        [SerializeField] private Sprite backgroundHover;
        
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
        
        private void Awake() {
            scale = transform.localScale;
            if(TryGetComponent(typeof(EventTrigger), out var btn))
                button = (EventTrigger)btn;
        }

        public void OnHover(bool hovering) {
            tweener = transform.DOScale(hovering ? scale * multiplicator : scale, tweenTime).SetUpdate(true);
            
            buttonText.color = hovering ? blueColor : whiteColor;
            backgroundImage.sprite = hovering ? backgroundHover: backgroundNormal;
            pressedGroup.gameObject.SetActive(false);
        }
        public void OnClicked() {
            tweener = transform.DOScale(scale, tweenTime).SetUpdate(true);

            if (!settingsButtons) {
                button.enabled = false;
                buttonText.color = blueColor;
                backgroundImage.gameObject.SetActive(false);
                pressedGroup.gameObject.SetActive(true);
                pressedGroup.DOFade(1, 0.3f).SetUpdate(true).SetEase(easeType);
            }
            
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().uiBttClickedSound);
            
            StartCoroutine(CallClickPostTimer());
        }
        
        private IEnumerator CallClickPostTimer() { 
            yield return new WaitForSecondsRealtime(callbackTime);
            
            onClickPostTimer?.Invoke();
            
            if (settingsButtons) {
                pressedGroup.DOFade(0, 0.3f).SetUpdate(true).SetEase(easeType);
                buttonText.color = blueColor;
                backgroundImage.gameObject.SetActive(true);
            }
        }

        private void OnEnable() {
            //sometimes the OnHover false of the disable doesn't work, this fixes it 
            tweener = transform.DOScale(scale, 0).SetUpdate(true);
            
            buttonText.color = whiteColor;
            backgroundImage.sprite = backgroundNormal;
            backgroundImage.gameObject.SetActive(true);
            pressedGroup.gameObject.SetActive(false);
            pressedGroup.alpha = 0;
            
            button.enabled = true;
        }

        private void OnDisable() {
            tweener?.Kill();
        }
    }
}
