    using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class GlassEditionAreaTrigger : MonoBehaviour {
        [SerializeField] private ColorEnum colorEdition;
        [SerializeField] private Material screenEffectMat;
        
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                GameInitializer.Instance.SetEditableArea(true, colorEdition);
                screenEffectMat.DOFloat(1, "_Progression", 1); //ajouté par paloma
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                GameInitializer.Instance.SetEditableArea(false, colorEdition);
                screenEffectMat.DOFloat(0, "_Progression", 1); //ajouté par paloma
            }
        }
        
    }
}