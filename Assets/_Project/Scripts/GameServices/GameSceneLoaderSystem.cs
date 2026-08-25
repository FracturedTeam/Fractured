using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
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
                    if(!SceneManager.GetSceneByName(GameSceneSettings.Instance.levelArt).isLoaded)
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

            if (!GameSceneSettings.HasInstance) yield break;
            
            var settings = GameSceneSettings.Instance;
            GameInitializer.Instance.PopulateLevel(settings.baseObjects.ToArray(), settings.sceneMasters.ToArray());
            PlayerController.Instance.Movement.SetPosition(settings.playerPosition, Direction.Up);
            GameInitializer.Instance.SetCurrentChapter(settings.ActColor);
        }
        
        public async Task LoadSceneFromDebug(SceneField scene) {
            GameInitializer.Instance.SaveData();
            GameInitializer.Instance.ResetCameras();
            
            scenesToLoad.Clear();

            await UnloadSceneAsync();
            GameInitializer.Instance.EmptyAll();
            
            await LoadSceneAsync(scene);
            await LoadSceneAsync(GameSceneSettings.Instance.levelArt);
            
            var settings = GameSceneSettings.Instance;
            GameInitializer.Instance.PopulateLevel(settings.baseObjects.ToArray(), settings.sceneMasters.ToArray());
            
            await Task.Delay(100);
            GameInitializer.Instance.LoadData();
            
            //Input la position joueur a spawn lorsqu'il entre dans la salle
            PlayerController.Instance.Movement.SetPosition(settings.playerPosition, Direction.Up);
        }
        
