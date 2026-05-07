using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Systems.Singletons;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameSceneSettings : Singleton<GameSceneSettings> {
        [Header("Level Art Scene")] [SerializeField]
        private SceneField levelArt;
        
        [Header("Scene Settings")]
        [SerializeField] private CinemachineCamera roomCamera;
        
        [Header("Puzzle Objects")]
        [SerializeField] private Glass[] glassShards;
        [SerializeField] public List<BaseObject> baseObjects;

        [Header("Debug Settings")]
        public Vector3 playerPosition;
        
        private SaveInstance saveInstance;

        private void Start() {
            roomCamera.Priority = 1;
            _ = GameSceneLoaderSystem.Instance.LoadSceneAsync(levelArt);
        }

        public void PopulateLevel() {
            GameInitializer.Instance.RepopulateInteractableOnLoadLevel();
            GameInitializer.Instance.AddShards(glassShards);
        }

        public void BindData() => saveInstance.Bind();
        public GameData GetGameData() => saveInstance.GetGameData();
        public void SetGameData(GameData gameData) => saveInstance.SetGameData(gameData);
        public List<Glass> GetAllShards() => saveInstance.GetShards();

        #if UNITY_EDITOR
        public void SetPlayerPos(Vector3 pos) {
            playerPosition = pos;
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
        
        public void SetInteractable() {
            baseObjects = new List<BaseObject>();
            
            //Set interactable
            baseObjects.AddRange(FindObjectsByType<BaseObject>(FindObjectsSortMode.None));
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            
            if(saveInstance == null) saveInstance = GetComponent<SaveInstance>();
            saveInstance.SetObjectData(baseObjects.ToArray(), glassShards);
        }
        #endif
    }

    [Serializable]
    public class FragmentData {
        [SerializeField] public Glass glassShards;
        public Vector3 position;
        public bool spawned;
    }
}