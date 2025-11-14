using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.Systems.HashSetUtil;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GlassText : MonoBehaviour
{
    private TMP_Text text;
    private string show;
    private string BaseText;
    internal Vector2 TagPositions;
    [SerializeField] private List<PossibleText>  possibleTexts = new List<PossibleText>();
    private const string Glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
    private ObservableHashSet<Glass> shardsOnTop;


    private int underRed = 0;
    private int underBlue = 0;

    private void Start()
    {
        if (TryGetComponent(typeof(TMP_Text), out var t))
            text = (TMP_Text)t;
        else
            text = gameObject.AddComponent<TMP_Text>();
        BaseText =  text.text;
        
        underRed = 0;
        underBlue = 0;
        
        shardsOnTop = new ObservableHashSet<Glass>();
        shardsOnTop.onUpdate += UpdateShards;

        var firstCharInfo = text.textInfo.characterInfo[text.text.IndexOf("{", StringComparison.Ordinal)];
        var lastCharInfo = text.textInfo.characterInfo[3];
        TagPositions = text.transform.TransformPoint((firstCharInfo.topLeft + lastCharInfo.bottomRight) / 2f);
        print(TagPositions);
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
    
    void OnDisable() {
        shardsOnTop.onUpdate -= UpdateShards;
    }
    
    private void SetText()
    {
        show = BaseText;
        foreach (PossibleText possibleText in possibleTexts)
        {
            if(possibleText.variableName == null)
                continue;

            var random = "";
            for (int i = 0; i < possibleText.letters ; i++)
            {
                random += Glyphs[Random.Range(0, Glyphs.Length)];
            }
            
            var replace = "";

            switch (underRed)
            {
                case > 0 when underBlue > 0:
                    replace = show.Replace("{" + $"{possibleText.variableName}" + "}",
                        possibleText.both == "" ? random : $"{possibleText.both}");
                    break;
                case > 0:
                    replace = show.Replace("{" + $"{possibleText.variableName}" + "}",
                        possibleText.red == "" ? random : $"{possibleText.red}");
                    break;
                default:
                {
                    if (underBlue > 0)
                        replace = show.Replace("{" + $"{possibleText.variableName}" + "}",
                            possibleText.blue == "" ? random : $"{possibleText.blue}");
                    else
                        replace = show.Replace("{" + $"{possibleText.variableName}" + "}",
                            possibleText.basic == "" ? random : $"{possibleText.basic}");
                    break;
                }
            }
            show = replace;
        }
        text.text = show;
    }

    [Serializable]
    private struct PossibleText
    {
        public string variableName;
        public string basic;
        public string red;
        public string blue;
        public string both;
        
        
        public int letters;
    }
}
