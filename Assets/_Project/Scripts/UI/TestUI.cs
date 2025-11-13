using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class TestUI : MonoBehaviour {
        
        [SerializeField] CanvasGroup interactionUI;
        [SerializeField] TextMeshProUGUI interactionText;
        
        [SerializeField] CanvasGroup memoryHUD;
        [SerializeField] private Image memoryImage;
        
        private EventBinding<InteractEvent> interactEventBinding;
        private EventBinding<MemoryEvent> memoryEventBinding;
        Tweener interactTween;
        Tweener memoryTween;
        
        void OnEnable() {
            interactEventBinding = new EventBinding<InteractEvent>(ShowInteraction);
            EventBus<InteractEvent>.Register(interactEventBinding);
            memoryEventBinding = new EventBinding<MemoryEvent>(ShowMemory);
            EventBus<MemoryEvent>.Register(memoryEventBinding);
        }

        void OnDisable() {
            EventBus<InteractEvent>.Deregister(interactEventBinding);
            EventBus<MemoryEvent>.Deregister(memoryEventBinding);
        }

        void ShowInteraction(InteractEvent e) {
            interactTween.Kill();

            interactionText.text = e.interaction switch {
                Interaction.Grab => "grab",
                Interaction.ObtainShard => "obtain shard",
                Interaction.EnterMemory => "enter memory",
                Interaction.LeaveMemory => "leave memory",
                Interaction.UseDoor  => "use door",
                Interaction.UseKey =>  "use key",
                Interaction.UseFragment => "use fragment",
                Interaction.needFragment => "need fragment",
                Interaction.needKey  => "need key",
                Interaction.needSomethingElse => "need something else",
                _ => "Not supported"
            };
            
            interactTween = interactionUI.DOFade(e.showInteraction ? 1f : 0f, 0.25f);
        }

        void ShowMemory(MemoryEvent e) {
            memoryTween.Kill();
            
            memoryImage.sprite = e.memory;
            
            memoryTween = memoryHUD.DOFade(e.showMemory ? 1f : 0f, 0.25f);
        }
    }
}