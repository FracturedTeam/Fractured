using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerFallState : PlayerBaseState
    {
        public PlayerFallState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            animator.CrossFade(FallHash,  DefaultCrossFadeDuration);
            player.SetMoveSpeed(PlayerSpeedEnum.Normal);
            player.SetInteraction(false);
        }

        public override void OnUpdate() {
            player.UpdateMovement();
        }

        public override void OnFixedUpdate() {
            player.FixedUpdateMovement();
        }

        public override void OnExit() {
            //Possible fall on ground animation
        }
    }
}
