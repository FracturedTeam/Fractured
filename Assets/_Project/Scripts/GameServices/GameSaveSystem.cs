using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Save;
using _Project.Scripts.Systems.Singletons;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.GameServices {

    [Serializable]
    public struct SaveFile {
        public string SaveName;
        public string CurrentScene;
        public PlayerData PlayerData;
        public List<GameData> SceneDatas;
    }
    
    public class GameSaveSystem : Singleton<GameSaveSystem> {
        private GameData gameData;
        private IDataService dataService;
        
        [SerializeField] public SaveFile saveFile;
        
        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }
        
        public void SaveGame() {
            SaveInstance.Instance.Bind();
            
            PlayerController.Instance.SaveData(saveFile.PlayerData);
            GameInitializer.Instance.SaveInteractable();
            GameInitializer.Instance.SaveShards();
            
            gameData.SceneName = gameObject.scene.name;
            saveFile.CurrentScene = gameData.SceneName;
            
            bool foundExisting = false;
            for (int i = 0; i < saveFile.SceneDatas.Count; i++)  {
                if (saveFile.SceneDatas[i].SceneName == gameData.SceneName) {
                    saveFile.SceneDatas[i] = gameData;
                    foundExisting = true;
                    Debug.Log($"[SaveSystem] Has Found Existing ");
                    break;
                }
            }

            if (!foundExisting) {
                Debug.Log($"[SaveSystem] Has Not Found Existing ");
                saveFile.SceneDatas.Add(gameData);
            }
            
            dataService.Save(saveFile);
            
            Debug.Log($"[SaveSystem] Saved Data to savefile");
        }

        public void LoadGame() {
            LoadGame(gameObject.scene.name);
        }
        
        public void LoadGame(string gameName) {
            Debug.Log($"Loading game {gameName}");
            
            saveFile = dataService.Load(saveFile.SaveName);
            
            bool foundExisting = false;
            for (int i = 0; i < saveFile.SceneDatas.Count; i++) {
                if (saveFile.SceneDatas[i].SceneName == gameName) {
                    gameData = saveFile.SceneDatas[i];
                    foundExisting = true;
                    break;
                }
            }

            //if (String.IsNullOrWhiteSpace(saveFile.Name)) {
            if(!foundExisting) {  
                Debug.Log($"Savefile {gameName} does not exist, creating new one");
                gameData.SceneName = gameName;
                SaveGame();
                return;
            }
            
            SaveInstance.Instance.Bind();
            
            //PlayerController.Instance.Load(gameData.PlayerData);
            GameInitializer.Instance.LoadInteractable();
            GameInitializer.Instance.LoadShards();
        }
        
        public void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }
        
        public void NewGame() {
            NewGame("NewGame");
        }

        public void NewGame(string gameName) {
            saveFile = new SaveFile {
                SaveName = gameName,
            };
        }
        
        public void SetRuntimeShard(List<Glass> shards) {
            for (int i = 0; i < shards.Count; i++) {
                SaveInstance.Instance.shards[i] = shards[i];
                gameData.FragmentDatas[i].glassShards = shards[i];
            }
        }
    }
}