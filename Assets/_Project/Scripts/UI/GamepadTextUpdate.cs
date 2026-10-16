using _Project.Scripts.Inputs;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class GamepadTextUpdate : MonoBehaviour
    {
        [SerializeField] private TextMeshPro BaseText;
        [SerializeField] private TextMeshPro HideText;

        [SerializeField] private string gamepadText;
        [SerializeField] private string keyboardText;

        [SerializeField] private GameObject sprite;
        
        private void OnEnable() {
            if (InputsBrain.HasInstance)
            {
                InputsBrain.Instance.OnGamepadControlled += DisplayGamepad;
                DisplayGamepad(!InputsBrain.Instance.IsKeyboardControl);
            }
            
        }

        private void OnDisable() {
            if (InputsBrain.HasInstance)
                InputsBrain.Instance.OnGamepadControlled -= DisplayGamepad;
        }

        private void DisplayGamepad(bool isGamepad) {
            sprite.SetActive(!isGamepad);
            BaseText.text = isGamepad ? gamepadText : keyboardText;
            HideText.text = isGamepad ? gamepadText : keyboardText;
        }
    }
}