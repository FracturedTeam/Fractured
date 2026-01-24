using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class GlassEditionAreaTrigger : MonoBehaviour {
        [SerializeField] private ColorEnum colorEdition;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player"))
                GameInitializer.Instance.SetEditableArea(true, colorEdition);
        }

        private void OnTriggerStay(Collider other) {
            if (other.CompareTag("Player"))
                GameInitializer.Instance.SetEditableArea(true, colorEdition);
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player"))
                GameInitializer.Instance.SetEditableArea(false, colorEdition);
        }
    }
}