using System;
using _Project.Scripts.DebugSystems;
using _Project.Scripts.DebugSystems.Services;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        private GameSystems gameSystems;

        private ShardService  shardService;

        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        [SerializeField] private DebugSystemInitializer debugSystemInitializer;
        #endif
        
        private new void Awake() {
            InitializeGameSystems();
            
            #if UNITY_EDITOR ||UNITY_DEVELOPMENT_BUILD
            InitializeDebugSystems();
            #endif
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

        public void UpdatePuzzleRoom(InteractableObject[] _interactable,  Glass[] _shards) =>
            shardService.PopulateService(_interactable,  _shards);
        
        #if UNITY_EDITOR ||UNITY_DEVELOPMENT_BUILD
        void InitializeDebugSystems() {
            var debugUIState = new DebugUIState();
            var debugSystem = new DebugSystem();
            
            //Add every debug services
            var debugInputService = new DebugInputService(debugUIState);
            debugSystem.Register(debugInputService);

            var playerDebugService = new PlayerDebugService(debugUIState);
            debugSystem.Register(playerDebugService);
            
            var shardDebugService = new ShardDebugService(shardService,  debugUIState);
            debugSystem.Register(shardDebugService);
            
            var cameraDebugService = new CameraDebugService(debugUIState);
            debugSystem.Register(cameraDebugService);

            //Set the debug system
            debugSystemInitializer.debugSystem = debugSystem;
        }
        #endif
        
        private void Start() {
            //Populate the glassShardService
            var _interactables = FindObjectsByType<InteractableObject>(FindObjectsSortMode.None);
            var _shards = FindObjectsByType<Glass>(FindObjectsSortMode.None);
            shardService.PopulateService(_interactables,  _shards);
        }
        
        private void Update() {
            gameSystems.Tick();
        }

        private void OnDestroy() {
            gameSystems.Dispose();
        }
    }
}