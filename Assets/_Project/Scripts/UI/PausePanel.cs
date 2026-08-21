using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class PausePanel : MonoBehaviour {
        
        [SerializeField] private CanvasGroup menuGroup;
        [SerializeField] private Animator menuAnimator;
        [SerializeField] private ButtonUI[] buttons;
        
        [Header("General Button")]
        [SerializeField] private GameObject[] MainMenuButtons;
        
        [Header("Settings Buttons")]
        [SerializeField] private GameObject[] settingsButtons;
        [SerializeField] private GameObject[] audioButtons;
        [SerializeField] private GameObject[] videoButtons;
        [SerializeField] private GameObject[] inputButtons;
        [SerializeField] private GameObject[] accessButtons;
        
        [Header("General")]
        [SerializeField] private MenuAnimation MainMenuPanel;
        [SerializeField] private InputDisplay inputsDisplay;
        private MenuAnimation CurrentMenu;
        
        private int currentIndex = 0;
        private int settingsIndex = 0;
        
        private CurrentMenu currentMenuType;
        private CurrentSettings currentSettings;
        
        private readonly CountdownTimer navigationTimer = new(0.25f);
        private readonly CountdownTimer settingsTimer = new(0.1f);
        
        private bool gameIsPaused;

        private void OnEnable() {
            if (InputsBrain.HasInstance) {
                InputsBrain.Instance.OnPause += OnEscapePressed;
                InputsBrain.Instance.OnBackBtt += Back;
                InputsBrain.Instance.OnSelectBtt += Select;
                InputsBrain.Instance.OnNavigation += Navigation;
                InputsBrain.Instance.OnSettingsView += SettingsView;
                InputsBrain.Instance.DisableUIInput(true);
            }
            
            CurrentMenu = MainMenuPanel;
            currentMenuType = UI.CurrentMenu.MainMenu;
            HoverButton(GetCurrentList()[currentIndex]);
        }

        private void OnDisable() {
            if (!InputsBrain.HasInstance) return;
            
            InputsBrain.Instance.OnPause -= OnEscapePressed;
            InputsBrain.Instance.OnBackBtt -= Back;
            InputsBrain.Instance.OnSelectBtt -= Select;
            InputsBrain.Instance.OnNavigation -= Navigation;
            InputsBrain.Instance.OnSettingsView -= SettingsView;
        }

        private void OnEscapePressed() {
            ChangeState();
        }

        public void ChangeState() {
            gameIsPaused = !gameIsPaused;
            Time.timeScale = gameIsPaused ? 0 : 1;

            InputsBrain.Instance.DisableUIInput(!gameIsPaused);
            InputsBrain.Instance.DisablePlayerInput(gameIsPaused);
            
            menuGroup.interactable = gameIsPaused;
            menuGroup.blocksRaycasts = gameIsPaused;
            menuGroup.DOFade(gameIsPaused ? 1 : 0, .3f).SetUpdate(true);
            
            inputsDisplay.FadeDisplay(gameIsPaused);

            menuAnimator.Play(gameIsPaused ? "A_PauseMenu_IN" : "A_PauseMenu_OUT");

            if (currentMenuType == UI.CurrentMenu.Settings) {
                CurrentMenu.Close();
                CurrentMenu = MainMenuPanel;
            }
            
            if (gameIsPaused) {
                foreach (var btt in buttons) {
                    btt.Enable();
                }
            }
        }

        public void LoadMenu() {
            InputsBrain.Instance.DisableUIInput(false);
            GameSceneLoaderSystem.Instance.LoadMenu();
        }
        
        public void UpdateCurrentMenu(MenuAnimation newMenu) {
            if(newMenu == null) return;
            
            UnHoverButton(GetCurrentList()[currentIndex]);
            
            CurrentMenu = newMenu;
            currentMenuType = newMenu.menuType;
            currentIndex = 0;
            settingsIndex = 0;
            
            HoverButton(GetCurrentList()[currentIndex]);
            
            inputsDisplay.UpdateDisplay(CurrentMenu == MainMenuPanel);
        }
        
        private void Back() {
            if(currentMenuType is UI.CurrentMenu.MainMenu) return;
            
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().ui_Back);
            
            UnHoverButton(GetCurrentList()[currentIndex]);
            
            CurrentMenu.Close();
            CurrentMenu.PreviousMenu.gameObject.SetActive(true);
            
            var previous = CurrentMenu.PreviousMenu;
            CurrentMenu = previous;
            currentMenuType = previous.menuType;
            
            currentIndex = 0;
            HoverButton(GetCurrentList()[currentIndex]);
            
            inputsDisplay.UpdateDisplay(CurrentMenu == MainMenuPanel);
        }

        private void Select() {
            ExecuteButtonScrip(GetCurrentList()[currentIndex]);
        }

        private void ExecuteButtonScrip(GameObject list) {
            if (list.TryGetComponent(out Toggle ui)) {
                ui.isOn = !ui.isOn;
            }
            else if (list.TryGetComponent(out IPointerDownHandler main)) {
                main.OnPointerDown(null);
            }
        }

        private void HoverButton(GameObject list) {
            if (list.TryGetComponent(out ToggleUI ui)) {
                ui.OnPointerEnter(null);
            }
            else if (list.TryGetComponent(out SliderUI sld)) {
                sld.OnPointerEnter(null);
            }
            else if (list.TryGetComponent(out IPointerEnterHandler pointer)) {
                pointer.OnPointerEnter(null);
            }
        }

        private void UnHoverButton(GameObject list) {
            if (list.TryGetComponent(out ToggleUI ui)) {
                ui.OnPointerExit(null);
            }
            else if (list.TryGetComponent(out SliderUI sld)) {
                sld.OnPointerExit(null);
            }
            else if (list.TryGetComponent(out IPointerExitHandler pointer)) {
                pointer.OnPointerExit(null);
            }
        }

        private void UpdateButton(GameObject list, bool add) {
            if (list.TryGetComponent(out DropDownUI dd)) {
                dd.UpdateIndex(add);
            }
            else if (list.TryGetComponent(out Slider sld)) {
                if(sld.wholeNumbers)
                    sld.value += add ? 1 : -1;
                else
                    sld.value += add ? 0.01f : -0.01f;
            }
        }

        private void Navigation(InputAction.CallbackContext ctx) {
            var dir = ctx.ReadValue<Vector2>();
            
            UpdateSettings(dir);
            if (!navigationTimer.IsRunning) {
                NavigateThroughButtons(dir);
            }
        }
        
        private void UpdateSettings(Vector2 dir) {
            if(currentMenuType is not UI.CurrentMenu.Settings) return;
            
            if (!settingsTimer.IsRunning) {
                if (dir.x < -0.5f) {
                    UpdateButton(GetCurrentList()[currentIndex], false);
                    settingsTimer.Start();
                }
                else if (dir.x > 0.5f) {
                    UpdateButton(GetCurrentList()[currentIndex], true);
                    settingsTimer.Start();
                }
            }
            
        }
        
        private void SettingsView(InputAction.CallbackContext ctx) {
            if(currentMenuType is not UI.CurrentMenu.Settings) return;
            
            var right = ctx.ReadValue<float>() > 0;

            UnHoverButton(settingsButtons[settingsIndex]);
            
            if (right) {
                settingsIndex++;
                if (settingsIndex > 3) {
                    settingsIndex = 0;
                }
            }
            else {
                settingsIndex--;
                if (settingsIndex < 0) {
                    settingsIndex = 3;
                }
            }

            currentSettings = settingsIndex switch {
                0 => CurrentSettings.Audio,
                1 => CurrentSettings.Video,
                2 => CurrentSettings.Access,
                3 => CurrentSettings.Input,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            ExecuteButtonScrip(settingsButtons[settingsIndex]);
            
            UnHoverButton(GetCurrentList()[currentIndex]);
            
            currentIndex = 0;
            HoverButton(GetCurrentList()[currentIndex]);
            
            HoverButton(settingsButtons[settingsIndex]);
        }

        private void NavigateThroughButtons(Vector2 dir) {
            if (dir.y > 0.5f) {
                UnHoverButton(GetCurrentList()[currentIndex]);
                
                // Up
                currentIndex--;

                if (currentIndex < 0) {
                    currentIndex = GetListLenght() - 1;
                }
                
                HoverButton(GetCurrentList()[currentIndex]);
                navigationTimer.Start();
            }
            else if (dir.y < -0.5f) {
                UnHoverButton(GetCurrentList()[currentIndex]);
                
                // Down
                currentIndex++;
                
                if (currentIndex > GetListLenght() - 1) {
                    currentIndex = 0;
                }
                
                HoverButton(GetCurrentList()[currentIndex]);
                navigationTimer.Start();
            }
        }
        
        private GameObject[] GetCurrentList() {
            return currentMenuType switch {
                UI.CurrentMenu.MainMenu => MainMenuButtons,
                UI.CurrentMenu.Settings => currentSettings switch {
                    CurrentSettings.Audio => audioButtons,
                    CurrentSettings.Video => videoButtons,
                    CurrentSettings.Input => inputButtons,
                    CurrentSettings.Access => accessButtons,
                    _ => throw new ArgumentOutOfRangeException()
                },
                //UI.CurrentMenu.Credits => C,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int GetListLenght() {
            return currentMenuType switch {
                UI.CurrentMenu.MainMenu => MainMenuButtons.Length,
                UI.CurrentMenu.Settings => currentSettings switch {
                    CurrentSettings.Audio => audioButtons.Length,
                    CurrentSettings.Video => videoButtons.Length,
                    CurrentSettings.Input => inputButtons.Length,
                    CurrentSettings.Access => accessButtons.Length,
                    _ => throw new ArgumentOutOfRangeException()
                },
                //UI.CurrentMenu.Credits => C,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
    }
}
