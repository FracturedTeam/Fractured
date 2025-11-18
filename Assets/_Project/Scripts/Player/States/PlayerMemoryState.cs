using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerMemoryState : PlayerBaseState {
        public PlayerMemoryState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Animator
            animator.SetLayerWeight(FullBodyLayer, 1);
            animator.CrossFade(EnterMemoryHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.movement.SetSpeed(PlayerSpeedEnum.Normal);
            player.interact.SetInteract(false);
            player.movement.FreezeController();
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            //Animator
            animator.CrossFade(LeaveMemoryHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            animator.SetLayerWeight(FullBodyLayer, 0);
            animator.CrossFade(EmptyHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(true);
            player.movement.UnfreezeController();
        }
    }
}