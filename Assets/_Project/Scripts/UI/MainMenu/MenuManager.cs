using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public enum CurrentMenu {
        MainMenu,
        Continue,
        Chapter,
        Settings,
        Credits
    }

    public enum CurrentSettings {
        Audio,
        Video,
        Input,
        Access
    }
    
    public class MenuManager : MonoBehaviour {
        [Header("General Button")]
        [SerializeField] private GameObject[] MainMenuButtons;
        [SerializeField] private GameObject[] ContinueButtons;
        [SerializeField] private GameObject[] ChapterButtons;
        
        [Header("Settings Buttons")]
        [SerializeField] private GameObject[] settingsButtons;
        [SerializeField] private GameObject[] audioButtons;
        [SerializeField] private GameObject[] videoButtons;
        [SerializeField] private GameObject[] inputButtons;
        [SerializeField] private GameObject[] accessButtons;
        
        [Header("Camera Ref")]
        [SerializeField] private Animation animatedCamera;

        [Header("Button Ref")]
        [SerializeField] private GameObject loadGameBtt;
        
        [Header("Current Panel")]
        [SerializeField] private MenuAnimation MainMenuPanel;
        [SerializeField] private InputDisplay inputsDisplay;
        private MenuAnimation CurrentMenu;
        
        private int currentIndex = 0;
        private int settingsIndex = 0;
        
        private CurrentMenu currentMenuType;
        private CurrentSettings currentSettings;
        
        private readonly CountdownTimer navigationTimer = new(0.25f);
        private readonly CountdownTimer settingsTimer = new(0.1f);
        
        [Header("UI Color")]
        [SerializeField] private Color act1Color;
        [SerializeField] private Color act2Color;
        [SerializeField] private Color act3Color;
        [SerializeField] private SetUIColor uiColor;
        
        public int ChapterIndex { get; private set;}
        
        private void Awake() {
            ChapterIndex = 1;
            if (GameInitializer.HasInstance) {
                loadGameBtt.SetActive(GameInitializer.Instance.ExistingSave());
                ChapterIndex = GameInitializer.Instance.GetLastChapter();
                GameInitializer.Instance.SetCurrentChapter(ChapterIndex);
            }

            switch (ChapterIndex) {
                case 1:
                    uiColor.SetSpriteColor(act1Color);
                    break;
                case 2:
                    uiColor.SetSpriteColor(act2Color);
                    break;
                case 3:
                    uiColor.SetSpriteColor(act3Color);
                    break;
            }

            if (InputsBrain.HasInstance) {
                InputsBrain.Instance.OnBackBtt += Back;
                InputsBrain.Instance.OnSelectBtt += Select;
                InputsBrain.Instance.OnNavigation += Navigation;
                InputsBrain.Instance.OnSettingsView += SettingsView;
            }
            
            CurrentMenu = MainMenuPanel;
            currentMenuType = UI.CurrentMenu.MainMenu;
            HoverButton(GetCurrentList()[currentIndex]);
        }

        private void OnDisable() { 
            if (!InputsBrain.HasInstance) return;
            InputsBrain.Instance.OnBackBtt -= Back;
            InputsBrain.Instance.OnSelectBtt -= Select;
            InputsBrain.Instance.OnNavigation -= Navigation;
            InputsBrain.Instance.OnSettingsView -= SettingsView;
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
                if (currentMenuType is UI.CurrentMenu.Chapter) {
                    NavigateThroughChapter(dir);
                    return;
                }
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

        private void NavigateThroughChapter(Vector2 dir) {
            UnHoverButton(GetCurrentList()[currentIndex]);
            
            // Only for chapter
            if (dir.x < -0.25f) {
                currentIndex--;

                if (currentIndex < 0) {
                    currentIndex = ChapterButtons.Length - 1;
                }
            
                HoverButton(GetCurrentList()[currentIndex]);
                navigationTimer.Start();
            }
            else if (dir.x > 0.25f) {
                currentIndex++;
            
                if (currentIndex > ChapterButtons.Length - 1) {
                    currentIndex = 0;
                }
            
                HoverButton(GetCurrentList()[currentIndex]);
                navigationTimer.Start();
            }
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

        private GameObject[] GetCurrentList() {
            return currentMenuType switch {
                UI.CurrentMenu.MainMenu => MainMenuButtons,
                UI.CurrentMenu.Continue => ContinueButtons,
                UI.CurrentMenu.Chapter => ChapterButtons,
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
                UI.CurrentMenu.Continue => ContinueButtons.Length,
                UI.CurrentMenu.Chapter => ChapterButtons.Length,
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
        
        public void ChangeTarget(string anim) {
            animatedCamera.Play(anim);
        }

        public void QuitGame() {
            Application.Quit();
        }
        
        public void NewGame() {
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().ui_Play);
            GameSceneLoaderSystem.Instance.NewGame();
        }

        public void LoadGame() {
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().ui_Play);
            GameSceneLoaderSystem.Instance.LoadGame();
        }

        public void LoadLevel(int levelIndex) {
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().ui_Play);
            GameSceneLoaderSystem.Instance.LoadLevel(levelIndex);
        }
    }
}
