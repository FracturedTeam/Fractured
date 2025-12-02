using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Save;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameServices {

    [Serializable]
    public struct GameData {
        public string Name;
        public string CurrentLevelName;
        public PlayerData PlayerData;
    }

    public interface ISaveable {
        SerializableGuid Id { get; set; }
    }

    public interface IBind<TData> where TData : ISaveable {
        SerializableGuid Id { get; set; }
        void Bind(TData data);
    }
    
    public class GameSaveSystem : Singleton<GameSaveSystem> {
        [SerializeField] public GameData gameData;

        private IDataService dataService;

        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }

        void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            Bind<PlayerController, PlayerData>(gameData.PlayerData);
        }
        
        private void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new() {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            if (entity != null) {
                if (data == null) {
                    data = new TData {Id = entity.Id};
                }
                entity.Bind(data);
            }
        }
        
        private void Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new() {
            var entities = FindObjectsByType<T>(FindObjectsSortMode.None);

            foreach (var entity in entities) {
                var data = datas.FirstOrDefault(d => d.Id == entity.Id);
                if (data == null) {
                    data = new TData {Id = entity.Id};
                    datas.Add(data);
                }
                entity.Bind(data);
            }
        }
        
        public void NewGame() {
            gameData = new GameData {
                Name = "New Game",
                CurrentLevelName = "S_InteractableTesting"
            };

            SceneManager.LoadScene(gameData.CurrentLevelName, LoadSceneMode.Additive);
        }

        public void SaveGame() {
            dataService.Save(gameData);
        }

        public void LoadGame(string gameName) {
            gameData = dataService.Load(gameName);

            if (String.IsNullOrWhiteSpace(gameData.CurrentLevelName)) {
                gameData.CurrentLevelName = "S_InteractableTesting";
            }
            
            SceneManager.LoadScene(gameData.CurrentLevelName, LoadSceneMode.Additive);
        }

        public void ReloadGame() => LoadGame(gameData.Name);
        
        public void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }
    }
}