using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.DebugSystems.Services {
    public class CameraDebugService : IDebugSystem, IDebugGUI, IDebugGizmos {

        private readonly CinemachineBrain currentCamera;
        private readonly DebugUIState debugUIState;
        private readonly CinemachineCamera[] cameras;
        private readonly CountdownTimer countdownTimer = new (0.1f);
        
        public CameraDebugService(DebugUIState debugUI, CinemachineCamera[] cameras) {
            debugUIState = debugUI;
            currentCamera = PlayerController.Instance.cinemachineBrain;
            this.cameras = cameras;
        }
        
        public void Initialize() {
            countdownTimer.OnTimerStop += GameInitializer.Instance.RepositionGlass;
        }

        public void Tick() {
        }

        public void DrawDebugGUI() {
            if(!debugUIState.IsVisible("Camera")) return;
            
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
            GUILayout.Label("Camera Debug Service", headerStyle);
            GUILayout.Label("Cinemachine Brain", sectionStyle);
            GUILayout.Label($"Current active camera : {currentCamera.ActiveVirtualCamera.Name}", debugStyle);
            GUILayout.Label($"Is camera switching : {currentCamera.IsBlending}", debugStyle);
            GUILayout.Label($"Camera blending time : {currentCamera.DefaultBlend.BlendTime}", debugStyle);
            GUILayout.Label("All Cameras", sectionStyle);
            foreach (var cam in cameras) {
                if (!GUILayout.Button($"Camera : {cam.Name}", buttonStyle)) continue;
                foreach (var c in cameras)
                    c.Priority = 0;
                
                cam.Priority = 1;
                
                countdownTimer.Start();
            }
            GUILayout.EndVertical();
        }

        public void DrawDebugGizmos() {
        }
        
        public void Dispose() {
            countdownTimer.OnTimerStop -= GameInitializer.Instance.RepositionGlass;
            countdownTimer.Dispose();
            
        }
    }
}