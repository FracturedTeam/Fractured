using System;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        private GameSystems gameSystems;

        private ShardService  shardService;

        private new void Awake() {
            Initialize();
            
            #if UNITY_EDITOR ||UNITY_DEVELOPMENT_BUILD
            InitializeDebugSystems();
            #endif
        }

        private void Initialize() {
            //Create all the game systems
            gameSystems = new GameSystems(); //First one to be created as it is the one that handle all the game services
            shardService = new ShardService();
            
            //Then register the game systems
            gameSystems.Register(shardService);
            
            //Then initialize the services (act as the awake method)
            gameSystems.Initialize();
        }

        private void Start() {
            //Populate the glassShardService
            var _interactables = FindObjectsByType<InteractableObject>(FindObjectsSortMode.None);
            var _shards = FindObjectsByType<Glass>(FindObjectsSortMode.None);
            shardService.PopulateService(_interactables,  _shards);
        }

        public void UpdatePuzzleRoom(InteractableObject[] _interactable,  Glass[] _shards) =>
            shardService.PopulateService(_interactable,  _shards);
        
#if UNITY_EDITOR ||UNITY_DEVELOPMENT_BUILD
        void InitializeDebugSystems() {
            
        }
        #endif
        
        private void Update() {
            gameSystems.Tick();
        }

        private void OnDestroy() {
            gameSystems.Dispose();
        }
    }
}