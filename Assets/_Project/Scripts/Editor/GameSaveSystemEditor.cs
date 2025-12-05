using _Project.Scripts.GameServices;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor {
    [CustomEditor(typeof(GameSaveSystem))]
    public class GameSaveSystemEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var saveSystem = (GameSaveSystem) target;
            var gameName = saveSystem.gameData.Name;
            
            DrawDefaultInspector();
            
            if(GUILayout.Button("Save Game"))
                saveSystem.SaveGame();
            
            if(GUILayout.Button("Load Game"))
                saveSystem.LoadGame(saveSystem.gameData.Name);
            
            if(GUILayout.Button("Delete Game"))
                saveSystem.DeleteGame(gameName);
            
            if(GUILayout.Button("Set Interactable"))
                saveSystem.GetInteractables();
        }
    }
}