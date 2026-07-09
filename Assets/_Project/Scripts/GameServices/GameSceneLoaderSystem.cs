using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.UI;
using Unity.VisualScripting;
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
        private bool newGameStarted = false;
        
        private void Start() {
            scenesToLoad = new List<SceneField>();
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (SceneManager.loadedSceneCount == 1 && SceneManager.GetSceneAt(0).name == "PersistentSceneManager") {
                _ = LoadSceneAsync(menuScene);
            }
            #if UNITY_EDITOR
            else {
                if (GameSceneSettings.HasInstance) {
                    GameInitializer.Instance.CreateNewSave();
                    _ = LoadSceneAsync(GameSceneSettings.Instance.levelArt);
                }
                
                
                if (!PlayerService.HasInstance) Instantiate(player);
                if (!HudManager.HasInstance) Instantiate(hudManager);
            
                GameInitializer.Instance.InitializeDebugSystems();

                StartCoroutine(SetSceneWithDelay());
            }
            #endif
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        IEnumerator SetSceneWithDelay() {
            yield return new WaitForNextFrameUnit();

            GameInitializer.Instance.UpdateAmbientLoop(SceneManager.GetActiveScene().buildIndex);
            
            if (GameSceneSettings.HasInstance) {
                GameInitializer.Instance.PopulateLevel(GameSceneSettings.Instance.baseObjects.ToArray(), GameSceneSettings.Instance.glassShards);
                
                PlayerController.Instance.movement.SetPosition(GameSceneSettings.Instance.playerPosition, Direction.Up);
                
                // GameInitializer.Instance.SaveData();
                // PlayerController.Instance.triggerEnterRoom = true;
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
        
#endif
        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.buildIndex == 12) {
                loadCredits = true;
            }
            
            GameInitializer.Instance.UpdateAmbientLoop(scene.buildIndex);
        }
        
        public void NewGame() 
            => _ = StartNewGame();
        
        public void LoadGame(string sceneName = "") 
            => _ = LoadSave(sceneName == "" ? GameInitializer.Instance.GetLastScene() : sceneName);
        
        public void LoadMenu() 
            => _ = LoadMenuAsync();
        
        private async Task LoadSceneAsync(SceneField newScene) {
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
                    await Task.Yield();
                }
            }
        }
        
        public async Task LoadGameplaySceneAsync(SceneSettings sceneSettings) {
            try {
                scenesToLoad.Clear();
                GameInitializer.Instance.SaveData();
                
                await FadeToBlack();
                
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

                await FadeToGame();
            }
            catch (Exception e) {
                Debug.LogError("LoadGameplaySceneAsync failed: It most likely is a need to SetInteractable in the P_SceneSettings prefab\n" + e);
                EventBus<FadeObject>.Raise(new FadeObject {
                    show = false
                });
            }
        }
        
        private async Task UnloadGameplaySceneAsync() {
            try {
                await UnloadSceneAsync();
                GameInitializer.Instance.EmptyAll();
                
                await Task.Yield();
                
                if (GameSceneSettings.HasInstance) {
                    GameInitializer.Instance.PopulateLevel(GameSceneSettings.Instance.baseObjects.ToArray(), GameSceneSettings.Instance.glassShards);
                }

                if (newGameStarted) {
                    newGameStarted = false;
                    return;
                }
                
                GameInitializer.Instance.LoadData();
            }
            catch (Exception e) {
                Debug.LogError("Unload Gameplay failed: \n" + e);
            }
        }
        
        private async Task LoadMenuAsync() {
            GameInitializer.Instance.SaveData();
            scenesToLoad.Clear();
            
            await FadeToBlack();
            
            await UnloadSceneAsync();
            
            GameInitializer.Instance.DisposeShards();
            GameInitializer.Instance.SetEditableArea(false, ColorEnum.Both);
            
            if(PlayerService.HasInstance) Destroy(PlayerService.Instance.gameObject);
            if(HudManager.HasInstance) Destroy(HudManager.Instance.gameObject);
            
            _ = LoadSceneAsync(menuScene);

            Time.timeScale = 1;

            await FadeToGame();
        }

        private async Task StartNewGame() {
            GameInitializer.Instance.CreateNewSave();
            scenesToLoad.Clear();
            
            await FadeToBlack();

            await LoadSceneAsync(newGameScene);
            await LoadSceneAsync(GameSceneSettings.Instance.levelArt);

            newGameStarted = true;
            
            _ = UnloadGameplaySceneAsync();
            
            if (!PlayerService.HasInstance) Instantiate(player);
            if (!HudManager.HasInstance) Instantiate(hudManager);
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            GameInitializer.Instance.InitializeDebugSystems();
            #endif
            
            GameInitializer.Instance.SaveData();
            PlayerController.Instance.triggerEnterRoom = true;
            
            await FadeToGame();
        }
        
        private async Task LoadSave(string lastOpenScene) {
            try {
                GameInitializer.Instance.LoadGame();
                scenesToLoad.Clear();

                await FadeToBlack();
                
                var foundScene = false;
                foreach (var scene in allScenes) {
                    if (scene.SceneName != lastOpenScene) continue;
                    foundScene = true;
                    await LoadSceneAsync(scene);
                    break;
                }

                if (!foundScene) {
                    Debug.LogError($"Failed to find scene {lastOpenScene}");
                    return;
                }
                
                await LoadSceneAsync(GameSceneSettings.Instance.levelArt);
                
                _ = UnloadGameplaySceneAsync();
                
                if (!PlayerService.HasInstance) Instantiate(player);
                if (!HudManager.HasInstance) Instantiate(hudManager);
                
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
                GameInitializer.Instance.InitializeDebugSystems();
                #endif

                await FadeToGame();
                GameInitializer.Instance.LoadPlayerData();
            }
            catch (Exception e) {
                Debug.LogError("LoadSaveGame failed: \n" + e);
            }
        }
        
        private async Task FadeToBlack() {
            EventBus<FadeObject>.Raise(new FadeObject {
                show = true
            });
            await Task.Delay(500);
        }
        private static async Task FadeToGame() {
            await Task.Delay(300);
            EventBus<FadeObject>.Raise(new FadeObject {
                show = false
            });
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
        public static implicit operator string( SceneField sceneField ) {
            return sceneField.SceneName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            
            var sceneAsset = property.FindPropertyRelative("m_SceneAsset");
            var sceneName = property.FindPropertyRelative("m_SceneName");
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            if (sceneAsset != null) {
                sceneAsset.objectReferenceValue = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false); 

                if( sceneAsset.objectReferenceValue != null ) {
                    sceneName.stringValue = ((SceneAsset)sceneAsset.objectReferenceValue).name;
                }
            }
            EditorGUI.EndProperty( );
        }
    }
#endif
}