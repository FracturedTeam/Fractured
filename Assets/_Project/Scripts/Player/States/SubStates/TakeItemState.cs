using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class TakeItemState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        private readonly CountdownTimer animationExitTimer;
        private readonly CountdownTimer playAnimTimer;

        public TakeItemState(PlayerController player, Animator animator, AnimationClip grab, AnimationClip inventory) : base(player, animator) {
            animationExitTimer = new CountdownTimer(grab.length - 0.5f + inventory.length);
            playAnimTimer = new CountdownTimer(grab.length - 0.5f);
            playAnimTimer.OnTimerStop += PlaySecondAnimation;
        }

        private void PlaySecondAnimation() {
            animator.CrossFade(PutObjectInInventoryHash, DefaultCrossFadeDuration, UpperBodyLayer);
            Debug.Log("Play Second Animation");
        }
        
        public override void OnEnter() {
            animationExitTimer.Start();
            playAnimTimer.Start();

            Debug.Log("Enter Take Item");
            
            player.Interact.TriggerPickUpItem = false;
            
            //Set the grab animation when entering holding state
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, UpperBodyLayer, 1f, 0.2f);
            animator.CrossFade(GrabLightObjectHash, DefaultCrossFadeDuration, UpperBodyLayer);
        }

        public override void OnUpdate() {
            animator.SetFloat(BlendingHash, player.SetAnimatorSpeed());
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            animationExitTimer.Stop();
            
            //Exit the grab animation when timer is finished
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, UpperBodyLayer, 0f, 0.2f);
            animator.CrossFade(EmptyHash, DefaultCrossFadeDuration, UpperBodyLayer);
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}