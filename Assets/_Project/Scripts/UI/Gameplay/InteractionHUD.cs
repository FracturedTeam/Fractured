using _Project.Scripts.Inputs;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class InteractionHUD : MonoBehaviour {
        [SerializeField] private InteractionPopUp interactionUI;
        [SerializeField] private Ease easeType = Ease.OutBack;

        [SerializeField] private string keyboardInput = "[E]";
        [SerializeField] private string gamepadInput = "<sprite index=1>";
        
        private Tweener interactTween;
        private EventBinding<InteractEvent> interactEventBinding;
    
        private bool isShown = false;
        private bool isGamepadControlled = false;
        private string currentObject;
        
        private RectTransform rectTransform;
        
        private void Start() {
            interactionUI.GetGroup.alpha = 0;
            rectTransform = interactionUI.GetComponent<RectTransform>();
        }

        private void OnEnable() {
            interactEventBinding = new EventBinding<InteractEvent>(ShowInteraction);
            EventBus<InteractEvent>.Register(interactEventBinding); 
            
            InputsBrain.Instance.OnGamepadControlled += UpdateGamepadControlled;
        }

        private void OnDisable() {
            EventBus<InteractEvent>.Deregister(interactEventBinding);
            
            if(InputsBrain.HasInstance) InputsBrain.Instance.OnGamepadControlled -= UpdateGamepadControlled;
            interactTween?.Kill();
        }

        private void UpdateGamepadControlled(bool isGamepad) {
            isGamepadControlled = isGamepad;
            
            var currentInput = "";
            if(isShown)
                currentInput = isGamepadControlled ? gamepadInput : keyboardInput;
            interactionUI.GetInteractionText.text = $"{currentInput} {currentObject}";
        }
        
        private void ShowInteraction(InteractEvent e) {
            if (isShown != e.ShowInteraction) {
                interactTween.Kill();
                interactTween = interactionUI.GetGroup.DOFade(e.ShowInteraction ? 1f : 0f, 0.25f).SetEase(easeType);
            }

            currentObject = e.ShowInteraction ? e.ObjectName : "";
            
            var currentInput = "";
            if(e.ShowInteraction)
                currentInput = isGamepadControlled ? gamepadInput : keyboardInput;
            
            interactionUI.GetInteractionText.text = $"{currentInput} {currentObject}";
         
            if(e.Position != Vector3.zero) {
                interactionUI.transform.position = e.Position;
            }
            
            isShown = e.ShowInteraction;
        }

        public void ShowInteractionMemory(bool doShow) {
            UpdateInteraction(doShow, "Leave memory", 175f);
        }
        
        public void ShowInteractionInspect(bool doShow) {
            UpdateInteraction(doShow, "Leave inspect", 40f);
        }

        private void UpdateInteraction(bool doShow, string text, float yPos) {
            if(!doShow && isShown) return;
            
            interactTween.Kill();
            
            interactTween = interactionUI.GetGroup.DOFade(doShow ? 1f : 0f, 0.25f).SetEase(easeType);
         
            currentObject = doShow ? text : "";
            var currentInput = "";
            if(doShow)
                currentInput = isGamepadControlled ? gamepadInput : keyboardInput;
            interactionUI.GetInteractionText.text = $"{currentInput} {currentObject}";
            
            rectTransform.anchoredPosition = new Vector3(0,yPos,0);
        }
        
        public void ForceInteractHUDVisibility(bool showPopUp) {
            interactionUI.gameObject.SetActive(showPopUp);
        }
    }
}
