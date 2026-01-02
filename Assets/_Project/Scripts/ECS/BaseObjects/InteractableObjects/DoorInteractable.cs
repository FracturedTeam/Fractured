using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class DoorInteractable : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        
        [Header("Door Type")]
        [SerializeField] public DoorType doorType;
        
        [Header("Settings")]
        [SerializeField] public Transform exitPoint;
        [SerializeField] public Transform triggerPoint;
        [SerializeField] public DoorInteractable linkedDoor;
        [SerializeField] public Direction exitDir;

        [Header("Load Scene")]
        [SerializeField] public SceneSettings sceneToLoad;
        [SerializeField] public Animator doorAnimator;
        
        private KeyInteractable key;
        private bool initialized = false;
        
        private bool hasBeenInteracted = false;

        private Collider[] cols = new Collider[4];
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject b)) baseObject = b;
                else Debug.LogError($"[DoorInteractable] Cannot find {nameof(BaseObject)} in {nameof(DoorInteractable)}");

                if(TryGetComponent(out KeyInteractable k)) key = k;
                
                baseObject.GetInteractionType = ObjectType.Door;
            }
            
            initialized = true;
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(hasBeenInteracted) return;
            
            if (key) {
                if(key.GetBaseObject().GetCompletion is not InteractionCompletion.Completed) {
                    if(doorType is DoorType.BigDoor) AudioManager.Instance.PlayLockedBigSound(transform.position);
                    else AudioManager.Instance.PlayLockedSmallSound(transform.position);
                    
                    if (other != null || baseObject.cantInteractDialogue is { alreadyInteracted: true, oneTime: true }) 
                        return;
                        
                    HudManager.Instance.SetText(baseObject.cantInteractDialogue.dialogue);
                    baseObject.cantInteractDialogue.alreadyInteracted = true;
                    
                    return;
                }
            }
            
            if (doorType is DoorType.BigDoor) {
                if (sceneToLoad == null) return;
                hasBeenInteracted = true;
                AudioManager.Instance.PlayOpenBigSound(transform.position);
                GameInitializer.Instance.LoadNewLevel(sceneToLoad);
                return;
            }
            
            if (linkedDoor.key) {
                if(linkedDoor.key.GetBaseObject().GetCompletion is not InteractionCompletion.Completed) {
                    AudioManager.Instance.PlayLockedSmallSound(transform.position);
                   
                    if(baseObject.failedDialogue is { oneTime: true, alreadyInteracted: true }) {
                        HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                        baseObject.failedDialogue.alreadyInteracted =  true;
                    }
                    return;
                }
            }

            if (!linkedDoor.GetBaseObject().GetCollider().enabled) {
                AudioManager.Instance.PlayLockedSmallSound(transform.position);
                return;
            }
        }

        public void Tick(float deltaTime) {
            if (doorType is DoorType.SmallDoor) {
                var mask = LayerMask.GetMask("Player");
                var size = Physics.OverlapBoxNonAlloc(triggerPoint.position, new Vector3(3f,4.5f,1), cols, transform.rotation, mask);

                if (size > 0) {
                    AudioManager.Instance.PlayOpenSmallSound(transform.position);
                    PlayerController.Instance.interact.StartUsingDoor();
                    PlayerController.Instance.movement.SetPosition(linkedDoor.exitPoint.position, exitDir);
                }
                
                if (!baseObject.GetGlass && !linkedDoor.baseObject.GetGlass) { //Pour les portes nécessitant des clés
                    if (baseObject.GetCompletion is InteractionCompletion.NotCompleted ||
                        linkedDoor.baseObject.GetCompletion is InteractionCompletion.NotCompleted)
                        SetDoor(false);
                    else
                        SetDoor(true);
                }
                else { //Pour les portes qui peuvent disparaitre
                    if(baseObject.GetRendered().enabled && linkedDoor.baseObject.GetRendered().enabled) 
                        SetDoor(true);
                    else
                        SetDoor(false);
                }
            }
        }

        private void SetDoor(bool canBeUsed) {
            doorAnimator.SetBool("CanBeInteract", canBeUsed);
            baseObject.SetInteract(!canBeUsed);
            baseObject.SetCollider(!canBeUsed);
        }
        
        public void CompleteObject() {
        }

        public void ResetObject() {
            if(key)
                key.ResetObject();
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }

        void OnDrawGizmos() {
            if (doorType is DoorType.SmallDoor) {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
                Gizmos.DrawWireCube(triggerPoint.localPosition, new Vector3(3f,4.5f,1));
            }
        }
    }
}