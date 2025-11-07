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
    private string ar;
    internal List<Vector2> TagPositions = new List<Vector2>();
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
        ar =  text.text;
        
        underRed = 0;
        underBlue = 0;
        
        shardsOnTop = new ObservableHashSet<Glass>();
        shardsOnTop.onUpdate += UpdateShards;
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
        show = ar;
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

    public void SetLinkHovering(bool on )
    {
       text.color = on ? Color.darkRed : Color.wheat;
    }
    
    Vector2 CalcLinkCenterPosition(TMP_LinkInfo linkInfo)
    {
        Vector3 bottomLeft = Vector3.zero;
        Vector3 topRight = Vector3.zero;

        float maxAscender = -Mathf.Infinity;
        float minDescender = Mathf.Infinity;
        
        TMP_CharacterInfo currentCharInfo = linkInfo.textComponent.textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex];

        maxAscender = Mathf.Max(maxAscender, currentCharInfo.ascender);
        minDescender = Mathf.Min(minDescender, currentCharInfo.descender);

        bottomLeft = new Vector3(currentCharInfo.bottomLeft.x, currentCharInfo.descender, 0);

        bottomLeft = transform.TransformPoint(new Vector3(bottomLeft.x, minDescender, 0));
        topRight = transform.TransformPoint(new Vector3(currentCharInfo.topRight.x, maxAscender, 0));

        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        Vector2 centerPosition = bottomLeft;
        centerPosition.x += width / 2;
        centerPosition.y += height / 2;

        return centerPosition;
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
