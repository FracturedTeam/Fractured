using System;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Systems.HashSetUtil;
using UnityEngine;

[RequireComponent(typeof(BaseObject))] 
public class GlassText : MonoBehaviour
{
    [SerializeField] private bool isVisibleFromStart; 
    [SerializeField] private GlassTextScriptableObject currentTextScriptableObject;
    [SerializeField] private GlassTextLink baseText;
    [SerializeField] private GlassTextLink fragAText;
    [SerializeField] private GlassTextLink fragBText;
    [SerializeField] private GlassTextLink bothText;

    private ObservableHashSet<Glass> shardsOnTop;
    private BaseObject baseObject;
    private bool isInitialized;
    

    internal void Initialize()
    {
        baseText.Initialize();
        fragAText.Initialize();
        fragBText.Initialize();
        bothText.Initialize();
        if (!isInitialized)
        {
            if (TryGetComponent(out BaseObject component)) baseObject = component;
            else
                throw new ArgumentNullException(
                    $"[GlassText] BaseObject on {gameObject.name} could not be found !");

            shardsOnTop = new ObservableHashSet<Glass>();
            shardsOnTop.onUpdate += UpdateShards;

            isInitialized = true;
        }

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

    private void UpdateShards()
    {
    }

    private void OnDestroy() {
        if (shardsOnTop == null) 
            return;
        
        shardsOnTop.onUpdate -= UpdateShards;
        shardsOnTop.Clear();
    }

    [ContextMenu("Manually Appear")]
    public void Appear()
    {
        SetAlpha( 1, 1);
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
        //Case 0XXX
        if (currentTextScriptableObject.baseText == "")
        {
            //Case 00XX
            if (currentTextScriptableObject.fragAText == "")
            {
                //Case 000X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 0000 - 
                    if (currentTextScriptableObject.bothText == "")
                    {
                        Error();
                        return; 
                    }
                    
                    //Case 0001 - Both
                    {
                        baseText.SetText(currentTextScriptableObject.bothText, ColorEnum.Both, true);
                        fragAText.SetText(currentTextScriptableObject.bothText, ColorEnum.Both, true);
                        fragBText.SetText(currentTextScriptableObject.bothText, ColorEnum.Both, true);
                        bothText.SetText(currentTextScriptableObject.bothText);
                        return;
                    }
                }
                //Case 001X
                {
                    //Case 0010 - B
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.fragBText, ColorEnum.ColorB);
                        fragAText.SetText(currentTextScriptableObject.fragBText, ColorEnum.ColorB);
                        fragBText.SetText(currentTextScriptableObject.fragBText);
                        bothText.SetText(currentTextScriptableObject.fragBText, ColorEnum.ColorB);
                        return;
                    }

                    //Case 0011 - BBoth
                    {
                        Error();
                        return;
                    }
                }
            }
            //Case 01XX
            {
                //Case 010X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 0100 - A
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.fragAText, ColorEnum.ColorA);
                        fragAText.SetText(currentTextScriptableObject.fragAText);
                        fragBText.SetText(currentTextScriptableObject.fragAText, ColorEnum.ColorA);
                        bothText.SetText(currentTextScriptableObject.fragAText, ColorEnum.ColorA);
                        return;
                    }
                    
                    //Case 0101 - ABoth
                    {
                        Error();
                        return;
                    }
                }
                //Case 011X
                {
                    //Case 0110 - AB
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.fragBText, ColorEnum.Both);
                        fragAText.SetText(currentTextScriptableObject.fragAText);
                        fragBText.SetText(currentTextScriptableObject.fragBText);
                        bothText.SetText(currentTextScriptableObject.fragBText, ColorEnum.Both);
                        return;
                    }
                    //Case 0111 -ABBOTH
                    {
                        Error();
                        return;
                    }
                   
                }
            }
        }
        // Case 1XXX
        {
             //Case 10XX
            if (currentTextScriptableObject.fragAText == "")
            {
                //Case 100X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 1000 - 
                    if (currentTextScriptableObject.bothText.Length == 0)
                    {
                        baseText.SetText(currentTextScriptableObject.baseText);
                        fragAText.SetText("");
                        fragBText.SetText("");
                        bothText.SetText("");
                        return;
                    }
                    //Case 1001 - Both
                    {
                        Error();
                        return;
                    }
                }
                //Case 101X
                {
                    //Case 1010 - B
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorB);
                        fragAText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorB);
                        fragBText.SetText(currentTextScriptableObject.fragBText);
                        bothText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorB);
                        return;
                    }
                    //Case 1011 - BBoth
                    {
                        Error();
                        return;
                    }
                }
            }
            //Case 11XX
            {
                //Case 110X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 1100 - A
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorA);
                        fragAText.SetText(currentTextScriptableObject.fragAText);
                        fragBText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorA);
                        bothText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorA);
                        return;
                    }
                    //Case 1101 - ABoth
                    {
                        Error();
                        return;
                    }
                }
                //Case 111X
                {
                    //Case 1110 - AB
                    if (currentTextScriptableObject.bothText == "")
                    {
                        Error();
                        return;
                    }
                    //Case 1111 -ABBOTH
                    {
                        baseText.SetText(currentTextScriptableObject.baseText, ColorEnum.Both);
                        fragAText.SetText(currentTextScriptableObject.fragAText, ColorEnum.Both);
                        fragBText.SetText(currentTextScriptableObject.fragBText, ColorEnum.Both);
                        bothText.SetText(currentTextScriptableObject.bothText, ColorEnum.Both);
                        return;
                    }
                   
                }
            }
        }
    }

    private void Error()
    {
        Debug.LogError("Error on Text Scriptable Object, Case not supported, see the documentation for more information " +
                       "<a href=\"https://docs.google.com/document/d/1IGWeNeqUure2vZgyoxgXxlqOPQhoYTO_nbDbxLvGX8A/edit?tab=t.0#heading=h.16tvc46zf4qe\">Case Table</a> and " +
                       "SetUp Document", transform);
    }
}
