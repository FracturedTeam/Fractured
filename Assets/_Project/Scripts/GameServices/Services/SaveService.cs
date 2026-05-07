using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.ECS;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Save;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.GameServices.Services {

    [Serializable]
    public struct SaveFile {
        public string SaveName;
        public string CurrentScene;
        public PlayerData PlayerData;
        public SavedMemory memory;
        public List<GameData> SceneDatas;
    }
    
    public class SaveService : IGameSystem {

        private GameData gameData;
        private IDataService dataService;
        private SaveFile saveFile;
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        private bool deleteSaveOnPlay = false;
        #endif
        
        public void Initialize() {
            dataService = new FileDataService(new JsonSerializer());
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(deleteSaveOnPlay) dataService.DeleteAll();
            #endif

            saveFile.SaveName = "New Game";
        }

        public void SaveData() {
            if (SaveInstance.HasInstance) { //Update SaveInstance to no longer be a singleton
                SaveInstance.Instance.Bind();
                gameData = SaveInstance.Instance.GetGameData();
                
                //Regarder pour bind plus efficacement les datas
                if(MemoryManager.HasInstance)  
                    MemoryManager.Instance.SaveData(saveFile.memory);
                
                PlayerController.Instance.SaveData(saveFile.PlayerData);
                GameInitializer.Instance.SaveInteractable();
                GameInitializer.Instance.SaveShards();
                
                gameData.SceneName = SaveInstance.Instance.gameObject.scene.name; //Répétition ici
                saveFile.CurrentScene = gameData.SceneName;

                bool foundExistingSave = false;
                for (int i = 0; i < saveFile.SceneDatas.Count; i++) {
                    if (saveFile.SceneDatas[i].SceneName == gameData.SceneName) {
                        saveFile.SceneDatas[i] = gameData;
                        foundExistingSave = true;
                        Debug.Log($"[SaveSystem] Has found existing Scene Save");
                        break;
                    }
                }

                if (!foundExistingSave) {
                    Debug.Log($"[SaveSystem] Has not found existing Scene Save, Creating new one !");
                    saveFile.SceneDatas.Add(gameData);
                }
            
                dataService.Save(saveFile);
            
                Debug.Log($"[SaveSystem] Saved Data to savefile {saveFile.SaveName}");
            }
        }
        
        public void LoadData() {
            if(SaveInstance.HasInstance)
                LoadData(SaveInstance.Instance.gameObject.scene.name);
        }
        
        public void LoadData(string gameName) {
            saveFile = dataService.Load(saveFile.SaveName); //Fail
            
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
                Debug.Log($"[SaveSystem] Has not found existing Scene Save, Creating new one !");
                gameData.SceneName = gameName;
                SaveData();
                return;
            }
            
            SaveInstance.Instance.SetGameData(saveFile.SceneDatas[index]);
            SaveInstance.Instance.Bind();
            if(MemoryManager.HasInstance)
                MemoryManager.Instance.Load(saveFile.memory);
            
            GameInitializer.Instance.LoadInteractable();
            GameInitializer.Instance.LoadShards();
            Debug.Log($"[SaveSystem] Save Loaded for scene {saveFile.SceneDatas[index].SceneName}");
        }

        public void LoadPlayerData() {
            PlayerController.Instance.Load(saveFile.PlayerData);
        }
        
        public void NewGame(string gameName = "New Game") {
            saveFile = new SaveFile {
                SaveName = gameName,
                PlayerData = new PlayerData(),
                SceneDatas = new List<GameData>(),
                memory = new SavedMemory ()
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
            if (SaveInstance.Instance.GetShards().Count < shards.Count) {
                shards = shards.Distinct().ToList();
            }
            foreach (var s in shards) {
                for (int i = 0; i < SaveInstance.Instance.GetShards().Count; i++) {
                    if (s.gameObject.name == SaveInstance.Instance.GetShards()[i].gameObject.name) {
                        SaveInstance.Instance.GetShards()[i] = shards[i];
                        SaveInstance.Instance.GetGameData().FragmentDatas[i].glassShards = shards[i];
                    }
                }
            }
        }

        public bool ExistingSave() {
            return dataService.FileDoesExist(saveFile.SaveName);
        }
        
        public void Tick() {
            //throw new System.NotImplementedException();
        }
        
        public void Dispose() {
            //throw new System.NotImplementedException();
        }
    }
}