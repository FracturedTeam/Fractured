using System;
using _Project.Scripts.UI;
using UnityEngine;

public class CameraTranstion : MonoBehaviour
{
    [SerializeField] Transform nextCameraPos;

    public void OnTrigger()
    {
        MenuManager.Instance.ChangeTarget(nextCameraPos);
    }
}
