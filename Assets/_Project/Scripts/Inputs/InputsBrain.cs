using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Inputs {
    public class InputsBrain : MonoBehaviour {
        private InputSystem_Actions inputs;

        public event Action<Vector2> OnPlayerMove = delegate { };
        public event Action<InputAction.CallbackContext> OnInteract = delegate { };
        public event Action<InputAction.CallbackContext> OnSecondaryInteract = delegate { };
        public event Action<float> OnLockUp = delegate { };
        public event Action<float> OnLockRight = delegate { };
    
        private void Awake() {
            inputs = new InputSystem_Actions();
        }

        private void OnEnable() {
            inputs.Player.Move.performed += PlayerMove;
            inputs.Player.Move.canceled += PlayerMove;
            inputs.Player.Interact.performed += Interact;
            inputs.Player.Interact.canceled += Interact;
            inputs.Player.SecondaryInteract.performed += SecondaryInteract;
            inputs.Player.SecondaryInteract.canceled += SecondaryInteract;
            inputs.Player.LockUp.performed += LockUp;
            inputs.Player.LockRight.performed += LockRight;
        
            inputs.Enable();
        }

        private void OnDisable() {
            inputs.Player.Move.performed -= PlayerMove;
            inputs.Player.Move.canceled -= PlayerMove;
            inputs.Player.Interact.performed -= Interact;
            inputs.Player.Interact.canceled -= Interact;
            inputs.Player.SecondaryInteract.performed -= SecondaryInteract;
            inputs.Player.SecondaryInteract.canceled -= SecondaryInteract;
            inputs.Player.LockUp.performed -= LockUp;
            inputs.Player.LockRight.performed -= LockRight;
            
            inputs.Disable();
        }

        private void PlayerMove(InputAction.CallbackContext context) {
            OnPlayerMove.Invoke(context.ReadValue<Vector2>());
        }

        private void Interact(InputAction.CallbackContext context) {
            OnInteract.Invoke(context);
        }

        private void SecondaryInteract(InputAction.CallbackContext context) {
            OnSecondaryInteract.Invoke(context);
        }

        private void LockUp(InputAction.CallbackContext context) {
            OnLockUp.Invoke(context.ReadValue<float>());
        }

        private void LockRight(InputAction.CallbackContext context) {
            OnLockRight.Invoke(context.ReadValue<float>());
        }

    }
}
