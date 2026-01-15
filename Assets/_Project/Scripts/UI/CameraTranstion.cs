using System;
using _Project.Scripts.UI;
using UnityEngine;

public class CameraTranstion : MonoBehaviour
{
    [SerializeField] Transform nextCameraPos;
    [SerializeField] GameObject openPanel;

    public void OnTrigger()
    {
        if(nextCameraPos)
            MenuManager.Instance.ChangeTarget(nextCameraPos);
        if(openPanel)
            openPanel.SetActive(true);
    }
}
