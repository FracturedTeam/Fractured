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
                if(EditorSceneManager.GetActiveScene().buildIndex == 0) return;
                
                string CurrentScene = EditorSceneManager.GetActiveScene().path;
                
                SceneManager.LoadScene(0);
                SceneManager.LoadScene(CurrentScene, LoadSceneMode.Additive);
            }
        }
    }
    
    #endif
}