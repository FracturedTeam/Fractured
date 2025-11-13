namespace _Project.Scripts.DebugSystems {
    public class DebugInputService : IDebugSystem {
        private readonly InputSystem_Actions inputs;
        private readonly DebugUIState debugUIState;

        public DebugInputService(DebugUIState debugUI) {
            debugUIState = debugUI;
            inputs = new InputSystem_Actions();
            
            inputs.Debug.Enable();
            inputs.Debug.TogglePlayer.performed += _ => debugUIState.Toggle("Player");
            inputs.Debug.ToggleShard.performed += _ => debugUIState.Toggle("Shard");
            inputs.Debug.ToggleCamera.performed += _ => debugUIState.Toggle("Camera");
            inputs.Debug.ToggleGeneral.performed += _ => debugUIState.Toggle("General");
        }
        
        public void Initialize() {
        }

        public void Tick() {
        }
        
        public void Dispose() {
            inputs.Dispose();
        }
    }
}