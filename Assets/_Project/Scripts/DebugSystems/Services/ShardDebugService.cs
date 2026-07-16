using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using UnityEngine;

namespace _Project.Scripts.DebugSystems.Services {
    public class ShardDebugService : IDebugSystem, IDebugGUI, IDebugGizmos{

        private readonly ShardService shardService;
        private readonly DebugUIState debugUIState;
        private readonly SceneMaster[] sceneMasters;
        private readonly MemoryFrameMaster frameMaster;
        
        public ShardDebugService(ShardService shard, DebugUIState debugUI, SceneMaster[] sceneMasters,  MemoryFrameMaster frameMaster) {
            shardService = shard;
            debugUIState = debugUI;
            this.sceneMasters = sceneMasters;
            this.frameMaster = frameMaster;
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
            GUILayout.Label("Interactable Debug Service", headerStyle);
            
            GUILayout.Label("Shards", sectionStyle);
            GUILayout.Label($"{shardService.ShardCount} Shards loaded", debugStyle);
            // GUILayout.Label($"Player in editable area : {shardService.PlayerInEditableArea}", debugStyle);
            // if (GUILayout.Button($"Edit shard anywhere : {editShardAnywhere}", buttonStyle)) {
            //     editShardAnywhere = !editShardAnywhere;
            //     foreach (var shard in shardService.shards) {
            //         shard.SetEditAnywhere(editShardAnywhere);
            //     }
            // }
            
            GUILayout.Label("Interactable", sectionStyle);
            GUILayout.Label($"{shardService.InteractableCount} Interactable loaded", debugStyle);
            if (GUILayout.Button("Unlock every Blocked interactable", buttonStyle)) {
                foreach (var interactable in GameInitializer.Instance.GetInteractable()) {
                    if (interactable.GetLockState is LockedState.Locked) {
                        interactable.GetBlockedAttribute().ForceUnlock();
                    }
                }
            }
            if (GUILayout.Button("Get Every Keys", buttonStyle)) {
                foreach (var interactable in GameInitializer.Instance.GetInteractable()) {
                    if (interactable.GetObjectType is ObjectType.Collectable) {
                        var collect = interactable.GetInteract as CollectableAttribute;
                        if (collect.IsKey()) {
                            interactable.OnInteract(ObjectInteraction.Grab);
                        }
                    }
                }
            }
            
            GUILayout.Label("Scenes", sectionStyle);
            foreach (var scene in sceneMasters) {
                GUILayout.Label($"Scene : {scene.gameObject.name}", debugStyle);
                GUILayout.Label($"Is Scene Completed : {scene.IsSceneValidated}", debugStyle);
                if (GUILayout.Button("Complete Scene", buttonStyle)) {
                    scene.LoadCompleteScene();
                }
            }
            
            
            GUILayout.Label("Memory Frame", sectionStyle);
            GUILayout.Label($"Is Memory Frame Completed : {frameMaster.IsMemoryCompleted}", debugStyle);
            if (GUILayout.Button("Complete Memory Frame", buttonStyle)) {
                frameMaster.DebugCompleteFrame();
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