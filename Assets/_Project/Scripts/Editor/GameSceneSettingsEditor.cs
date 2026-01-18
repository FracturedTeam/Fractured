using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor {
    [CustomEditor(typeof(GameSceneSettings))]
    public class GameSceneSettingsEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var sceneSettings = (GameSceneSettings)target;
            
            DrawDefaultInspector();

            if (GUILayout.Button("Set Player Pos")) {
                sceneSettings.SetPlayerPos(FindAnyObjectByType<PlayerController>().transform.position);
            }
        }
    }
}