using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameServices {
    public class GameSceneLoaderSystem : PersistentSingleton<GameSceneLoaderSystem> {
        private List<SceneField> scenesToLoad;
        
        [SerializeField] private SceneField menuScene;
        [SerializeField] private SceneField newGameScene;

        [SerializeField] public SceneField[] allScenes;
        
        private void Start() {
            scenesToLoad = new List<SceneField>();
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (SceneManager.loadedSceneCount == 1 && SceneManager.GetSceneAt(0).name == "PersistentSceneManager") {
                _ = LoadSceneAsync(menuScene);
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            /*if (scene.name == newGameScene) {
                var unload = UnloadGameplaySceneAsync();
            }*/
        }

        public async Task LoadSceneAsync(SceneField newScene) {
            scenesToLoad.Add(newScene);
            
            var loading = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

            if (loading is null) {
                Debug.LogError($"Failed to load scene {newScene.SceneName}, Verify Build Settings Or if it is Referenced");
                return;
            }
            
            while (loading is { isDone: false }) {
                await Task.Yield();
            }
        }
        
        public async Task LoadGameplaySceneAsync(SceneSettings sceneSettings) {
            scenesToLoad.Clear();
            
            await LoadSceneAsync(sceneSettings.levelDesign);
            
            PlayerController.Instance.movement.SetPosition(sceneSettings.playerPosition, sceneSettings.direction);

            //Faire une fonction qui active le blend je penses pour ça
            /*while (PlayerController.Instance.cinemachineBrain.IsBlending) {
                await Task.Delay(4000);
            }*/

            _ = UnloadGameplaySceneAsync();
        }

        public async Task LoadSceneFromDebug(SceneField scene) {
            GameSaveSystem.Instance.SaveGame();
            GameInitializer.Instance.EmptyAll();
            GameInitializer.Instance.ResetCameras();
            
            scenesToLoad.Clear();

            await LoadSceneAsync(scene);
            await UnloadGameplaySceneAsync();
            
            //Input la position joueur a spawn lorsqu'il entre dans la salle
            PlayerController.Instance.movement.SetPosition(GameSceneSettings.Instance.playerPosition, Direction.Up);
        }
        
        private async Task UnloadGameplaySceneAsync() {
            await UnloadSceneAsync();
            
            GameInitializer.Instance.RepopulateInteractableOnLoadLevel();
        }
        
        private async Task UnloadSceneAsync() {
            var keepScenes = new HashSet<string>(scenesToLoad.Select(s => s.SceneName));
            var scenesToUnload = new List<string>();
            var sceneCount = SceneManager.sceneCount;
            
            for (var i = sceneCount - 1; i > 0; i--) {
                if(!SceneManager.GetSceneAt(i).isLoaded) continue;
                if(keepScenes.Contains(SceneManager.GetSceneAt(i).name)) continue;
                
                scenesToUnload.Add(SceneManager.GetSceneAt(i).name);
            }

            foreach (var scene in scenesToUnload) {
                var unload = SceneManager.UnloadSceneAsync(scene);
                if(unload == null) continue;

                while (!unload.isDone) {
                    await Task.Delay(10);
                }
            }
        }

        public void LoadMenu() {
            GameSaveSystem.Instance.SaveGame();
            scenesToLoad.Clear();
            
            _ = UnloadSceneAsync();
            _ = LoadSceneAsync(menuScene);
            
            Destroy(PlayerService.Instance.gameObject);
            Destroy(GameInitializer.Instance.gameObject);
            Destroy(HudManager.Instance.gameObject);
        }
        
        public void NewGame() {
            _ = StartNewGame();
        }

        private async Task StartNewGame() {
            scenesToLoad.Clear();
            await LoadSceneAsync(newGameScene);
            _ = UnloadGameplaySceneAsync();
        }
        
        public void LoadGame(string sceneName) {
            _ = LoadSave(sceneName);
        }

        private async Task LoadSave(string sceneName) {
            scenesToLoad.Clear();
            
            var foundScene = false;
            foreach (var scene in allScenes) {
                if (scene.SceneName != sceneName) continue;
                foundScene = true;
                await LoadSceneAsync(scene);
                break;
            }

            if (!foundScene) {
                Debug.LogError($"Failed to find scene {sceneName}");
                return;
            }
            
            _ = UnloadGameplaySceneAsync();
            GameSaveSystem.Instance.LoadPlayerData();
        }

        public List<SceneField> GetLoadedScenes() {
            return scenesToLoad;
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