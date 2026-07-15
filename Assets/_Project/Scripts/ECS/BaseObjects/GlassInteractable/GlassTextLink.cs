using System;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.HashSetUtil;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GlassTextLink : MonoBehaviour
{
    private int lastIndex;
    private TMP_Text baseText;
    private bool isHoveringObject;
    private int m_selectedLink = -1;
    private ObservableHashSet<Glass> shardsOnTop;
    private int underRed;
    private int underBlue;

    public void Initialize() //Initialize
    {
        baseText = GetComponent<TMP_Text>();
        shardsOnTop = new ObservableHashSet<Glass>();
        shardsOnTop.onUpdate += UpdateShards;
    }

    public void SetAlpha(float alpha, float time)
    {
        baseText.DOFade(alpha, time);
    }

    private void UpdateShards()
    {
        underBlue = 0;
        underRed = 0;

        foreach (var shard in shardsOnTop.Items)
            switch (shard.GetColor)
            {
                case ColorEnum.ColorA:
                    underBlue++;
                    break;
                case ColorEnum.ColorB:
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

    internal void OnInteract(bool isUnder, Glass shard)
    {

    }

    ///Auto Setup the collision
    private void Set2DPoints()
    {

    }

    public void SetText(string newText, ColorEnum colorEnum = ColorEnum.None, bool special = false)
    {
        if(newText.Contains($"<link='censored'>"))
        {
            var start = newText.IndexOf($"<link='censored'>", StringComparison.Ordinal);
            var end = newText.IndexOf("</link>", StringComparison.Ordinal);
            if (baseText)
                baseText.text = Replace(newText, special ? "⠀" : "█", start, end - start, colorEnum, special);
            else if (GetComponent<TMP_Text>())
                GetComponent<TMP_Text>().text =
                    Replace(newText, special ? "⠀" : "█", start, end - start, colorEnum, special);
            return;
        }
        
        var replace = AddColor(colorEnum, newText, special);
        if (baseText)
            baseText.text = replace;
        else if (GetComponent<TMP_Text>())
            GetComponent<TMP_Text>().text = replace;
    }

    private static string AddColor(ColorEnum color, string input, bool special = false)
    {
        var newString = input;
        switch (color)
        {
            case ColorEnum.ColorA:
                newString = special? "<color=#00000000>" + newString + "</color>" : "<color=yellow>" + newString + "</color>";
                break;
            case ColorEnum.ColorB:
                newString = special? "<color=#00000000>" + newString + "</color>" : "<color=#ff00ffff>" + newString + "</color>";
                break;
            case ColorEnum.Both:
                newString = special? "<color=#00000000>" + newString + "</color>" : "<color=#ffa500ff>" + newString + "</color>";
                break;
            case ColorEnum.None:
                newString = special? "<color=#00000000>" + newString + "</color>" : "<color=#ffffffff>" + newString + "</color>";
                break;
        }
        return newString;
    }
    
    static string Replace(string output, string replacement, int index, int length, ColorEnum colorEnum = ColorEnum.None, bool special = false)
    {
        var replace ="";
        for (int l = 0; l < length - "<link='censored'>".Length; l++)
        {
            replace += replacement;
        }
        
        string removeString = output.Substring(index, length);

        if (colorEnum == ColorEnum.None)
            replace = removeString;
        
        replace = AddColor(colorEnum, replace, special);
        replace = "<mspace=0.5em>" + replace + "</mspace>";
            
        return output.Replace(removeString, replace);
    }

        /*

        public void Link()
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(baseText, Mouse.current.position.value, camera);
            if ((linkIndex == -1 && m_selectedLink != -1) || linkIndex != m_selectedLink)
            {
                m_selectedLink = -1;
            }

            if (linkIndex != -1 && linkIndex != m_selectedLink)
            {
                m_selectedLink = linkIndex;

                TMP_LinkInfo linkInfo = baseText.textInfo.linkInfo[linkIndex];

                // Debug.Log("Link ID: \"" + linkInfo.GetLinkID() + "\"   Link Text: \"" + linkInfo.GetLinkText() + "\""); // Example of how to retrieve the Link ID and Link Text.

                Vector3 worldPointInRectangle;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(baseText.rectTransform, Mouse.current.position.value, camera, out worldPointInRectangle);

                print(linkInfo.GetLinkID() == "id_01" ? "01" : "test"); // 100041637: // id_01
            }
        }
        */
}
