using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Editor {
   #if UNITY_EDITOR
    
    [InitializeOnLoad]
    public static class DefaultSceneLoader {
        private const string PrefKey = "DefaultSceneLoader.playWithPersistent";

        public static bool playWithPersistent {
            get => EditorPrefs.GetBool(PrefKey, false);
            set => EditorPrefs.SetBool(PrefKey, value);
        }

        static DefaultSceneLoader() {
            EditorApplication.playModeStateChanged += LoadDefaultScene;
        }

        private static void LoadDefaultScene(PlayModeStateChange state) {
            if(!playWithPersistent) return;
            
            if(state == PlayModeStateChange.ExitingEditMode)
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            if (state == PlayModeStateChange.EnteredPlayMode) {
                if(SceneManager.GetActiveScene().buildIndex == 0) return;

                if (SceneManager.GetActiveScene().buildIndex == 1) {
                    SceneManager.LoadScene(0);
                    return;
                }
                
                var currentScene = SceneManager.GetActiveScene().path;
                
                SceneManager.LoadScene(0);
                SceneManager.LoadScene(currentScene, LoadSceneMode.Additive);
            }
        }
    }
    
    #endif
}