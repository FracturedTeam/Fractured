using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class FailedDropObject : PlayerBaseState {
        private readonly CountdownTimer animationExitTimer;

        public FailedDropObject(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
        }
        
        public override void OnEnter() {
            animationExitTimer.Start();
            animationExitTimer.OnTimerStop += UnSet;
            
            //Set the grab animation when entering holding state
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, UpperBodyLayer, 1f, 0.2f);
            animator.CrossFade(FailedDropHash, defaultCrossFadeDuration, UpperBodyLayer);
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            animationExitTimer.Stop();
            
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, UpperBodyLayer, 0f, 0.2f);
            animator.CrossFade(EmptyHash, defaultCrossFadeDuration, UpperBodyLayer);
        }

        private void UnSet() {
            player.interact.triggerFailedDrop = false;
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}