using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class LeavePiedestalState : PlayerBaseState {
        private readonly CountdownTimer animationExitTimer;
        
        public LeavePiedestalState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
        }

        public override void OnEnter() {
            animationExitTimer.Start();
            animator.CrossFade(LeavePiedestalHash,  defaultCrossFadeDuration, FullBodyLayer);
        }
        
        public override void OnExit() {
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 0f, 0.2f);
            
            animator.CrossFade(EmptyHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(true);
            player.movement.UnfreezeController();
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}