#endif
        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            // if (scene.buildIndex == 12) {
            //     loadCredits = true;
            // }
            
            GameInitializer.Instance.UpdateAmbientLoop(scene.buildIndex);
        }
        
        public void NewGame() => _ = StartNewGame();
        
        public void LoadGame(string sceneName = "") => _ = LoadSave(sceneName == "" ? GameInitializer.Instance.GetLastScene() : sceneName);

        public void LoadLevel(int levelIndex) {
            _ = StartNewGame(levelIndex);
        }
        
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
                InputsBrain.Instance.DisablePlayerInput(true);
                
                scenesToLoad.Clear();
                GameInitializer.Instance.SaveData();
                
                await FadeToBlack();
                
                await LoadSceneAsync(sceneSettings.levelDesign);
                
                var settings = GameSceneSettings.Instance;
                await LoadSceneAsync(settings.levelArt);

                await WaitForSecondsAsync(0.25f);
                
                DiscordRichPresence.Instance.UpdateRichPresence(settings.transitionTextSO.title, "");
                
                EventBus<TransitionTextEvent>.Raise(new TransitionTextEvent {
                    show = true,
                    title = settings.transitionTextSO.title,
                    description = settings.transitionTextSO.description,
                });

                PlayerController.Instance.Movement.SetPosition(sceneSettings.playerPosition, sceneSettings.direction);
                await UnloadGameplaySceneAsync();
                
                if (loadCredits) {
                    await UnloadSceneAsync();
                    
                    Destroy(PlayerService.Instance.gameObject);
                    Destroy(HudManager.Instance.gameObject);
                    GameInitializer.Instance.EmptyAll();
                    loadCredits = false;
                }

                InputsBrain.Instance.OnContinue += LeaveTransitionFade;
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
                GameInitializer.Instance.EmptyAll();
                await UnloadSceneAsync();
                
                await Task.Yield();

                var settings = GameSceneSettings.Instance;
                
                if (GameSceneSettings.HasInstance) {
                    GameInitializer.Instance.PopulateLevel(settings.baseObjects.ToArray(), settings.sceneMasters.ToArray());
                    GameInitializer.Instance.UpdateDebugCameras();
                    GameInitializer.Instance.SetCurrentChapter(settings.ActColor);
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
            Time.timeScale = 1;
            scenesToLoad.Clear();

            await FadeToBlack();
            
            await UnloadSceneAsync();
            
            GameInitializer.Instance.DisposeShards();
            DiscordRichPresence.Instance.UpdateRichPresence("In main menu", "");
            if(PlayerService.HasInstance) Destroy(PlayerService.Instance.gameObject);
            if(HudManager.HasInstance) Destroy(HudManager.Instance.gameObject);
            
            _ = LoadSceneAsync(menuScene);


            await FadeToGame();
        }

        private async Task StartNewGame(int index = 0) {
            InputsBrain.Instance.DisablePlayerInput(true);
            GameInitializer.Instance.CreateNewSave();
            scenesToLoad.Clear();
            
            await FadeToBlack();
            
            await LoadSceneAsync(index == 0 ? newGameScene : allScenes[index]);
            await LoadSceneAsync(GameSceneSettings.Instance.levelArt);

            DiscordRichPresence.Instance.UpdateRichPresence( "...", "");
            
            newGameStarted = true;
            
            if (!HudManager.HasInstance) Instantiate(hudManager);
            if (!PlayerService.HasInstance) Instantiate(player);
            
            _ = UnloadGameplaySceneAsync();
                            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            GameInitializer.Instance.InitializeDebugSystems();
#endif

            await WaitForSecondsAsync(0.25f);
            
            EventBus<TransitionTextEvent>.Raise(new TransitionTextEvent {
                show = true,
                title = GameSceneSettings.Instance.transitionTextSO.title,
                description = GameSceneSettings.Instance.transitionTextSO.description,
            });
            
            GameInitializer.Instance.SaveData();
            InputsBrain.Instance.OnContinue += LeaveTransitionFade;
        }
        
        private async Task LoadSave(string lastOpenScene) {
            try {
                InputsBrain.Instance.DisablePlayerInput(true);
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
                
                await WaitForSecondsAsync(0.25f);
                DiscordRichPresence.Instance.UpdateRichPresence(GameSceneSettings.Instance.transitionTextSO.title, "");
                
                EventBus<TransitionTextEvent>.Raise(new TransitionTextEvent {
                    show = true,
                    title = GameSceneSettings.Instance.transitionTextSO.title,
                    description = GameSceneSettings.Instance.transitionTextSO.description,
                });
                
                _ = UnloadGameplaySceneAsync();
                
                if (!PlayerService.HasInstance) Instantiate(player);
                if (!HudManager.HasInstance) Instantiate(hudManager);
                
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                GameInitializer.Instance.InitializeDebugSystems();
#endif
                GameInitializer.Instance.LoadPlayerData();
                InputsBrain.Instance.OnContinue += LeaveTransitionFade;
            }
            catch (Exception e) {
                Debug.LogError("LoadSaveGame failed: \n" + e);
            }
        }

        private void LeaveTransitionFade() {
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().room_Enter);
            InputsBrain.Instance.DisablePlayerInput(false);
            PlayerController.Instance.triggerEnterRoom = true;
            EventBus<TransitionTextEvent>.Raise(new TransitionTextEvent {
                show = false,
                title = "",
                description = "",
            });
            
            _ = FadeToGame();
            
            InputsBrain.Instance.OnContinue -= LeaveTransitionFade;
        }
        
        private async Task FadeToBlack() {
            EventBus<FadeObject>.Raise(new FadeObject {
                show = true
            });
            await WaitForSecondsAsync(0.5f);
        }
        
        private async Task FadeToGame() {
            await WaitForSecondsAsync(0.5f);
            EventBus<FadeObject>.Raise(new FadeObject {
                show = false
            });
        }

        private Task WaitForSecondsAsync(float seconds) {
            var task = new TaskCompletionSource<bool>();
            StartCoroutine(WaitCoroutine(seconds, task));
            return task.Task;
        }

        private IEnumerator WaitCoroutine(float seconds, TaskCompletionSource<bool> task) {
            yield return new WaitForSecondsRealtime(seconds);
            task.SetResult(true);
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