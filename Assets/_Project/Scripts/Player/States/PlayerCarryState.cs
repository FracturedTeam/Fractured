using _Project.Scripts.Systems.StateMachine;

namespace _Project.Scripts.Player.States {
    public class PlayerCarryState : PlayerBaseState
    {
        public PlayerCarryState(PlayerController player) : base(player) {
        }

        public override void OnEnter() {
            player.movement.SetSpeed(PlayerSpeedEnum.Normal);
            player.interact.SetInteract(true);
        }

        public override void OnUpdate() {
            player.movement.HandleUpdate();
            player.interact.HandleUpdate(player.movement.previousMoveDir);
        }

        public override void OnFixedUpdate() {
            player.movement.HandleMovement();
        }

        public override void OnExit() {
            
        }
    }
}