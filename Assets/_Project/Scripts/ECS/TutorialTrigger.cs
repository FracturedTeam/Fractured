using UnityEngine;

namespace _Project.Scripts.ECS
{
    public class TutorialTrigger : MonoBehaviour
    {
        [SerializeField] private TutorialElement startElement;
        private bool triggered;


        [ContextMenu("Force Event Start")]
        void TriggerEventStart()
        {
            startElement.TriggerEventStart();
            triggered = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || triggered) 
                return;

            TriggerEventStart();
        }
    }
}
