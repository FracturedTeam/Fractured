using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.HashSetUtil;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GlassText : MonoBehaviour
{
    //debug, will be changed 
    [SerializeField] private Image forceShowImage;
    
    internal Vector2 TagPositions;
    private DialogueScriptableObject dialogue;
    private const string Glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
    private ObservableHashSet<Glass> shardsOnTop;
    private TMP_Text text;
    public CanvasGroup textCanva;
    private string show;
    private string BaseText;

    private int underRed = 0;
    private int underBlue = 0;

    private Tweener tween;

    private void Start() {
        if (TryGetComponent(typeof(TMP_Text), out var t))
            text = (TMP_Text)t;
        else
            text = gameObject.AddComponent<TMP_Text>();
        
        text.text = "";
        textCanva.alpha = 0;
        forceShowImage.gameObject.SetActive(false);
        
        shardsOnTop = new ObservableHashSet<Glass>();
        shardsOnTop.onUpdate += UpdateShards;
    }
    

    [SerializeField] private int i;

    public void Setup(DialogueScriptableObject scriptableObject) {
        tween.Kill();
        
        if (scriptableObject == null) {
            tween = textCanva.DOFade(0, 0.5f);
            textCanva.blocksRaycasts = false;
            textCanva.interactable = false;
            forceShowImage.gameObject.SetActive(false);
            
            //text.text = "";
            dialogue = null;
            return;
        }
        
        forceShowImage.gameObject.SetActive(true);
        tween = textCanva.DOFade(1, 0.5f);
        textCanva.blocksRaycasts = true;
        textCanva.interactable = true;
        
        dialogue = scriptableObject;
        BaseText =  scriptableObject.dialogue;
        text.text = BaseText;

        //BaseText
        var begin = 0;      
        var ending = 0;
        text.ForceMeshUpdate(); 
        

        underRed = 0;
        underBlue = 0;
        

        for (int j = 0; j < text.textInfo.characterCount; j++)
        {
            if (text.textInfo.characterInfo[j].character == '{')
            {
                print("first" + j);
                begin = j;
            }
            if (text.textInfo.characterInfo[j].character == '}')
            {
                print("last" + j);
                ending = j;
            }
        }
        
        SetText();
        TagPositions = (text.textInfo.characterInfo[begin].bottomLeft);
        print(TagPositions);
        Instantiate(text, TagPositions, text.gameObject.transform.rotation, text.transform.parent);
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
                    dialogue.both == " " ? $" <#B761FA><u>{dialogue.basic}</u>" : $"<#B761FA><u>{dialogue.both}</u>")  +  "<color=\"white\"> </u>";
                break;
            case > 0:
                replace = show.Replace("{" + $"{dialogue.variableName}" + "}",
                    dialogue.red ==  "" ? $" <#B761FA><u>{dialogue.basic}</u>" : $"<#B761FA><u>{dialogue.red}</u>")  +  "<color=\"white\"></u>";
                break;
            default:
            {
                if (underBlue > 0)
                    replace = show.Replace("{" + $"{dialogue.variableName}" + "}",
                        dialogue.blue ==  "" ? $" <#B761FA><u>{dialogue.basic}</u>" : $"<#B761FA><u>{dialogue.blue}</u>" + "<color=\"white\"></u>");
                else
                    replace = show.Replace("{" + $"{dialogue.variableName}" + "}",
                        dialogue.basic == "" ? random : $"<#B761FA> <u> {dialogue.basic}</u> <color=\"white\">" );
                break;
            }
        }
        show = replace;
        text.text = show;
    }
}
