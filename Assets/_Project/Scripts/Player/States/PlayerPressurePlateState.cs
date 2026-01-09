using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerPressurePlateState : PlayerBaseState{
        public PlayerPressurePlateState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Animator
            animator.SetLayerWeight(FullBodyLayer, 1);
            animator.CrossFade(BreakGlassHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(false);
            player.movement.FreezeController();
        }

        public override void OnUpdate() {
            
        }

        public override void OnFixedUpdate() {
        }
        
        public override void OnExit() {
            //Animator
            animator.SetLayerWeight(FullBodyLayer, 0);
            animator.CrossFade(EmptyHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(true);
            player.movement.UnfreezeController();
        }
    }
}