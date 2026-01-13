using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Save;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;

namespace _Project.Scripts.GameServices {

    [Serializable]
    public struct SaveFile {
        public string SaveName;
        public string CurrentScene;
        public PlayerData PlayerData;
        public List<GameData> SceneDatas;
    }
    
    public class GameSaveSystem : PersistentSingleton<GameSaveSystem> {
        private GameData gameData;
        private IDataService dataService;
        
        [SerializeField] public SaveFile saveFile;
        public bool deleteSaveOnPlay = true;
        
        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
            
            if (deleteSaveOnPlay) {
                dataService.DeleteAll();
            }
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
                    Debug.Log($"[SaveSystem] Has found existing Scene Save");
                    break;
                }
            }

            if (!foundExisting) {
                Debug.Log($"[SaveSystem] Has not found existing Scene Save, Creating new one !");
                saveFile.SceneDatas.Add(gameData);
            }
            
            dataService.Save(saveFile);
            
            Debug.Log($"[SaveSystem] Saved Data to savefile {saveFile.SaveName}");
        }

        public void LoadData() {
            LoadData(SaveInstance.Instance.gameObject.scene.name);
        }
        
        public void LoadData(string gameName) {
            Debug.Log($"Loading Scene Save {gameName} from saveFile {saveFile.SaveName}");
            
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

        public void LoadPlayerData() {
            PlayerController.Instance.Load(saveFile.PlayerData);
        }
        
        public void NewGame(string gameName = "New Game") {
            saveFile = new SaveFile {
                SaveName = gameName,
                PlayerData = new PlayerData(),
                SceneDatas = new List<GameData>()
            };
            GameSceneLoaderSystem.Instance.NewGame();
        }
        
        public void LoadGame() {
            saveFile = dataService.Load(saveFile.SaveName);
            GameSceneLoaderSystem.Instance.LoadGame(saveFile.CurrentScene);
        }
        
        public void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }
        
        public void SetRuntimeShard(List<Glass> shards) {
            for (int i = 0; i < shards.Count; i++) {
                SaveInstance.Instance.GetShards()[i] = shards[i];
                SaveInstance.Instance.GetGameData().FragmentDatas[i].glassShards = shards[i];
            }
        }
    }
}