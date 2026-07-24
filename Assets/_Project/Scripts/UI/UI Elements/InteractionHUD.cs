using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.UI;
using DG.Tweening;
using UnityEngine;

public class InteractionHUD : MonoBehaviour
{
    [SerializeField] private InteractionPopUp interactionUI;
    private Tweener interactTween;
    private EventBinding<InteractEvent> interactEventBinding;
    
    [Header("Interaction Texts")] 
    [SerializeField] private string grab = "Pick up";
    [SerializeField] private string obtainShard = "Break the frame";
    [SerializeField] private string enterMemory = "View Memory";
    [SerializeField] private string leaveMemory = "Leave";
    [SerializeField] private string useDoor = "Enter";
    [SerializeField] private string useKey = "Unlock the door";
    [SerializeField] private string useFragment = "Put";
    [SerializeField] private string needFragment = "[E] to interact";
    [SerializeField] private string needKey = "Door locked";
    [SerializeField] private string needSomethingElse = "[E] to interact";
    [SerializeField] private string dialogueInteraction = "";
    [SerializeField] private string enterPressurePlate = "[E] to interact";
    [SerializeField] private string leavePressurePlate = "[E] to leave";
    [SerializeField] private string putObjectPressurePlate = "[E] to put object on";
    [SerializeField] private string pickObjectPressurePlate = "Hold [E] to pick up";
    private bool forceHide;
        
    [Header("Interaction Image")] 
    [SerializeField] private Sprite spriteNormal;
    [SerializeField] private Sprite spriteGlass;
    [SerializeField] private Sprite spriteUseDoor;
    [SerializeField] private Sprite spriteKey;
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;


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
                if(forceHide)
                    return;
                
                if (!e.ShowInteraction || e.Interaction == Interaction.None) {
                    interactTween = interactionUI.GetGroup.DOFade(0f, 0.25f);
                    return;
                }
                
                interactionUI.GetInteractionText.text = e.Interaction switch {
                    Interaction.Grab => $"{grab} {e.ObjectName}",
                    Interaction.ObtainShard => $"{obtainShard}",
                    Interaction.EnterMemory => $"{enterMemory}",
                    Interaction.LeaveMemory => $"{leaveMemory}",
                    Interaction.UseDoor  => $"{useDoor} {e.ObjectName}",
                    Interaction.UseKey =>  $"{useKey}",
                    Interaction.UseFragment => $"{useFragment} {e.ObjectName}",
                    Interaction.NeedFragment => $"{needFragment}",
                    Interaction.NeedKey  => $"{needKey}",
                    Interaction.NeedSomethingElse => $"{needSomethingElse}",
                    Interaction.Dialogue => $"{dialogueInteraction}",
                    Interaction.EnterPressurePlate => $"{enterPressurePlate}",
                    Interaction.LeavePressurePlate => $"{leavePressurePlate}",
                    Interaction.PutObjectOnPressurePlate => $"{putObjectPressurePlate}",
                    Interaction.PickObjectOnPressurePlate => $"{pickObjectPressurePlate}",
                    _ => "Not supported"
                };
                
                interactionUI.GetInteractionImage.sprite = e.Interaction switch
                {
                    Interaction.Grab => spriteNormal,
                    Interaction.PickObjectOnPressurePlate => spriteUp,
                    Interaction.ObtainShard => spriteGlass,
                    Interaction.UseDoor => spriteUseDoor,
                    Interaction.UseKey => spriteKey,
                    Interaction.UseFragment => spriteDown,
                    Interaction.NeedFragment => spriteGlass,
                    Interaction.NeedKey => spriteKey,
                    _ => spriteNormal
                };
                
                interactTween = interactionUI.GetGroup.DOFade(e.ShowInteraction ? 1f : 0f, 0.25f);
            }
    public void ForceInteractHUDVisibility(bool isOn)
    {
        interactionUI.GetGroup.DOFade(isOn ? 1 : 0, 0);
        forceHide = !isOn;
    }

    public void InteractionSetPosition(Vector3 position)
    {
        //Instance.interactionParent.transform.position = position;
    }

}
