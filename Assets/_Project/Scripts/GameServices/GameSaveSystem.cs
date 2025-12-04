using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
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
    }
    
    public class GameSaveSystem : Singleton<GameSaveSystem> {
        [SerializeField] public GameData gameData;
        private IDataService dataService;
        [SerializeField] private List<BaseObject> baseObjects;

        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }

        private void Bind() {
            for(var i = 0; i < baseObjects.Count; i++) {
                gameData.ObjectDatas[i].baseObject = baseObjects[i];
                gameData.ObjectDatas[i].baseObject.Bind(gameData.ObjectDatas[i]);
            }
        }
        
        public void SaveGame() {
            Bind();
            
            PlayerController.Instance.SaveData(gameData.PlayerData);
            GameInitializer.Instance.SaveInteractable();
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
        }
        
        public void DeleteGame(string gameName) {
            dataService.Delete(gameName);
        }
        
        public void NewGame() {
            gameData = new GameData {
                Name = ""
            };
        }
        
        public void GetInteractables() {
            baseObjects = new List<BaseObject>();
            gameData.ObjectDatas = new List<ObjectData>();
            
            var interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
            baseObjects.AddRange(interactables);
            foreach (var interactable in interactables) {
                gameData.ObjectDatas.Add(new ObjectData{baseObject = interactable});
            }

            
            
            #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            #endif
        }
    }
}