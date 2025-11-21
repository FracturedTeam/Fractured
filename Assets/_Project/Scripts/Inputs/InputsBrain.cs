using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Inputs {
    public class InputsBrain : MonoBehaviour {
        private InputSystem_Actions inputs;

        public event Action<Vector2> OnPlayerMove = delegate { };
        public event Action<InputAction.CallbackContext> OnInteract = delegate { };
    
        private void Awake() {
            inputs = new InputSystem_Actions();
        }

        private void OnEnable() {
            inputs.Player.Move.performed += PlayerMove;
            inputs.Player.Move.canceled += PlayerMove;
            inputs.Player.Interact.performed += Interact;
            inputs.Player.Interact.canceled += Interact;
        
            inputs.Enable();
        }

        private void OnDisable() {
            inputs.Player.Move.performed -= PlayerMove;
            inputs.Player.Move.canceled -= PlayerMove;
            inputs.Player.Interact.performed -= Interact;
            inputs.Player.Interact.canceled -= Interact;
        }

        private void PlayerMove(InputAction.CallbackContext context) {
            OnPlayerMove.Invoke(context.ReadValue<Vector2>());
        }

        private void Interact(InputAction.CallbackContext context) {
            OnInteract.Invoke(context);
        }
    }
}
