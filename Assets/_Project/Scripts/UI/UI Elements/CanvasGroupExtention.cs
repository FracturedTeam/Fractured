using UnityEngine;

namespace _Project.Scripts.UI {
    public class CanvasGroupExtention : MonoBehaviour {
        
        private CanvasGroup canvasGroup;

        private void Start() {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetActive(bool active) {
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
            canvasGroup.alpha = active ? 1 : 0;
        }
    }
}