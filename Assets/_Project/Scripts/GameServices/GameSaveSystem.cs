using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.Systems.Save;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameServices {

    [Serializable]
    public struct GameData {
        public string Name;
        public string CurrentLevelName;
    }

    public interface ISaveable {
        string Id { get; set; }
    }

    public interface IBind<TData> where TData : ISaveable {
        string Id { get; set; }
        void Bind(TData data);
    }
    
    public class GameSaveSystem : Singleton<GameSaveSystem> {
        [SerializeField] public GameData gameData;

        private IDataService dataService;

        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
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