using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class TutorialElement : MonoBehaviour
    {
        [SerializeField] Sprite eventPicto;
        [SerializeField] string eventText;
    
        [ContextMenu("Force Event Start")]
        internal void TriggerEventStart()
        {
            print("TriggerEventStart");
            HudManager.Instance.EventInteraction(GetComponent<BaseObject>().GetUIPosition(true), eventText, eventPicto);
        }
    
        [ContextMenu("Force Event Stop")]
        internal void TriggerEventStop()
        {
            HudManager.Instance.StopEventInteraction();
        }

    }
}
