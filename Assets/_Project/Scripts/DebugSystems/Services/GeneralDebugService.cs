using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.DebugSystems.Services {
    public class GeneralDebugService : IDebugSystem, IDebugGUI, IDebugGizmos{
        private readonly DebugUIState debugUIState;
        
        public GeneralDebugService(DebugUIState debugUI) {
            debugUIState = debugUI;
        }
        
        public void Dispose() {
        }

        public void Initialize() {
        }

        public void Tick() {
        }

        public void DrawDebugGUI() {
            if(!debugUIState.IsVisible("General")) return;
            
            var headerStyle = new GUIStyle(GUI.skin.label) {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                alignment = TextAnchor.UpperLeft
            };
            
            var sectionStyle = new GUIStyle(GUI.skin.label) {
                fontStyle = FontStyle.Bold,
                fontSize = 10,
                alignment = TextAnchor.MiddleLeft,
                normal = {
                    textColor = Color.crimson
                },
                hover = {
                    textColor = Color.crimson
                },
            };

            var debugStyle = new GUIStyle(GUI.skin.label) {
                fontStyle = FontStyle.Normal,
                fontSize = 10,
                alignment = TextAnchor.MiddleLeft
            };

            var buttonStyle = new GUIStyle(GUI.skin.button) {
                fontStyle = FontStyle.Bold,
                fontSize = 10,
                alignment = TextAnchor.MiddleCenter,
                normal = {
                textColor = Color.ghostWhite
                }
            };
            
            GUILayout.BeginVertical("box");
            GUILayout.Label("General Debug", headerStyle);
            GUILayout.Label("Game", sectionStyle);
            if(GUILayout.Button("Quit Game", buttonStyle))
                Application.Quit();
            
            GUILayout.Label("Save System", sectionStyle);
            if(GUILayout.Button("Save game", buttonStyle))
                GameSaveSystem.Instance.SaveGame();
            if(GUILayout.Button("Load save", buttonStyle))
                GameSaveSystem.Instance.LoadGame();
            /*if(GUILayout.Button("Start New Game", buttonStyle))
                GameSceneLoaderSystem.Instance.NewGame();*/
            
            GUILayout.Label("Scenes", sectionStyle);
            for(var i = 0; i < GameSceneLoaderSystem.Instance.allScenes.Length; i++) {
                var s =  GameSceneLoaderSystem.Instance.allScenes[i];
                if (GUILayout.Button($"Load : {s.SceneName}", buttonStyle)) {
                    if (PlayerController.Instance.interact.HasObject)
                        PlayerController.Instance.interact.SetDropObjectDebug();
                    _ = GameSceneLoaderSystem.Instance.LoadSceneFromDebug(s);
                }
            }
            
            GUILayout.EndVertical();
        }

        public void DrawDebugGizmos() {
        }
    }
}