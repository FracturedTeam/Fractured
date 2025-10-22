using _Project.Scripts.Player;

namespace _Project.Scripts.Systems.StateMachine {
    public class PlayerBaseState : IState {
        protected readonly PlayerController player;
        
        protected PlayerBaseState(PlayerController player) {
            this.player = player;
        }
        
        public virtual void OnEnter() {
            
        }

        public virtual void OnUpdate() {
            
        }

        public virtual void OnFixedUpdate() {
            
        }

        public virtual void OnExit() {
            
        }
}
}
