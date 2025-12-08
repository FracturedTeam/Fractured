using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Systems.Singletons;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    
    [Serializable]
    public struct GameData {
        public string SceneName;
        public List<ObjectData> ObjectDatas;
        public List<FragmentData> FragmentDatas;
    }
    
    public class SaveInstance : Singleton<SaveInstance> {
        [SerializeField] public GameData gameData;
        public List<BaseObject> baseObjects { get; private set; }
        public List<Glass> shards {get; private set;}
        
        public void Bind() {
            for(var i = 0; i < baseObjects.Count; i++) {
                gameData.ObjectDatas[i].baseObject = baseObjects[i];
                gameData.ObjectDatas[i].baseObject.Bind(gameData.ObjectDatas[i]);
            }

            for (var i = 0; i < shards.Count; i++) {
                gameData.FragmentDatas[i].glassShards = shards[i];
                gameData.FragmentDatas[i].glassShards.Bind(gameData.FragmentDatas[i]);
            }
        }
        
#if UNITY_EDITOR
        public void SetInteractables() {
            baseObjects = new List<BaseObject>();
            shards = new List<Glass>();
            gameData.ObjectDatas = new List<ObjectData>();
            gameData.FragmentDatas = new List<FragmentData>();
            
            //Set Shards
            shards.AddRange(GameSceneSettings.Instance.glassShards);
            
            foreach (var shard in GameSceneSettings.Instance.glassShards) {
                gameData.FragmentDatas.Add(new FragmentData{glassShards = shard});
            }
            
            //Set interactable
            var interactables = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
            baseObjects.AddRange(interactables);
            
            foreach (var interactable in interactables) {
                gameData.ObjectDatas.Add(new ObjectData{baseObject = interactable});
                
                //Set shard in interactable
                if (interactable.TryGetComponent(out ObtainShardInteractable shard)) {
                    shards.AddRange(shard.shards);
                    foreach (var s in shard.shards) {
                        gameData.FragmentDatas.Add(new FragmentData{glassShards = s});
                    }
                }
            }
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
#endif
    }
}