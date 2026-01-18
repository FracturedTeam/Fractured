using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using System.Collections;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.UI {

    public struct FadeObject : IEvent {
        public bool show;
    }
    
    public class PersistentCanva : MonoBehaviour {

        private EventBinding<FadeObject> fadeEventBinding;
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        [SerializeField] private float fadeInDuration = 0.5f;
        [SerializeField] private float fadeOutDuration = 0.5f;
        
        private void OnEnable() {
            fadeEventBinding = new EventBinding<FadeObject>(Fade);
            EventBus<FadeObject>.Register(fadeEventBinding);
        }

        private void OnDisable() {
            EventBus<FadeObject>.Deregister(fadeEventBinding);
        }

        void Fade(FadeObject f) {
            if (f.show) {
                fadeCanvasGroup.DOFade(1f, fadeInDuration);
                fadeCanvasGroup.blocksRaycasts = true;
            }
            else {
                fadeCanvasGroup.DOFade(0f, fadeOutDuration);
                fadeCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}