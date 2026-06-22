using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerLocomotionState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        public PlayerLocomotionState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            animator.CrossFade(IdleHash,  DefaultCrossFadeDuration);
            player.SetMoveSpeed(PlayerSpeedEnum.Normal);
            player.SetInteraction(true);
        }

        public override void OnUpdate() {
            player.UpdateMovement();
            player.UpdateInteraction();

            animator.SetFloat(BlendingHash, player.SetAnimatorSpeed());
        }

        public override void OnFixedUpdate() {
            player.FixedUpdateMovement();
        }

        public override void OnExit() {
            
        }
    }
}
