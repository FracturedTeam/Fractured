using _Project.Scripts.Enums;
using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States {
    public class PlayerEnteringRoomState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        private readonly CountdownTimer exitStateTimer;
        private Vector3 startPos;
        private Vector3 endPos;
        
        public PlayerEnteringRoomState(PlayerController player, Animator animator) : base(player, animator) {
            exitStateTimer = new CountdownTimer(2f);
        }

        public override void OnEnter() {
            player.triggerEnterRoom = false;
            startPos = player.transform.position;
            endPos = startPos + player.GetForwardDir() * 2f;
            
            exitStateTimer.Start();
        }

        public override void OnUpdate() {
            player.GetRigidbody().MovePosition(Vector3.Lerp(endPos, startPos, Mathf.Clamp(exitStateTimer.Progress, 0.2f, 1f)));
            //player.movement.GetRigidbody().DOMove(endPos, 1.8f).SetEase(Ease.OutSine).SetUpdate(UpdateType.Fixed);

            animator.SetFloat(BlendingHash,
                exitStateTimer.Progress > 0.2f
                    ? .8f
                    : Mathf.Clamp(animator.GetFloat(BlendingHash) - Time.deltaTime * 2f, 0, 1f));
        }

        public override void OnExit() {
            player.FreezeController(false);
        }

        public bool IsStateFinished() {
            return exitStateTimer.IsFinished;
        }
        
    }
}