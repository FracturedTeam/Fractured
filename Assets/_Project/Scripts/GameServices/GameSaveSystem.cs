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

        protected override void Awake() {
            base.Awake();
            dataService = new FileDataService(new JsonSerializer());
        }

        public void SaveGame() {
            for(var i = 0; i < gameData.ObjectDatas.Count; i++) {
                gameData.ObjectDatas[i].baseObject.Bind(gameData.ObjectDatas[i]);
            }
            
            PlayerController.Instance.SaveData(gameData.PlayerData);
            GameInitializer.Instance.SaveInteractable();
            gameData.Name = gameObject.scene.name;
            
            dataService.Save(gameData);
        }

        public void LoadGame(string gameName) {
            gameData = dataService.Load(gameName);

            if (String.IsNullOrWhiteSpace(gameData.Name)) {
                gameData.Name = "S_InteractableTesting";
            }
            
            for(var i = 0; i < gameData.ObjectDatas.Count; i++) { //Attribue les trucs au bons trucs
                gameData.ObjectDatas[i].baseObject.Bind(gameData.ObjectDatas[i]);
            }
            
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
            gameData.ObjectDatas = new List<ObjectData>();
            
            var interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
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