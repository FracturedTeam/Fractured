using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerCarryState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        public PlayerCarryState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Animator -- Set the hold animation while holding object
            animator.SetLayerWeight(UpperBodyLayer, 1);
            animator.CrossFade(CarryHash,  defaultCrossFadeDuration, UpperBodyLayer);
            
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
            //Animator
            animator.SetLayerWeight(UpperBodyLayer, 0);
            animator.CrossFade(EmptyHash, defaultCrossFadeDuration, UpperBodyLayer);
        }
    }
}