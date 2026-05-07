using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameServices {
    public class GameSceneLoaderSystem : PersistentSingleton<GameSceneLoaderSystem> {
        private List<SceneField> scenesToLoad;
        
        [SerializeField] private PlayerService player;
        [SerializeField] private HudManager hudManager;
        
        [SerializeField] private SceneField menuScene;
        [SerializeField] private SceneField newGameScene;

        [SerializeField] public SceneField[] allScenes;

        private bool loadCredits = false;
        
        private void Start() {
            scenesToLoad = new List<SceneField>();
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (SceneManager.loadedSceneCount == 1 && SceneManager.GetSceneAt(0).name == "PersistentSceneManager") {
                _ = LoadSceneAsync(menuScene);
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            ManageAudio(scene.buildIndex);

            if (scene.buildIndex == 12) {
                loadCredits = true;
            }
        }

        private void ManageAudio(int index) {
            //ManageAudio Loop
            if (index == 2) {
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    loop = Loop.ambientCoffin
                });
            }
            else if (index is > 2 and < 8) {
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    loop = Loop.ambientTuto
                });
            }
            else if (index is > 7 and < 12) {
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    loop = Loop.ambientZone1
                });
            }
            else if (index is 12) {
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    loop = Loop.credits
                });
            }
            else if(index is 0 or 1)
                EventBus<ManageAmbientAudio>.Raise(new ManageAmbientAudio {
                    loop = Loop.mainMenu
                });
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
            try {
                scenesToLoad.Clear();
                GameInitializer.Instance.SaveData();
                EventBus<FadeObject>.Raise(new FadeObject {
                    show = true
                });
                await Task.Delay(500);
                await Task.Yield();
                
                HudManager.Instance.StopEventInteraction();
                
                await LoadSceneAsync(sceneSettings.levelDesign);
                await LoadSceneAsync(GameSceneSettings.Instance.levelArt);

                PlayerController.Instance.movement.SetPosition(sceneSettings.playerPosition, sceneSettings.direction);
                await Task.Yield();
                PlayerController.Instance.triggerEnterRoom = true;

                await UnloadGameplaySceneAsync();
                
                if (loadCredits) {
                    await UnloadSceneAsync();
                    
                    Destroy(PlayerService.Instance.gameObject);
                    Destroy(GameInitializer.Instance.gameObject);
                    Destroy(HudManager.Instance.gameObject);
                    loadCredits = false;
                }
                
                await Task.Delay(500);
                EventBus<FadeObject>.Raise(new FadeObject {
                    show = false
                });
            }
            catch (Exception e) {
                Debug.LogError("LoadGameplaySceneAsync failed: It most likely is a need to SetInteractable in the P_SceneSettings prefab\n" + e);
                EventBus<FadeObject>.Raise(new FadeObject {
                    show = false
                });
            }
        }

        public async Task LoadSceneFromDebug(SceneField scene) {
            GameInitializer.Instance.SaveData();
            GameInitializer.Instance.ResetCameras();
            
            scenesToLoad.Clear();

            await UnloadSceneAsync();
            GameInitializer.Instance.EmptyAll();
            HudManager.Instance.StopEventInteraction();
            
            await LoadSceneAsync(scene);
            await LoadSceneAsync(GameSceneSettings.Instance.levelArt);
            
            if(GameSceneSettings.HasInstance)
                GameInitializer.Instance.PopulateLevel(GameSceneSettings.Instance.baseObjects.ToArray(), GameSceneSettings.Instance.glassShards);
            
            await Task.Delay(100);
            GameInitializer.Instance.LoadData();
            
            //Input la position joueur a spawn lorsqu'il entre dans la salle
            PlayerController.Instance.movement.SetPosition(GameSceneSettings.Instance.playerPosition, Direction.Up);
        }
        
        private async Task UnloadGameplaySceneAsync() {
            try {
                await UnloadSceneAsync();
                GameInitializer.Instance.EmptyAll();
                
                await Task.Delay(200);
                
                if (GameSceneSettings.HasInstance) {
                    GameInitializer.Instance.PopulateLevel(GameSceneSettings.Instance.baseObjects.ToArray(), GameSceneSettings.Instance.glassShards);
                }
                GameInitializer.Instance.LoadData();
            }
            catch (Exception e) {
                Debug.LogError("Unload Gameplay failed: \n" + e);
            }
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
            GameInitializer.Instance.SaveData();
            scenesToLoad.Clear();
            _ = LoadMenuAsync();
        }

        private async Task LoadMenuAsync() {
            EventBus<FadeObject>.Raise(new FadeObject {
                show = true
            });
            await Task.Delay(600);
            
            _ = UnloadSceneAsync();
            _ = LoadSceneAsync(menuScene);
            
            GameInitializer.Instance.DisposeShards();
            
            if(PlayerService.HasInstance) Destroy(PlayerService.Instance.gameObject);
            //if(GameInitializer.HasInstance) Destroy(GameInitializer.Instance.gameObject);
            if(HudManager.HasInstance) Destroy(HudManager.Instance.gameObject);

            Time.timeScale = 1;
            
            await Task.Delay(600);
            EventBus<FadeObject>.Raise(new FadeObject {
                show = false
            });
        }
        
        public void NewGame() {
            GameInitializer.Instance.CreateNewSave();
            _ = StartNewGame();
        }

        private async Task StartNewGame() {
            scenesToLoad.Clear();
            
            EventBus<FadeObject>.Raise(new FadeObject {
                show = true
            });
            await Task.Delay(600);
            
            await LoadSceneAsync(newGameScene);
            await LoadSceneAsync(GameSceneSettings.Instance.levelArt);
            
            _ = UnloadGameplaySceneAsync();
            
            if (!PlayerService.HasInstance) Instantiate(player);
            if (!HudManager.HasInstance) Instantiate(hudManager);
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            GameInitializer.Instance.InitializeDebugSystems();
            #endif
            
            GameInitializer.Instance.SaveData();
            PlayerController.Instance.triggerEnterRoom = true;
            
            await Task.Delay(600);
            EventBus<FadeObject>.Raise(new FadeObject {
                show = false
            });
        }
        
        public void LoadGame(string sceneName) {
            GameInitializer.Instance.LoadGame();
            _ = LoadSave(sceneName);
        }
        
        public void LoadGame() {
            GameInitializer.Instance.LoadGame();
            _ = LoadSave(GameInitializer.Instance.GetLastScene());
        }

        private async Task LoadSave(string sceneName) {
            try {
                scenesToLoad.Clear();

                EventBus<FadeObject>.Raise(new FadeObject {
                    show = true
                });
                await Task.Delay(600);
                
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
                await LoadSceneAsync(GameSceneSettings.Instance.levelArt);
                
                _ = UnloadGameplaySceneAsync();
                
                if (!PlayerService.HasInstance) Instantiate(player);
                if (!HudManager.HasInstance) Instantiate(hudManager);
                
                #if UNITY_EDITOR || DEVELOPMENT_BUILD
                GameInitializer.Instance.InitializeDebugSystems();
                #endif
                
                await Task.Delay(600);
                
                GameInitializer.Instance.LoadPlayerData();
                EventBus<FadeObject>.Raise(new FadeObject {
                    show = false
                });
            }
            catch (Exception e) {
                Debug.LogError("LoadSaveGame failed: \n" + e);
            }
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