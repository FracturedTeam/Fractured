using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class FailedDropObject : PlayerBaseState {
        private readonly CountdownTimer animationExitTimer;

        public FailedDropObject(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
        }
        
        public override void OnEnter() {
            animationExitTimer.Start();
            player.interact.triggerFailedDrop = false;
            
            //Set the grab animation when entering holding state
            animator.SetLayerWeight(UpperBodyLayer, 1);
            animator.CrossFade(FailedDropHash, defaultCrossFadeDuration, UpperBodyLayer);
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            animationExitTimer.Stop();
            
            //Exit the grab animation when timer is finished
            animator.SetLayerWeight(UpperBodyLayer, 0);
            animator.CrossFade(EmptyHash, defaultCrossFadeDuration, UpperBodyLayer);
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}