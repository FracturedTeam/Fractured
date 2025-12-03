using _Project.Scripts.ECS;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameServices {
    public class GameSceneSettings : MonoBehaviour {
        [SerializeField] private CinemachineCamera roomCamera;
        [SerializeField] private Glass[] glassShards;
        
        private void OnEnable() {
            roomCamera.Priority = 1;
            
            GameInitializer.Instance.AddShards(glassShards);
        }
        
    }
}