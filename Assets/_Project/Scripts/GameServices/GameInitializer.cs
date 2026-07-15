using System;
using System.Collections.Generic;
using System.Diagnostics;
using _Project.Scripts.DebugSystems;
using _Project.Scripts.DebugSystems.Services;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.UI;
using FMOD.Studio;
using FMODUnity;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        //SYSTEM REGISTRY SERVICE
        private GameSystems gameSystems;
        
        //INTERNAL SERVICES
        private ShardService shardService;
        private SaveService saveService;
        private AudioService audioService;

        [Header("Save service")] 
        [SerializeField] private bool deleteSaveOnPay;
        
        [Header("Audio Bank")]
        [SerializeField] private AudioBank audioBank;
        
        [Header("ScreenEffect")]
        [SerializeField] private Material screenEffectMat;
        private float fadeTime = 1.0f;

        [Header("ShardMaterials")] 
        [SerializeField] private Material chapter1A;
        [SerializeField] private Material chapter1B;        
        [SerializeField] private Material chapter2A;
        [SerializeField] private Material chapter2B;
        [SerializeField] private Material chapter3A;
        [SerializeField] private Material chapter3B;
        
        private float fadeTimer = 0.0f;
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        [SerializeField] private DebugSystemInitializer debugSystemInitializer;
        [SerializeField] private bool initializeDebugger = true;
        #endif
        
        protected override void Awake() {
            base.Awake();
            
            InitializeGameSystems();
            
            //Shard Edition area Screen Effect
            screenEffectMat.SetFloat("_Progression", 0f);
        }

        private void InitializeGameSystems() {
            //Create all the game systems
            gameSystems = new GameSystems(); //First one to be created as it is the one that handle all the game services
            shardService = new ShardService();
            saveService = new SaveService(shardService, deleteSaveOnPay);
            audioService = new AudioService(audioBank);
            
            //Then register the game systems
            gameSystems.Register(shardService);
            gameSystems.Register(saveService);
            gameSystems.Register(audioService);
            
            //Then initialize the services (act as the awake method)
            gameSystems.Initialize();
            saveService.Initialize();
            audioService.Initialize();
        }

        public Material GetCurrentFragmentMaterial(bool isA, int chapter)
        {
            if (isA)
            {
                return chapter switch
                {
                    1 => chapter1A,
                    2 => chapter2A,
                    3 => chapter3A,
                    _ => null
                };
            }
            return chapter switch
            {
                1 => chapter1B,
                2 => chapter2B,
                3 => chapter3B,
                _ => null
            };
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
            
            var shardDebugService = new ShardDebugService(shardService,  debugUIState);
            debugSystem.Register(shardDebugService);

            var cameras = GetCameras();
            var cameraDebugService = new CameraDebugService(debugUIState, cameras);
            debugSystem.Register(cameraDebugService);
            
            var generalDebug =  new GeneralDebugService(debugUIState, saveService);
            debugSystem.Register(generalDebug);

            //Set the debug system
            debugSystemInitializer.SetDebugSystem(debugSystem);
        }
        #endif
        
        private void Update() {
            gameSystems.Tick();
            
            UpdateScreenEffect();
        }

        void UpdateScreenEffect() {
            fadeTimer = InEditableArea() ? Mathf.Clamp(fadeTimer + Time.deltaTime, 0, fadeTime):
                fadeTimer = Mathf.Clamp(fadeTimer - Time.deltaTime, 0, fadeTime);
            
            screenEffectMat.SetFloat("_Progression", fadeTimer);
        }
        
        private void OnDestroy() {
            screenEffectMat.SetFloat("_Progression", 0f);
            gameSystems.Dispose();
        }

        private CinemachineCamera[] GetCameras() {
            return FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        }

        public void ResetCameras() {
            var cam = GetCameras();
            foreach (var c in cam) {
                c.Priority = 0;
            }
        }

        #region SaveService

        public void SaveData() => saveService.SaveData();
        public void LoadData() => saveService.LoadData();
        public void LoadPlayerData() => saveService.LoadPlayerData();
        public void LoadGame() => saveService.LoadGame();
        public string GetLastScene() => saveService.GameData.CurrentScene;
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
        }

        public void PopulateLevel(BaseObject[] _baseObjects, Glass[] _shards) {
            shardService.RepopulateBaseObjet(_baseObjects);
            if(_shards.Length > 0)
                AddShards(_shards);
        }
        
        public BaseObject[] GetInteractables() {
            return shardService.interactables.ToArray();
        }
        
        public void AddShards(Glass[] shards) {
            var newShards = new List<Glass>();
            foreach (var shard in shards) {
                var s = Instantiate(shard, HudManager.Instance.glassHolder);
                newShards.Add(s);
            }
            
            shardService.AddShards(newShards.ToArray());
            saveService.SetRuntimeShard(shardService.shards);
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
        
        public void UpdatePuzzleRoom(BaseObject[] _interactable,  Glass[] _shards) =>
            shardService.PopulateService(_interactable,  _shards);

        public void SetEditableArea(bool inArea, ColorEnum color) {
            switch (color) {
                case ColorEnum.ColorA:
                    shardService.SetBlueEditableArea(inArea);
                    break;
                case ColorEnum.ColorB:
                    shardService.SetRedEditableArea(inArea);
                    break;
                case ColorEnum.Both:
                    shardService.SetEditableArea(inArea);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
        
        public bool InEditableArea() {
            audioService.PlayEditableSoundLoop(shardService.PlayerInEditableArea);
            return shardService.PlayerInEditableArea;
        }
        
        public bool InBlueEditableArea() {
            audioService.PlayEditableSoundLoop(shardService.PlayerInEditableArea);
            return shardService.PlayerInBlueEditableArea;
        }
        
        public bool InRedEditableArea() {
            audioService.PlayEditableSoundLoop(shardService.PlayerInEditableArea);
            return shardService.PlayerInRedEditableArea;
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
        
        public void PlaySound2D(EventReference audioClip) {
            audioService.PlayOneShot2D(audioClip);
        }

        public void PlayRevealSound(Vector3 position) {
            audioService.PlayRevealObjectSound(position);
        }

        public void PlayHideSound(Vector3 position) {
            audioService.PlayHideObjectSound(position);
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
                0 => GetSettings.MainVolume,
                1 => GetSettings.MusicVolume,
                2 => GetSettings.SFXVolume,
                _ => 0
            };
        }

        public void SetVolume(int index, float volume) {
            audioService.SetSound(index, volume);
        }
        #endregion
       
    }
}