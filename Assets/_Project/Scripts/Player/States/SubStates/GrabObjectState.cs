using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class GrabObjectState : PlayerBaseState{
        public readonly CountdownTimer animationExitTimer;

        public GrabObjectState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
        }
        
        public override void OnEnter() {
            animationExitTimer.Start();
            
            //Set the grab animation when entering holding state
            animator.SetLayerWeight(FullBodyLayer, 1);
            animator.CrossFade(GrabObjectHash, defaultCrossFadeDuration, FullBodyLayer);
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            animationExitTimer.Stop();
            
            //Exit the grab animation when timer is finished
            animator.SetLayerWeight(FullBodyLayer, 0);
            animator.CrossFade(EmptyHash, defaultCrossFadeDuration, FullBodyLayer);
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}