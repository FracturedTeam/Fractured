using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerObtainShardState : PlayerBaseState {
        public readonly CountdownTimer animationExitTimer;
        public PlayerObtainShardState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
        }

        public override void OnEnter() {
            //Animator
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 1f, 0.2f);
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
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 0f, 0.2f);
            animator.CrossFade(EmptyHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(true);
            player.movement.UnfreezeController();
        }
    }
}