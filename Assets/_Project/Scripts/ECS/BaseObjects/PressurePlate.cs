using System;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects {
    [RequireComponent(typeof(BaseObject))]
    public class PressurePlate : MonoBehaviour , IInteractable {
        private BaseObject baseObject;
        
        [Header("Pressure Plate Settings")]
        [SerializeField] private float timeToMoveObject;
        
        [Header("Moved Object Settings")]
        [SerializeField] private ObjectMoved[] movedObjects;
        
        private float lerpValue;
        private float timer;
        
        private bool isActive;

        private KeyInteractable key;
        private bool initialized = false;

        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(PressurePlate)}");
                
                if(TryGetComponent(out KeyInteractable k)) key = k;
                
                baseObject.GetInteractionType = ObjectType.PressurePlate;
            }
            
            baseObject?.SetInteract(true);
            initialized = true;
            foreach (var obj in movedObjects) {
                obj.objectMoved.position = obj.initialPos.position;
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(other != null) return;
            
            if(interaction is not ObjectInteraction.Remove && baseObject.GetCompletion is InteractionCompletion.Completed) return;
            
            switch (interaction) {
                case ObjectInteraction.EnterPressurePlate:
                    isActive = true;
                    break;
                case ObjectInteraction.LeavePressurePlate:
                    isActive = false;
                    break;
                case ObjectInteraction.Remove:
                    key?.OnInteract(ObjectInteraction.Remove);
                    break;
                default:
                    Debug.LogWarning($"[PressurePlate] Unhandled interaction {interaction} on {nameof(PressurePlate)}");
                    break;
            }
        }

        public void Tick(float deltaTime) {
            if (!baseObject.CanBeInteractedWith()) {
                timer -= deltaTime;
                return;
            }
            
            if(baseObject.GetCompletion is InteractionCompletion.Completed && !isActive) isActive = true;
            else if(baseObject.GetCompletion is InteractionCompletion.NotCompleted && isActive) isActive = false;
            
            timer += isActive ? deltaTime : -deltaTime;

            if (isActive) {
                var dia = baseObject.GetCompletion is InteractionCompletion.Completed ? baseObject.successDialogue : baseObject.cantInteractDialogue;
                if (!dia.oneTime || !dia.alreadyInteracted) {
                    HudManager.Instance.SetText(dia.dialogue);
                    
                    if (baseObject.GetCompletion is InteractionCompletion.Completed) baseObject.successDialogue.alreadyInteracted = true;
                    else baseObject.cantInteractDialogue.alreadyInteracted = true;
                }
            }
            else {
                if (!baseObject.failedDialogue.oneTime || !baseObject.failedDialogue.alreadyInteracted) {
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.failedDialogue.alreadyInteracted = true;
                }
            }
            
            
            timer = Mathf.Clamp(timer, 0, timeToMoveObject);

            foreach (var obj in movedObjects) {
                obj.objectMoved.position = Vector3.Lerp(obj.initialPos.position, obj.movedPos.position, lerpValue);
            }
            
            lerpValue = timer / timeToMoveObject;
        }

        public void CompleteObject() {
            
        }

        public void ResetObject() {
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }

    [Serializable]
    public struct ObjectMoved {
        public Transform objectMoved;
        public Transform initialPos;
        public Transform movedPos;
    }
}