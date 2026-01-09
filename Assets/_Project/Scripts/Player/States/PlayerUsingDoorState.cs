using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerUsingDoorState : PlayerBaseState {
        public readonly CountdownTimer animationExitTimer;
        public PlayerUsingDoorState(PlayerController player, Animator animator, AnimationClip clip) : base(player, animator) {
            animationExitTimer = new CountdownTimer(clip.length);
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