using System;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;
    
    private void OnTriggerExit(Collider other) {
        
        Vector3 exitDir = (other.transform.position - transform.position).normalized;;
        Vector3 localExitDir = transform.InverseTransformDirection(exitDir);

        //Modifier cette partie de code pour ajouter des check plus propre de si tel caméra est bien référencer
        if (Mathf.Abs(localExitDir.x) > Mathf.Abs(localExitDir.z)) {
            Debug.Log(localExitDir.x > 0 ? "Exited from right side" : "Exited from left side");

            if (customInspectorObjects.swapCamera) {
                if(!customInspectorObjects.cameraOnRight.IsLive && localExitDir.x > 0) {
                    SetCameraPriorityZero();
                    if(customInspectorObjects.cameraOnRight)
                        customInspectorObjects.cameraOnRight.Priority = 1;
                }
                else if (!customInspectorObjects.cameraOnLeft.IsLive && localExitDir.x < 0) {
                    SetCameraPriorityZero();
                    if(customInspectorObjects.cameraOnLeft)
                        customInspectorObjects.cameraOnLeft.Priority = 1;
                }
            }
        }
        else {
            Debug.Log(localExitDir.z > 0 ? "Exited from front side" : "Exited from back side");
            
            if (customInspectorObjects.swapCamera) {
                if(!customInspectorObjects.cameraOnFront.IsLive && localExitDir.z > 0) {
                    SetCameraPriorityZero();
                    if(customInspectorObjects.cameraOnFront)
                        customInspectorObjects.cameraOnFront.Priority = 1;
                }
                else if (!customInspectorObjects.cameraOnBack.IsLive && localExitDir.z < 0) {
                    SetCameraPriorityZero();
                    if(customInspectorObjects.cameraOnBack)
                        customInspectorObjects.cameraOnBack.Priority = 1;
                }
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
    public bool swapCamera = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineCamera cameraOnLeft;
    [HideInInspector] public CinemachineCamera cameraOnRight;
    [HideInInspector] public CinemachineCamera cameraOnFront;
    [HideInInspector] public CinemachineCamera cameraOnBack;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = .35f;
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}