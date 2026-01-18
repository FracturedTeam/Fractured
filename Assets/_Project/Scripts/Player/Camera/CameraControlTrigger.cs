using System;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player.Camera {
    public class CameraControlTrigger : MonoBehaviour
    {
        public CustomInspectorObjects customInspectorObjects;
        
        private void OnTriggerExit(Collider other) {
            if(!other.CompareTag("Player")) return;
            
            Vector3 exitDir = (other.transform.position - transform.position).normalized;;
            Vector3 localExitDir = transform.InverseTransformDirection(exitDir);

            if (Mathf.Abs(localExitDir.x) > Mathf.Abs(localExitDir.z)) {
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
