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
    public class GameData {
        public string SaveName;
        public string CurrentScene;
        public PlayerData PlayerData;
        public SavedMemory memory;
        public List<SceneData> SceneDatas;
    }
    
    public class SaveService : IGameSystem {
        private readonly ShardService shardService;
        private readonly string saveFileName = "New Game";
        
        public GameData GameData;
        private IDataService dataService;
        
        private SceneData sceneData;
        
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        private bool deleteSaveOnPlay = false;
        #endif

        
        public SaveService(ShardService shard, bool deleteOnPlay) {
            shardService = shard;
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            deleteSaveOnPlay = deleteOnPlay;
            #endif
        }
        
        public void Initialize() {
            dataService = new FileDataService(new JsonSerializer());
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            if(deleteSaveOnPlay) dataService.DeleteAll();
            #endif

            if (GameData == null) {
                NewGame();
            }
            else {
                GameData.SaveName = saveFileName;
            }
        }
        
        public void SaveData() {
            if (GameSceneSettings.HasInstance) { 
                
                Debug.Log($"[SaveSystem]::Saving - Saving on scene {GameSceneSettings.Instance.gameObject.scene.name}");
                
                // Check immédiatement pour voir si un scene data existe déjà
                bool foundExistingSceneData = false;
                int index = 0;
                for (int i = 0; i < GameData.SceneDatas.Count; i++) {
                    if (GameData.SceneDatas[i].SceneName == GameSceneSettings.Instance.gameObject.scene.name) {
                        foundExistingSceneData = true;
                        index = i;
                        Debug.Log($"[SaveSystem]::Saving - Has found saved Scene Data");
                        break;
                    }
                }

                // Bind des data
                if(!foundExistingSceneData) // Use bool to create and bind GUID for the first time
                    GameSceneSettings.Instance.BindData(true);
                if(MemoryManager.HasInstance) MemoryManager.Instance.SaveData(GameData.memory);
                PlayerController.Instance.SaveData(GameData.PlayerData); // Lui donner accès au shard Service (Pourquoi ? j'ai oublié)
                
                //Save Interactable
                foreach (var interactable in shardService.interactables) {
                    interactable.SaveData();
                }
                //Save Shard
                foreach (var shard in shardService.shards) {
                    shard.SaveData();

                    for (var i = 0; i < GameSceneSettings.Instance.GetAllShards().Count; i++) {
                        if (shard.Guid == GameSceneSettings.Instance.GetSceneData().FragmentDatas[i].Guid) {
                            GameSceneSettings.Instance.GetSceneData().FragmentDatas[i] = shard.data;
                            break;
                        }
                    }
                }

                
                sceneData = GameSceneSettings.Instance.GetSceneData();
                GameData.CurrentScene = sceneData.SceneName;

                if (!foundExistingSceneData) {
                    GameData.SceneDatas.Add(sceneData);
                    Debug.Log($"[SaveSystem]::Saving - Has not found existing Scene Save, Registering new one !");
                }
                else {
                    GameData.SceneDatas[index] = sceneData;
                    Debug.Log($"[SaveSystem]::Saving - Has updated scene data in save file");
                }
            
                dataService.Save(GameData);
            
                Debug.Log($"[SaveSystem]::Saving - Saved Data to savefile {GameData.SaveName}");
            }
        }
        
        public void LoadData() {
            if(GameSceneSettings.HasInstance)
                LoadData(GameSceneSettings.Instance.gameObject.scene.name);
            else {
                Debug.LogError("[SaveService]::Load - Game scene setting not found");
            }
        }
        
        private void LoadData(string sceneName) {
            Debug.Log($"[SaveService]::Load - Scene wanting to load {sceneName}");
            
            if (!dataService.FileDoesExist(saveFileName)) {
                dataService.Save(GameData);
                return;
            }
            
            // TODO ajouter un check pour ne pas avoir a recharger la sauvegarde à chaque fois que l'on appel la méthode
            GameData = dataService.Load(GameData.SaveName); //Fail - Faut que je regarde pourquoi j'ai mis Fail
            
            var foundExisting = false;
            var index = 0;
            for (var i = 0; i < GameData.SceneDatas.Count; i++) {
                if (GameData.SceneDatas[i].SceneName != sceneName) continue;
                
                sceneData = GameData.SceneDatas[i];
                foundExisting = true;
                index = i;
                break;
            }

            if(!foundExisting) {  
                Debug.Log($"[SaveSystem]::Load - Has not found existing Scene Save, Creating new one !");
                sceneData.SceneName = sceneName;
                SaveData();
                return;
            }
            
            GameSceneSettings.Instance.SetSceneData(GameData.SceneDatas[index]);
            GameSceneSettings.Instance.BindData(false);
            
            if(MemoryManager.HasInstance)
                MemoryManager.Instance.Load(GameData.memory);
            
            foreach (var interactable in shardService.interactables) {
                interactable.Load();
            }
            
            foreach (var shard in shardService.shards) {
                shard.LoadData();
            }
            
            Debug.Log($"[SaveSystem]::Load - Save Loaded for scene {GameData.SceneDatas[index].SceneName}");
        }

        public void LoadPlayerData() {
            PlayerController.Instance.Load(GameData.PlayerData);
        }
        
        public void NewGame(string gameName = "") {
            if (gameName == "") gameName = saveFileName;
            
            GameData = new GameData {
                SaveName = gameName,
                PlayerData = new PlayerData(),
                SceneDatas = new List<SceneData>(),
                memory = new SavedMemory ()
            };
        }
        
        public void LoadGame() {
            GameData = dataService.Load(GameData.SaveName);
        }
        
        public void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }
        
        public void SetRuntimeShard(List<Glass> shards) {
            Debug.Log($"[SaveService] Set Runtime Shard : {shards.Count}");
            
            // Check si la liste n'excède pas le nombre de shard et la met à jour
            if (GameSceneSettings.Instance.GetAllShards().Count < shards.Count) {
                shards = shards.Distinct().ToList();
            }
            
            foreach (var s in shards) {
                for (var i = 0; i < GameSceneSettings.Instance.GetAllShards().Count; i++) {
                    if (s.Guid == GameSceneSettings.Instance.GetAllShards()[i].Guid) {
                        GameSceneSettings.Instance.GetAllShards()[i] = s;
                        break;
                    }
                }
            }
        }

        public bool ExistingSave() {
            return dataService.FileDoesExist(GameData.SaveName);
        }
        
        public void Tick() {
            //throw new System.NotImplementedException();
        }
        
        public void Dispose() {
            //throw new System.NotImplementedException();
        }
    }
}