using System;
using System.Collections;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.HashSetUtil;
using _Project.Scripts.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GlassText : MonoBehaviour
{
    [SerializeField] private GlassTextScriptableObject currentTextScriptableObject;
    [SerializeField] private GlassTextLink baseText;
    [SerializeField] private GlassTextLink fragAText;
    [SerializeField] private GlassTextLink fragBText;
    [SerializeField] private GlassTextLink bothText;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        if (currentTextScriptableObject)
            Setup(currentTextScriptableObject);
        canvasGroup.alpha = 0;
    }

    public void Setup(GlassTextScriptableObject newData)
    {
        canvasGroup.DOFade(0, 0);
        currentTextScriptableObject = newData;
        baseText.SetText(currentTextScriptableObject.baseText);
        fragAText.SetText(currentTextScriptableObject.fragAText);
        fragBText.SetText(currentTextScriptableObject.fragBText);
        bothText.SetText(currentTextScriptableObject.bothText);
        canvasGroup.DOFade(1, 1);
    }
}
