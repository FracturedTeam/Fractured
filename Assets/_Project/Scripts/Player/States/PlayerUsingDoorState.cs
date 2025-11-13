using _Project.Scripts.Systems.StateMachine;

namespace _Project.Scripts.Player.States {
    public class PlayerUsingDoorState : PlayerBaseState {
        public PlayerUsingDoorState(PlayerController player) : base(player) {
        }

        public override void OnEnter() {
            player.movement.SetSpeed(PlayerSpeedEnum.Normal);
            player.interact.SetInteract(false);
        }

        public override void OnUpdate() {
            //Handle le movement pour déplacer le joueur a un point donner / une direction
            //2 directions
            //Une lorsque le joueur rentre dans la porte
            //Une lorsque le joueur sort de la porte
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            
        }
    }
}