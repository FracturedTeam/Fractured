using System.Collections.Generic;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.UI;
using UnityEngine;

public class GlassDocument : MonoBehaviour {
    [SerializeField] private float distance;
    [SerializeField] private List<GlassDocumentTemplate> templates;
    
    public float targetScreenFraction = 0.35f;
    public float meshHeightAtScaleOne = 1f;
    
    private Transform cameraTrans;
    
    private EventBinding<DocumentEvent> documentEventBinding;

    private void Start() {
        foreach (var temp in templates) {
            temp.gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        documentEventBinding = new EventBinding<DocumentEvent>(OpenDocument);
        EventBus<DocumentEvent>.Register(documentEventBinding);
    }

    private void OnDisable() {
        EventBus<DocumentEvent>.Deregister(documentEventBinding);
    }

    private void OpenDocument(DocumentEvent e) {
        SetUp(e.document, e.isOn);
    }
    
    private void SetUp(GlassDocumentScriptableObject data, bool isOn = true) {
        foreach (var temp in templates) {
            temp.gameObject.SetActive(false);
        }
        
        HudManager.Instance.interact.ShowInteractionInspect(isOn);
        
        cameraTrans = PlayerController.Instance.cinemachineBrain.OutputCamera.transform;
        
        transform.position = cameraTrans.position + cameraTrans.forward * distance;
        
        transform.LookAt(cameraTrans); 
        transform.eulerAngles = new Vector3(30, 180 + transform.eulerAngles.y, 0);
        
        templates[(int)data.type].gameObject.SetActive(isOn);
        templates[(int)data.type].SetUp(data);

        if (isOn) {
            UpdateDocumentScale();
        }
    }
    
    private void UpdateDocumentScale() {
        var cam = PlayerController.Instance.cinemachineBrain.OutputCamera;
         
        var fov = cam.fieldOfView;
        var visibleHeight= 2f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
        var targetWorld= visibleHeight * targetScreenFraction;
        var scale= targetWorld / meshHeightAtScaleOne;

        transform.localScale = Vector3.one * scale;
    }
}