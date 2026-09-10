using _Project.Scripts.Player;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Systems.StateMachine {
    public class PlayerBaseState : IState {
        protected readonly PlayerController player;
        protected readonly Animator animator;
        
        //Animation Hash
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        
        protected static readonly int IdleBreak1Hash = Animator.StringToHash("IdleBreak1");
        protected static readonly int IdleBreak2Hash = Animator.StringToHash("IdleBreak2");
        protected static readonly int IdleBreak3Hash = Animator.StringToHash("IdleBreak3");
        
        protected static readonly int FallHash = Animator.StringToHash("Fall");
        protected static readonly int EmptyHash = Animator.StringToHash("Empty");
        protected static readonly int CrouchHash = Animator.StringToHash("Crouch");
        
        protected static readonly int CarryHeavyHash = Animator.StringToHash("CarryHeavyObject");
        protected static readonly int CarryLightHash = Animator.StringToHash("CarryLightObject");
        
        protected static readonly int FailedDropHash = Animator.StringToHash("FailedDrop");
        
        protected static readonly int GrabHeavyObjectHash = Animator.StringToHash("GrabHeavyObject");
        protected static readonly int DropHeavyObjectHash = Animator.StringToHash("DropHeavyObject");
        
        protected static readonly int GrabLightObjectHash = Animator.StringToHash("GrabLightObject");
        protected static readonly int DropLightObjectHash = Animator.StringToHash("DropLightObject");
        
        protected static readonly int PutObjectInInventoryHash = Animator.StringToHash("PutItemInInventory");
        protected static readonly int TakeObjectOutInventoryHash = Animator.StringToHash("TakeOutInventory");
        
        protected static readonly int EnterMemoryHash = Animator.StringToHash("EnterMemory");
        protected static readonly int LeaveMemoryHash = Animator.StringToHash("LeaveMemory");
        protected static readonly int OpenDoorHash = Animator.StringToHash("OpenDoor");
        protected static readonly int FailedOpenDoorHash = Animator.StringToHash("FailedOpenDoor");
        
        //protected static readonly int BreakGlassHash = Animator.StringToHash("BreakGlass");
        
        //Layer Hash
        protected const int MovementLayer = 0;
        protected const int UpperBodyLayer = 1;
        protected const int FullBodyLayer = 2;

        //Cross Fade Duration
        protected const float DefaultCrossFadeDuration = 0.25f;

        protected Tween AnimWeightTween;
        
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
