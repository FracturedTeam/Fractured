using System;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class LockCharacter : MonoBehaviour, IEndDragHandler, IDragHandler
{
    private PadlockHud hud;
    private int current;
    [SerializeField] private float spaceBetweenCharacters = 37.89368182f;
    

    private void Start()
    {
        hud = HudManager.Instance.padLock;
        SetCharacterPosition(0);
      
    }
    
    
    
    
    public void RoundPosition()
    {
        print("round");
        GetComponent<RectTransform>().anchoredPosition = 
            new Vector2(GetComponent<RectTransform>().anchoredPosition.x, 
                Mathf.Round(GetComponent<RectTransform>().anchoredPosition.y /
                            spaceBetweenCharacters) * spaceBetweenCharacters);
    }

    public void SetCharacterPosition(int index)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, (index) * spaceBetweenCharacters);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Invoke("RoundPosition", 2);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var y = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
        if(y < 0)
            gameObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, 9 * spaceBetweenCharacters - y);
        if(y > 9 * spaceBetweenCharacters)
            gameObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, 0 * spaceBetweenCharacters + 9 * spaceBetweenCharacters - y);
    }
}
