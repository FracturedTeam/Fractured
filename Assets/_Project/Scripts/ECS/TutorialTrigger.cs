using UnityEngine;

namespace _Project.Scripts.ECS
{
    public class TutorialTrigger : MonoBehaviour
    {
        [SerializeField] private TutorialElement startElement;
        private bool triggered;
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") && !triggered)
                startElement.TriggerEventStart();
        }
    }
}
