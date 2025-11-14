using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.HashSetUtil;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GlassText : MonoBehaviour
{
    internal Vector2 TagPositions;
    private DialogueScriptableObject dialogue;
    private const string Glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
    private ObservableHashSet<Glass> shardsOnTop;
    private TMP_Text text;
    private string show;
    private string BaseText;

    private int underRed = 0;
    private int underBlue = 0;

    private void Start() {
        if (TryGetComponent(typeof(TMP_Text), out var t))
            text = (TMP_Text)t;
        else
            text = gameObject.AddComponent<TMP_Text>();
        
        text.text = "";
        
        shardsOnTop = new ObservableHashSet<Glass>();
        shardsOnTop.onUpdate += UpdateShards;
    }


    public void Setup(DialogueScriptableObject scriptableObject) {
        dialogue = scriptableObject;
        BaseText =  scriptableObject.dialogue;
        
        underRed = 0;
        underBlue = 0;
        
        var firstCharInfo = text.textInfo.characterInfo[0];
        
        
        TagPositions = text.transform.TransformPoint(firstCharInfo.topRight);

        SetText();
    }
    
    internal void OnInteract(bool isUnder, Glass shard) {
        if (isUnder) 
            shardsOnTop.Add(shard);
        else if(shardsOnTop.Contains(shard))
            shardsOnTop.Remove(shard);
    }
    
    private void Update()
    {
        UpdateShards();
        SetText();
        
        foreach (var t in text.textInfo.linkInfo)
        {
            print(underBlue);
            print(underRed);
        }
    }
    
    private void UpdateShards() {
        underBlue = 0;
        underRed = 0;
        
        foreach (var shard in shardsOnTop.Items)
            switch (shard.GetColor) {
                case ColorEnum.Blue:
                    underBlue++;
                    break;
                case ColorEnum.Red:
                    underRed++;
                    break;
                case ColorEnum.Both:
                    underBlue++;
                    underRed++;
                    break;
                default:
                    Debug.LogWarning($"[GlassInteractable] Unknown shard color {shard.GetColor}");
                    break;
            }
    }

    private void OnDisable() {
        if(shardsOnTop != null)
            shardsOnTop.onUpdate -= UpdateShards;
    }
    
    private void SetText()
    {
        if(!dialogue)
            return;
        
        show = BaseText;
        if(dialogue.variableName == null)
            return;

        var random = "";
        for (int i = 0; i < dialogue.letters ; i++)
        {
            random += Glyphs[Random.Range(0, Glyphs.Length)];
        }
            
        var replace = "";

        switch (underRed)
        {
            case > 0 when underBlue > 0:
                replace = show.Replace("{" + $"{dialogue.variableName}" + "}",
                    dialogue.both == "<u>" + "" ? $" <u>{dialogue.basic}</u>" : $"<u>{dialogue.both}</u>") + "</u>";
                break;
            case > 0:
                replace = show.Replace("{" + $"{dialogue.variableName}" + "}",
                    dialogue.red == "<u>" + "" ? $" <u>{dialogue.basic}</u>" : $"<u>{dialogue.red}</u>")+ "</u>";
                break;
            default:
            {
                if (underBlue > 0)
                    replace = show.Replace("{" + $"{dialogue.variableName}" + "}",
                        dialogue.blue == "<u>" + "" ? $" <u>{dialogue.basic}</u>" : $"<u>{dialogue.blue}</u>" + "</u>");
                else
                    replace = show.Replace("{" + $"{dialogue.variableName}" + "}",
                        dialogue.basic == "" ? random : $" <u>{dialogue.basic}</u>" );
                break;
            }
        }
        show = replace;
        text.text = show;
    }
}
