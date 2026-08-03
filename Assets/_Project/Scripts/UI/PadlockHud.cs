using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PadlockHud : MonoBehaviour
{
    [SerializeField] private CanvasGroup codeObject;
    [SerializeField] private Image selectionImage;
    private LockCharacter character;
    private PadlockAttribute current;
    [SerializeField] private List<LockCharacter> characters = new List<LockCharacter>();

    private void Start()
    {
        codeObject.DOFade( 0, 0);
    }

    public void SetCurrent(PadlockAttribute newLock)
    {
        codeObject.DOFade(newLock ? 1 : 0, 0.5f);
        if(!newLock)
            return;

        codeObject.transform.position =
            PlayerController.Instance.cinemachineBrain.OutputCamera.WorldToScreenPoint(newLock.transform.position) + (Vector3)newLock.offset;
        current = newLock;
        UpdateCode();
    }

    public void ScrollInput(int newCode, LockCharacter newSelected)
    {
        HudManager.Instance.padLock.SetSelected(newSelected, true);
        
        var index = newSelected == characters[0] ? 0 :
            newSelected == characters[1] ? 1 :
            newSelected == characters[2] ? 2 :
            newSelected == characters[3] ? 3 : 99;
        
        if(index == 99)
            return;

        var currentNumber = current.currentCode;
        var numberAtIndex = index switch
        {
            0 => currentNumber / 1000,
            1 => (currentNumber % 1000) / 100,
            2 => ((currentNumber % 1000) % 100) / 10,
            3 =>((currentNumber % 1000) % 100) % 10
        };
        
        print(currentNumber);
        print( Mathf.Pow(10, 3 - index));
        print( numberAtIndex * Mathf.Pow(10, 3 - index));
        print(currentNumber - numberAtIndex * Mathf.Pow(10, 3 - index));
        print(currentNumber - numberAtIndex * Mathf.Pow(10, 3 - index) + newCode * Mathf.Pow(10, 3 - index));
        
        
        current.ForceSetInput((int)(currentNumber - numberAtIndex * Mathf.Pow(10, 3 - index) + newCode * Mathf.Pow(10, 3 - index)), index);
        
    }
  
    public void UpdateCode()
    {
        var currentNumber = current.currentCode;
        characters[0].SetCharacterPosition(currentNumber / 1000);
        characters[1].SetCharacterPosition((currentNumber % 1000) / 100);
        characters[2].SetCharacterPosition(((currentNumber % 1000) % 100) / 10);
        characters[3].SetCharacterPosition(((currentNumber % 1000) % 100) % 10);
    }

    public void SetSelected(LockCharacter newChar, bool isSelected)
    {
        if (!isSelected)
        {
            newChar.SetSelectionState(false);
        }

        character?.SetSelectionState(false);
        
        character = isSelected ? newChar : newChar == character ? null : character;
        
        if(character && isSelected)
            character.SetSelectionState(true);
    }
    
    public void SetSelected(int newChar)
    {
        if(newChar > characters.Count)
            return;
        
        character?.SetSelectionState(false);
        character = characters[newChar];
        character.SetSelectionState(true);
    }
}
