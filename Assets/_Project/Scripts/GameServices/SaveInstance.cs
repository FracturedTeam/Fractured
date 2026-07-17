using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    
    [Serializable]
    public struct SceneData {
        public string SceneName;
        public List<ObjectData> ObjectDatas;
        public List<FragmentData> FragmentDatas;
        public List<SceneMasterSave> sceneMasterDatas;
    }
    
    public class SaveInstance : MonoBehaviour {
        [SerializeField, HideInInspector] private SceneData sceneData;
        [SerializeField, HideInInspector] private List<BaseObject> baseObjects;
        [SerializeField, HideInInspector] private List<Glass> shards;
        [SerializeField, HideInInspector] private List<SceneMaster> masters;
        
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
                
                sceneData.sceneMasterDatas = new List<SceneMasterSave>();
                foreach (var master in masters) {
                    sceneData.sceneMasterDatas.Add(new SceneMasterSave{Guid = master.Guid});
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
                for (var x = 0; x < sceneData.FragmentDatas.Count; x++) { // Pour chaque shard -> Itérer sur chaque fragment data
                    if (shards[i].Guid == sceneData.FragmentDatas[x].Guid) {
                        shards[i].Bind(sceneData.FragmentDatas[x]);
                        break;
                    }
                }
            }
            
            for(var i = 0; i < masters.Count; i++) { // Itérer a travers les baseObject
                for (var x = 0; x < sceneData.sceneMasterDatas.Count; x++) { // Pour chaque baseObject -> Itère sur chaque ObjectData pour comparer les Guid
                    if (masters[i].Guid == sceneData.sceneMasterDatas[x].Guid) {
                        masters[i].Bind(sceneData.sceneMasterDatas[x]);
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
        public void SetObjectData(BaseObject[] _baseObjects, Glass[] _shards, SceneMaster[] _masters) {
            baseObjects = new List<BaseObject>();
            shards = new List<Glass>();
            masters = new List<SceneMaster>();
            
            // Set Shards
            shards.AddRange(_shards); 
            
            //Set interactable
            baseObjects.AddRange(_baseObjects);
            
            //Set scene Master
            masters.AddRange(_masters);
            
            foreach (var interactable in _baseObjects) {
                if (String.IsNullOrEmpty(interactable.Guid)) { // Generate Object GUID
                    interactable.GenerateGuid();
                }
            }
            
            foreach (var scene in _masters) {
                if (String.IsNullOrEmpty(scene.Guid)) { // Generate Object GUID
                    scene.GenerateGuid();
                }
            }
            
            EditorUtility.SetDirty(this);
            if (!Application.isPlaying)
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
#endif
    }
}