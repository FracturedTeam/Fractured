using System;
using _Project.Scripts.DebugSystems;
using _Project.Scripts.DebugSystems.Services;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Player;
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

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        //SYSTEM REGISTRY SERVICE
        private GameSystems gameSystems;
        
        //INTERNAL SERVICES
        private ShardService shardService;
        private SaveService saveService;
        private AudioService audioService;
        private RumbleService rumbleService;
        
        [Header("Save service")] 
        [SerializeField] private bool deleteSaveOnPay;
        
        [Header("Audio Bank")]
        [SerializeField] private AudioBank audioBank;
        
        [Header("PostProcess")]
        [SerializeField] private VolumeProfile postProcess;

        [Header("Shard Materials")] 
        [SerializeField] private Material chapter1A;
        [SerializeField] private Material chapter1B;        
        [SerializeField] private Material chapter2A;
        [SerializeField] private Material chapter2B;
        [SerializeField] private Material chapter3A;
        [SerializeField] private Material chapter3B;

        [Header("Gamepad Color Settings")]
        [SerializeField] private Color chapter1Color;
        [SerializeField] private Color chapter2Color;
        [SerializeField] private Color chapter3Color;
        [Space]
        [SerializeField] private Color chapter1ShardAColor;
        [SerializeField] private Color chapter1ShardBColor;
        [Space]
        [SerializeField] private Color chapter2ShardAColor;
        [SerializeField] private Color chapter2ShardBColor;
        [Space]
        [SerializeField] private Color chapter3ShardAColor;
        [SerializeField] private Color chapter3ShardBColor;
        
        [Header("Floor Material for sound")]
        [SerializeField] private Material woodFloor;
        [SerializeField] private Material tileFloor1;
        [SerializeField] private Material tileFloor2;
        [SerializeField] private Material tileFloor3;
        [SerializeField] private Material carpetFloor;
        
        public int CurrentChapter {get; private set;}
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        [SerializeField] private DebugSystemInitializer debugSystemInitializer;
        [SerializeField] private bool initializeDebugger = true;
        private ShardDebugService shardDebugService;
        private CameraDebugService cameraDebugService;
        #endif
        
        private Vector3 lastStepPosition;
        
        protected override void Awake() {
            base.Awake();
            
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
            Shader.SetGlobalFloat("_ActGlobalTransition", 0);
            
            if(HudManager.HasInstance) HudManager.Instance.UpdateUIColor(CurrentChapter);
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
            EmptyInteractable();
            EmptyShards();
        }
        
        private void EmptyInteractable() {
            shardService.interactables.Clear();
        }

        public void EmptyShards() {
            for (int i = shardService.shards.Count - 1; i >= 0; i--) {
                Destroy(shardService.shards[i].gameObject);
                shardService.shards.RemoveAt(i);
            }
            
            if(HudManager.HasInstance) HudManager.Instance.SetGlass(false);
        }

        public void PopulateLevel(BaseObject[] baseObjects) {
            shardService.RepopulateBaseObjet(baseObjects);
            var camSwitches = FindObjectsByType<CameraControlTrigger>(FindObjectsSortMode.None);
            foreach (var cam in camSwitches) {
                cam.Initialize();
            }
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(!initializeDebugger) return;
            var scenes = GetScenes();
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
        
        public Material GetCurrentFragmentMaterial(bool isA)
        {
            if (isA) {
                return CurrentChapter switch {
                    1 => chapter1A,
                    2 => chapter2A,
                    3 => chapter3A,
                    _ => null
                };
            }
            return CurrentChapter switch {
                1 => chapter1B,
                2 => chapter2B,
                3 => chapter3B,
                _ => null
            };
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

        #region RumbleService

        public void RumblePulse(float lowFrequency, float highFrequency, float duration) => rumbleService.RumblePulse(lowFrequency, highFrequency, duration);
        public void RumblePulseColor(float lowFrequency, float highFrequency, float duration, Color color) => rumbleService.RumblePulseAndColor(lowFrequency, highFrequency, duration, color);
        public void SetGamepadColor(Color color) => rumbleService.SetGamepadColor(color);
        
        public Color GetCurrentChapterColor() {
            return CurrentChapter switch {
                1 => chapter1Color,
                2 => chapter2Color,
                3 => chapter3Color,
                _ => Color.white
            };
        }
        
        public Color GetCurrentShardAColor() {
            return CurrentChapter switch {
                1 => chapter1ShardAColor,
                2 => chapter2ShardAColor,
                3 => chapter3ShardAColor,
                _ => Color.white
            };
        }
        
        public Color GetCurrentShardBColor() {
            return CurrentChapter switch {
                1 => chapter1ShardBColor,
                2 => chapter2ShardBColor,
                3 => chapter3ShardBColor,
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
    }
}