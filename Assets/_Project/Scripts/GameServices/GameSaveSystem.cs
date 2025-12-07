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
    public struct GameData {
        public string Name;
        public PlayerData PlayerData;
        public List<ObjectData> ObjectDatas;
        public List<FragmentData> FragmentDatas;
    }
    
    public class GameSaveSystem : Singleton<GameSaveSystem> {
        [SerializeField] public GameData gameData;
        private IDataService dataService;
        
        [SerializeField, HideInInspector] private List<BaseObject> baseObjects;
        [SerializeField, HideInInspector] private List<Glass> shards;
        
        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }

        private void Update() {
            
        }

        private void Bind() {
            for(var i = 0; i < baseObjects.Count; i++) {
                gameData.ObjectDatas[i].baseObject = baseObjects[i];
                gameData.ObjectDatas[i].baseObject.Bind(gameData.ObjectDatas[i]);
            }

            for (var i = 0; i < shards.Count; i++) {
                gameData.FragmentDatas[i].glassShards = shards[i];
                gameData.FragmentDatas[i].glassShards.Bind(gameData.FragmentDatas[i]);
            }
        }
        
        public void SaveGame() {
            Bind();
            
            PlayerController.Instance.SaveData(gameData.PlayerData);
            GameInitializer.Instance.SaveInteractable();
            GameInitializer.Instance.SaveShards();
            gameData.Name = gameObject.scene.name;
            
            dataService.Save(gameData);
            
            Debug.Log($"[SaveSystem] Saved Data to savefile");
        }

        public void LoadGame() {
            LoadGame(gameObject.scene.name);
        }
        
        public void LoadGame(string gameName) {
            Debug.Log($"Loading game {gameName}");
            
            gameData = dataService.Load(gameName);

            if (String.IsNullOrWhiteSpace(gameData.Name)) {
                Debug.Log($"Savefile {gameName} does not exist, creating new one");
                gameData.Name = gameName;
                SaveGame();
                return;
            }
            
            Bind();
            
            PlayerController.Instance.Load(gameData.PlayerData);
            GameInitializer.Instance.LoadInteractable();
            GameInitializer.Instance.LoadShards();
        }
        
        public void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }
        
        public void NewGame() {
            gameData = new GameData {
                Name = ""
            };
        }

        public void SetRuntimeShard(List<Glass> shards) {
            for (int i = 0; i < shards.Count; i++) {
                this.shards[i] = shards[i];
                gameData.FragmentDatas[i].glassShards = shards[i];
            }
        }
        
        #if UNITY_EDITOR
        public void GetInteractables() {
            baseObjects = new List<BaseObject>();
            shards = new List<Glass>();
            gameData.ObjectDatas = new List<ObjectData>();
            gameData.FragmentDatas = new List<FragmentData>();
            
            //Set Shards
            shards.AddRange(GameSceneSettings.Instance.glassShards);
            
            foreach (var shard in GameSceneSettings.Instance.glassShards) {
                gameData.FragmentDatas.Add(new FragmentData{glassShards = shard});
            }
            
            //Set interactable
            var interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
            baseObjects.AddRange(interactables);
            
            foreach (var interactable in interactables) {
                gameData.ObjectDatas.Add(new ObjectData{baseObject = interactable});
                
                //Set shard in interactable
                if (interactable.TryGetComponent(out ObtainShardInteractable shard)) {
                    shards.AddRange(shard.shards);
                    foreach (var s in shard.shards) {
                        gameData.FragmentDatas.Add(new FragmentData{glassShards = s});
                    }
                }
            }

            
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
        #endif
    }
}