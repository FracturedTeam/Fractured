using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI {
    public class ToggleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private CanvasGroup hoverGroup;
        [SerializeField] private Ease easeType;
        private Tweener tween;

        private void Start() {
            hoverGroup.alpha = 0;
            hoverGroup.gameObject.SetActive(true);
        }

        private void OnEnable() {
            hoverGroup.alpha = 0;
        }

        private void OnDisable() {
            tween?.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            tween = hoverGroup.DOFade(1f, 0.25f).SetUpdate(true).SetEase(easeType);
        }

        public void OnPointerExit(PointerEventData eventData) {
            tween = hoverGroup.DOFade(0f, 0.25f).SetUpdate(true).SetEase(easeType);
        }
    }
}