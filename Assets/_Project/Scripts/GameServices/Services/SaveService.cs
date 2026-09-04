using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.ECS;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Save;
using Unity.Cinemachine;

namespace _Project.Scripts.GameServices.Services {

    [Serializable]
    public class GameData {
        public string SaveName;
        public string CurrentScene;
        public PlayerData PlayerData;
        public List<SceneData> SceneDatas;
        public int CurrentActColor;
        public int LastPlayedLevel;
        public string lastActiveCameraName;
    }
    
    [Serializable]
    public class SettingData {
        public float mainVolume;
        public float musicVolume;
        public float sfxVolume;
        public int screenResolutionIndex;
        public int fullScreenMode;
        public int quality;
        public bool vSyncEnabled;
        public int brightness;
        public int contrast;
        public bool chromaticAberration;

        public float enviroColorIntensity;
        public float uiColorIntensity;
    }
    
    public class SaveService : IGameSystem {
        private readonly ShardService shardService;
        private readonly string saveFileName = "savefile0";
        private readonly string settingsFileName = "settings";
        
        public GameData GameData;
        public SettingData SettingData;
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

            if (dataService.FileDoesExist(saveFileName)) {
                GameData = dataService.Load<GameData>(saveFileName);
            }
            else {
                NewGame();
            }
            
            if (dataService.FileDoesExist(settingsFileName)) {
                LoadSettings();
            }
            else {
                NewSettings();
            }
           
        }
        
        public void NewGame(string gameName = "") {
            if (gameName == "") gameName = saveFileName;

            var currentChap = 1;
            var lastLevel = 1;
            if (ExistingSave()) {
                GameData = dataService.Load<GameData>(GameData.SaveName);
                currentChap = GameData.CurrentActColor;
                lastLevel = GameData.LastPlayedLevel;
            }
            
            DeleteGame(saveFileName);
            
            GameData = new GameData {
                SaveName = gameName,
                PlayerData = new PlayerData(),
                SceneDatas = new List<SceneData>(),
                CurrentActColor = currentChap,
                lastActiveCameraName = "",
                LastPlayedLevel = lastLevel
            };
        }
        
        private void NewSettings() {
            SettingData = new SettingData() { 
                mainVolume = 1f, 
                musicVolume = 1f,
                sfxVolume = 1f,
                screenResolutionIndex = -1,
                vSyncEnabled = true,
                brightness = (int)GameInitializer.Instance.GetColorAdjustments().postExposure.value,
                contrast = (int)GameInitializer.Instance.GetColorAdjustments().contrast.value,
                quality = 3,
                enviroColorIntensity = 0,
                uiColorIntensity = 1,
            };
        }
        
        public void SaveData() {
            if (GameSceneSettings.HasInstance) { 
                var settings = GameSceneSettings.Instance;
                // Debug.Log($"[SaveSystem]::Saving - Saving on scene {GameSceneSettings.Instance.gameObject.scene.name}");
                
                // Check immédiatement pour voir si un scene data existe déjà
                bool foundExistingSceneData = false;
                int index = 0;
                for (int i = 0; i < GameData.SceneDatas.Count; i++) {
                    if (GameData.SceneDatas[i].SceneName == settings.gameObject.scene.name) {
                        foundExistingSceneData = true;
                        index = i;
                        // Debug.Log($"[SaveSystem]::Saving - Has found saved Scene Data");
                        break;
                    }
                }

                // Bind des data
                if(!foundExistingSceneData) // Use bool to create and bind GUID for the first time in the save file
                    settings.BindData(true);
                PlayerController.Instance.SaveData(GameData.PlayerData); // Lui donner accès au shard Service (Pourquoi ? j'ai oublié)

                foreach (var scene in shardService.sceneMasters) {
                    scene.SaveData();
                }
                
                //Save Interactable
                foreach (var interactable in shardService.interactables) {
                    interactable.SaveData();
                }
                //Save Shard
                foreach (var shard in shardService.shards) {
                    shard.SaveData();

                    for (var i = 0; i < settings.GetAllShards().Count; i++) {
                        if (shard.Guid == settings.GetSceneData().FragmentDatas[i].Guid) {
                            settings.GetSceneData().FragmentDatas[i] = shard.data;
                            break;
                        }
                    }
                }
                
                sceneData = settings.GetSceneData();
                GameData.CurrentScene = sceneData.SceneName;
                if(GameInitializer.Instance.CurrentChapter > GameData.CurrentActColor)
                    GameData.CurrentActColor = GameInitializer.Instance.CurrentChapter;
                
                if(GameSceneSettings.Instance.gameObject.scene.buildIndex - 2 > GameData.LastPlayedLevel)
                    GameData.LastPlayedLevel = GameSceneSettings.Instance.gameObject.scene.buildIndex - 2;
                
                GameData.lastActiveCameraName = CinemachineBrain.GetActiveBrain(0).ActiveVirtualCamera.Name;
                
                if (!foundExistingSceneData) {
                    GameData.SceneDatas.Add(sceneData);
                    // Debug.Log($"[SaveSystem]::Saving - Has not found existing Scene Save, Registering new one !");
                }
                else {
                    GameData.SceneDatas[index] = sceneData;
                    // Debug.Log($"[SaveSystem]::Saving - Has updated scene data in save file");
                }
            
                dataService.Save(GameData, GameData.SaveName);
            
                // Debug.Log($"[SaveSystem]::Saving - Saved Data to savefile {GameData.SaveName}");
            }
        }
        
