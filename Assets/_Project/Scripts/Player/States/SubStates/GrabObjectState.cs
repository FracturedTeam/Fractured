using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Systems.StateMachine;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.States.SubStates {
    public class GrabObjectState : PlayerBaseState {
        static readonly int BlendingHash = Animator.StringToHash("Blend");
        
        private readonly float grabHeavyLength;
        private readonly float putInInventoryLength;
        
        private CountdownTimer animationExitTimer;

        public GrabObjectState(PlayerController player, Animator animator, AnimationClip heavy, AnimationClip inventory) : base(player, animator) {
            grabHeavyLength = heavy.length;
            putInInventoryLength = inventory.length;
        }
        
        public override void OnEnter() {
            animationExitTimer = 
                player.Interact.HasItemObject ? new CountdownTimer(grabHeavyLength) : new CountdownTimer(putInInventoryLength);
            
            animationExitTimer.Start();
            
            //Set the grab animation when entering holding state
            AnimWeightTween?.Kill();
            AnimWeightTween = FadeLayer(animator, UpperBodyLayer, 1f, 0.2f);
            animator.CrossFade(player.Interact.HasItemObject ? TakeObjectOutInventoryHash : GrabHeavyObjectHash, DefaultCrossFadeDuration, UpperBodyLayer);
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
        }
        
        public bool IsClipFinished() => animationExitTimer.IsFinished;
    }
}