using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerUsingDoorState : PlayerBaseState {
        public readonly CountdownTimer animationExitTimer;
        public PlayerUsingDoorState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length + 1.2f);
        }

        public override void OnEnter() {
            //Animator
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, FullBodyLayer, 1f, 0.2f);
            animator.CrossFade(OpenDoorHash, DefaultCrossFadeDuration, FullBodyLayer);
            
            player.SetInteraction(false);
            animationExitTimer.Start();
            player.SetDoorTriggered(false);
            player.FreezeController(true);
        }

        public override void OnUpdate() {
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            //Animator
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, FullBodyLayer, 0f, 0.2f);
            animator.CrossFade(EmptyHash, DefaultCrossFadeDuration, FullBodyLayer);
            
            player.FreezeController(false);
        }
    }
}