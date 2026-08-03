using System;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class LockCharacter : MonoBehaviour, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private PadlockHud hud;
    private RectTransform trans;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float spaceBetweenCharacters = 37.89368182f;
    

    private void Start()
    {
        hud = HudManager.Instance.padLock;
        trans = GetComponent<RectTransform>();
        SetCharacterPosition(0);
    }
    
    public void RoundPosition()
    {
        print("round");
        trans.anchoredPosition = 
            new Vector2(trans.anchoredPosition.x, 
                Mathf.Round(trans.anchoredPosition.y /
                            spaceBetweenCharacters) * spaceBetweenCharacters);
        
        
        var current = (trans.anchoredPosition.y / spaceBetweenCharacters) ;
        hud.ScrollInput((int)current,this);
    }

    public void SetCharacterPosition(int index)
    {
        trans.anchoredPosition =
            new Vector2(trans.anchoredPosition.x, (index) * spaceBetweenCharacters);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Invoke("RoundPosition", 2);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var y = trans.anchoredPosition.y;
        if(y < 0)
            trans.anchoredPosition =
                new Vector2(trans.anchoredPosition.x, 9 * spaceBetweenCharacters - y);
        if(y > 9 * spaceBetweenCharacters)
            trans.anchoredPosition =
                new Vector2(trans.anchoredPosition.x, 0 * spaceBetweenCharacters + 9 * spaceBetweenCharacters - y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hud)
            hud = HudManager.Instance.padLock;
        
        hud.SetSelected(this, true);
    }

    public void SetSelectionState(bool isSelected)
    {
        text.color = isSelected ? Color.red : Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hud.SetSelected(this, false);
    }
}
