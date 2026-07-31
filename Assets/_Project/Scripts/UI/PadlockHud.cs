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
    }

   

    public void SetPos(Vector2 newPos)
    {
        codeObject.transform.position = newPos;
    }

}
