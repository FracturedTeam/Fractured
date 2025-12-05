using System;
using _Project.Scripts.ECS;
using _Project.Scripts.Systems.Singletons;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameSceneSettings : Singleton<GameSceneSettings> {
        [SerializeField] private CinemachineCamera roomCamera;
        [SerializeField] public Glass[] glassShards;
        
        private void Start() {
            roomCamera.Priority = 1;
            
            GameInitializer.Instance.AddShards(glassShards);
        }
        
    }

    [Serializable]
    public class FragmentData {
        [SerializeField] public Glass glassShards;
        public Vector3 position;
    }
}