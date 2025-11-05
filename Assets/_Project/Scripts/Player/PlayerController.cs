using _Project.Scripts.Inputs;
using _Project.Scripts.Player.States;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.Systems.StateMachine;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player {
    
    [RequireComponent(typeof(InputsBrain), typeof(PlayerMovementController))]
    public class PlayerController : Singleton<PlayerController>
    {
        InputsBrain inputsBrain;
        StateMachine stateMachine;

        public CinemachineBrain cinemachineBrain;
        
        [HideInInspector]
        public PlayerMovementController movement;
        [HideInInspector]
        public PlayerInteract interact;
        

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
            var locomotionState = new PlayerLocomotionState(this);
            var fallState = new PlayerFallState(this);
            
            //Define all states transitions
            At(locomotionState, fallState, new FuncPredicate(() => !movement.IsGrounded()));
            At(fallState, locomotionState, new FuncPredicate(() => movement.IsGrounded()));
            
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
}
