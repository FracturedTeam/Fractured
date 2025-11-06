using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GlassText : MonoBehaviour
{
    private TMP_Text text;
    private string show;
    private string ar;
    [SerializeField] private List<PossibleText>  possibleTexts = new List<PossibleText>();
    private const string Glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

    private int underRed = 0;
    private int underBlue = 0;

    private void Start()
    {
        if (TryGetComponent(typeof(TMP_Text), out var t))
            text = (TMP_Text)t;
        else
            text = gameObject.AddComponent<TMP_Text>();
        ar =  text.text;
    }

    private void Update()
    {
        SetText();
    }

    private void UpdateGlass(ColorEnum colorEnum)
    {
        switch (colorEnum)
        {
            case ColorEnum.Red:
                break;
            case ColorEnum.Blue:
                break;
            case ColorEnum.Both:
                break;
        }
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
