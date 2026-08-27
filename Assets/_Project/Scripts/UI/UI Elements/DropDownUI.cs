using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class DropDownUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [Header("Visual")]
        [SerializeField] TextMeshProUGUI settingText;
        [SerializeField] private CanvasGroup hoverGroup;
        [SerializeField] private Ease easeType;
        [SerializeField] private Image[] selectedItems;
        
        public Action<int> OnValueChanged = delegate { };
        
        private float unselectedValue = 0.07843138f;
        
        private Tweener tween;

        private Dropdown.OptionDataList m_Options = new Dropdown.OptionDataList();
        
        private int m_Value;
        public int value {
            get => m_Value;
            set {
                SetValue(value); 
            }
        }
        
        private List<Dropdown.OptionData> options {
            get => m_Options.options;
            set {m_Options.options = value; RefreshShownValue();}
        }
        
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

        public void UpdateIndex(bool add) {
            var predictValue = add ? m_Value + 1 : m_Value - 1;
            if(predictValue > options.Count - 1)
                return;
            if(predictValue < 0)
                return;
            
            value = predictValue;
        }
        
        public void AddOptions(List<string> options) {
            for (int i = 0; i < options.Count; i++)
                this.options.Add(new Dropdown.OptionData(options[i]));

            RefreshShownValue();
        }
        
        public void ClearOptions() {
            options.Clear();
        }
        
        public void RefreshShownValue() {
            foreach (var item in selectedItems) {
                item.gameObject.SetActive(false);
            }

            for (var i = 0; i < options.Count; i++) {
                selectedItems[i].gameObject.SetActive(true);
            }
            
            foreach (var item in selectedItems) {
                item.color = new Color(item.color.r, item.color.g, item.color.b, unselectedValue);
            }
            
            selectedItems[value].color = new Color(selectedItems[value].color.r, selectedItems[value].color.g, selectedItems[value].color.b, 1);
            settingText.text = options[value].text;
        }
        
        private void SetValue(int value) {
            if (Application.isPlaying && (value == m_Value || options.Count == 0))
                return;
            
            m_Value = value;
            OnValueChanged.Invoke(m_Value);
            RefreshShownValue();
        }
    }
}