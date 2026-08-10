using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MenuManager : MonoBehaviour {
        [Header("Camera Ref")]
        [SerializeField] private Animation animatedCamera;

        [Header("Button Ref")]
        [SerializeField] private GameObject loadGameBtt;
        
        [Header("Current Panel")]
        [SerializeField] private MenuAnimation MainMenuPanel;
        [SerializeField] private InputDisplay inputsDisplay;
        private MenuAnimation CurrentMenu;
        
        private void Start() {
            if(GameInitializer.HasInstance)
                loadGameBtt.SetActive(GameInitializer.Instance.ExistingSave());

            if (InputsBrain.HasInstance)
                InputsBrain.Instance.OnBackBtt += Back;
            
            CurrentMenu = MainMenuPanel;
        }

        private void Back() {
            if(CurrentMenu == MainMenuPanel) return;
            
            CurrentMenu.Close();
            CurrentMenu.PreviousMenu.gameObject.SetActive(true);
            var previous = CurrentMenu.PreviousMenu;
            CurrentMenu = previous;
            inputsDisplay.UpdateDisplay(CurrentMenu == MainMenuPanel);
        }

        public void UpdateCurrentMenu(MenuAnimation newMenu) {
            CurrentMenu = newMenu;
            inputsDisplay.UpdateDisplay(CurrentMenu == MainMenuPanel);
        }
        
        public void ChangeTarget(string anim) {
            animatedCamera.Play(anim);
        }

        public void QuitGame() {
            Application.Quit();
        }
        
        public void NewGame() {
            GameSceneLoaderSystem.Instance.NewGame();
        }

        public void LoadGame() {
            GameSceneLoaderSystem.Instance.LoadGame();
        }

        public void LoadLevel(int levelIndex) {
            GameSceneLoaderSystem.Instance.LoadLevel(levelIndex);
        }
    }
}
