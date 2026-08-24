using System;
using System.Collections;
using _Project.Scripts.GameServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
        [Header("General Settings")]
        [SerializeField] private bool settingsButtons;
        
        [Header("Text Settings")]
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Color whiteColor;
        [SerializeField] private Color act1Color;
        [SerializeField] private Color act2Color;
        [SerializeField] private Color act3Color;

        private Color alternateColor;
        
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
        
        private Vector3 scale;
        private Tweener tweener;
        
        private void Awake() {
            scale = transform.localScale;
        }
        
        private IEnumerator CallClickPostTimer() { 
            yield return new WaitForSecondsRealtime(callbackTime);
            
            onClickPostTimer?.Invoke();
            
            if (settingsButtons) {
                buttonText.color = alternateColor;
            }
            else {
                buttonText.color = whiteColor;
                backgroundImage.sprite = backgroundNormal;
            }
            
            pressedGroup.DOFade(0, 0.3f).SetUpdate(true).SetEase(easeType);
            backgroundImage.gameObject.SetActive(true);
        }

        private void Start() {
            if (GameInitializer.HasInstance) {
                var ChapterIndex = GameInitializer.Instance.GetLastChapter();
                alternateColor = ChapterIndex switch {
                    1 => act1Color,
                    2 => act2Color,
                    3 => act3Color,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private void OnEnable() {
            Enable();
        }

        public void Enable() {
            tweener = transform.DOScale(scale, 0).SetUpdate(true);
            
            buttonText.color = whiteColor;
            backgroundImage.sprite = backgroundNormal;
            backgroundImage.gameObject.SetActive(true);
            pressedGroup.gameObject.SetActive(false);
            pressedGroup.alpha = 0;
        }
        
        private void OnDisable() {
            tweener?.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            tweener = transform.DOScale(scale * multiplicator, tweenTime).SetUpdate(true);
            
            buttonText.color = alternateColor;
            backgroundImage.sprite = backgroundHover;
            pressedGroup.gameObject.SetActive(false);
        }

        public void OnPointerExit(PointerEventData eventData) {
            tweener = transform.DOScale(scale, tweenTime).SetUpdate(true);
            
            buttonText.color = whiteColor;
            backgroundImage.sprite = backgroundNormal;
            pressedGroup.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData) {
            tweener = transform.DOScale(scale, tweenTime).SetUpdate(true);

            if (!settingsButtons) {
                buttonText.color = alternateColor;
                backgroundImage.gameObject.SetActive(false);
                pressedGroup.gameObject.SetActive(true);
                pressedGroup.DOFade(1, 0.3f).SetUpdate(true).SetEase(easeType);
            }
            
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().ui_Clicked);
            
            StartCoroutine(CallClickPostTimer());
        }
    }
}
