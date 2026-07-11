using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI {

    public struct FadeObject : IEvent {
        public bool show;
    }
    
    public class PersistentCanva : MonoBehaviour {

        private EventBinding<FadeObject> fadeEventBinding;
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        [SerializeField] private float fadeInDuration = 0.3f;
        [SerializeField] private float fadeOutDuration = 0.3f;

        private Tweener tween;
        
        private void OnEnable() {
            fadeEventBinding = new EventBinding<FadeObject>(Fade);
            EventBus<FadeObject>.Register(fadeEventBinding);
        }

        private void OnDisable() {
            EventBus<FadeObject>.Deregister(fadeEventBinding);
            
            tween?.Kill();
        }

        void Fade(FadeObject f) {
            tween?.Kill();
            if (f.show) {
                tween = fadeCanvasGroup.DOFade(1f, fadeInDuration);
                fadeCanvasGroup.blocksRaycasts = true;
            }
            else {
                tween = fadeCanvasGroup.DOFade(0f, fadeOutDuration);
                fadeCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}