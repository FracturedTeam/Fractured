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
        
        private void Start() {
            var task = GameSceneLoaderSystem.Instance.LoadLevelArtAsync(levelArt);
            
            roomCamera.Priority = 1;
            GameInitializer.Instance.AddShards(glassShards);
            //var unload = GameSceneLoaderSystem.Instance.UnloadSceneAsync();
        }
        
    }

    [Serializable]
    public class FragmentData {
        [SerializeField] public Glass glassShards;
        public Vector3 position;
    }
}