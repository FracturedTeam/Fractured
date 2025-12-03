using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player.States;
using _Project.Scripts.Systems.Save;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.Systems.StateMachine;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player {
    
    [RequireComponent(typeof(InputsBrain), typeof(PlayerMovementController))]
    public class PlayerController : Singleton<PlayerController>{
        [SerializeField] private PlayerData data;
        
        [ContextMenu("Load")]
        public void Load(PlayerData data) {
            this.data = data;
            transform.position = data.position;
        }
        
        [ContextMenu("Save")]
        public void SaveData(PlayerData data) {
            this.data = data;
            data.position = transform.position;
        }
        
        
        InputsBrain inputsBrain;
        StateMachine stateMachine;

        public CinemachineBrain cinemachineBrain;
        
        [HideInInspector]
        public PlayerMovementController movement;
        [HideInInspector]
        public PlayerInteract interact;

        [SerializeField] private Animator animator;
        

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
            var doorState = new PlayerUsingDoorState(this, animator);
            var obtainShardState = new PlayerObtainShardState(this, animator);
            
            //Define all states transitions
            //Locomotion State
            At(locomotionState, fallState, new FuncPredicate(() => !movement.IsGrounded()));
            At(fallState, locomotionState, new FuncPredicate(() => movement.IsGrounded()));
            
            //Carrying State
            At(locomotionState, carryState, new FuncPredicate(() => interact.IsCarrying()));
            At(carryState, locomotionState, new FuncPredicate(() => !interact.IsCarrying()));
            At(carryState, fallState, new FuncPredicate(() => interact.IsCarrying() && !movement.IsGrounded()));
            At(fallState, carryState, new FuncPredicate(() => interact.IsCarrying() && movement.IsGrounded()));
            
            //Memory State
            At(locomotionState, memoryState, new FuncPredicate(() => interact.IsInMemory()));
            At(memoryState, locomotionState, new FuncPredicate(() => !interact.IsInMemory() && !interact.IsCarrying()));
            At(carryState, memoryState, new FuncPredicate(() => interact.IsInMemory()));
            
            //Using door state
            At(locomotionState, doorState, new FuncPredicate(() => interact.UsingDoor()));
            At(doorState, locomotionState, new FuncPredicate(() => !interact.UsingDoor() && !interact.IsCarrying()));
            At(carryState, doorState, new FuncPredicate(() => interact.UsingDoor()));
            At(doorState, carryState, new FuncPredicate(() => !interact.UsingDoor() && interact.IsCarrying()));
            
            //Obtenir un éclat de verre
            //Faut que je regarde comment trigger le state
            //At(locomotionState, obtainShardState, new FuncPredicate(() => interact.));
            
            //Set the initial player State
            stateMachine.SetState(locomotionState);
        }

        private void Update() {
            stateMachine.Update();
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
