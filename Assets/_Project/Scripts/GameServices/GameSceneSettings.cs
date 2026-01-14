using System;
using _Project.Scripts.ECS;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.Systems.Timers;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameServices {
    public class GameSceneSettings : Singleton<GameSceneSettings> {
        [Header("Level Art Scene")] [SerializeField]
        private SceneField levelArt;
        
        [Header("Scene Settings")]
        [SerializeField] private CinemachineCamera roomCamera;
        [SerializeField] public Glass[] glassShards;
        
        [Header("Game Service")]
        [SerializeField] private GameInitializer gameInitializer;

        [Header("Debug Settings")]
        public Vector3 playerPosition;
        
        bool hasInitializedGame = false;

        protected override void Awake() {
            base.Awake();
            if (!GameInitializer.HasInstance) Instantiate(gameInitializer);
        }

        private void Start() {
            roomCamera.Priority = 1;
            _ = GameSceneLoaderSystem.Instance.LoadSceneAsync(levelArt);
        }

        public void ResetShard() {
            GameInitializer.Instance.AddShards(glassShards);
        }

        public void SetPlayerPos(Vector3 pos) {
            playerPosition = pos;
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
    }

    [Serializable]
    public class FragmentData {
        [SerializeField] public Glass glassShards;
        public Vector3 position;
    }
}