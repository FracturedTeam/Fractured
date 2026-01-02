using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerObtainShardState : PlayerBaseState {
        public readonly CountdownTimer animationExitTimer;
        public PlayerObtainShardState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
        }

        public override void OnEnter() {
            //Animator
            animator.SetLayerWeight(FullBodyLayer, 1);
            animator.CrossFade(BreakGlassHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(false);
            player.movement.FreezeController();
            
            animationExitTimer.Start();
            player.interact.triggerShard = false;
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