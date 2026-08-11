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
        [SerializeField] private Material transitionMaterial;

        private Tweener tween;
        
        private void OnEnable() {
            fadeEventBinding = new EventBinding<FadeObject>(Fade);
            EventBus<FadeObject>.Register(fadeEventBinding);
        }

        private void OnDisable() {
            EventBus<FadeObject>.Deregister(fadeEventBinding);
            
            tween?.Kill();
            transitionMaterial.SetFloat("_Animation", 1.1f);
        }

        void Fade(FadeObject f) {
            tween?.Kill();
            
            fadeCanvasGroup.blocksRaycasts = f.show;
            tween = transitionMaterial.DOFloat(f.show ? 0 : 1.1f, "_Animation", f.show ? fadeInDuration : fadeOutDuration);
        }
    }
}