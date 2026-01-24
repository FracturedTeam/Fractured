using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerPressurePlateState : PlayerBaseState {
        private readonly CountdownTimer animationFinished;
        
        public PlayerPressurePlateState(PlayerController player, Animator animator) : base(player, animator) {
            animationFinished = new CountdownTimer(1f);
            animationFinished.OnTimerStop += IdlePiedestal;
        }

        public override void OnEnter() {
            //Animator
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 1f, 0.2f);
            animator.CrossFade(UsePedestalHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            animationFinished.Start();
            
            player.interact.SetInteract(false);
            player.movement.FreezeController();
        }

        private void IdlePiedestal() {
            animator.CrossFade(IdlePedestalHash,  defaultCrossFadeDuration, FullBodyLayer);
        }
        
        public override void OnUpdate() {
            
        }

        public override void OnFixedUpdate() {
        }
        
        public override void OnExit() {
        }
    }
}