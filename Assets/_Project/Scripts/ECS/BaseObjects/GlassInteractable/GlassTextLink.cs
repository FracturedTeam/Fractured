using System;
using _Project.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GlassTextLink : MonoBehaviour, IPointerEnterHandler
{
    private int lastIndex;
    private Camera camera;
    private TMP_Text baseText;

    private void Start()
    {
        camera = PlayerController.Instance.cinemachineBrain.OutputCamera;
        baseText = GetComponent<TMP_Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
        var newIndex = TMP_TextUtilities.FindIntersectingLink(baseText,Mouse.current.position.value, camera);
        if (newIndex == -1)
            return;

        newIndex = TMP_TextUtilities.FindIntersectingWord(baseText,Mouse.current.position.value, camera);
        if (lastIndex == newIndex)
            return;
        lastIndex = newIndex;

        TMP_WordInfo wordInfo = baseText.textInfo.wordInfo[lastIndex];
        var w = wordInfo.textComponent.text.Substring(wordInfo.firstCharacterIndex, wordInfo.characterCount);
        var nw = string.Format("<u><b>{0}</b></u>", w);
        baseText.text = baseText.text.Replace(w, nw);
        Debug.Log(w);
    }
}
