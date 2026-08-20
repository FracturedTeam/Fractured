using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI {

    public struct FadeObject : IEvent {
        public bool show;
    }

    public struct TransitionTextEvent : IEvent {
        public bool show;
        public string title;
        public string description;
    } 
    
    public class PersistentCanva : MonoBehaviour {

        private EventBinding<FadeObject> fadeEventBinding;
        private EventBinding<TransitionTextEvent> transitionEventBinding;
        
        [Header("Global Transition")]
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        [SerializeField] private float fadeInDuration = 0.3f;
        [SerializeField] private float fadeOutDuration = 0.3f;
        [SerializeField] private Material transitionMaterial;
        
        [Header("Text Transition")]
        [SerializeField] private CanvasGroup transitionCanvasGroup;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI continueText;

        private Tweener tween;
        
        private void OnEnable() {
            fadeEventBinding = new EventBinding<FadeObject>(Fade);
            EventBus<FadeObject>.Register(fadeEventBinding);
            transitionEventBinding = new EventBinding<TransitionTextEvent>(SetText);
            EventBus<TransitionTextEvent>.Register(transitionEventBinding);
            
            InputsBrain.Instance.OnGamepadControlled += GamepadControlled;
        }

        private void OnDisable() {
            EventBus<FadeObject>.Deregister(fadeEventBinding);
            EventBus<TransitionTextEvent>.Deregister(transitionEventBinding);
            
            tween?.Kill();
            transitionMaterial.SetFloat("_Animation", 1.1f);
            InputsBrain.Instance.OnGamepadControlled -= GamepadControlled;
        }

        private void Fade(FadeObject f) {
            tween?.Kill();
            
            tween = transitionMaterial.DOFloat(f.show ? 0 : 1.1f, "_Animation", f.show ? fadeInDuration : fadeOutDuration);
            fadeCanvasGroup.blocksRaycasts = f.show;
        }

        private void SetText(TransitionTextEvent e) {
            titleText.text = e.title;
            descriptionText.text = e.description;
            
            tween?.Kill();
            tween = transitionCanvasGroup.DOFade(e.show ? 1f : 0f, e.show ? 1f : fadeOutDuration);
        }

        private void GamepadControlled(bool isGamepadControlled) {
            continueText.text = isGamepadControlled ? "<sprite index=1> to continue" : "[Space] to continue";
        }
    }
}