using _Project.Scripts.Systems.StateMachine;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerPressurePlateState : PlayerBaseState{
        public PlayerPressurePlateState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Animator
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 1f, 0.2f);
            animator.CrossFade(UsePiedestalHash,  defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(false);
            player.movement.FreezeController();
        }

        public override void OnUpdate() {
            
        }

        public override void OnFixedUpdate() {
        }
        
        public override void OnExit() {
        }
    }
}