using System;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Inputs {
    public class InputsBrain : PersistentSingleton<InputsBrain> {
        private InputSystem_Actions inputs;

        public event Action<Vector2> OnPlayerMove = delegate { };
        public event Action<InputAction.CallbackContext> OnInteract = delegate { };
        public event Action<InputAction.CallbackContext> OnSecondaryInteract = delegate { };
        public event Action<float> OnLockUp = delegate { };
        public event Action<float> OnLockRight = delegate { };
        
        public event Action<InputAction.CallbackContext> OnInventoryOpen = delegate { };
        public event Action<InputAction.CallbackContext> OnShardA = delegate { };
        public event Action<InputAction.CallbackContext> OnShardB = delegate { };
        
        public event Action<InputAction.CallbackContext> OnInventorySelect = delegate { };
        
        public event Action<bool> OnGamepadControlled = delegate { };
        public event Action OnBackBtt = delegate { };
        public event Action OnSelectBtt = delegate { };
        public event Action<InputAction.CallbackContext> OnNavigation = delegate { };
        public event Action<InputAction.CallbackContext> OnSettingsView = delegate { };
        public event Action OnPause = delegate { };
    
        public bool IsKeyboardControl { get; private set; }
        
        protected override void Awake() {
            base.Awake();
            inputs = new InputSystem_Actions();
            IsKeyboardControl = true;
        }

        private void OnEnable() {
            // Player Inputs
            inputs.Player.Move.performed += PlayerMove;
            inputs.Player.Move.canceled += PlayerMove;
            inputs.Player.Interact.performed += Interact;
            inputs.Player.Interact.canceled += Interact;
            inputs.Player.SecondaryInteract.performed += SecondaryInteract;
            inputs.Player.SecondaryInteract.canceled += SecondaryInteract;
            inputs.Player.LockUp.performed += LockUp;
            inputs.Player.LockRight.performed += LockRight;
        
            inputs.Player.OpenInventory.performed += OpenInventory;
            inputs.Player.AShard.performed += ShardA;
            inputs.Player.AShard.canceled += ShardA;
            inputs.Player.BShard.performed += ShardB;
            inputs.Player.BShard.canceled += ShardB;
            inputs.Player.InventorySelect.performed += InventorySelect;

            // UI Inputs
            inputs.UI.Select.performed += OnSelect;
            inputs.UI.Back.performed += OnBack;
            inputs.UI.Navigation.performed += Navigation;
            inputs.UI.SettingsView.performed += SettingsView;
                
            inputs.Pause.Pause.performed += Pause;
            
            InputSystem.onActionChange += InputActionChangeCallback;
            OnGamepadControlled.Invoke(false);
            
            inputs.Enable();
        }

        private void OnDisable() {
            // Player Inputs
            inputs.Player.Move.performed -= PlayerMove;
            inputs.Player.Move.canceled -= PlayerMove;
            inputs.Player.Interact.performed -= Interact;
            inputs.Player.Interact.canceled -= Interact;
            inputs.Player.SecondaryInteract.performed -= SecondaryInteract;
            inputs.Player.SecondaryInteract.canceled -= SecondaryInteract;
            inputs.Player.LockUp.performed -= LockUp;
            inputs.Player.LockRight.performed -= LockRight;
            
            inputs.Player.OpenInventory.performed -= OpenInventory;
            inputs.Player.AShard.performed -= ShardA;
            inputs.Player.AShard.canceled -= ShardA;
            inputs.Player.BShard.performed -= ShardB;
            inputs.Player.BShard.canceled -= ShardB;
            inputs.Player.InventorySelect.performed -= InventorySelect;
            
            // UI Inputs
            inputs.UI.Select.performed -= OnSelect;
            inputs.UI.Back.performed -= OnBack;
            inputs.UI.Navigation.performed -= Navigation;
            inputs.UI.SettingsView.performed -= SettingsView;
            
            inputs.Pause.Pause.performed -= Pause;
            
            InputSystem.onActionChange -= InputActionChangeCallback;
            
            inputs.Disable();
        }

        private void InputActionChangeCallback(object obj, InputActionChange change) {
            if (obj != null && obj is InputAction action) {
                if (action.activeControl == null) return;
                
                var lastDevice = action.activeControl.device;

                if ((lastDevice.name.Equals("Keyboard") || lastDevice.name.Equals("Mouse")) && !IsKeyboardControl) {
                    IsKeyboardControl = true;
                    Cursor.visible = true;
                    OnGamepadControlled.Invoke(false);
                    Debug.Log(IsKeyboardControl ? "Switch to keyboard and mouse control" :  "Switch to gamepad control");
                }
                else if (!lastDevice.name.Equals("Keyboard") && !lastDevice.name.Equals("Mouse") && IsKeyboardControl) {
                    IsKeyboardControl = false;
                    Cursor.visible = false;
                    OnGamepadControlled.Invoke(true);
                    Debug.Log(IsKeyboardControl ? "Switch to keyboard and mouse control" :  "Switch to gamepad control");
                }
            }
        }
        
        private void PlayerMove(InputAction.CallbackContext context) => OnPlayerMove.Invoke(context.ReadValue<Vector2>());
        private void Interact(InputAction.CallbackContext context) => OnInteract.Invoke(context);
        private void SecondaryInteract(InputAction.CallbackContext context) => OnSecondaryInteract.Invoke(context);
        private void LockUp(InputAction.CallbackContext context) => OnLockUp.Invoke(context.ReadValue<float>());
        private void LockRight(InputAction.CallbackContext context) => OnLockRight.Invoke(context.ReadValue<float>());
        private void OpenInventory(InputAction.CallbackContext context) => OnInventoryOpen.Invoke(context);
        private void ShardA(InputAction.CallbackContext context) => OnShardA.Invoke(context);
        private void ShardB(InputAction.CallbackContext context) => OnShardB.Invoke(context);
        private void InventorySelect(InputAction.CallbackContext context) => OnInventorySelect.Invoke(context);
        
        private void OnSelect(InputAction.CallbackContext context) => OnSelectBtt.Invoke();
        private void OnBack(InputAction.CallbackContext context) => OnBackBtt.Invoke();

        private void Navigation(InputAction.CallbackContext context) {
            OnNavigation.Invoke(context);
        }

        private void SettingsView(InputAction.CallbackContext context) {
            OnSettingsView.Invoke(context);
        }

        private void Pause(InputAction.CallbackContext context) {
            OnPause.Invoke();
        }
    }
}
