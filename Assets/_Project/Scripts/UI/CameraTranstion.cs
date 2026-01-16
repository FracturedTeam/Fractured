using System;
using _Project.Scripts.UI;
using UnityEngine;

public class CameraTranstion : MonoBehaviour
{
    [SerializeField] Transform nextCameraPos;
    [SerializeField] Transform previousCameraPos;
    [SerializeField] GameObject openPanel;
    private bool returning;

    public void OnTrigger()
    {
        if(nextCameraPos) 
            MenuManager.Instance.ChangeTarget(returning ? previousCameraPos ? previousCameraPos: nextCameraPos : nextCameraPos);
        if(openPanel)
            openPanel.SetActive(true);
        returning = !returning;
    }
}
