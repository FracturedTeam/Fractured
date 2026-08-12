using System.Collections.Generic;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;

public class GlassDocument : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private List<GlassDocumentTemplate> templates;
    
    private Transform cameraTrans;
    
    public void SetUp(GlassDocumentScriptableObject data, bool isOn = true) {
        foreach (var temp in templates) {
            temp.gameObject.SetActive(false);
        }
        
        HudManager.Instance?.interact?.ForceInteractHUDVisibility(!isOn);
        cameraTrans = PlayerController.Instance.cinemachineBrain.OutputCamera.transform;
        transform.position = cameraTrans.position + cameraTrans.forward * distance;
        transform.LookAt(cameraTrans);
        transform.eulerAngles = new Vector3(20, 180 + transform.eulerAngles.y, 0);
        templates[(int)data.type].gameObject.SetActive(isOn);
        templates[(int)data.type].SetUp(data);
    }
}