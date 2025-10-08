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

        if (Mathf.Abs(localExitDir.x) > Mathf.Abs(localExitDir.z)) {
            Debug.Log(localExitDir.x > 0 ? "Exited from right side" : "Exited from left side");

            if (customInspectorObjects.swapCamera) {
                if(customInspectorObjects.cameraOnLeft.IsLive && localExitDir.x > 0) {
                    customInspectorObjects.cameraOnLeft.Priority = 0;
                    customInspectorObjects.cameraOnRight.Priority = 1;
                }
                else if (customInspectorObjects.cameraOnRight.IsLive && localExitDir.x < 0) {
                    customInspectorObjects.cameraOnLeft.Priority = 1;
                    customInspectorObjects.cameraOnRight.Priority = 0;
                }
            }
        }
        else
            Debug.Log(localExitDir.z > 0 ? "Exited from front side" : "Exited from back side");
        
        
    }
}


[Serializable]
public class CustomInspectorObjects
{
    public bool swapCamera = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineCamera cameraOnLeft;
    [HideInInspector] public CinemachineCamera cameraOnRight;

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