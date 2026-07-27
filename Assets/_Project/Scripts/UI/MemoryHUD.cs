using System;
using _Project.Scripts.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MemoryHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text memoryDialogue;
    [SerializeField] private CanvasGroup confirmMemoryButton;

    private void Start()
    {
        confirmMemoryButton.alpha = 0;
        SetActiveMemoryButton(false);
        memoryDialogue.text = "";
    }
    public void SetActiveMemoryButton(bool isOn)
    {
        confirmMemoryButton.DOFade(isOn ? 1 : 0, isOn ? 0.5f : 0);
    }
    
    public void SetMemoryDialogue(string dialogue, Vector3 pos)
    {
        memoryDialogue.DOFade(dialogue == "" ? 0 : 1, .5f);
        if(dialogue == "") return;
        memoryDialogue.text = dialogue;
        var newPos = PlayerController.Instance.cinemachineBrain.OutputCamera.WorldToScreenPoint(pos);
        newPos = new Vector3(newPos.x, newPos.y - 425);
        memoryDialogue.gameObject.transform.position = newPos;
    }
    
}
