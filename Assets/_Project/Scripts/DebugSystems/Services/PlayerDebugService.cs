using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.DebugSystems.Services {
    public class PlayerDebugService : IDebugSystem, IDebugGUI, IDebugGizmos {

        private readonly PlayerController player;
        private readonly DebugUIState debugUIState;
        
        public PlayerDebugService(DebugUIState debugUI) {
            player = PlayerController.Instance;
            debugUIState = debugUI;
        }
        
        public void Initialize() {
        }

        public void Tick() {
        }

        public void DrawDebugGUI() {
            if(!debugUIState.IsVisible("Player")) return;
            
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
            
            var buttonStyle = new GUIStyle(GUI.skin.button) {
                fontStyle = FontStyle.Bold,
                fontSize = 10,
                alignment = TextAnchor.MiddleCenter,
                normal = {
                    textColor = Color.ghostWhite
                }
            };

            GUILayout.BeginVertical("box");
            GUILayout.Label("Player Debug Service", headerStyle);
            
            GUILayout.Label("State Machine", sectionStyle);
            GUILayout.Label($"Current state is {player.GetCurrentState()}", debugStyle);
            
            GUILayout.Label("Movement", sectionStyle);
            GUILayout.Label($"Player current speed is {player.movement.CurrentSpeed} | Full speed is {player.movement.CurrentMaxSpeed}", debugStyle);
            GUILayout.Label($"Player current accel is {player.movement.AccelTime / player.movement.playerConfig.accelTime * 100}% | Player current decel is {player.movement.DecelTime / player.movement.playerConfig.decelTime * 100}%", debugStyle);
            GUILayout.Label($"Player time before moving {player.movement.playerConfig.timeBeforeMoving} | Current percentage {player.movement.TimeBeforeMoving / player.movement.playerConfig.timeBeforeMoving * 100}%", debugStyle);
            GUILayout.Label($"Player is on slope : {player.movement.IsOnSlope()} | Current slope angle {player.movement.CurrentSlopeAngle}° | Max walkable slope angle {player.movement.playerConfig.maxSlopeAngle}", debugStyle);
            GUILayout.Label($"Player is on ground : {player.movement.IsGrounded()} | Current fall speed {player.movement.CurrentFallSpeed} | Max fall speed {player.movement.playerConfig.maxFallSpeed}", debugStyle);
            GUILayout.Label($"Player is frozen : {player.movement.IsPlayerFrozen()}", debugStyle);
            
            GUILayout.Label("Interaction", headerStyle);
            GUILayout.Label($"{player.interact.Size} Object in the interact area", debugStyle);
            GUILayout.Label($"Can player interact with object : {player.interact.CanInteract}", debugStyle);
            GUILayout.Label($"Is player holding an object : {player.interact.HasObject}", debugStyle);
            // GUILayout.Label($"Is player in a memory : {player.interact.IsInMemory()}", debugStyle);
            
            GUILayout.Label("Debug Buttons", headerStyle);
            if(GUILayout.Button("Drop Current Object", buttonStyle))
                PlayerController.Instance.interact.SetDropObjectDebug();
            if(GUILayout.Button("Reset Player Position", buttonStyle))
                PlayerController.Instance.movement.SetPosition(GameSceneSettings.Instance.playerPosition, Direction.Up);
            // if(GUILayout.Button("Exit Memory", buttonStyle))
            //     PlayerController.Instance.interact.LeaveMemory();
            
            GUILayout.EndVertical();
        }
        
        public void DrawDebugGizmos() {
            if(!debugUIState.IsVisible("Player")) return;
            
            //Draw player collider
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(player.transform.position + Vector3.down / 2, player.transform.lossyScale.x / 2);
            Gizmos.DrawSphere(player.transform.position + Vector3.up / 2, player.transform.lossyScale.x / 2);
            Gizmos.DrawSphere(player.transform.position, player.transform.lossyScale.x / 2);
            
            //Draw ground collider
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(player.movement.feetPosition.position, player.movement.feetSize);
            
            //Draw interact zone
            Gizmos.color = Color.gold;
            Gizmos.matrix = Matrix4x4.TRS(player.interact.interactCenterZone.position, player.transform.rotation, player.interact.interactZoneSize);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
        
        public void Dispose() {
        }

        
    }
}