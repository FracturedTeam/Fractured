using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Singletons;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameServices {
    public class GameSceneLoaderSystem : Singleton<GameSceneLoaderSystem> {
        private List<SceneField> scenesToLoad = new List<SceneField>();
        [SerializeField] private SceneField[] persistentScenes;
        
        public bool levelIsLoading { get; private set; }

        private void Start() {
            /*var toLoad = new HashSet<string>(scenesToLoad.Select(s => s.SceneName));
            var sceneCount = SceneManager.sceneCount;

            for (var i = sceneCount - 1; i > 0; i--) {
                var sceneAt = SceneManager.GetSceneAt(i);
                if(sceneAt.isLoaded) continue;
                
                if(!toLoad.Contains(sceneAt.name)) continue;
                
                var loading = SceneManager.LoadSceneAsync(sceneAt.name, LoadSceneMode.Additive);
                Debug.Log($"Loading scene {sceneAt.name}");
            }
            
            GameInitializer.Instance.EmptyInteractable();
            GameInitializer.Instance.RepopulateInteractable();*/
        }

        public async Task LoadSceneAsync(SceneSettings sceneSettings) { //Handle ce qu'il faut pour déplacer le joueur etc.
            levelIsLoading = true;
            
            GameSaveSystem.Instance.SaveGame();
            
            UnloadObjects();
            
            scenesToLoad.Clear();
            scenesToLoad.AddRange(persistentScenes);
            scenesToLoad.Add(sceneSettings.levelDesign);
            
            var loadingLevel = SceneManager.LoadSceneAsync(sceneSettings.levelDesign, LoadSceneMode.Additive);

            if (loadingLevel is null) {
                Debug.LogError($"Failed to load LD scene {sceneSettings.levelDesign.SceneName}, Verify Build Settings");
                return;
            }
            
            while (!loadingLevel.isDone) {
                await Task.Delay(100);
            }
            
            //Une fois la scène chargé, attend de switch de caméra avant de décharger les autres scènes
            PlayerController.Instance.movement.SetPosition(sceneSettings.playerPosition, sceneSettings.direction);
            GameInitializer.Instance.ResetCameras();
            
            while(PlayerController.Instance.cinemachineBrain.IsBlending)
                await Task.Yield();

            levelIsLoading = false;
            Debug.Log($"Load scene {sceneSettings.levelDesign.SceneName} Successfully");
            
            await UnloadSceneAsync();
        }
        
        public async Task UnloadSceneAsync() {
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
            
            GameInitializer.Instance.RepopulateInteractable();

            await Task.Delay(100);
            
            //Save System Load data
            GameSaveSystem.Instance.LoadGame();
        }

        public async Task LoadLevelArtAsync(SceneField levelArt) {
            scenesToLoad.Add(levelArt);
            
            var loadingArt = SceneManager.LoadSceneAsync(levelArt, LoadSceneMode.Additive);

            if (loadingArt is null) {
                Debug.LogError($"Failed to load Art scene {levelArt.SceneName}, Verify Build Settings Or if it is Referenced");
                return;
            }
            
            while (loadingArt is { isDone: false }) {
                await Task.Delay(100);
            }

            Debug.Log($"Load scene {levelArt.SceneName} Successfully");
        }

        public void SetSceneToLoad(SceneField[] scenes) {
            scenesToLoad.Clear();
            scenesToLoad.AddRange(scenes);
        }
        
        private void UnloadObjects() {
            GameInitializer.Instance.EmptyInteractable();
            GameInitializer.Instance.EmptyShards();
        }
    }
    
    [Serializable]
    public class SceneSettings {
        public SceneField levelDesign;
        public Vector3 playerPosition;
        public Direction direction;
    }
    
    [Serializable]
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