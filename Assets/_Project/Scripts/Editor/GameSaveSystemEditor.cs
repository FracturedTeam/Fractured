using _Project.Scripts.GameServices;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor {
    // [CustomEditor(typeof(GameSaveSystem))]
    // public class GameSaveSystemEditor : UnityEditor.Editor {
    //     public override void OnInspectorGUI() {
    //         var saveSystem = (GameSaveSystem) target;
    //         var gameName = saveSystem.saveFile.CurrentScene;
    //         
    //         DrawDefaultInspector();
    //         
    //         if(GUILayout.Button("Save Game"))
    //             saveSystem.SaveGame();
    //         
    //         if(GUILayout.Button("Load Game"))
    //             saveSystem.LoadData(saveSystem.saveFile.CurrentScene);
    //         
    //         if(GUILayout.Button("Delete Game"))
    //             saveSystem.DeleteGame(gameName);
    //     }
    // }
}