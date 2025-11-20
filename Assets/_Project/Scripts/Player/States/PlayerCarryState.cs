using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerCarryState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        public PlayerCarryState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Prévoir l'animation d'entré dans le state
            //Animator
            animator.SetLayerWeight(UpperBodyLayer, 1);
            animator.CrossFade(CarryHash,  defaultCrossFadeDuration, UpperBodyLayer);
            
            player.movement.SetSpeed(PlayerSpeedEnum.Normal);
            player.interact.SetInteract(true);
        }

        public override void OnUpdate() {
            player.movement.HandleUpdate();
            player.interact.HandleUpdate(player.movement.previousMoveDir);
            
            animator.SetFloat(BlendingHash, player.movement.GetSpeedRatio());
        }

        public override void OnFixedUpdate() {
            player.movement.HandleMovement();
        }

        public override void OnExit() {
            //Prévoir l'animation de sortie dans le state
            //Animator
            animator.SetLayerWeight(UpperBodyLayer, 0);
            animator.CrossFade(EmptyHash, defaultCrossFadeDuration, UpperBodyLayer);
        }
    }
}