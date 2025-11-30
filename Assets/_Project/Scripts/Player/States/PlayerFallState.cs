using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerFallState : PlayerBaseState
    {
        public PlayerFallState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            animator.CrossFade(FallHash,  defaultCrossFadeDuration);
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
            //Possible fall on ground animation
        }
    }
}
