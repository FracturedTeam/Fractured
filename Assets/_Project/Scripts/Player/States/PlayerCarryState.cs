using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerCarryState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        public PlayerCarryState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Animator -- Set the hold animation while holding object
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, UpperBodyLayer, 1f, 0.2f);
            animator.CrossFade(CarryHash,  DefaultCrossFadeDuration, UpperBodyLayer);
            
            player.SetMoveSpeed(PlayerSpeedEnum.Normal);
            player.SetInteraction(true);
        }

        public override void OnUpdate() {
            player.UpdateMovement();
            player.UpdateInteraction();
            
            animator.SetFloat(BlendingHash, player.SetAnimatorSpeed());
        }

        public override void OnFixedUpdate() {
            player.FixedUpdateMovement();
        }

        public override void OnExit() {
            //Animator
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, UpperBodyLayer, 0f, 0.2f);
            animator.CrossFade(EmptyHash, DefaultCrossFadeDuration, UpperBodyLayer);
        }
    }
}