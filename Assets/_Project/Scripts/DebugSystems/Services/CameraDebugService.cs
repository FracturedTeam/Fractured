using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.DebugSystems.Services {
    public class CameraDebugService : IDebugSystem, IDebugGUI, IDebugGizmos {

        private readonly CinemachineBrain camera;
        private readonly DebugUIState debugUIState;
        
        public CameraDebugService(DebugUIState debugUI) {
            debugUIState = debugUI;
            camera = PlayerController.Instance.cinemachineBrain;
        }
        
        public void Initialize() {
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
                }
            };

            var debugStyle = new GUIStyle(GUI.skin.label) {
                fontStyle = FontStyle.Normal,
                fontSize = 10,
                alignment = TextAnchor.MiddleLeft
            };

            GUILayout.BeginVertical("box");
            GUILayout.Label("Camera Debug Service", headerStyle);
            GUILayout.Label("Cinemachine Brain", sectionStyle);
            GUILayout.Label($"Current active camera : {camera.ActiveVirtualCamera.Name}", debugStyle);
            GUILayout.Label($"Is camera switching : {camera.IsBlending}", debugStyle);
            GUILayout.Label($"Camera blending time : {camera.DefaultBlend.BlendTime}", debugStyle);
            GUILayout.EndVertical();
        }

        public void DrawDebugGizmos() {
        }
        
        public void Dispose() {
        }
    }
}