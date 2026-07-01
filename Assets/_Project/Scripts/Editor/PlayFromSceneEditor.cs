using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR

namespace _Project.Scripts.Editor {
    [ExecuteInEditMode]
    public class PlayFromSceneEditor : EditorWindow {
        [SerializeField] private string lastScene = "";
        [SerializeField] private int targetScene = 0;
        [SerializeField] private string waitScene = null;
        [SerializeField] private bool hasPlayed = false;
    
        private static string PersistentScenePath = "Assets/_Project/Scenes/Persistent/PersistentSceneManager.unity";
        private static string TargetScene = "";

        [MenuItem("Edit/Open Scene %0")]
        public static void Run() {
            EditorWindow.GetWindow<PlayFromSceneEditor>();
        }

        private static string[] sceneNames;
        private static EditorBuildSettingsScene[] scenes;

        void OnEnable() {
            scenes = EditorBuildSettings.scenes;
            sceneNames = scenes.Select(x => (Path.GetFileNameWithoutExtension(x.path))).ToArray();
        }

        void OnGUI() {
            if (EditorApplication.isPlaying) {
                if (EditorSceneManager.GetActiveScene().path == waitScene) {
                    waitScene = null;
                }
                GUILayout.Label("Editor is in play mode", EditorStyles.boldLabel);
            
                return;
            }
        
            if(null == sceneNames) return;
        
            targetScene = EditorGUILayout.Popup("Target Scene", targetScene, sceneNames);
            if (GUILayout.Button("Open Scene")) {
                lastScene = EditorSceneManager.GetActiveScene().path;
                waitScene = scenes[targetScene].path;
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(waitScene);
                TargetScene = waitScene;
            }
        }
    }
}

#endif