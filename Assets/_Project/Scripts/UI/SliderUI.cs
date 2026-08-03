using System;
using _Project.Scripts.GameServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class SliderUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        private enum OptionType {
            Audio,
            Graphics,
            Accessibility
        }
        
        private enum VolumeType {
            Master,
            Sfx,
            Music
        }
        
        private enum GraphicsType {
            Brightness = 0,
            Contrast = 1
        }

        private Slider slider;
        
        [Header("Visuals")]
        [SerializeField] private CanvasGroup hoverGroup;
        [SerializeField] private Ease easeType;
        [SerializeField] private TextMeshProUGUI percentText;
        
        [Header("Settings")]
        [SerializeField] private OptionType optionType;
        [SerializeField] private VolumeType volumeType;
        [SerializeField] private GraphicsType graphicsType;

        private Tweener tween;
        
        private void Start() {
            slider = GetComponent<Slider>();

            if (!GameInitializer.HasInstance) return;
            
            switch (optionType) {
                case OptionType.Audio:
                    slider.value = GameInitializer.Instance.GetVolume((int)volumeType);
                    percentText.text = $"{slider.value * 100:F0}%";
                    break;
                case OptionType.Graphics:
                    slider.value = GameInitializer.Instance.GetPostProcess((int)graphicsType);
                    percentText.text = $"{slider.value}";
                    break;
                case OptionType.Accessibility:
                    percentText.text = $"{slider.value * 100:F0}%";
                    break;
            }
        }

        private void OnEnable() {
            hoverGroup.alpha = 0;
            hoverGroup.gameObject.SetActive(true);
        }

        void OnDisable() {
            tween?.Kill();
        }

        public void OnSliderValueChanged() {
            switch (optionType) {
               case OptionType.Audio:
                   GameInitializer.Instance.SetVolume((int)volumeType, slider.value);
                   percentText.text = $"{slider.value * 100:F0}%";
                   break;
               case OptionType.Graphics:
                  GameInitializer.Instance.SetPostProcess((int)graphicsType, (int)slider.value);
                   percentText.text = $"{slider.value}";
                   break;
               case OptionType.Accessibility:
                   percentText.text = $"{slider.value * 100:F0}%";
                   break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            tween = hoverGroup.DOFade(1f, 0.25f).SetUpdate(true).SetEase(easeType);
        }

        public void OnPointerExit(PointerEventData eventData) {
            tween = hoverGroup.DOFade(0f, 0.25f).SetUpdate(true).SetEase(easeType);
        }
    }
}