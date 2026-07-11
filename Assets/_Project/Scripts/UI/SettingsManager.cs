using System;
using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class SettingsManager : MonoBehaviour{
        
        [Header("Display Settings")]
        [SerializeField] private TMP_Dropdown fullscreenDropDown;
        [SerializeField] private TMP_Dropdown resolutionDropDown;
        [SerializeField] private Toggle vSync;
        
        private Resolution[] allResolutions;

        private SettingData settingData;
        
        void Start() {
            if(GameInitializer.HasInstance) settingData = GameInitializer.Instance.GetSettings;
            
            InitResolutionsDropdown();
            InitFullscreenDropdown();
            InitVSyncToggle();
        }

        #region Initialization

        private void InitResolutionsDropdown() {
            
            Debug.Log("Has Settings Loaded // SETTINGS MANAGER");
            
            resolutionDropDown.ClearOptions();
            allResolutions = Screen.resolutions;
            
            var options = new List<string>();
            var savedIndex = settingData.ScreenResolutionIndex;
            var currentIndex = savedIndex;

            for (var i = 0; i < allResolutions.Length; i++) {
                options.Add(allResolutions[i].width + "x" + allResolutions[i].height);
                if (savedIndex == -1) {
                    var r = allResolutions[i];
                    if (r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height) {
                        currentIndex = i;
                    }
                }
            }
            
            resolutionDropDown.AddOptions(options);
            resolutionDropDown.value = currentIndex;
            resolutionDropDown.RefreshShownValue();
            resolutionDropDown.onValueChanged.AddListener(OnResolutionChanged);
        }

        private void InitFullscreenDropdown() {
            fullscreenDropDown.ClearOptions();
            fullscreenDropDown.AddOptions(new List<string> { 
                "Exclusive Fullscreen",
                "Fullscreen Window",
                "Maximized Window",
                "Windowed"
            });

            var saved = settingData.FullScreenMode;
            fullscreenDropDown.value = saved;
            fullscreenDropDown.RefreshShownValue();
            fullscreenDropDown.onValueChanged.AddListener(OnFullscreenChanged);
        }

        void InitVSyncToggle() {
            var saved = settingData.vSyncEnabled;
            vSync.isOn = saved;
            QualitySettings.vSyncCount = saved ? 1 : 0;
            vSync.onValueChanged.AddListener(OnVSyncChanged);
        }

        #endregion

        #region Callbacks

        private void OnResolutionChanged(int index) {
            var res = allResolutions[index];
            Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
            
            settingData.ScreenResolutionIndex = index;
            GameInitializer.Instance.SaveSettings();
        }
    
        private void OnFullscreenChanged(int index) {
            Screen.fullScreenMode = (FullScreenMode)index;
            
            settingData.FullScreenMode = index;
            GameInitializer.Instance.SaveSettings();
        }

        private void OnVSyncChanged(bool enable) {
            QualitySettings.vSyncCount = enable ? 1 : 0;
            
            settingData.vSyncEnabled = enable;
            GameInitializer.Instance.SaveSettings();
        }

        #endregion
    }
}