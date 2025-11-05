using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class TestUI : MonoBehaviour {
        
        [SerializeField] CanvasGroup interactionUI;
        [SerializeField] TextMeshProUGUI interactionText;
        
        private EventBinding<InteractEvent> interactEventBinding;
        Tweener tweener;
        
        void OnEnable() {
            interactEventBinding = new EventBinding<InteractEvent>(ShowInteraction);
            EventBus<InteractEvent>.Register(interactEventBinding);
        }

        void OnDisable() {
            EventBus<InteractEvent>.Deregister(interactEventBinding);
        }

        void ShowInteraction(InteractEvent e) {
            tweener.Kill();

            if (e.needKey)
                interactionText.text = "Find a key";
            else
                interactionText.text = "E to Interact";
            
            tweener = interactionUI.DOFade(e.showInteraction ? 1f : 0f, 0.25f);
        }
    }
}