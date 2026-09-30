using System.Collections;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.UI
{
    public class CreditManager : MonoBehaviour
    {
        [SerializeField] private AnimationClip endText_Clip;
        [SerializeField] private CanvasGroup speedUpGroup;
        [SerializeField] private TextMeshProUGUI text;
        
        private bool canSpeedUp = false;
        
        private void Start()
        {
            StartCoroutine(WaitForShowingInput(endText_Clip.length + 1f));
        }

        IEnumerator WaitForShowingInput(float time)
        {
            yield return new WaitForSeconds(time);

            speedUpGroup.DOFade(1f, 1f);
            canSpeedUp = true;
        }
        
        private void OnEnable()
        {
            if (InputsBrain.HasInstance)
            {
                InputsBrain.Instance.OnInteract += ProcessInput;
                InputsBrain.Instance.OnGamepadControlled += UpdateInput;
            }
        }

        private void OnDisable()
        {
            if (InputsBrain.HasInstance)
            {
                InputsBrain.Instance.OnInteract -= ProcessInput;
                InputsBrain.Instance.OnGamepadControlled -= UpdateInput;
            }
        }

        private void ProcessInput(InputAction.CallbackContext ctx)
        {
            if(!canSpeedUp) return;

            if (ctx.performed)
            {
                Time.timeScale = 3f;
            }
            else if(ctx.canceled)
            {
                Time.timeScale = 1f;
            }
            
        }

        private void UpdateInput(bool gamepad)
        {
            text.text = gamepad ? "Speed Up <sprite index=1>" : "Speed Up [E]";
        }
        
        public void LoadMenu() { 
            Time.timeScale = 1f;
            GameSceneLoaderSystem.Instance.LoadMenu();
        }
    }
}
