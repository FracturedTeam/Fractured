using System;
using System.Collections.Generic;
using _Project.Scripts.DebugSystems;
using _Project.Scripts.DebugSystems.Services;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        private GameSystems gameSystems;
        private ShardService  shardService;

        [Header("Services")]
        [SerializeField] private GameSaveSystem gameSaveSystem;
        [SerializeField] private GameSceneLoaderSystem gameSceneLoaderSystem;
        [SerializeField] private PlayerService player;
        [SerializeField] private HudManager hudManager;
        
        [Header("ScreenEffect")]
        [SerializeField] private Material screenEffectMat;
        private float fadeTime = 1.0f;
        private float fadeTimer = 0.0f;
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        [SerializeField] private DebugSystemInitializer debugSystemInitializer;
        [SerializeField] private bool InitializeDebugger = true;
        #endif
        
        protected override void Awake() {
            base.Awake();
            if (!GameSaveSystem.HasInstance) Instantiate(gameSaveSystem);
            if (!GameSceneLoaderSystem.HasInstance) Instantiate(gameSceneLoaderSystem);
            if (!PlayerService.HasInstance) Instantiate(player);
            if (!HudManager.HasInstance) Instantiate(hudManager);
            
            InitializeGameSystems();
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            InitializeDebugSystems();
            #endif
            
            //Populate the glassShardService
            PopulateShardOnStart();

            screenEffectMat.SetFloat("_Progression", 0f);
        }

        private void InitializeGameSystems() {
            //Create all the game systems
            gameSystems = new GameSystems(); //First one to be created as it is the one that handle all the game services
            shardService = new ShardService();
            
            //Then register the game systems
            gameSystems.Register(shardService);
            
            //Then initialize the services (act as the awake method)
            gameSystems.Initialize();
        }
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        void InitializeDebugSystems() {
            if(!InitializeDebugger) return;
            
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
            
            var generalDebug =  new GeneralDebugService(debugUIState);
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

        public CinemachineCamera[] GetCameras() {
            return FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        }

        public void ResetCameras() {
            var cam = GetCameras();
            foreach (var c in cam) {
                c.Priority = 0;
            }
        }

        #region ShardService

        private void PopulateShardOnStart() {
            //gameObject.AddComponent<EventSystem>();
            //gameObject.AddComponent<InputSystemUIInputModule>();
            
            var _interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
            var _shards = FindObjectsByType<Glass>(FindObjectsSortMode.None);
            var _text = FindObjectsByType<GlassText>(FindObjectsSortMode.None);
            shardService.PopulateService(_interactables, _shards, _text);
        }

        public void EmptyAll() {
            EmptyInteractable();
            EmptyShards();
        }
        
        private void EmptyInteractable() {
            shardService.interactables.Clear();
        }

        private void EmptyShards() {
            for (int i = shardService.shards.Count - 1; i >= 0; i--) {
                Destroy(shardService.shards[i].gameObject);
                shardService.shards.RemoveAt(i);
            }
        }

        public void RepopulateInteractableOnLoadLevel() {
            shardService.RepopulateBaseObjet(SaveInstance.Instance.baseObjects.ToArray());
        }

        public BaseObject[] GetInteractables() {
            return shardService.interactables.ToArray();
        }

        public void SaveInteractable() {
            foreach (var interactable in shardService.interactables) {
                interactable.SaveData();
            }
        }
        
        public void SaveShards() {
            foreach (var shard in shardService.shards) {
                shard.SaveData();
            }
        }
        
        public void LoadInteractable() {
            foreach (var interactable in shardService.interactables) {
                interactable.Load();
            }
        }
        
        public void LoadShards() {
            /*foreach (var shard in shardService.shards) { //Previous method who does not accuratly work
                shard.LoadData();
            }*/
            foreach (var shard in SaveInstance.Instance.GetShards()) {
                shard.LoadData();
            }
        }
        
        public void AddShards(Glass[] shards) {
            var newShards = new List<Glass>();
            foreach (var shard in shards) {
                var s = Instantiate(shard, HudManager.Instance.glassHolder);
                newShards.Add(s);
            }
            
            shardService.AddShards(newShards.ToArray());
            GameSaveSystem.Instance.SetRuntimeShard(shardService.shards);
        }

        public void ResetInteractable() {
            EmptyShards();
            foreach (var interactable in shardService.interactables)
                interactable.ResetInteract();
            
            GameSceneSettings.Instance.ResetShard();
        }
        
        public void UpdatePuzzleRoom(BaseObject[] _interactable,  Glass[] _shards, GlassText[] _text) =>
            shardService.PopulateService(_interactable,  _shards, _text);

        public void SetEditableArea(bool inArea, ColorEnum color) {
            switch (color) {
                case ColorEnum.Blue:
                    shardService.SetBlueEditableArea(inArea);
                    break;
                case ColorEnum.Red:
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
            EventBus<EditableSound>.Raise(new EditableSound { inEditable = shardService.PlayerInEditableArea });
            return shardService.PlayerInEditableArea;
        }
        
        public bool InBlueEditableArea() {
            EventBus<EditableSound>.Raise(new EditableSound { inEditable = shardService.PlayerInBlueEditableArea });
            return shardService.PlayerInBlueEditableArea;
        }
        
        public bool InRedEditableArea() {
            EventBus<EditableSound>.Raise(new EditableSound { inEditable = shardService.PlayerInRedEditableArea });
            return shardService.PlayerInRedEditableArea;
        }
        

        #endregion

        #region LoadScene

        public void LoadNewLevel(SceneSettings sceneSettings) {
            GameSaveSystem.Instance.SaveGame();
            //EmptyAll();
            ResetCameras();
            
            _ = GameSceneLoaderSystem.Instance.LoadGameplaySceneAsync(sceneSettings);
        }

        #endregion
       
    }
}