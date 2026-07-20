using System;
using _Project.Scripts.GameServices;
using _Project.Scripts.Systems.Timers;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player.Camera {
    public class CameraControlTrigger : MonoBehaviour
    {
        public CustomInspectorObjects customInspectorObjects;
        private Collider collider;
        private bool isPlayerIn;

        private Collider[] playerCollider = new Collider[1];
        private int Size;
        private Vector3 extentSize;
        private LayerMask mask;
        
        private readonly CountdownTimer countdownTimer = new (0.1f);
        
        private void Start() {
            collider = GetComponent<Collider>();
            extentSize = new Vector3(
                collider.bounds.size.x * transform.lossyScale.x, 
                collider.bounds.size.y * transform.lossyScale.y, 
                collider.bounds.size.z * transform.lossyScale.z
            );
            
            mask =  LayerMask.GetMask("Player");
        }

        void OnEnable() {
            countdownTimer.OnTimerStop += GameInitializer.Instance.RepositionGlass;
        }
        
        // void OnDisable() {
        //     countdownTimer.OnTimerStop -= GameInitializer.Instance.RepositionGlass;
        //     countdownTimer.Dispose();
        // }
        
        private void Update() {
            DetectPlayer();
        }

        private void DetectPlayer() {
            Size = Physics.OverlapBoxNonAlloc(transform.position, extentSize, playerCollider,
                transform.rotation, mask);
            
            if(Size == 0 && !isPlayerIn)
                return;
            
            if (Size == 0 && isPlayerIn) {
                isPlayerIn = false;
                SwitchCamera();
                return;
            }
            
            if(Size == 1 && !isPlayerIn) 
                isPlayerIn = true;

        }

        private void SwitchCamera() {
            var exitDir = (PlayerController.Instance.transform.position - transform.position);
            var localExitDir = transform.InverseTransformDirection(exitDir);
            
            countdownTimer.Start();
            
            if (Mathf.Abs(localExitDir.x) > 0 && Mathf.Abs(localExitDir.z) < transform.localScale.z) {
                if (localExitDir.x > 0) {
                    if (!customInspectorObjects.cameraOnRight) return;
                    
                    SetCameraPriorityZero();
                    customInspectorObjects.cameraOnRight.Priority = 1;
                }
                else {
                    if (!customInspectorObjects.cameraOnLeft) return;
                    
                    SetCameraPriorityZero();
                    customInspectorObjects.cameraOnLeft.Priority = 1;
                }
            }
            else {
                if (localExitDir.z > 0) {
                    if (!customInspectorObjects.cameraOnFront) return;
                    
                    SetCameraPriorityZero();
                    customInspectorObjects.cameraOnFront.Priority = 1;
                }
                else {
                    if (!customInspectorObjects.cameraOnBack) return;
                    
                    SetCameraPriorityZero();
                    customInspectorObjects.cameraOnBack.Priority = 1;
                }
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.limeGreen;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 4);
            
            Gizmos.color = Color.deepPink;
            Gizmos.DrawLine(transform.position, transform.position - transform.right * 4);
            
            Gizmos.color = Color.cadetBlue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 4);
            
            Gizmos.color = Color.darkOrange;
            Gizmos.DrawLine(transform.position, transform.position - transform.forward * 4);
        }
        
        private void SetCameraPriorityZero() {
            if(customInspectorObjects.cameraOnLeft)
                customInspectorObjects.cameraOnLeft.Priority = 0;
            if(customInspectorObjects.cameraOnRight)
                customInspectorObjects.cameraOnRight.Priority = 0;
            if(customInspectorObjects.cameraOnFront)
                customInspectorObjects.cameraOnFront.Priority = 0;
            if(customInspectorObjects.cameraOnBack)
                customInspectorObjects.cameraOnBack.Priority = 0;
        }
    }


    [Serializable]
    public class CustomInspectorObjects
    {
        [HideInInspector] public CinemachineCamera cameraOnLeft;
        [HideInInspector] public CinemachineCamera cameraOnRight;
        [HideInInspector] public CinemachineCamera cameraOnFront;
        [HideInInspector] public CinemachineCamera cameraOnBack;
    }
}
