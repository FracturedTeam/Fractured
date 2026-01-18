using System;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects {
    [RequireComponent(typeof(BaseObject))]
    public class PressurePlate : MonoBehaviour , IInteractable {
        private BaseObject baseObject;
        
        [Header("Pressure Plate Settings")]
        [SerializeField] private float timeToMoveObject;
        [SerializeField] public Transform objectPosition;
        
        [Header("Moved Object Settings")]
        [SerializeField] private ObjectMoved[] movedObjects;
        
        private float lerpValue;
        private float timer;
        
        private bool isActive;
        private bool initialized = false;

        [HideInInspector] public MoveableObject objectOnPressurePlate;
        [SerializeField] BaseObject[] lockedBehindThis;

        private CountdownTimer deactivateOnInitalization = new CountdownTimer(0.5f);

        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(PressurePlate)}");
                
                baseObject.GetInteractionType = ObjectType.PressurePlate;
                baseObject.GetCompletion = InteractionCompletion.NotCompleted;
            }
            foreach (var obj in movedObjects) {
                obj.objectMoved.position = obj.initialPos.position;
            }
            
            baseObject?.SetInteract(true);
            initialized = true;
            
            deactivateOnInitalization.OnTimerStop += DeactivateLinkedObject;
            deactivateOnInitalization.Start();
        }

        private void DeactivateLinkedObject() {
            foreach (var locked in lockedBehindThis) {
                locked.SetInteract(false);
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(other != null && objectOnPressurePlate != null) return;
            if (other != null && objectOnPressurePlate == null) {
                objectOnPressurePlate = other as MoveableObject;
                objectOnPressurePlate?.GetBaseObject().SetOnPressurePlate(true);
                objectOnPressurePlate?.SetPressurePlateOn(this);
                
                other.OnInteract(ObjectInteraction.Drop, this);
                
                isActive = true;
                baseObject.GetCompletion = InteractionCompletion.Completed;
                foreach (var locked in lockedBehindThis) {
                    locked.SetInteract(true);
                }
            }
            
            if(interaction is not ObjectInteraction.Remove && baseObject.GetCompletion is InteractionCompletion.Completed) return;
            
            switch (interaction) {
                case ObjectInteraction.EnterPressurePlate:
                    if(objectOnPressurePlate != null) return;
                    isActive = true;
                    break;
                case ObjectInteraction.LeavePressurePlate:
                    if(objectOnPressurePlate != null) return;
                    isActive = false;
                    break;
                case ObjectInteraction.Remove:
                    if(objectOnPressurePlate is null) return;
                    
                    objectOnPressurePlate.GetBaseObject().SetInteract(true);
                    PlayerController.Instance.interact.SetGrabbedObject(objectOnPressurePlate.GetBaseObject());
                    objectOnPressurePlate.GetBaseObject().SetOnPressurePlate(false);
                    objectOnPressurePlate?.SetPressurePlateOn(null);
                    objectOnPressurePlate = null;
                    
                    isActive = false;
                    baseObject.GetCompletion = InteractionCompletion.NotCompleted;
                    break;
                default:
                    Debug.LogWarning($"[PressurePlate] Unhandled interaction {interaction} on {nameof(PressurePlate)}");
                    break;
            }
            
            if (isActive) {
                var dia = baseObject.GetCompletion is InteractionCompletion.Completed ? baseObject.successDialogue : baseObject.cantInteractDialogue;
                if (!dia.oneTime || !dia.alreadyInteracted) {
                    HudManager.Instance.SetText(dia.dialogue);
                    
                    if (baseObject.GetCompletion is InteractionCompletion.Completed) baseObject.successDialogue.alreadyInteracted = true;
                    else baseObject.cantInteractDialogue.alreadyInteracted = true;
                }

                foreach (var locked in lockedBehindThis) {
                    locked.SetInteract(true);
                }
                
                AudioManager.Instance.PlayPlateActiveSound(transform.position);
            }
            else {
                if (!baseObject.failedDialogue.oneTime || !baseObject.failedDialogue.alreadyInteracted) {
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.failedDialogue.alreadyInteracted = true;
                }
                
                foreach (var locked in lockedBehindThis) {
                    locked.SetInteract(false);
                }
                
                AudioManager.Instance.PlayPlateInactiveSound(transform.position);
            }
        }

        public void Tick(float deltaTime) {
            /*if (!baseObject.CanBeInteractedWith()) {
                return;
            }*/
            
            timer += isActive ? deltaTime : -deltaTime;
            timer = Mathf.Clamp(timer, 0, timeToMoveObject);

            foreach (var obj in movedObjects) {
                obj.objectMoved.position = Vector3.Lerp(obj.initialPos.position, obj.movedPos.position, lerpValue);
            }
            
            lerpValue = timer / timeToMoveObject;
        }

        public void Dispose() {
            
        }

        public void CompleteObject() {
            isActive = true;
            objectOnPressurePlate?.OnInteract(ObjectInteraction.Drop, this);
            
            foreach (var locked in lockedBehindThis) {
                locked.SetInteract(true);
            }
        }

        public void ResetObject() {
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }

        public void SetActivation(bool activate) {
            isActive = activate;
        }
    }

    [Serializable]
    public struct ObjectMoved {
        public Transform objectMoved;
        public Transform initialPos;
        public Transform movedPos;
    }
}