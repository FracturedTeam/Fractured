using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlassText : MonoBehaviour
{
    private TMP_Text text;
    private string show;
    [SerializeField] private List<PossibleText>  possibleTexts = new List<PossibleText>();

    private void Start()
    {
        if (TryGetComponent(typeof(TMP_Text), out var t))
            text = (TMP_Text)t;
        else
            text = gameObject.AddComponent<TMP_Text>();
        
        show = text.text;
        
        foreach (PossibleText possibleText in possibleTexts)
        {
            if(possibleText.basic == null)
                return;

            
            for (int i = 0; i < possibleText.basic.Split(' ').Length - 1; i++)
            {
                
            }

            var replace = show.Replace("{" +$"{possibleText.variableName}"+"}", possibleText.basic == "" ? "": $"{possibleText.basic}");
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
    }
    
}
