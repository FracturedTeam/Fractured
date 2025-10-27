using _Project.Scripts.Systems.StateMachine;

namespace _Project.Scripts.Player.States {
    public class PlayerFallState : PlayerBaseState
    {
        public PlayerFallState(PlayerController player) : base(player) {
        }

        public override void OnEnter() {
            player.movement.SetSpeed(PlayerSpeedEnum.Normal);
            player.interact.SetInteract(false);
        }

        public override void OnUpdate() {
            player.movement.HandleUpdate();
        }

        public override void OnFixedUpdate() {
            player.movement.HandleMovement();
        }

        public override void OnExit() {
            
        }
    }
}
