using System;
using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.HashSetUtil;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlassText : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> texts = new ();
    internal Vector2 TagPositions;
    private DialogueScriptableObject dialogue;
    private const string Glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
    private ObservableHashSet<Glass> shardsOnTop = new();
    public CanvasGroup textCanva;
    private string show;
    private string BaseText;

    private int underRed = 0;
    private int underBlue = 0;

    private Tweener tween;

    private void Start() {
        textCanva.alpha = 0;
        
        shardsOnTop = new ObservableHashSet<Glass>();
        shardsOnTop.onUpdate += UpdateShards;
    }
    

    public void Setup(DialogueScriptableObject scriptableObject) {
        tween.Kill();
        
        if (scriptableObject == null) {
            tween = textCanva.DOFade(0, 0.5f);
            textCanva.blocksRaycasts = false;
            textCanva.interactable = false;
            
            //text.text = "";
            dialogue = null;
            return;
        }
        
        tween = textCanva.DOFade(1, 0.5f);
        textCanva.blocksRaycasts = true;
        textCanva.interactable = true;
        
        dialogue = scriptableObject;
        BaseText =  scriptableObject.dialogue;
        texts[0].text = BaseText;

        underRed = 0;
        underBlue = 0;
        
        SetText();
    }
    
    internal void OnInteract(bool isUnder, Glass shard) {
        if(!shard)
            return;
        if (isUnder) 
            shardsOnTop.Add(shard);
        else if(shardsOnTop.Contains(shard))
            shardsOnTop.Remove(shard);
    }
    
    private void Update()
    {
        UpdateShards();
        SetText();
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
            
        var first = "";
        var middle = "";
        var last = "";

        switch (underRed)
        {
            case > 0 when underBlue > 0:
                middle = dialogue.both == " " ? $" <#B761FA><u>{dialogue.basic}</u>" : $" <#B761FA><u>{dialogue.both}</u>"  +  "<color=\"white\"> </u>";
                break;
            case > 0:
                middle = dialogue.red ==  "" ? $" <#B761FA><u>{dialogue.basic}</u>" : $" <#B761FA><u>{dialogue.red}</u>" +  "<color=\"white\"></u>";
                break;
            default:
            {
                if (underBlue > 0)
                    middle = dialogue.blue ==  "" ? $" <#B761FA><u>{dialogue.basic}</u>" : $" <#B761FA><u>{dialogue.blue}</u>" + "<color=\"white\"></u>";
                else
                    middle = dialogue.basic == "" ? random : $" <#B761FA><u>{dialogue.basic}</u> <color=\"white\">" ;
                break;
            }
        }
        texts[0].text = show.Split('{')[0];
        texts[1].text = middle;
        texts[2].text = show.Contains('}') ? show.Split('}')[1] : "";
        TagPositions = texts[1].transform.position;
    }
}
