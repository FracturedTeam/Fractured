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
        
        private void OnEnable() {
            fadeEventBinding = new EventBinding<FadeObject>(Fade);
            EventBus<FadeObject>.Register(fadeEventBinding);
        }

        private void OnDisable() {
            EventBus<FadeObject>.Deregister(fadeEventBinding);
        }

        void Fade(FadeObject f) {
            if (f.show) {
                fadeCanvasGroup.DOFade(1f, 0.5f);
                fadeCanvasGroup.blocksRaycasts = true;
            }
            else {
                fadeCanvasGroup.DOFade(0f, 0.5f);
                fadeCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}