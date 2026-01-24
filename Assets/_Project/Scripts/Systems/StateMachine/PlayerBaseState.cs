using _Project.Scripts.Player;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Systems.StateMachine {
    public class PlayerBaseState : IState {
        protected readonly PlayerController player;
        protected readonly Animator animator;
        
        //Animation Hash
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int FallHash = Animator.StringToHash("Fall");
        protected static readonly int EmptyHash = Animator.StringToHash("Empty");
        protected static readonly int CarryHash = Animator.StringToHash("Carry");
        protected static readonly int FailedDropHash = Animator.StringToHash("FailedDrop");
        protected static readonly int GrabObjectHash = Animator.StringToHash("GrabObject");
        protected static readonly int DropObjectHash = Animator.StringToHash("DropObject");
        protected static readonly int EnterMemoryHash = Animator.StringToHash("EnterMemory");
        protected static readonly int LeaveMemoryHash = Animator.StringToHash("LeaveMemory");
        protected static readonly int OpenDoorHash = Animator.StringToHash("OpenDoor");
        protected static readonly int FailedOpenDoorHash = Animator.StringToHash("FailedOpenDoor");
        protected static readonly int BreakGlassHash = Animator.StringToHash("BreakGlass");
        protected static readonly int UsePedestalHash = Animator.StringToHash("UsePedestal");
        protected static readonly int IdlePedestalHash = Animator.StringToHash("IdlePedestal");
        protected static readonly int LeavePedestalHash = Animator.StringToHash("LeavePedestal");
        
        //Layer Hash
        protected const int MovementLayer = 0;
        protected const int UpperBodyLayer = 1;
        protected const int FullBodyLayer = 2;

        //Cross Fade Duration
        protected const float defaultCrossFadeDuration = 0.25f;

        protected Tween animWeightTween;
        
        protected PlayerBaseState(PlayerController player, Animator animator) {
            this.player = player;
            this.animator = animator;
        }
        
        public virtual void OnEnter() {
            
        }

        public virtual void OnUpdate() {
            
        }

        public virtual void OnFixedUpdate() {
            
        }

        public virtual void OnExit() {
            
        }
        
        public Tween FadeLayer(
            Animator animator,
            int layer,
            float target,
            float duration
        ) {
            return DOTween.To(
                () => animator.GetLayerWeight(layer),
                x => animator.SetLayerWeight(layer, x),
                target,
                duration
            );
        }
    }
}
