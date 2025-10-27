using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class TestUI : MonoBehaviour {
        
        [SerializeField] CanvasGroup interactionUI;
        
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
            
            if (e.showInteraction)
                tweener = interactionUI.DOFade(1f, 0.25f);
            else 
                tweener = interactionUI.DOFade(0f, 0.25f);
        }
    }
}