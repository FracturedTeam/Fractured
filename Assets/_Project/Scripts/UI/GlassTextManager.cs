using System;
using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;

public class GlassTextManager :  Singleton<GlassTextManager>
{
    [SerializeField] private Canvas glassTextHolderWorldSpace;
    [SerializeField] private Canvas glassTextHolderManuscript;
    [SerializeField] private GlassText prefab;
    [SerializeField] private GlassDocument glassDocument;
    private List<GlassText> freeGlassTexts = new List<GlassText>();
    private List<GlassText> usingGlassTexts = new List<GlassText>();

    private Camera camera;

    private void Start()
    {
        camera = PlayerController.Instance.cinemachineBrain.OutputCamera;
    }

    public void SetUpWorldSpaceText(Transform newTransform, GlassTextScriptableObject data)
    {
        if (freeGlassTexts.Count <= 0)
        {
            var newGlassText = Instantiate(prefab, glassTextHolderWorldSpace.transform);
            freeGlassTexts.Add(newGlassText);
        }
        var currentUseGlassText = freeGlassTexts[0];
        currentUseGlassText.Setup(data);
        currentUseGlassText.transform.position = newTransform.position;
        currentUseGlassText.transform.eulerAngles = newTransform.eulerAngles;

        freeGlassTexts.RemoveAt(0);
        usingGlassTexts.Add(currentUseGlassText);
    }

    public void SetUpManuscriptText(GlassDocumentScriptableObject data)
    {
        glassDocument.SetUp(data);
    }
}