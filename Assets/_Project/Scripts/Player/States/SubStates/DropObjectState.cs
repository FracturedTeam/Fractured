using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class DropObjectState : PlayerBaseState {
        private readonly CountdownTimer animationExitTimer;

        public DropObjectState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
        }
        
        public override void OnEnter() {
            animationExitTimer.Start();
            
            //Set the grab animation when entering holding state
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 1f, 0.2f);
            animator.CrossFade(DropObjectHash, defaultCrossFadeDuration, FullBodyLayer);
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            animationExitTimer.Stop();
            
            //Exit the grab animation when timer is finished
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 0f, 0.2f);
            animator.CrossFade(EmptyHash, defaultCrossFadeDuration, FullBodyLayer);
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}