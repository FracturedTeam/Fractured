using System;
using _Project.Scripts.ECS;
using _Project.Scripts.Systems.Singletons;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameSceneSettings : Singleton<GameSceneSettings> {
        [Header("Level Art Scene")] [SerializeField]
        private SceneField levelArt;
        
        [Header("Scene Settings")]
        [SerializeField] private CinemachineCamera roomCamera;
        [SerializeField] public Glass[] glassShards;
        
        [Header("Game Service")]
        [SerializeField] private GameInitializer gameInitializer;

        protected override void Awake() {
            base.Awake();
            if(!GameInitializer.HasInstance) Instantiate(gameInitializer);
        }

        private void Start() {
            _ = GameSceneLoaderSystem.Instance.LoadSceneAsync(levelArt);
            
            GameInitializer.Instance.AddShards(glassShards);
            
            roomCamera.Priority = 1;
        }
    }

    [Serializable]
    public class FragmentData {
        [SerializeField] public Glass glassShards;
        public Vector3 position;
    }
}