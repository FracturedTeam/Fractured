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
    [SerializeField] private bool isVisibleFromStart; 
    [SerializeField] private GlassTextScriptableObject currentTextScriptableObject;
    [SerializeField] private GlassTextLink baseText;
    [SerializeField] private GlassTextLink fragAText;
    [SerializeField] private GlassTextLink fragBText;
    [SerializeField] private GlassTextLink bothText;

    internal void Initialize()
    {
        baseText.Initialize();
        fragAText.Initialize();
        fragBText.Initialize();
        bothText.Initialize();
        
        ForceSet();
        
        SetAlpha(isVisibleFromStart && currentTextScriptableObject ? 1 : 0);
    }

    [ContextMenu("Set Texts")]
    private void ForceSet()
    {
        if (currentTextScriptableObject)
            Setup(currentTextScriptableObject);
    }

    private void SetAlpha(float alpha, float time = 0)
    {
        baseText.SetAlpha(alpha, time);
        fragAText.SetAlpha(alpha, time);
        fragBText.SetAlpha(alpha, time);
        bothText.SetAlpha(alpha, time);
    }

    internal void OnInteract(bool isColliding, Glass shard)
    {
        fragAText.OnInteract(isColliding, shard);
        fragBText.OnInteract(isColliding, shard);
        bothText.OnInteract(isColliding, shard);
    }

    public void Setup(GlassTextScriptableObject newData)
    {
        currentTextScriptableObject = newData;
        baseText.SetText(currentTextScriptableObject.baseText);
        fragAText.SetText(currentTextScriptableObject.fragAText);
        fragBText.SetText(currentTextScriptableObject.fragBText);
        bothText.SetText(currentTextScriptableObject.bothText);
    }
}
