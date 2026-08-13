using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class InteractionHUD : MonoBehaviour {
        [SerializeField] private InteractionPopUp interactionUI;
        [SerializeField] private Ease easeType = Ease.OutBack;
        
        private Tweener interactTween;
        private EventBinding<InteractEvent> interactEventBinding;
    
        private bool isShown = false;
        
        private void Start() {
            interactionUI.GetGroup.alpha = 0;
        }

        private void OnEnable() {
            interactEventBinding = new EventBinding<InteractEvent>(ShowInteraction);
            EventBus<InteractEvent>.Register(interactEventBinding); 
        }

        private void OnDisable() {
            EventBus<InteractEvent>.Deregister(interactEventBinding);
            interactTween?.Kill();
        }

        private void ShowInteraction(InteractEvent e) {
            if (isShown != e.ShowInteraction) {
                interactTween.Kill();
                interactTween = interactionUI.GetGroup.DOFade(e.ShowInteraction ? 1f : 0f, 0.25f).SetEase(easeType);
            }
            
            interactionUI.GetInteractionText.text = e.ShowInteraction ? e.ObjectName : "";
         
            if(e.Position != Vector3.zero) {
                interactionUI.transform.position = e.Position;
            }
            
            isShown = e.ShowInteraction;
        }

        public void ShowInteractionMemory(bool doShow) {
            if(!doShow && isShown) return;
            
            interactTween.Kill();
            
            interactTween = interactionUI.GetGroup.DOFade(doShow ? 1f : 0f, 0.25f).SetEase(easeType);
            interactionUI.GetInteractionText.text = doShow ? "Leave  memory" : "";
         
            interactionUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,175,0);
        }
        
        public void ShowInteractionInspect(bool doShow) {
            if(!doShow && isShown) return;
            
            interactTween.Kill();
            
            interactTween = interactionUI.GetGroup.DOFade(doShow ? 1f : 0f, 0.25f).SetEase(easeType);
            interactionUI.GetInteractionText.text = doShow ? "Leave  inspect" : "";
         
            interactionUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,40,0);
        }
        
        public void ForceInteractHUDVisibility(bool showPopUp) {
            interactionUI.gameObject.SetActive(showPopUp);
        }
    }
}
