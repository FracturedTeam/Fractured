using System;
using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class SettingsManager : MonoBehaviour{
        
        [Header("Display Settings")]
        [SerializeField] private DropDownUI fullscreenDropDown;
        [SerializeField] private DropDownUI resolutionDropDown;
        [SerializeField] private Toggle vSync;
        [SerializeField] private Toggle depthOfField;
        [SerializeField] private Toggle chromaticAberration;
        
        private Resolution[] allResolutions;

        private SettingData settingData;
        
        void Start() {
            if (GameInitializer.HasInstance)
                settingData = GameInitializer.Instance.GetSettings;
            
            InitResolutionsDropdown();
            InitFullscreenDropdown();
            InitVSyncToggle();
            InitChromaticToggle();
            InitDOFToggle();
        }

        #region Initialization

        private void InitResolutionsDropdown() {
            resolutionDropDown.ClearOptions();
            
            var options = new List<string>();
            
            var savedIndex = settingData?.screenResolutionIndex ?? -1;
            var currentIndex = savedIndex;
            
            // Determine the highest resolution
            string highestRes = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
            switch (highestRes) {
                case "1280x720":
                    options.Clear();
                    options.Add("800x600");
                    options.Add("1280x720");
                    if (savedIndex == -1)
                        currentIndex = 1;

                    allResolutions = new Resolution[2];
                    allResolutions[0].width = 800;
                    allResolutions[0].height = 600;
                    allResolutions[1].width = 1280;
                    allResolutions[1].height = 720;
                    
                    break;
                case "1366x768":
                    options.Clear();
                    options.Add("800x600");
                    options.Add("1280x720");
                    options.Add("1366x768");
                    if (savedIndex == -1)
                        currentIndex = 2;
                    
                    allResolutions = new Resolution[3];
                    allResolutions[0].width = 800;
                    allResolutions[0].height = 600;
                    allResolutions[1].width = 1280;
                    allResolutions[1].height = 720;
                    allResolutions[2].width = 1366;
                    allResolutions[2].height = 768;
                    
                    break;
                case "1440x900":
                    options.Clear();
                    options.Add("800x600");
                    options.Add("1280x720");
                    options.Add("1366x768");
                    options.Add("1440x900");
                    if (savedIndex == -1)
                        currentIndex = 3;
                    
                    allResolutions = new Resolution[4];
                    allResolutions[0].width = 800;
                    allResolutions[0].height = 600;
                    allResolutions[1].width = 1280;
                    allResolutions[1].height = 720;
                    allResolutions[2].width = 1366;
                    allResolutions[2].height = 768;
                    allResolutions[3].width = 1440;
                    allResolutions[3].height = 900;
                    
                    break;
                case "1920x1080":
                    options.Clear();
                    options.Add("1280x720");
                    options.Add("1366x768");
                    options.Add("1440x900");
                    options.Add("1920x1080");
                    if (savedIndex == -1)
                        currentIndex = 3;
                    
                    allResolutions = new Resolution[4];
                    allResolutions[0].width = 1280;
                    allResolutions[0].height = 720;
                    allResolutions[1].width = 1366;
                    allResolutions[1].height = 768;
                    allResolutions[2].width = 1440;
                    allResolutions[2].height = 900;
                    allResolutions[3].width = 1920;
                    allResolutions[3].height = 1080;
                    
                    break;
                case "2560x1440":
                    options.Clear();
                    options.Add("1280x720");
                    options.Add("1366x768");
                    options.Add("1440x900");
                    options.Add("1920x1080");
                    options.Add("2560x1440");
                    if (savedIndex == -1)
                        currentIndex = 4;
                    
                    allResolutions = new Resolution[5];
                    allResolutions[0].width = 1280;
                    allResolutions[0].height = 720;
                    allResolutions[1].width = 1366;
                    allResolutions[1].height = 768;
                    allResolutions[2].width = 1440;
                    allResolutions[2].height = 900;
                    allResolutions[3].width = 1920;
                    allResolutions[3].height = 1080;
                    allResolutions[4].width = 2560;
                    allResolutions[4].height = 1440;
                    
                    break;
                default:
                    options.Clear();
                    options.Add("1280x720");
                    options.Add("1366x768");
                    options.Add("1440x900");
                    options.Add("1920x1080");
                    options.Add("2560x1440");
                    options.Add(Screen.currentResolution.width + "x" + Screen.currentResolution.height);
                    if (savedIndex == -1)
                        currentIndex = 5;
                    
                    allResolutions = new Resolution[6];
                    allResolutions[0].width = 1280;
                    allResolutions[0].height = 720;
                    allResolutions[1].width = 1366;
                    allResolutions[1].height = 768;
                    allResolutions[2].width = 1440;
                    allResolutions[2].height = 900;
                    allResolutions[3].width = 1920;
                    allResolutions[3].height = 1080;
                    allResolutions[4].width = 2560;
                    allResolutions[4].height = 1440;
                    allResolutions[5].width = Screen.currentResolution.width;
                    allResolutions[5].height = Screen.currentResolution.height;
                    
                    break;
            }
            
            resolutionDropDown.AddOptions(options);
            resolutionDropDown.value = currentIndex;
            resolutionDropDown.OnValueChanged += OnResolutionChanged;
            resolutionDropDown.RefreshShownValue();
        }

        private void InitFullscreenDropdown() {
            fullscreenDropDown.ClearOptions();
            fullscreenDropDown.AddOptions(new List<string> { 
                "Exclusive Fullscreen",
                "Fullscreen Window",
                "Maximized Window",
                "Windowed"
            });

            var saved = settingData?.fullScreenMode ?? 0;
            fullscreenDropDown.value = saved;
            fullscreenDropDown.OnValueChanged += OnFullscreenChanged;
            fullscreenDropDown.RefreshShownValue();
        }

        void InitVSyncToggle() {
            var saved = settingData?.vSyncEnabled ?? true;
            vSync.isOn = saved;
            QualitySettings.vSyncCount = saved ? 1 : 0;
            vSync.onValueChanged.AddListener(OnVSyncChanged);
        }
        
        void InitDOFToggle() {
            var saved = settingData?.dof ?? true;
            GameInitializer.Instance.GetVolumeProfile().TryGet(out DepthOfField dof);
            depthOfField.isOn = saved;
            if(dof)
                dof.active = saved;
            depthOfField.onValueChanged.AddListener(OnDofChanged);
        }
        
        void InitChromaticToggle() {
            var saved = settingData?.chromaticAberration ?? true;
            GameInitializer.Instance.GetVolumeProfile().TryGet(out ChromaticAberration chroma);
            chromaticAberration.isOn = saved;
            if(chroma)
                chroma.active = saved;
            chromaticAberration.onValueChanged.AddListener(OnChromaChanged);
        }

        #endregion

        #region Callbacks

        private void OnResolutionChanged(int index) {
            var res = allResolutions[index];
            Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
            
            settingData.screenResolutionIndex = index;
            GameInitializer.Instance.SaveSettings();
        }
    
        private void OnFullscreenChanged(int index) {
            Screen.fullScreenMode = (FullScreenMode)index;
            
            settingData.fullScreenMode = index;
            GameInitializer.Instance.SaveSettings();
        }

        private void OnVSyncChanged(bool enable) {
            QualitySettings.vSyncCount = enable ? 1 : 0;
            
            settingData.vSyncEnabled = enable;
            GameInitializer.Instance.SaveSettings();
        }

        private void OnDofChanged(bool enable) {
            GameInitializer.Instance.GetVolumeProfile().TryGet(out DepthOfField dof);
            if(dof) dof.active = enable;
            settingData.dof = enable;
            
            GameInitializer.Instance.SaveSettings();
        }
        
        private void OnChromaChanged(bool enable) {
            GameInitializer.Instance.GetVolumeProfile().TryGet(out ChromaticAberration chroma);
            if(chroma) chroma.active = enable;
            settingData.chromaticAberration = enable;
            
            GameInitializer.Instance.SaveSettings();
        }
        #endregion
    }
}