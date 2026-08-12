using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class InteractionHUD : MonoBehaviour
    {
        [SerializeField] private InteractionPopUp interactionUI;
        private Tweener interactTween;
        private EventBinding<InteractEvent> interactEventBinding;
    
        [Header("Interaction Texts")] 
        [SerializeField] private string grab = "Pick up";
        [SerializeField] private string obtainShard = "Break the frame";
        [SerializeField] private string leaveMemory = "Leave";
        [SerializeField] private string useDoor = "Enter";
        [SerializeField] private string useKey = "Unlock the door";
        [SerializeField] private string useFragment = "Put";
        [SerializeField] private string needFragment = "interact";
        [SerializeField] private string needKey = "Door locked";
        [SerializeField] private string needSomethingElse = "interact";
        [SerializeField] private string dialogueInteraction = "";
    
        private void Start()
        {
            interactionUI.GetGroup.alpha = 0;
        }

        private void OnEnable()
        {
            interactEventBinding = new EventBinding<InteractEvent>(ShowInteraction);
            EventBus<InteractEvent>.Register(interactEventBinding); 
        }

        private void OnDisable()
        {
            EventBus<InteractEvent>.Deregister(interactEventBinding);
            interactTween?.Kill();
        }

        private void ShowInteraction(InteractEvent e) {
        
            interactTween.Kill();
                
            if (!e.ShowInteraction || e.Interaction == Interaction.None) {
                interactTween = interactionUI.GetGroup.DOFade(0f, 1f);
                return;
            }
         
            if(e.Position != Vector3.zero)
            {
                interactionUI.transform.position = e.Position;
            }
                
            interactionUI.GetInteractionText.text = e.Interaction switch {
                Interaction.Grab => $"{grab} {e.ObjectName}",
                Interaction.ObtainShard => $"{obtainShard}",
                Interaction.LeaveMemory => $"{leaveMemory}",
                Interaction.UseDoor  => $"{useDoor} {e.ObjectName}",
                Interaction.UseKey =>  $"{useKey}",
                Interaction.UseFragment => $"{useFragment} {e.ObjectName}",
                Interaction.NeedFragment => $"{needFragment}",
                Interaction.NeedKey  => $"{needKey}",
                Interaction.NeedSomethingElse => $"{needSomethingElse}",
                Interaction.Dialogue => $"{dialogueInteraction}",
                _ => "Not supported"
            };
                
            interactTween = interactionUI.GetGroup.DOFade( 1f, 1f);
        }
        public void ForceInteractHUDVisibility(bool showPopUp)
        {
            interactionUI.gameObject.SetActive(showPopUp);
        }
    }
}
