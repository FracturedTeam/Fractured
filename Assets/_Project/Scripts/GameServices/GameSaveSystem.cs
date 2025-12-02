using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;

namespace _Project.Scripts.GameServices {

    [Serializable]
    public struct GameData {
        public string Name;
    }
    
    public class GameSaveSystem : Singleton<GameSaveSystem> {
        [SerializeField] public GameData gameData;
        
    }

    public struct SaveFile {
        public string fileName;
        public int currentScene;

        public Vector3 playerPosition;

        public List<SceneSaveFile> sceneFile;
    }

    [Serializable]
    public struct SceneSaveFile {
        public int buildIndex;
        public ObjectsSaveFile[] objectsSave;
    }

    [Serializable]
    public struct ObjectsSaveFile {
        public Vector3 position;
        public InteractionCompletion completion;
    }
}