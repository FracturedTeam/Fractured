using _Project.Scripts.Systems.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerUsingDoorState : PlayerBaseState {
        public PlayerUsingDoorState(PlayerController player, Animator animator) : base(player, animator) {
        }

        public override void OnEnter() {
            //Animator
            animator.SetLayerWeight(FullBodyLayer, 1);
            animator.CrossFade(OpenDoorHash, defaultCrossFadeDuration, FullBodyLayer);
            
            player.interact.SetInteract(false);
        }

        public override void OnUpdate() {
            //Handle le movement pour déplacer le joueur a un point donner / une direction
            //2 directions
            //Une lorsque le joueur rentre dans la porte
            //Une lorsque le joueur sort de la porte
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