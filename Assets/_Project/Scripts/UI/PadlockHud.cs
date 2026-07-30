using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PadlockHud : MonoBehaviour
{
    [SerializeField] private CanvasGroup codeObject;
    [SerializeField] private TMP_Text textObject;
    [SerializeField] private Image selectionImage;

    [SerializeField] private float spaceBetweenCharacters = 41.5f;

    [SerializeField] private List<TMP_Text> numbersTMP;

    private void Start()
    { 
        RoundPosition();
    }

    public void RoundPosition()
    {
        foreach (var number in numbersTMP)
        {
            number.gameObject.GetComponent<RectTransform>().anchoredPosition = 
                new Vector2(number.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 
                Mathf.Round(number.gameObject.GetComponent<RectTransform>().anchoredPosition.y /
                            spaceBetweenCharacters) * spaceBetweenCharacters);

            if (number.gameObject.GetComponent<RectTransform>().anchoredPosition.y == 0)
                number.gameObject.GetComponent<RectTransform>().anchoredPosition =   
                    new Vector2(number.gameObject.GetComponent<RectTransform>().anchoredPosition.x, spaceBetweenCharacters * 9);
        }
    }

    public void SetPos(Vector2 newPos)
    {
        codeObject.transform.position = newPos;
    }

}
