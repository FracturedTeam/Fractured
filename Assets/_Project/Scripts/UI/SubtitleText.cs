using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.HashSetUtil;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SubtitleText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private DialogueScriptableObject dialogue;
    public CanvasGroup textCanva;
    private string show;

    private Tweener tween;

    private void Start() {
        textCanva.alpha = 0;
    }
    

    public void Setup(DialogueScriptableObject scriptableObject) {
        tween?.Kill();
        SetText();
    }
    

    private void OnDisable() {
        tween?.Kill();
    }
    
    private void SetText()
    {
        if(!dialogue)
            return;
        text.text = dialogue.dialogue;
        textCanva.alpha = 1;
    }
}
