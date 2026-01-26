using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.UI
{
    public class CreditManager : MonoBehaviour
    {
        [SerializeField] private InputAction speedUpKey;
        [SerializeField] private float maxSpeed = 3;
        private bool isSpeedingUp = false;
        private static readonly int SpeedMultiplicator = Animator.StringToHash("SpeedMultiplicator");
        private void Awake()
        {
            speedUpKey.performed += OnSpeedUp;
            speedUpKey.canceled += OnSpeedDown;
        }
        private void OnEnable()
        {
            speedUpKey.Enable();
        }

        private void OnDisable()
        {
            speedUpKey.Disable();
        }

        private void OnSpeedUp(InputAction.CallbackContext obj)
        {
           isSpeedingUp = true;
        }
        private void OnSpeedDown(InputAction.CallbackContext obj)
        {
            isSpeedingUp = false;
        }

        private void Update()
        {
            SpeedUp(isSpeedingUp);
        }

        private void SpeedUp(bool speeding)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale += (speeding? Time.deltaTime : -Time.deltaTime) , 1, maxSpeed);
        }
    }
}
