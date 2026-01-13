using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using System.Collections;
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
            StartCoroutine(Fade(f.show));
        }

        IEnumerator Fade(bool show)
        {
            Debug.Log("Fade Canva " + show);
            if (show) {
                while(fadeCanvasGroup.alpha != 1){
                    fadeCanvasGroup.alpha += Time.deltaTime * 2.0f;
                    yield return null;
                }
                fadeCanvasGroup.blocksRaycasts = true;
                fadeCanvasGroup.interactable = false;
            }
            else {
                while (fadeCanvasGroup.alpha != 1)
                {
                    fadeCanvasGroup.alpha += Time.deltaTime * 2.0f;
                    yield return null;
                }
                fadeCanvasGroup.DOFade(0f, 0.5f);
                fadeCanvasGroup.blocksRaycasts = false;
                fadeCanvasGroup.interactable = false;
            }
        }
        
    }
}