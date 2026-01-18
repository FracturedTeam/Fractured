using _Project.Scripts.Systems.StateMachine;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerMemoryState : PlayerBaseState {
        public PlayerMemoryState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Animator
            animWeightTween?.Kill();
            animWeightTween = FadeLayer(animator, FullBodyLayer, 1f, 0.2f);
            animator.CrossFade(EnterMemoryHash,  defaultCrossFadeDuration, FullBodyLayer);
            
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