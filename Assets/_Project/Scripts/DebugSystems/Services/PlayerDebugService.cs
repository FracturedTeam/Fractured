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

            GUILayout.BeginVertical("box");
            GUILayout.Label("Player Debug Service", headerStyle);
            
            GUILayout.Label("State Machine", sectionStyle);
            GUILayout.Label($"Current state is {player.GetCurrentState()}", debugStyle);
            
            GUILayout.Label("Movement", sectionStyle);
            GUILayout.Label($"Player current speed is {player.movement.currentSpeed} | Full speed is {player.movement.currentMaxSpeed}", debugStyle);
            GUILayout.Label($"Player current accel is {player.movement.accelTime / player.movement.playerConfig.accelTime * 100}% | Player current decel is {player.movement.decelTime / player.movement.playerConfig.decelTime * 100}%", debugStyle);
            GUILayout.Label($"Player time before moving {player.movement.playerConfig.timeBeforeMoving} | Current percentage {player.movement.timeBeforeMoving / player.movement.playerConfig.timeBeforeMoving * 100}%", debugStyle);
            GUILayout.Label($"Player is on slope : {player.movement.IsOnSlope()} | Current slope angle {player.movement.currentSlopeAngle}° | Max walkable slope angle {player.movement.playerConfig.maxSlopeAngle}", debugStyle);
            GUILayout.Label($"Player is on ground : {player.movement.IsGrounded()} | Current fall speed {player.movement.currentFallSpeed} | Max fall speed {player.movement.playerConfig.maxFallSpeed}", debugStyle);
            GUILayout.Label($"Player is frozen : {player.movement.IsPlayerFrozen()}", debugStyle);
            
            GUILayout.Label("Interaction", sectionStyle);
            GUILayout.Label($"Can player interact : {player.interact.CanInteract}", debugStyle);
            GUILayout.Label($"{player.interact.size} Object in the interact area", debugStyle);
            GUILayout.Label($"Is player holding an object : {player.interact.hasObject}", debugStyle);
            
            // -> Est-ce que le joueur est en train d'interagir avec un souvenir ou autre ?
            
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