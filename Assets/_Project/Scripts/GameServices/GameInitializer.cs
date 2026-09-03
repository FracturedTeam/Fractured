using System;
using _Project.Scripts.DebugSystems;
using _Project.Scripts.DebugSystems.Services;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Player.Camera;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.UI;
using FMOD.Studio;
using FMODUnity;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        //SYSTEM REGISTRY SERVICE
        private GameSystems gameSystems;
        
        //INTERNAL SERVICES
        public ShardService shardService {get; private set;}
        public SaveService saveService {get; private set;}
        public AudioService audioService {get; private set;}
        public RumbleService rumbleService {get; private set;}
        
        [Header("Save service")] 
        [SerializeField] private bool deleteSaveOnPay;
        
        [Header("Audio Bank")]
        [SerializeField] private AudioBank audioBank;
        
        [Header("PostProcess")]
        [SerializeField] private VolumeProfile postProcess;

        [FormerlySerializedAs("chapter1A")]
        [Header("Shard Materials")] 
        [SerializeField] private Material shardMaterialA;
        [SerializeField] private Material shardMaterialB;
        
        [Header("Text Colors")] 
        [SerializeField] private ColorTextProfile chapter1TextProfile;
        [SerializeField] private ColorTextProfile chapter2TextProfile;
        [SerializeField] private ColorTextProfile chapter3TextProfile;

        [Header("Gamepad Color Settings")]
        [SerializeField] private Color chapter1Color;
        [SerializeField] private Color chapter2Color;
        [SerializeField] private Color chapter3Color;
        
        [Header("Floor Material for sound")]
        [SerializeField] private Material woodFloor;
        [SerializeField] private Material tileFloor1;
        [SerializeField] private Material tileFloor2;
        [SerializeField] private Material tileFloor3;
        [SerializeField] private Material carpetFloor;
        
        [Header("Color Profile")]
        [SerializeField] private Profil colorProfile;
        
        public int CurrentChapter {get; private set;}
        public ColorTextProfile currentTextColors;
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        [SerializeField] private DebugSystemInitializer debugSystemInitializer;
        [SerializeField] private bool initializeDebugger = true;
        private ShardDebugService shardDebugService;
        private CameraDebugService cameraDebugService;
        #endif
        
        private Vector3 lastStepPosition;
        
        protected override void Awake() {
            base.Awake();
            
            SetColorProfile();
            InitializeGameSystems();
        }

        private void InitializeGameSystems() {
            Debug.Log("Initializing Game Systems");
            //Create all the game systems
            gameSystems = new GameSystems(); //First one to be created as it is the one that handle all the game services
            shardService = new ShardService();
            saveService = new SaveService(shardService, deleteSaveOnPay);
            audioService = new AudioService(audioBank);
            rumbleService = new RumbleService(Gamepad.current);
            
            //Then register the game systems
            gameSystems.Register(shardService);
            gameSystems.Register(saveService);
            gameSystems.Register(audioService);
            gameSystems.Register(rumbleService);
            
            //Then initialize the services (act as the awake method)
            gameSystems.Initialize();
        }
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        public void InitializeDebugSystems() {
            if(!initializeDebugger) return;
            
            var debugUIState = new DebugUIState();
            var debugSystem = new DebugSystem();
            
            //Add every debug services
            var debugInputService = new DebugInputService(debugUIState);
            debugSystem.Register(debugInputService);

            var playerDebugService = new PlayerDebugService(debugUIState);
            debugSystem.Register(playerDebugService);

            var scenes = GetScenes();
            var frameMaster = FindAnyObjectByType<MemoryFrameMaster>();
            shardDebugService = new ShardDebugService(shardService,  debugUIState, scenes, frameMaster);
            debugSystem.Register(shardDebugService);

            cameraDebugService = new CameraDebugService(debugUIState, GetCameras());
            debugSystem.Register(cameraDebugService);
            
            var generalDebug =  new GeneralDebugService(debugUIState, saveService);
            debugSystem.Register(generalDebug);

            //Set the debug system
            debugSystemInitializer.SetDebugSystem(debugSystem);
        }
        
        [ContextMenu("ReloadColors 1")] void ReloadColors1() =>   currentTextColors = chapter1TextProfile;
        [ContextMenu("ReloadColors 2")] void ReloadColors2() =>   currentTextColors = chapter2TextProfile;
        [ContextMenu("ReloadColors 3")] void ReloadColors3() =>   currentTextColors = chapter3TextProfile;
        
