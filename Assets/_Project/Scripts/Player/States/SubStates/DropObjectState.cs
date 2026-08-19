using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class DropObjectState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        private readonly float grabHeavyLength;
        private readonly float grabLightLength;
        private readonly float putInInventoryLength;
        
        private CountdownTimer animationExitTimer;

        public DropObjectState(PlayerController player, Animator animator, AnimationClip heavy, AnimationClip light, AnimationClip inventory) : base(player, animator) {
            grabHeavyLength = heavy.length;
            grabLightLength = light.length;
            putInInventoryLength = inventory.length;
        }
        
        public override void OnEnter() {
            var doPlayerCrouch = player.transform.position.y - player.Interact.pickUpObjectYPos > 1.15f;
            
            int hash = 0;
            switch (player.Interact.dropType) {
                case PlayerInteract.DropType.Heavy:
                    animationExitTimer = new(grabHeavyLength);
                    hash = DropHeavyObjectHash;
                    break;
                case PlayerInteract.DropType.Light:
                    animationExitTimer = new(grabLightLength);
                    hash = DropLightObjectHash;
                    break;
                case PlayerInteract.DropType.Inventory:
                    animationExitTimer = new(putInInventoryLength);
                    hash = PutObjectInInventoryHash;
                    GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().avatar_PuttingAway_Object, player.transform.position);
                    break;
            }
            
            animationExitTimer.Start();
            player.Interact.ResetDrop();
            
            //Set the grab animation when entering holding state
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, UpperBodyLayer, 1f, 0.2f);
            animator.CrossFade(hash, DefaultCrossFadeDuration, UpperBodyLayer);
            if (doPlayerCrouch) {
                animator.CrossFade(CrouchHash, DefaultCrossFadeDuration);
            }
        }

        public override void OnUpdate() {
            animator.SetFloat(BlendingHash, player.GetAnimatorSpeed());
        }

        public override void OnFixedUpdate() {
        }

        public override void OnExit() {
            animationExitTimer.Stop();
            
            //Exit the grab animation when timer is finished
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, UpperBodyLayer, 0f, 0.2f);
            animator.CrossFade(EmptyHash, DefaultCrossFadeDuration, UpperBodyLayer);
            animator.CrossFade(IdleHash, DefaultCrossFadeDuration);
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}