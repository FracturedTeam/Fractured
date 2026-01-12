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

        private CountdownTimer waitToSpawnShard = new CountdownTimer(0.5f);
        
        protected override void Awake() {
            base.Awake();
            if (!GameInitializer.HasInstance) Instantiate(gameInitializer);
        }

        private void Start() {
            roomCamera.Priority = 1;
            waitToSpawnShard.OnTimerStop += ResetShard;
            waitToSpawnShard.Start();
            
            _ = GameSceneLoaderSystem.Instance.LoadSceneAsync(levelArt);
            ManageAudio();
        }

        private void ManageAudio() {
            //ManageAudio Loop
            var index = gameObject.scene.buildIndex;
            if (index == 2) {
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    ambientSoundCoffin = true,
                    ambientSoundTuto = false,
                    ambientSoundZone1 = false
                });
            }
            else if (index > 2 && index < 8) {
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    ambientSoundCoffin = false,
                    ambientSoundTuto = true,
                    ambientSoundZone1 = false
                });
            }
            else if (index > 7 && index < 12) {
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    ambientSoundCoffin = false,
                    ambientSoundTuto = false,
                    ambientSoundZone1 = true
                });
            }
            else
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    ambientSoundCoffin = false,
                    ambientSoundTuto = false,
                    ambientSoundZone1 = false
                });
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