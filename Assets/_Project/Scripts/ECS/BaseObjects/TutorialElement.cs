using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialElement : MonoBehaviour
{
    [SerializeField] Sprite eventPicto;
    [SerializeField] string eventText;
    
    internal void TriggerEventStart()
    {
        print("TriggerEventStart");
        HudManager.Instance.EventInteraction(Camera.main.WorldToScreenPoint(GetComponent<BaseObject>().GetUIPosition(true)), eventText, eventPicto);
    }
    
    internal void TriggerEventStop()
    {
        HudManager.Instance.StopEventInteraction();
    }

}
