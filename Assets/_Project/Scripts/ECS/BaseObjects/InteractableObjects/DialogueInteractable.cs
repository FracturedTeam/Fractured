using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects{

[RequireComponent(typeof(BaseObject))]
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        private bool initialized = false;
        private BaseObject baseObject;

        public void Initialize()
        {
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component)) baseObject = component as BaseObject;
                else Debug.LogError($"[DialogueInteractable] Cannot find {nameof(BaseObject)} in {nameof(DialogueInteractable)}");

                baseObject.GetInteractionType = ObjectType.Dialogue;
                baseObject.GetCompletion = InteractionCompletion.NotCompleted;
                baseObject?.SetInteract(true);
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null)
        {
            HudManager.Instance.SetText(baseObject.successDialogue.dialogue);
        }

        public void Tick(float deltaTime) {
        }

        public void Dispose() {
            
        }

        public void CompleteObject() {
           
        }

        public void ResetObject() {
            
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}
