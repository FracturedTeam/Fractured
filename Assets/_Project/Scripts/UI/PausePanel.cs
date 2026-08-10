using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PausePanel : MonoBehaviour {
        
        [SerializeField] private CanvasGroup menuGroup;
        [SerializeField] private Animator menuAnimator;
        [SerializeField] private ButtonUI[] buttons;
        
        private bool gameIsPaused;

        private void OnEnable() {
            InputsBrain.Instance.OnPause += OnEscapePressed;
        }

        private void OnDisable() {
            InputsBrain.Instance.OnPause -= OnEscapePressed;
        }

        private void OnEscapePressed() {
            ChangeState();
        }

        public void ChangeState() {
            gameIsPaused = !gameIsPaused;
            Time.timeScale = gameIsPaused ? 0 : 1;
            
            
            menuGroup.interactable = gameIsPaused;
            menuGroup.blocksRaycasts = gameIsPaused;
            menuGroup.DOFade(gameIsPaused ? 1 : 0, .3f).SetUpdate(true);

            menuAnimator.Play(gameIsPaused ? "A_PauseMenu_IN" : "A_PauseMenu_OUT");

            if (gameIsPaused) {
                foreach (var btt in buttons) {
                    btt.Enable();
                }
            }
        }

        public void LoadMenu() {
            GameSceneLoaderSystem.Instance.LoadMenu();
        }
        
    }
}
