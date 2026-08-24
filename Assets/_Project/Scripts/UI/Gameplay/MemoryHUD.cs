using System;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class MemoryHUD : MonoBehaviour {
        [Header("References")]
        [SerializeField] private TMP_Text memoryDialogue;
        [SerializeField] private CanvasGroup memoryObject;
        [SerializeField] private CanvasGroup confirmMemoryButton;
        [SerializeField] private TextMeshProUGUI confirmText;
        [SerializeField] private Image fillImage;
        
        [Header("Text")]
        [SerializeField] private string keyboardInput = "[F]";
        [SerializeField] private string gamepadInput = "<sprite index=2>";
        [SerializeField] private GameObject gamepadVisual;
        
        private bool isGamepadControlled = false;
        private bool isShown = false;
        
        private void Start() {
            confirmMemoryButton.alpha = 0;
            memoryDialogue.text = "";
            memoryObject.alpha = 0;
        }
        
        private void OnEnable() {
            InputsBrain.Instance.OnGamepadControlled += UpdateGamepadControlled;
        }

        private void OnDisable() {
            if(InputsBrain.HasInstance) InputsBrain.Instance.OnGamepadControlled -= UpdateGamepadControlled;
        }

        private void UpdateGamepadControlled(bool isGamepad) {
            isGamepadControlled = isGamepad;
            var icon = isGamepad ? gamepadInput : keyboardInput;
            confirmText.text = $"Hold {icon} to confirm";
            gamepadVisual.SetActive(isGamepadControlled && isShown);
        }
        
        public void SetActiveMemoryButton(bool isOn) {
            confirmMemoryButton.DOFade(isOn ? 1 : 0, 0.5f);
        }
        
        public void SetMemoryDialogue(string dialogue, Vector3 pos) {
            memoryObject.DOFade(dialogue == "" ? 0 : 1, .5f);
        
            if(dialogue == "") return;
            memoryDialogue.text = dialogue;
        
            var newPos = PlayerController.Instance.cinemachineBrain.OutputCamera.WorldToScreenPoint(pos);
            newPos = new Vector3(newPos.x, newPos.y - Screen.height * 0.25f);
        
            memoryObject.gameObject.transform.position = newPos;
        }

        public void IsInFrame(bool isIn) {
            isShown = isIn;
            gamepadVisual.SetActive(isGamepadControlled && isShown);
        }

        private void LateUpdate() {
            if(confirmMemoryButton.alpha == 0) return;

            fillImage.fillAmount = PlayerController.Instance.Interact.GetMemoryValidation();
        }
    }
}