using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Systems.Singletons;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameServices {
    public class GameSceneLoaderSystem : Singleton<GameSceneLoaderSystem> {
        private List<SceneField> scenesToLoad = new List<SceneField>();
        [SerializeField] private SceneField[] persistentScenes;
        
        public bool LoadingLevel { get; private set; }
        
        public async Task LoadSceneAsync(SceneField sceneField) { //Handle ce qu'il faut pour déplacer le joueur etc.
            LoadingLevel = true;
            
            scenesToLoad.Clear();
            scenesToLoad.AddRange(persistentScenes);
            scenesToLoad.Add(sceneField);
            
            var loading = SceneManager.LoadSceneAsync(sceneField, LoadSceneMode.Additive);
            
            while (!loading.isDone) {
                await Task.Delay(100);
            }
            
            Debug.Log($"Load scene {sceneField.SceneName}");
            
            await UnloadSceneAsync();
        }

        private async Task UnloadSceneAsync() {
            var keepScenes = new HashSet<string>(scenesToLoad.Select(s => s.SceneName));
            
            var scenesToUnload = new List<string>();
            var sceneCount = SceneManager.sceneCount;

            for (var i = sceneCount - 1; i > 0; i--) {
                var sceneAt = SceneManager.GetSceneAt(i);
                if(!sceneAt.isLoaded) continue;
                
                if(keepScenes.Contains(sceneAt.name)) continue;
                
                var sceneName = sceneAt.name;
                scenesToUnload.Add(sceneName);
            }

            foreach (var scene in scenesToUnload) {
                var unload = SceneManager.UnloadSceneAsync(scene);
                if(unload == null) continue;

                while (!unload.isDone) {
                    await Task.Delay(100);
                }
            }

            //await Ressources.UnloadUnusedAssets();
        }
    }
    
    [System.Serializable]
    public class SceneField
    {
        [SerializeField]
        private Object m_SceneAsset;

        [SerializeField]
        private string m_SceneName = "";
        public string SceneName => m_SceneName;

        // makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string( SceneField sceneField )
        {
            return sceneField.SceneName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            EditorGUI.BeginProperty(_position, GUIContent.none, _property);
            var sceneAsset = _property.FindPropertyRelative("m_SceneAsset");
            var sceneName = _property.FindPropertyRelative("m_SceneName");
            _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
            if (sceneAsset != null)
            {
                sceneAsset.objectReferenceValue = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false); 

                if( sceneAsset.objectReferenceValue != null )
                {
                    sceneName.stringValue = ((SceneAsset)sceneAsset.objectReferenceValue).name;
                }
            }
            EditorGUI.EndProperty( );
        }
    }
#endif
}