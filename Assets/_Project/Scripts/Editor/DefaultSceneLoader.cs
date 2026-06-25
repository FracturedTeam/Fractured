using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Editor {
   #if UNITY_EDITOR
    
    [InitializeOnLoad]
    public static class DefaultSceneLoader {

        static DefaultSceneLoader() {
            EditorApplication.playModeStateChanged += LoadDefaultScene;
        }

        private static void LoadDefaultScene(PlayModeStateChange state) {
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