using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerUsingDoorState : PlayerBaseState {
        public readonly CountdownTimer animationExitTimer;
        public PlayerUsingDoorState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length + 1.2f);
        }

        public override void OnEnter() {
            //Animator
            animator.SetLayerWeight(FullBodyLayer, 1);
            animator.CrossFade(OpenDoorHash, defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(false);
            animationExitTimer.Start();
            player.interact.triggerDoor = false;
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            //Animator
            animator.SetLayerWeight(FullBodyLayer, 0);
            animator.CrossFade(EmptyHash, defaultCrossFadeDuration, FullBodyLayer);
        }
    }
}