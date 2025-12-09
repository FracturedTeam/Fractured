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
            SaveInstance.Instance.Bind(gameData = SaveInstance.Instance.GetGameData());
            
            PlayerController.Instance.SaveData(saveFile.PlayerData);
            GameInitializer.Instance.SaveInteractable();
            GameInitializer.Instance.SaveShards();

            gameData.SceneName = SaveInstance.Instance.gameObject.scene.name;
            saveFile.CurrentScene = gameData.SceneName;
            
            bool foundExisting = false;
            for (int i = 0; i < saveFile.SceneDatas.Count; i++)  {
                if (saveFile.SceneDatas[i].SceneName == gameData.SceneName) {
                    saveFile.SceneDatas[i] = gameData;
                    foundExisting = true;
                    Debug.Log($"[SaveSystem] Has found existing Scene");
                    break;
                }
            }

            if (!foundExisting) {
                Debug.Log($"[SaveSystem] Has not found existing Scene, Creating new one !");
                saveFile.SceneDatas.Add(gameData);
            }
            
            dataService.Save(saveFile);
            
            Debug.Log($"[SaveSystem] Saved Data to savefile {saveFile.SaveName}");
        }

        public void LoadGame() { //Data are loaded here
            LoadGame(SaveInstance.Instance.gameObject.scene.name);
        }
        
        public void LoadGame(string gameName) {
            Debug.Log($"Loading save {gameName} from saveFile {saveFile.SaveName}");
            
            saveFile = dataService.Load(saveFile.SaveName);
            
            var foundExisting = false;
            var index = 0;
            for (var i = 0; i < saveFile.SceneDatas.Count; i++) {
                if (saveFile.SceneDatas[i].SceneName != gameName) continue;
                
                gameData = saveFile.SceneDatas[i];
                foundExisting = true;
                index = i;
                break;
            }

            //if (String.IsNullOrWhiteSpace(saveFile.Name)) {
            if(!foundExisting) {  
                gameData.SceneName = gameName;
                SaveGame();
                return;
            }
            
            SaveInstance.Instance.SetGameData(saveFile.SceneDatas[index]);
            SaveInstance.Instance.Bind(saveFile.SceneDatas[index]);
            
            GameInitializer.Instance.LoadInteractable();
            GameInitializer.Instance.LoadShards();
        }
        
        public void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }

        public void NewGame(string gameName = "New Game") {
            saveFile = new SaveFile {
                SaveName = gameName,
            };
        }
        
        public void SetRuntimeShard(List<Glass> shards) {
            for (int i = 0; i < shards.Count; i++) {
                SaveInstance.Instance.GetShards()[i] = shards[i];
                SaveInstance.Instance.GetGameData().FragmentDatas[i].glassShards = shards[i];
            }
        }
    }
}