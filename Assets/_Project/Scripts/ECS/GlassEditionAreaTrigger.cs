using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class GlassEditionAreaTrigger : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                GameInitializer.Instance.SetEditableArea(true);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                GameInitializer.Instance.SetEditableArea(false);
            }
        }
        
    }
}