         public void SaveSettings()
         {
             dataService.Save(SettingData, settingsFileName);
             // Debug.Log($"[SaveSystem]::Saving - Saved Data to savefile {settingsFileName}");
         }
        
        public void LoadData() {
            if(GameSceneSettings.HasInstance)
                LoadData(GameSceneSettings.Instance.gameObject.scene.name);
            // else {
            //     Debug.LogError("[SaveService]::Load - Game scene setting not found");
            // }
        }
        
        private void LoadData(string sceneName) {
            
            if (!dataService.FileDoesExist(saveFileName)) {
                dataService.Save(GameData, GameData.SaveName);
                // Debug.Log($"[SaveService]::Load - SaveFile does not exist, creating a new one");
                return;
            }
            
            GameData ??= dataService.Load<GameData>(GameData.SaveName);
            
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
                // Debug.Log($"[SaveSystem]::Load - Has not found existing Scene Save, Creating new one !");
                sceneData.SceneName = sceneName;
                SaveData();
                return;
            }
            
            GameSceneSettings.Instance.SetSceneData(GameData.SceneDatas[index]);
            GameSceneSettings.Instance.BindData(false);

            foreach (var scene in shardService.sceneMasters) {
                scene.Load();
            }
            
            foreach (var interactable in shardService.interactables) {
                interactable.Load();
            }
            
            foreach (var shard in shardService.shards) {
                shard.LoadData();
            }

            var foundCam = false;
            foreach (var camera in GameInitializer.Instance.GetCameras()) {
                if (camera.name == GameData.lastActiveCameraName) {
                    camera.Priority = 1;
                    foundCam = true;
                }
                else
                    camera.Priority = 0;
            }

            if (!foundCam) {
                GameSceneSettings.Instance.roomCamera.Priority = 1;
            }
            
            // Debug.Log($"[SaveSystem]::Load - Save Loaded for scene {GameData.SceneDatas[index].SceneName}");
        }

        public void LoadPlayerData() {
            PlayerController.Instance.Load(GameData.PlayerData);
        }
        
        public void LoadSaveFile() {
            GameData = dataService.Load<GameData>(GameData.SaveName);
        }
        
        private void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }
        
        public void LoadSettings() {
            SettingData = dataService.Load<SettingData>(settingsFileName);
        }
        
        public void DeleteSettings() {
            dataService.Delete(settingsFileName);
        }
        
        public void SetRuntimeShard(List<Glass> shards) {
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
            return dataService.FileDoesExist(saveFileName);
        }
        
        public void Tick() {
            //throw new System.NotImplementedException();
        }
        
        public void Dispose() {
            //throw new System.NotImplementedException();
        }
    }
}