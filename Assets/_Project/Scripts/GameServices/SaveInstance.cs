using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    
    [Serializable]
    public struct SceneData {
        public string SceneName;
        public List<ObjectData> ObjectDatas;
        public List<FragmentData> FragmentDatas;
    }
    
    public class SaveInstance : MonoBehaviour {
        [SerializeField, HideInInspector] private SceneData sceneData;
        [SerializeField, HideInInspector] private List<BaseObject> baseObjects;
        [SerializeField, HideInInspector] private List<Glass> shards;
        
        public void Bind(bool firstTimeBind) {
            sceneData.SceneName = gameObject.scene.name;

            if (firstTimeBind) {
                sceneData.ObjectDatas = new List<ObjectData>();
                foreach (var interactable in baseObjects) {
                    sceneData.ObjectDatas.Add(new ObjectData{Guid = interactable.Guid});
                }

                sceneData.FragmentDatas = new List<FragmentData>();
                foreach (var shard in shards) {
                    sceneData.FragmentDatas.Add(new FragmentData{Guid = shard.Guid});
                }
            }
            
            for(var i = 0; i < baseObjects.Count; i++) { // Itérer a travers les baseObject
                for (var x = 0; x < sceneData.ObjectDatas.Count; x++) { // Pour chaque baseObject -> Itère sur chaque ObjectData pour comparer les Guid
                    if (baseObjects[i].Guid == sceneData.ObjectDatas[x].Guid) {
                        baseObjects[i].Bind(sceneData.ObjectDatas[x]);
                        break;
                    }
                }
            }

            for (var i = 0; i < shards.Count; i++) { // Itérer a travers les shards
                for (var x = 0; x < sceneData.ObjectDatas.Count; x++) { // Pour chaque shard -> Itérer sur chaque fragment data
                    if (shards[i].Guid == sceneData.FragmentDatas[x].Guid) {
                        shards[i].Bind(sceneData.FragmentDatas[x]);
                        break;
                    }
                }
            }
        }

        public SceneData GetGameData() {
            return sceneData;   
        }

        public void SetGameData(SceneData data) {
            sceneData = data;
        }
        
        public List<Glass> GetShards() {
            return shards;
        }
        
#if UNITY_EDITOR
        public void SetObjectData(BaseObject[] _baseObjects, Glass[] _shards) {
            baseObjects = new List<BaseObject>();
            shards = new List<Glass>();
            
            // Set Shards
            shards.AddRange(_shards); 
            
            //Set interactable
            baseObjects.AddRange(_baseObjects);
            
            foreach (var interactable in _baseObjects) {
                if (System.String.IsNullOrEmpty(interactable.Guid)) { // Generate Object GUID
                    interactable.GenerateGuid();
                }
                
                if (interactable.TryGetComponent(out ObtainShardInteractable shard)) {
                    foreach (var s in shard.shards) {
                        if (System.String.IsNullOrEmpty(s.Guid)) { // Generate Object GUID
                            s.GenerateGuid();
                        }
                    }
                    shards.AddRange(shard.shards);
                }
            }
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
#endif
    }
}