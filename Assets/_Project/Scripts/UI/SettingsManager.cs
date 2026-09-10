using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class SettingsManager : MonoBehaviour{
        
        [Header("Display Settings")]
        [SerializeField] private DropDownUI fullscreenDropDown;
        [SerializeField] private DropDownUI resolutionDropDown;
        [SerializeField] private DropDownUI qualityDropDown;
        [SerializeField] private Toggle vSync;
        [SerializeField] private Toggle chromaticAberration;
        
        [Header("Accessibility")]
        [SerializeField] private DropDownUI enviroColorDropDown;
        [SerializeField] private DropDownUI uiColorDropDown;
        
        private Resolution[] allResolutions;

        private SettingData settingData;
        
        void Start() {
            if (!GameInitializer.HasInstance) return;
            
            settingData = GameInitializer.Instance.GetSettings;
            
            InitResolutionsDropdown();
            InitFullscreenDropdown();
            InitQualityDropdown();
            InitVSyncToggle();
            InitChromaticToggle();
            
            InitEnviroColorIntensityDropdown();
            InitUIColorIntensityDropdown();
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
                "Windowed",
                "Exclusive Fullscreen"
            });

            var saved = settingData?.fullScreenMode ?? 1;
            fullscreenDropDown.value = saved;
            fullscreenDropDown.OnValueChanged += OnFullscreenChanged;
            fullscreenDropDown.RefreshShownValue();
        }

        private void InitQualityDropdown() {
            qualityDropDown.ClearOptions();
            qualityDropDown.AddOptions(new List<string> {
                "Low",
                "Medium",
                "High",
                "Ultra"
            });

            var saved = settingData?.quality ?? 3;
            qualityDropDown.value = saved;
            qualityDropDown.OnValueChanged += OnQualityChanged;
            qualityDropDown.RefreshShownValue();
        }

        private void InitEnviroColorIntensityDropdown() {
            enviroColorDropDown.ClearOptions();
            enviroColorDropDown.AddOptions(new List<string> {
                "0%",
                "25%",
                "50%",
                "100%",
            });
            
            var saved = settingData?.enviroColorIntensity ?? 1;

            var value = saved switch {
                1f => 0,
                .5f => 1,
                .25f => 2,
                0f => 3
            };
            
            enviroColorDropDown.value = value;
            enviroColorDropDown.OnValueChanged += OnEnviroColorChanged;
            enviroColorDropDown.RefreshShownValue();
        }

        private void InitUIColorIntensityDropdown() {
            uiColorDropDown.ClearOptions();
            uiColorDropDown.AddOptions(new List<string> {
                "0%",
                "25%",
                "50%",
                "100%",
            });
            
            var saved = settingData?.uiColorIntensity ?? 1;

            var value = saved switch {
                1f => 3,
                .5f => 2,
                .25f => 1,
                0f => 0
            };
            
            uiColorDropDown.value = value;
            uiColorDropDown.OnValueChanged += OnUIColorChanged;
            uiColorDropDown.RefreshShownValue();
        }
        
        void InitVSyncToggle() {
            var saved = settingData?.vSyncEnabled ?? true;
            vSync.isOn = saved;
            QualitySettings.vSyncCount = saved ? 1 : 0;
            vSync.onValueChanged.AddListener(OnVSyncChanged);
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
            Screen.fullScreenMode = index == 0 ? FullScreenMode.Windowed : FullScreenMode.ExclusiveFullScreen;
            
            settingData.fullScreenMode = index;
            GameInitializer.Instance.SaveSettings();
        }
        
        private void OnQualityChanged(int index) {
            QualitySettings.SetQualityLevel(index, true);
            
            settingData.quality = index;
            GameInitializer.Instance.SaveSettings();
        }

        private void OnVSyncChanged(bool enable) {
            QualitySettings.vSyncCount = enable ? 1 : 0;
            
            settingData.vSyncEnabled = enable;
            GameInitializer.Instance.SaveSettings();
        }
        
        private void OnChromaChanged(bool enable) {
            GameInitializer.Instance.GetVolumeProfile().TryGet(out ChromaticAberration chroma);
            if(chroma) chroma.active = enable;
            settingData.chromaticAberration = enable;
            
            GameInitializer.Instance.SaveSettings();
        }

        private void OnEnviroColorChanged(int index) {
            var value = index switch {
                3 => 0f,
                2 => 0.25f,
                1 => 0.5f,
                0 => 1f,
            };
            
            settingData.enviroColorIntensity = value;
            GameInitializer.Instance.AdjustEnviroColorIntensity(value);
            GameInitializer.Instance.SaveSettings();
        }
        
        private void OnUIColorChanged(int index) {
            var value = index switch {
                0 => 0f,
                1 => 0.25f,
                2 => 0.5f,
                3 => 1f,
            };
            
            settingData.uiColorIntensity = value;
            GameInitializer.Instance.AdjustUIColorIntensity(value);
            GameInitializer.Instance.SaveSettings();
        }
        
        #endregion
    }
}