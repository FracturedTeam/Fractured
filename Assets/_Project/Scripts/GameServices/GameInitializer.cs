using System;
using System.Collections.Generic;
using _Project.Scripts.DebugSystems;
using _Project.Scripts.DebugSystems.Services;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Systems.Save;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.UI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        private GameSystems gameSystems;
        private ShardService  shardService;

        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        [SerializeField] private DebugSystemInitializer debugSystemInitializer;
        [SerializeField] private bool InitializeDebugger = true;
        public bool deleteSaveOnPlay = true;
        #endif
        
        private new void Awake() {
            InitializeGameSystems();
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            InitializeDebugSystems();
            #endif
            
            //Populate the glassShardService
            PopulateShardOnStart();

            if (deleteSaveOnPlay) {
                var dataService = new FileDataService(new JsonSerializer());
                dataService.DeleteAll();
            }
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
        }

        private void OnDestroy() {
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
            if (!FindAnyObjectByType<EventSystem>()) { //Add event system component if there is none
                gameObject.AddComponent<EventSystem>();
                gameObject.AddComponent<InputSystemUIInputModule>();
            }
            
            var _interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
            var _shards = FindObjectsByType<Glass>(FindObjectsSortMode.None);
            var _text = FindObjectsByType<GlassText>(FindObjectsSortMode.None);
            shardService.PopulateService(_interactables, _shards, _text);
        }

        public void EmptyInteractable() {
            shardService.interactables.Clear();
        }

        public void EmptyShards() {
            for (int i = shardService.shards.Count - 1; i >= 0; i--) {
                Destroy(shardService.shards[i].gameObject);
                shardService.shards.RemoveAt(i);
            }
        }

        public void RepopulateInteractable() {
            var _interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
            shardService.RepopulateBaseObjet(_interactables);
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
            foreach (var shard in shardService.shards) {
                shard.LoadData();
            }
        }
        
        public void AddShards(Glass[] shards) {
            var newShards = new List<Glass>();
            foreach (var shard in shards) {
                var s = Instantiate(shard, HudManager.Instance.transform);
                newShards.Add(s);
            }
            
            shardService.AddShards(newShards.ToArray());
            GameSaveSystem.Instance.SetRuntimeShard(shardService.shards);
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
            return shardService.PlayerInEditableArea;
        }
        
        public bool InBlueEditableArea() {
            return shardService.PlayerInBlueEditableArea;
        }
        
        public bool InRedEditableArea() {
            return shardService.PlayerInRedEditableArea;
        }

        #endregion
       
    }
}