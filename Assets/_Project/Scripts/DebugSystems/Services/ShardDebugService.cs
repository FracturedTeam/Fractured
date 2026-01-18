using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using UnityEngine;

namespace _Project.Scripts.DebugSystems.Services {
    public class ShardDebugService : IDebugSystem, IDebugGUI, IDebugGizmos{

        private readonly ShardService shardService;
        private readonly DebugUIState debugUIState;
        private bool editShardAnywhere;
        public ShardDebugService(ShardService shard, DebugUIState debugUI) {
            shardService = shard;
            debugUIState = debugUI;
        }
        
        public void Initialize() {
        }

        public void Tick() {
        }

        public void DrawDebugGUI() {
            if(!debugUIState.IsVisible("Shard")) return;
            
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
            GUILayout.Label("Shard Debug Service", headerStyle);
            
            GUILayout.Label("Shards", sectionStyle);
            GUILayout.Label($"{shardService.ShardCount} Shards loaded", debugStyle);
            GUILayout.Label($"Player in editable area : {shardService.PlayerInEditableArea}", debugStyle);
            if (GUILayout.Button($"Edit shard anywhere : {editShardAnywhere}", buttonStyle)) {
                editShardAnywhere = !editShardAnywhere;
                foreach (var shard in shardService.shards) {
                    shard.SetEditAnywhere(editShardAnywhere);
                }
            }
            
            GUILayout.Label("Interactable", sectionStyle);
            GUILayout.Label($"{shardService.InteractableCount} Interactable loaded", debugStyle);
            if (GUILayout.Button("Reset interactable", buttonStyle)) {
                GameInitializer.Instance.ResetInteractable();
            }
            
            GUILayout.EndVertical();
        }
        public void DrawDebugGizmos() {
            if(!debugUIState.IsVisible("Shard")) return;
            
            Gizmos.color = Color.cornflowerBlue;
            Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
            foreach (var interactable in shardService.interactables) {
                Gizmos.DrawWireSphere(interactable.transform.position, 1f);
            }
        }
        
        public void Dispose() {
        }

    }
}