using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerLocomotionState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        public PlayerLocomotionState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            animator.CrossFade(IdleHash,  defaultCrossFadeDuration);
            player.movement.SetSpeed(PlayerSpeedEnum.Normal);
            player.interact.SetInteract(true);
        }

        public override void OnUpdate() {
            player.movement.HandleUpdate();
            player.interact.HandleUpdate(player.movement.previousMoveDir);

            animator.SetFloat(BlendingHash, player.movement.SetAnimatorSpeed());
        }

        public override void OnFixedUpdate() {
            player.movement.HandleMovement();
        }

        public override void OnExit() {
            
        }
    }
}
