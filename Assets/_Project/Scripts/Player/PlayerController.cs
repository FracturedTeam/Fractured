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
    
    [RequireComponent(typeof(InputsBrain), typeof(PlayerMovementController))]
    public class PlayerController : Singleton<PlayerController>{
        [SerializeField, HideInInspector] private PlayerData data;
        
        [ContextMenu("Load")]
        public void Load(PlayerData data) {
            this.data = data;
            movement.SetPosition(data.position, Direction.Up);
        }
        
        [ContextMenu("Save")]
        public void SaveData(PlayerData data) {
            this.data = data;
            data.position = transform.position;
        }
        
        
        InputsBrain inputsBrain;
        StateMachine stateMachine;

        [Header("Cinemachine Brain")]
        public CinemachineBrain cinemachineBrain;
        
        [HideInInspector]
        public PlayerMovementController movement;
        [HideInInspector]
        public PlayerInteract interact;

        [Header("Animations Settings")]
        [SerializeField] private Animator animator;
        [SerializeField] public AnimationClip useDoorClip;
        [SerializeField] private AnimationClip grabObjectClip;
        [SerializeField] private AnimationClip dropObjectClip;
        [SerializeField] private AnimationClip failedDropClip;
        [SerializeField] private AnimationClip breakObjectClip;
        [SerializeField] private AnimationClip failedDoorClip;
        [SerializeField] private AnimationClip leaveMemoryClip;
        

        private void Start() {
            stateMachine = new StateMachine();
            
            //Get every component needed
            if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");
            
            if(TryGetComponent(out PlayerMovementController _movement)) movement = _movement;
            else Debug.LogWarning("[PlayerController] No PlayerMovementController found");
            
            if(TryGetComponent(out PlayerInteract _interact)) interact = _interact;
            else Debug.LogWarning("[PlayerController] No PlayerInteract found");
            
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
            var pressurePlateState = new PlayerPressurePlateState(this, animator);
            
            //Define subState
            var grabObject = new GrabObjectState(this, animator, grabObjectClip);
            var dropObject = new DropObjectState(this, animator, dropObjectClip);
            var failedDropObject = new FailedDropObject(this, animator, failedDropClip);
            var failedDoor = new FailedOpeningDoor(this, animator, failedDoorClip);
            var leaveMemory = new LeaveMemory(this, animator, leaveMemoryClip);
            
            //Define all states transitions
            //Locomotion State
            At(locomotionState, fallState, new FuncPredicate(() => !movement.IsGrounded() && !interact.IsCarrying()));
            At(fallState, locomotionState, new FuncPredicate(() => movement.IsGrounded() && !interact.IsCarrying()));
            
            //Carrying State
            At(locomotionState, grabObject, new FuncPredicate(() => interact.IsCarrying()));
            At(grabObject, carryState, new FuncPredicate(() => interact.IsCarrying() && grabObject.IsClipFinished()));
            
            At(carryState, dropObject, new FuncPredicate(() => !interact.IsCarrying()));
            At(dropObject, locomotionState, new FuncPredicate(() => !interact.IsCarrying() && dropObject.IsClipFinished()));
            
            At(carryState, failedDropObject, new FuncPredicate(() => interact.triggerFailedDrop));
            At(failedDropObject, carryState, new FuncPredicate(() => !interact.triggerFailedDrop && failedDropObject.IsClipFinished()));
            
            /*At(carryState, fallState, new FuncPredicate(() => interact.IsCarrying() && !movement.IsGrounded()));
            At(fallState, carryState, new FuncPredicate(() => interact.IsCarrying() && movement.IsGrounded()));*/
            
            //Memory State
            At(locomotionState, memoryState, new FuncPredicate(() => interact.IsInMemory()));
            At(carryState, memoryState, new FuncPredicate(() => interact.IsInMemory()));
            At(memoryState, leaveMemory, new FuncPredicate(() => !interact.IsInMemory() && !interact.IsCarrying()));
            At(leaveMemory, locomotionState, new FuncPredicate(() => leaveMemory.IsClipFinished()));
            
            //Using door state
            At(locomotionState, doorState, new FuncPredicate(() => interact.triggerDoor));
            At(doorState, locomotionState, new FuncPredicate(() => !interact.triggerDoor && doorState.animationExitTimer.IsFinished));
            /*At(carryState, doorState, new FuncPredicate(() => interact.UsingLockedDoor()));
            At(doorState, carryState, new FuncPredicate(() => !interact.UsingLockedDoor() && interact.IsCarrying()));*/
            
            //Failed Door
            At(locomotionState, failedDoor, new FuncPredicate(() => interact.UsingLockedDoor()));
            At(failedDoor, locomotionState, new FuncPredicate(() => !interact.UsingLockedDoor() && failedDoor.IsClipFinished()));
            
            //Use PressurePlate
            At(locomotionState, pressurePlateState, new FuncPredicate(() => interact.IsInPressurePlate()));
            At(pressurePlateState, locomotionState, new FuncPredicate(() => !interact.IsInPressurePlate()));
            
            //Obtenir un éclat de verre
            At(locomotionState, obtainShardState, new FuncPredicate(() => interact.triggerShard));
            At(obtainShardState, locomotionState, new FuncPredicate(() => obtainShardState.animationExitTimer.IsFinished));
            
            //Set the initial player State
            stateMachine.SetState(locomotionState);
        }

        private void Update() {
            stateMachine.Update();
            
            //Pour la build, à virer
            if(transform.position.y < -10)
                transform.position = new Vector3(transform.position.x, 10, transform.position.z);
                
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
    }

    [Serializable]
    public class PlayerData {
        public Vector3 position;
    }
}
