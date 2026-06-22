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
    
    public class SaveInstance : MonoBehaviour {
        [SerializeField] public GameData gameData;
        [SerializeField] public List<BaseObject> baseObjects;
        [SerializeField] private List<Glass> shards;
        
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

        public GameData GetGameData() {
            return gameData;   
        }

        public void SetGameData(GameData data) {
            gameData = data;
        }
        
        public List<Glass> GetShards() {
            return shards;
        }
        
#if UNITY_EDITOR
        public void SetObjectData(BaseObject[] _baseObjects, Glass[] _shards) {
            baseObjects = new List<BaseObject>();
            shards = new List<Glass>();
            gameData.ObjectDatas = new List<ObjectData>();
            gameData.FragmentDatas = new List<FragmentData>();
            
            //Set Shards
            shards.AddRange(_shards); 
            
            foreach (var shard in shards) {
                gameData.FragmentDatas.Add(new FragmentData{glassShards = shard});
            }
            
            //Set interactable
            baseObjects.AddRange(_baseObjects);
            
            foreach (var interactable in baseObjects) {
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