#endif
        
        private void Update() {
            gameSystems.Tick();
        }
        
        private void OnDisable() {
            gameSystems.Dispose();
        }

        private CinemachineCamera[] GetCameras() {
            return FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        }
        
        private SceneMaster[] GetScenes() {
            return FindObjectsByType<SceneMaster>(FindObjectsSortMode.None);
        }

        public void ResetCameras() {
            var cam = GetCameras();
            foreach (var c in cam) {
                c.Priority = 0;
            }
        }

        public void SetCurrentChapter(int index) {
            CurrentChapter = index;

            Shader.SetGlobalFloat("_CurrentAct", CurrentChapter);
            Shader.SetGlobalFloat("_ActGlobalTransition", saveService.SettingData.enviroColorIntensity);
            
            currentTextColors = index switch
            {
                1 => chapter1TextProfile,
                2 => chapter2TextProfile,
                3 => chapter3TextProfile,
                _ => chapter1TextProfile
            };
            
            AdjustUIColorIntensity(saveService.SettingData.uiColorIntensity);
            if(GameSceneSettings.HasInstance) GameSceneSettings.Instance.ForceSetInteractableColor();
        }

        public void AdjustEnviroColorIntensity(float value) {
            Shader.SetGlobalFloat("_ActGlobalTransition", value);
            if (GameSceneSettings.HasInstance) {
                GameSceneSettings.Instance.UpdateVolumeWeight(value);
            }
        }
        
        public void AdjustUIColorIntensity(float value) {
            if (HudManager.HasInstance) {
                HudManager.Instance.UpdateUIColor(CurrentChapter, value);
                return;
            }

            var menu = FindAnyObjectByType<MenuManager>();
            if (menu != null) {
                menu.UpdateUIColor(value);
            }
        }
        
        #region SaveService

        public void SaveData() => saveService.SaveData();
        public void LoadData() => saveService.LoadData();
        public void LoadPlayerData() => saveService.LoadPlayerData();
        public void LoadGame() => saveService.LoadGame();
        public string GetLastScene() => saveService.GameData.CurrentScene;

        public int GetLastChapter() {
            if (ExistingSave()) {
                LoadGame();
                return saveService.GameData.CurrentChapter;
            }
            
            return 1;
        }

        public bool ExistingSave() => saveService.ExistingSave();
        public void CreateNewSave() => saveService.NewGame();
        public void LoadSettings() => saveService.LoadSettings();
        public void SaveSettings() => saveService.SaveSettings();
        public SettingData GetSettings => saveService.SettingData;

        #endregion

        #region ShardService

        public void DisposeShards() {
            shardService.ClearAll();
        }

        public void EmptyAll() {
            shardService.stopUpdate = true;
            shardService.interactables.Clear();
            shardService.sceneMasters.Clear();
            EmptyShards();
        }

        public void EmptyShards() {
            for (int i = shardService.shards.Count - 1; i >= 0; i--) {
                Destroy(shardService.shards[i].gameObject);
                shardService.shards.RemoveAt(i);
            }
            
            if(HudManager.HasInstance) HudManager.Instance.SetGlass(false);
        }

        public void PopulateLevel(BaseObject[] baseObjects, SceneMaster[] scenes) {
            shardService.RepopulateBaseObjet(baseObjects, scenes);
            var camSwitches = FindObjectsByType<CameraControlTrigger>(FindObjectsSortMode.None);
            foreach (var cam in camSwitches) {
                cam.Initialize();
            }
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(!initializeDebugger) return;
            var frameMaster = FindAnyObjectByType<MemoryFrameMaster>();
            shardDebugService.UpdateSceneDebug(scenes, frameMaster);
            #endif
            
            shardService.stopUpdate = false;
        }
        
        public BaseObject[] GetInteractable() {
            return shardService.interactables.ToArray();
        }
        
        public void AddShards(Glass[] shards)
        {
            foreach (var shard in shards) {
                var s = Instantiate(shard, HudManager.Instance.glassHolder);
                shardService.AddShards(s, shard.GetColor is ColorEnum.ColorA);
            }
            
            saveService.SetRuntimeShard(shardService.shards);
            if(HudManager.HasInstance) HudManager.Instance.SetGlass(true);
        }
        
        public void ResetAllInteractable() {
            EmptyShards();
            foreach (var interactable in shardService.interactables)
                interactable.ResetInteract();
        }

        public void ResetGlassInteractable() {
            foreach (var interact in shardService.interactables) {
                interact.GetGlassInteract?.ResetObject();
            }
        }

        public void RepositionGlass() {
            foreach (var shard in shardService.shards) {
                shard.Set3DShard();
            }
        }
        
        public void SetShardsOnOff(bool isOn) {
            foreach (var shard in shardService.shards)
                shard.SetUp3dShard(isOn);
        }
        
        public Material GetCurrentFragmentMaterial(bool isA) {
            return isA ? shardMaterialA : shardMaterialB;
        }
        
        #endregion

        #region AudioService

        public AudioBank GetBank() {
            return audioBank;
        }

        public EventInstance CreateInstance(EventReference reference) {
            return audioService.CreateInstance(reference);
        }
        
        public void PlaySound3D(EventReference audioClip, Vector3 position) {
            audioService.PlayOneShot3D(audioClip, position);
        }

        public void PlayPlayerFootstep(Vector3 position) {
            lastStepPosition = position;
            Physics.Raycast(position + new Vector3(0,0.2f,0), -Vector3.up, out var hit, 3f);
            
            if (!hit.collider) {
                audioService.PlayOneShot3D(GetBank().avatar_Walking_Neutral, position);
                return;
            }

            if (hit.collider.TryGetComponent(out MeshRenderer render)) {
                var matName = render.materials[0].name;
                
                if (matName.Contains(woodFloor.name)) {
                    audioService.PlayOneShot3D(GetBank().avatar_Walking_Wood, position);
                }
                else if (matName.Contains(tileFloor1.name) || matName.Contains(tileFloor2.name) || matName.Contains(tileFloor3.name)) {
                    audioService.PlayOneShot3D(GetBank().avatar_Walking_Tile, position);
                }
                else if (matName.Contains(carpetFloor.name)) {
                    audioService.PlayOneShot3D(GetBank().avatar_Walking_Carpet, position);
                }
                else {
                    audioService.PlayOneShot3D(GetBank().avatar_Walking_Neutral, position);
                }
            }
            else {
                audioService.PlayOneShot3D(GetBank().avatar_Walking_Neutral, position);
            }
        }
        
        public void PlaySound2D(EventReference audioClip) {
            audioService.PlayOneShot2D(audioClip);
        }

        public void PlayHideSound(Vector3 position) {
            audioService.PlayHideObjectSound(position);
        }

        public void PlayShardMoving(bool doPlay) {
            audioService.PlayMovingShardLoop(doPlay);
        }
        
        public void UpdateAmbientLoop(int sceneIndex) {
            audioService.UpdateAmbientLoop(sceneIndex);
        }

        public void SetMemoryLoop(bool inMemory) {
            audioService.UpdateMemory(inMemory);
        }
        
        public float GetVolume(int index)
        {
            return index switch
            {
                0 => GetSettings.mainVolume,
                1 => GetSettings.sfxVolume,
                2 => GetSettings.musicVolume,
                _ => 0
            };
        }

        public void SetVolume(int index, float volume) {
            audioService.SetSound(index, volume);
        }
        #endregion

        #region GamePad GetColor
        
        public Color Pad_GetCurrentChapterColor() {
            return CurrentChapter switch {
                1 => chapter1Color,
                2 => chapter2Color,
                3 => chapter3Color,
                _ => Color.white
            };
        }

        #endregion
        
        public void UpdateDebugCameras() {
            cameraDebugService.UpdateCameras(GetCameras());
        }
        
        public int GetPostProcess(int index) {
            return index switch {
                0 => GetSettings.brightness,
                1 => GetSettings.contrast,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
        }

        public ColorAdjustments GetColorAdjustments() {
            postProcess.TryGet(out ColorAdjustments colorAdjustments);
            return colorAdjustments;
        }

        public VolumeProfile GetVolumeProfile() {
            return postProcess;
        }
        
        public void SetPostProcess(int index, int value) {
            postProcess.TryGet(out ColorAdjustments color);

            switch (index) {
                case 0:
                    color.postExposure.value = value;
                    GetSettings.brightness = value;
                    break;
                case 1:
                    color.contrast.value = value;
                    GetSettings.contrast = value;
                    break;
            }
            
            SaveSettings();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(lastStepPosition + new Vector3(0,0.2f,0), lastStepPosition + new Vector3(0,0.2f,0) + -Vector3.up * 3f);
        }
        
        private void SetColorProfile() { 
            Shader.SetGlobalFloat("_ActGlobalTransition", colorProfile.transition);
            Shader.SetGlobalFloat("_CurrentAct", colorProfile.act);

            Shader.SetGlobalColor("_ACT1_Color_A", colorProfile.act1_Color_A);
            Shader.SetGlobalVector("_ACT1_Color_A_Location", colorProfile.act1_Color_A_Location);
            Shader.SetGlobalColor("_ACT1_Color_B", colorProfile.act1_Color_B);
            Shader.SetGlobalVector("_ACT1_Color_B_Location", colorProfile.act1_Color_B_Location);
            Shader.SetGlobalColor("_ACT1_Color_C", colorProfile.act1_Color_C);

            Shader.SetGlobalColor("_ACT2_Color_A", colorProfile.act2_Color_A);
            Shader.SetGlobalVector("_ACT2_Color_A_Location", colorProfile.act2_Color_A_Location);
            Shader.SetGlobalColor("_ACT2_Color_B", colorProfile.act2_Color_B);
            Shader.SetGlobalVector("_ACT2_Color_B_Location", colorProfile.act2_Color_B_Location);
            Shader.SetGlobalColor("_ACT2_Color_C", colorProfile.act2_Color_C);

            Shader.SetGlobalColor("_ACT3_Color_A", colorProfile.act3_Color_A);
            Shader.SetGlobalVector("_ACT3_Color_A_Location", colorProfile.act3_Color_A_Location);
            Shader.SetGlobalColor("_ACT3_Color_B", colorProfile.act3_Color_B);
            Shader.SetGlobalVector("_ACT3_Color_B_Location", colorProfile.act3_Color_B_Location);
            Shader.SetGlobalColor("_ACT3_Color_C", colorProfile.act3_Color_C);
            
            Shader.SetGlobalColor("_FRAG_ACT1_A_BaseColor", colorProfile.FRAG_ACT1_A_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT1_A_HighlightColor", colorProfile.FRAG_ACT1_A_HighlightColor);
            Shader.SetGlobalColor("_FRAG_ACT1_B_BaseColor", colorProfile.FRAG_ACT1_B_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT1_B_HighlightColor", colorProfile.FRAG_ACT1_B_HighlightColor);
            Shader.SetGlobalColor("_FRAG_ACT1_AB_BaseColor", colorProfile.FRAG_ACT1_AB_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT1_AB_HighlightColor", colorProfile.FRAG_ACT1_AB_HighlightColor);

            Shader.SetGlobalColor("_FRAG_ACT2_A_BaseColor", colorProfile.FRAG_ACT2_A_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT2_A_HighlightColor", colorProfile.FRAG_ACT2_A_HighlightColor);
            Shader.SetGlobalColor("_FRAG_ACT2_B_BaseColor", colorProfile.FRAG_ACT2_B_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT2_B_HighlightColor", colorProfile.FRAG_ACT2_B_HighlightColor);
            Shader.SetGlobalColor("_FRAG_ACT2_AB_BaseColor", colorProfile.FRAG_ACT2_AB_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT2_AB_HighlightColor", colorProfile.FRAG_ACT2_AB_HighlightColor);

            Shader.SetGlobalColor("_FRAG_ACT3_A_BaseColor", colorProfile.FRAG_ACT3_A_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT3_A_HighlightColor", colorProfile.FRAG_ACT3_A_HighlightColor);
            Shader.SetGlobalColor("_FRAG_ACT3_B_BaseColor", colorProfile.FRAG_ACT3_B_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT3_B_HighlightColor", colorProfile.FRAG_ACT3_B_HighlightColor);
            Shader.SetGlobalColor("_FRAG_ACT3_AB_BaseColor", colorProfile.FRAG_ACT3_AB_BaseColor);
            Shader.SetGlobalColor("_FRAG_ACT3_AB_HighlightColor", colorProfile.FRAG_ACT3_AB_HighlightColor);
        }
        
        [Serializable]
        public struct ColorTextProfile
        {
            public Color colorA;
            public Color colorB;
            public Color colorAB;
        }
    }
}