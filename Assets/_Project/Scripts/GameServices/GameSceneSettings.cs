using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameServices {
    public class GameSceneSettings : MonoBehaviour {
        [SerializeField] private CinemachineCamera roomCamera;
        
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if(mode != LoadSceneMode.Additive) return;

            roomCamera.Priority = 1;
        }
        
    }
}