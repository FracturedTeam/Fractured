using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Singletons;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts.GameServices {
    public class GameSceneSettings : Singleton<GameSceneSettings> {
        [Header("Level Art Scene")] 
        [SerializeField] public SceneField levelArt;
        
        [Header("Scene Settings")]
        [SerializeField] public CinemachineCamera roomCamera;
        [SerializeField] public int ActColor = 1;
        [SerializeField] public TransitionTextSO transitionTextSO;
        
        [Header("Puzzle Objects")]
        [SerializeField] public List<BaseObject> baseObjects;
        [SerializeField] public List<SceneMaster> sceneMasters;
        
        [Header("Debug Settings")]
        public Vector3 playerPosition;
        
        private SaveInstance saveInstance;
        private Volume volume;
        
        private void Start() {
            if(saveInstance == null)
                saveInstance = GetComponent<SaveInstance>();
            
            roomCamera.Priority = 1;
        }

        public void UpdateVolumeWeight(float intensity) {
            if(volume != null)
                volume.weight = 1 - intensity;
            else {
                var vol = FindAnyObjectByType<Volume>();
                if(vol != null) {
                    volume = vol;
                    volume.weight = 1 - GameInitializer.Instance.GetSettings.enviroColorIntensity;
                }
            }
        }

        public void BindData(bool firstTimeBind) => saveInstance.Bind(firstTimeBind);
        public SceneData GetSceneData() => saveInstance.GetGameData();
        public void SetSceneData(SceneData objectData) => saveInstance.SetGameData(objectData);
        public List<Glass> GetAllShards() => saveInstance.GetShards();

        public void ForceSetInteractableColor() {
            foreach (var baseObject in baseObjects)
                baseObject.GetTextInteractable?.ForceSet();
        }

        #if UNITY_EDITOR
        public void SetPlayerPos(Vector3 pos) {
            playerPosition = pos;
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
        
        public void SetInteractable() {
            baseObjects = new List<BaseObject>();
            sceneMasters =  new List<SceneMaster>();
            
            //Set interactable
            baseObjects.AddRange(FindObjectsByType<BaseObject>(FindObjectsSortMode.None));
            sceneMasters.AddRange(FindObjectsByType<SceneMaster>(FindObjectsSortMode.None));
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            
            if(saveInstance == null) saveInstance = GetComponent<SaveInstance>();
            saveInstance.SetObjectData(baseObjects.ToArray(), sceneMasters.ToArray());
        }
        #endif
    }
}