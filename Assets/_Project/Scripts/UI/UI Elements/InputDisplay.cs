using System;
using _Project.Scripts.Inputs;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class InputDisplay : MonoBehaviour {
        
        [Header("Gamepad Assets Sprite")]
        [SerializeField] private string westBtt;
        [SerializeField] private string northBtt;
        [SerializeField] private string eastBtt;
        [SerializeField] private string southBtt;
        [SerializeField] private CanvasGroup group;
        
        [Header("Visuals")]
        [SerializeField] private TextMeshProUGUI backBtt; 
        [SerializeField] private TextMeshProUGUI selectBtt;
        
        private void Start() {
            if (InputsBrain.HasInstance)
                InputsBrain.Instance.OnGamepadControlled += DisplayGamepad;
            
            UpdateDisplay(true);
            DisplayGamepad(false);
        }

        private void OnDisable() {
            if (InputsBrain.HasInstance)
                InputsBrain.Instance.OnGamepadControlled -= DisplayGamepad;
        }

        private void DisplayGamepad(bool isGamepad) {
            backBtt.text = isGamepad ? $"Back {eastBtt}" : "Back [Escape]";
            selectBtt.text = isGamepad ? $"Select {southBtt}" : "Select [Left Click]";
            
            selectBtt.gameObject.SetActive(isGamepad);
        }

        public void ShowDisplay(bool doShow) {
            group.DOFade(doShow ? 1 : 0, .25f).SetUpdate(true);
        }
        
        public void UpdateDisplay(bool isInMainMenu) {
            backBtt.gameObject.SetActive(!isInMainMenu);   
        }
    }
}