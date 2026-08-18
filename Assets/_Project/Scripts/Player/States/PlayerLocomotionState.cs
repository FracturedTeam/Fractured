using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerLocomotionState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");

        private readonly CountdownTimer idleBreak = new (20f);
        
        public PlayerLocomotionState(PlayerController player, Animator animator) : base(player, animator) {
            idleBreak.OnTimerStop += PlayIdleBreak;
        }

        public override void OnEnter() {
            animator.CrossFade(IdleHash,  DefaultCrossFadeDuration);
            player.SetMoveSpeed(PlayerSpeedEnum.Normal);
            player.SetInteraction(true);
        }

        public override void OnUpdate() {
            player.UpdateMovement();
            player.UpdateInteraction();

            if(player.GetAnimatorSpeed() == 0 && !idleBreak.IsRunning)
                idleBreak.Start();
            
            if(idleBreak.IsRunning && player.GetAnimatorSpeed() != 0) 
                idleBreak.CompleteStop(); 
            
            animator.SetFloat(BlendingHash, player.GetAnimatorSpeed());
        }

        private void PlayIdleBreak() {
            var rdm = Random.Range(0, 3);
            switch (rdm) {
                case 0:
                    animator.CrossFade(IdleBreak1Hash, DefaultCrossFadeDuration);
                    break;
                case 1:
                    animator.CrossFade(IdleBreak2Hash, DefaultCrossFadeDuration);
                    break;
                case 2:
                    animator.CrossFade(IdleBreak3Hash, DefaultCrossFadeDuration);
                    break;
            }
        }
        
        public override void OnFixedUpdate() {
            player.FixedUpdateMovement();
        }

        public override void OnExit() {
            
        }
    }
}
