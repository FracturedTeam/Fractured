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
    public class ButtonMainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
        [Header("Text Settings")]
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Color whiteColor;
        [SerializeField] private Color act1Color;
        [SerializeField] private Color act2Color;
        [SerializeField] private Color act3Color;

        private Color alternateColor;
        
        [Header("background Settings")]
        [SerializeField] private CanvasGroup hoverGroup;
        
        [Header("Pressed Settings")]
        [SerializeField] private CanvasGroup pressedGroup;
        [SerializeField] private Ease easeType;
        
        [Header("Others")]
        [SerializeField] private float callbackTime = 0.5f;
        [SerializeField] private float tweenTime = 0.25f;
        [SerializeField] private float multiplicator = 1.15f;
        [SerializeField] private MenuManager menuManager;
        [SerializeField] private MenuAnimation openedMenu;
        [SerializeField] private bool isPlayBtt;
        
        [Header("Event On Clicked")]
        public UnityEvent onClickPostTimer;
        
        private Vector3 scale = Vector3.one;
        private Tweener tweener;
        
        private Image backgroundImg;
        
        private bool pressed;
        
        private void Awake() {
            if(TryGetComponent(out Image img))
                backgroundImg = img;
        }

        private void Start() {
            alternateColor = FindFirstObjectByType<MenuManager>().ChapterIndex switch {
                1 => act1Color,
                2 => act2Color,
                3 => act3Color,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private IEnumerator CallClickPostTimer() { 
            yield return new WaitForSecondsRealtime(callbackTime);
            
            menuManager?.UpdateCurrentMenu(openedMenu);
            onClickPostTimer?.Invoke();
        }

        private void OnEnable() {
            //sometimes the OnHover false of the disable doesn't work, this fixes it 
            transform.localScale = scale;
            backgroundImg.enabled = true;
            buttonText.color = whiteColor;
            hoverGroup.alpha = 0f;
            hoverGroup.gameObject.SetActive(true);
            pressedGroup.gameObject.SetActive(false);
            pressedGroup.alpha = 0;
            pressed = false;
        }

        private void OnDisable() {
            tweener?.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if(pressed) return;
            
            tweener = transform.DOScale(scale * multiplicator, tweenTime).SetUpdate(true);
            
            buttonText.color = alternateColor;
            hoverGroup.DOFade(0.36f, 0.3f).SetUpdate(true).SetEase(easeType);
            pressedGroup.gameObject.SetActive(false);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(pressed) return;
            
            tweener = transform.DOScale(scale, tweenTime).SetUpdate(true);
            
            buttonText.color = whiteColor;
            hoverGroup.DOFade(0f, 0.3f).SetUpdate(true).SetEase(easeType);
            pressedGroup.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData) {
            tweener = transform.DOScale(scale, tweenTime).SetUpdate(true);
            
            pressed = true;
            buttonText.color = alternateColor;
            backgroundImg.enabled = false;
            hoverGroup.DOFade(0, 0.15f).SetUpdate(true).SetEase(easeType);
            pressedGroup.gameObject.SetActive(true);
            pressedGroup.DOFade(1, 0.3f).SetUpdate(true).SetEase(easeType);
            
            GameInitializer.Instance.PlaySound2D(
                isPlayBtt ? GameInitializer.Instance.GetBank().ui_Play : GameInitializer.Instance.GetBank().ui_Clicked);
            
            StartCoroutine(CallClickPostTimer());
        }
    }
}