using _Project.Scripts.DebugSystems;
using _Project.Scripts.DebugSystems.Services;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Systems.Singletons;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameInitializer : PersistentSingleton<GameInitializer> {
        private GameSystems gameSystems;
        private ShardService  shardService;

        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        [SerializeField] private DebugSystemInitializer debugSystemInitializer;
        [SerializeField] private bool InitializeDebugger = true;
        #endif
        
        private new void Awake() {
            InitializeGameSystems();
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            InitializeDebugSystems();
            #endif
            
            //Populate the glassShardService
            PopulateShardOnStart();
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
            
            var cameras = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
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

        #region ShardService

        private void PopulateShardOnStart() {
            var _interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
            var _shards = FindObjectsByType<Glass>(FindObjectsSortMode.None);
            var _text = FindObjectsByType<GlassText>(FindObjectsSortMode.None);
            shardService.PopulateService(_interactables, _shards, _text);
        }
        
        public void UpdatePuzzleRoom(BaseObject[] _interactable,  Glass[] _shards, GlassText[] _text) =>
            shardService.PopulateService(_interactable,  _shards, _text);

        public void SetEditableArea(bool inArea) {
            shardService.SetEditableArea(inArea);
        }
        
        public bool InEditableArea() {
            return shardService.PlayerInEditableArea;
        }

        #endregion
       
    }
}