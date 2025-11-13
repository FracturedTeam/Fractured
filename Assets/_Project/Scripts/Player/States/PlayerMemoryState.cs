using _Project.Scripts.Systems.StateMachine;

namespace _Project.Scripts.Player.States {
    public class PlayerMemoryState : PlayerBaseState {
        public PlayerMemoryState(PlayerController player) : base(player) {
        }

        public override void OnEnter() {
            player.movement.SetSpeed(PlayerSpeedEnum.Normal);
            player.interact.SetInteract(false);
            player.movement.FreezeController();
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            player.interact.SetInteract(true);
            player.movement.UnfreezeController();
        }
    }
}