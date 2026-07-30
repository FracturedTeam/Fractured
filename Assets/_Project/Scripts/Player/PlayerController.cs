using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player.States;
using _Project.Scripts.Player.States.SubStates;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.Systems.StateMachine;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player {
    
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerController : Singleton<PlayerController>{
        [SerializeField, HideInInspector] private PlayerData data;
        
        public void Load(PlayerData data) {
            this.data = data;
            Movement.SetPosition(data.position, Direction.Up);
        }
        
        public void SaveData(PlayerData data) {
            if(data == null) return;
            
            this.data = data;
            data.position = transform.position;
        }
        
        // InputsBrain inputsBrain;
        StateMachine stateMachine;

        [Header("Cine Machine Brain")]
        public CinemachineBrain cinemachineBrain;
        
        public PlayerMovementController Movement { get; private set; }
        public PlayerInteract Interact { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public PlayerIK PlayerIK { get; private set; }
        
        [Header("Animations Settings")]
        [SerializeField] private Animator animator;
        [SerializeField] public AnimationClip useDoorClip;
        [SerializeField] private AnimationClip grabObjectClip;
        [SerializeField] private AnimationClip dropObjectClip;
        [SerializeField] private AnimationClip failedDropClip;
        [SerializeField] private AnimationClip breakObjectClip;
        [SerializeField] private AnimationClip failedDoorClip;
        [SerializeField] private AnimationClip leaveMemoryClip;
        [SerializeField] private AnimationClip usePedestalClip;

        private Action enterRoom;
        [HideInInspector] public bool triggerEnterRoom = false;

        private void Start() {
            stateMachine = new StateMachine();
            // quick fix for that art part, need rework for the steam version
            cinemachineBrain.gameObject.SetActive(false);
            
            if(TryGetComponent(out PlayerMovementController movement)) Movement = movement;
            else Debug.LogWarning("[PlayerController] No PlayerMovementController found");
            
            if(TryGetComponent(out PlayerInteract interact)) Interact = interact;
            else Debug.LogWarning("[PlayerController] No PlayerInteract found");
            
            if(TryGetComponent(out PlayerInventory inventory)) Inventory = inventory;
            else Debug.LogWarning("[PlayerController] No PlayerInventory found");
            
            if(TryGetComponent(out PlayerIK ik)) PlayerIK = ik;
            else Debug.LogWarning("[PlayerController] No Player IK found");
            
            //Define state machine
            DefineState();
            
        }

        void DefineState() {
            //Create All State
            var locomotionState = new PlayerLocomotionState(this, animator);
            var fallState = new PlayerFallState(this, animator);
            var carryState = new PlayerCarryState(this, animator);
            var memoryState = new PlayerMemoryState(this, animator);
            var doorState = new PlayerUsingDoorState(this, animator, useDoorClip);
            var obtainShardState = new PlayerObtainShardState(this, animator, breakObjectClip);
            var playerEnterRoomState = new PlayerEnteringRoomState(this, animator);
            
            //Define subState
            var grabObject = new GrabObjectState(this, animator, grabObjectClip);
            var dropObject = new DropObjectState(this, animator, dropObjectClip);
            var failedDropObject = new FailedDropObject(this, animator, failedDropClip);
            var failedDoor = new FailedOpeningDoor(this, animator, failedDoorClip);
            var leaveMemory = new LeaveMemory(this, animator, leaveMemoryClip);
            
            //Define all states transitions
            //Locomotion State
            At(locomotionState, fallState, new FuncPredicate(() => !Movement.IsGrounded() && !Interact.IsCarrying()));
            At(fallState, locomotionState, new FuncPredicate(() => Movement.IsGrounded() && !Interact.IsCarrying()));
            
            //Carrying State
            At(locomotionState, grabObject, new FuncPredicate(() => Interact.IsCarrying()));
            At(grabObject, carryState, new FuncPredicate(() => Interact.IsCarrying() && grabObject.IsClipFinished()));
            At(grabObject, locomotionState, new  FuncPredicate(() => !Interact.IsCarrying() && grabObject.IsClipFinished()));
            
            At(carryState, dropObject, new FuncPredicate(() => !Interact.IsCarrying()));
            At(dropObject, locomotionState, new FuncPredicate(() => !Interact.IsCarrying() && dropObject.IsClipFinished()));
            
            At(carryState, failedDropObject, new FuncPredicate(() => Interact.triggerFailedDrop));
            At(failedDropObject, carryState, new FuncPredicate(() => !Interact.triggerFailedDrop && failedDropObject.IsClipFinished()));
            
            /*At(carryState, fallState, new FuncPredicate(() => interact.IsCarrying() && !movement.IsGrounded()));
            At(fallState, carryState, new FuncPredicate(() => interact.IsCarrying() && movement.IsGrounded()));*/
            
            //Memory State
            At(locomotionState, memoryState, new FuncPredicate(() => Interact.IsInMemory));
            At(carryState, memoryState, new FuncPredicate(() => Interact.IsInMemory));
            At(memoryState, leaveMemory, new FuncPredicate(() => !Interact.IsInMemory));
            At(leaveMemory, locomotionState, new FuncPredicate(() => leaveMemory.IsClipFinished()));
            
            //Using door state
            At(locomotionState, doorState, new FuncPredicate(() => Interact.triggerDoor));
            At(doorState, locomotionState, new FuncPredicate(() => !Interact.triggerDoor && doorState.animationExitTimer.IsFinished));
            /*At(carryState, doorState, new FuncPredicate(() => interact.UsingLockedDoor()));
            At(doorState, carryState, new FuncPredicate(() => !interact.UsingLockedDoor() && interact.IsCarrying()));*/
            
            //Failed Door
            At(locomotionState, failedDoor, new FuncPredicate(() => Interact.UsingLockedDoor()));
            At(failedDoor, locomotionState, new FuncPredicate(() => !Interact.UsingLockedDoor() && failedDoor.IsClipFinished()));
            
            //Obtenir un éclat de verre
            At(locomotionState, obtainShardState, new FuncPredicate(() => Interact.triggerShard));
            At(obtainShardState, locomotionState, new FuncPredicate(() => obtainShardState.animationExitTimer.IsFinished));
            
            //Entering Room State
            Any(playerEnterRoomState, new FuncPredicate(() => triggerEnterRoom));
            At(playerEnterRoomState, locomotionState, new FuncPredicate(() => playerEnterRoomState.IsStateFinished()));
            
            //Set the initial player State
            stateMachine.SetState(locomotionState);
            
            cinemachineBrain.gameObject.SetActive(true);
        }

        private void Update() {
            stateMachine.Update();

			#if UNITY_EDITOR
            if(transform.position.y < -10)
                transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        	#endif
        }
        
        void FixedUpdate() {
            stateMachine.FixedUpdate();
        }
        
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        
        public bool IsCurrentState<TState>() where TState : IState {
            return stateMachine.IsCurrentState<TState>();
        }
        
        public bool TryGetCurrentStateAs<TState>(out TState state) where TState : IState {
            return stateMachine.TryGetCurrentStateAs(out state);
        }

        public IState GetCurrentState() {
            return stateMachine.CurrentState;
        }
        
        #region Movement Helper/Setter

        public void UpdateMovement() => Movement.HandleUpdate();
        public void FixedUpdateMovement() => Movement.HandleFixedUpdate();
        public float SetAnimatorSpeed() => Movement.SetAnimatorSpeed();
        public void FreezeController(bool doFreeze) => Movement.SetKinematic(doFreeze);
        public bool IsFrozen() => Movement.IsPlayerFrozen();
        public void SetMoveSpeed(PlayerSpeedEnum speed) => Movement.SetSpeed(speed);
        public float GetRotationSpeed() => Movement.playerConfig.rotationSpeed;
        public Vector3 GetForwardDir() => Movement.mesh.forward;
        public Rigidbody GetRigidbody() => Movement.GetRigidbody();
        #endregion
        
        #region Interaction Helper/Setter
        public void UpdateInteraction() => Interact.HandleUpdate(Movement.PreviousMoveDir);
        public void SetInteraction(bool canInteract) => Interact.SetInteract(canInteract);
        public void SetInMemory(bool inMemory) => Interact.SetInMemory(inMemory);
        public void SetDoorTriggered(bool triggeredDoor) => Interact.triggerDoor = triggeredDoor;
        public void SetFailedDrop(bool hasFailed) => Interact.triggerFailedDrop = hasFailed;
        public void SetShardTriggered(bool triggeredShard) => Interact.triggerShard = triggeredShard;
        public bool IsUsingDoor() => Interact.IsUsingDoor();
        public bool GetFailedDrop() => Interact.triggerFailedDrop;
        #endregion
    }

    [Serializable]
    public class PlayerData {
        public Vector3 position;
    }
}
