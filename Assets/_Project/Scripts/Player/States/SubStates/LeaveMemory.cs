using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class LeaveMemory : PlayerBaseState {
        private readonly CountdownTimer animationExitTimer;
        
        public LeaveMemory(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length - defaultCrossFadeDuration);
        }

        public override void OnEnter() {
            animationExitTimer.Start();
            
            animator.SetLayerWeight(FullBodyLayer, 1);
            animator.CrossFade(LeaveMemoryHash,  defaultCrossFadeDuration, FullBodyLayer);
        }
        
        public override void OnExit() {
            animator.SetLayerWeight(FullBodyLayer, 0);
            animator.CrossFade(EmptyHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(true);
            player.movement.UnfreezeController();
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}