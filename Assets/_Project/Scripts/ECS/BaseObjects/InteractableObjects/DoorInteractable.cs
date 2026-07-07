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
                
                baseObject.GetObjectType = ObjectType.Door;
            }
            
            if(doorType is DoorType.BigDoor)
                doorAnimator.SetBool("CanBeInteract", false);
            
            initialized = true;
            baseObject?.SetInteract(true);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (doorType is DoorType.None) {
                if (baseObject.failedDialogue is not { oneTime: true, alreadyInteracted: true }) {
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.cantInteractDialogue.alreadyInteracted = true;
                }
                return;
            }
            
            if(hasBeenInteracted) return;
            
            if (key) {
                if (!key.GetBaseObject()) {
                    key.Initialize();
                }
                if(key.GetBaseObject().GetCompletion is not InteractionCompletion.Completed) {
                    if(doorType is DoorType.BigDoor) 
                        GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().lockedBigDoorSound, transform.position);
                    else 
                        GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().lockedSmallDoorSound, transform.position);
                    
                    PlayerController.Instance.interact.StartUsingLockedDoor();
                    
                    if (other != null || baseObject.cantInteractDialogue is { alreadyInteracted: true, oneTime: true }) 
                        return;
                        
                    HudManager.Instance.SetText(baseObject.cantInteractDialogue.dialogue);
                    baseObject.cantInteractDialogue.alreadyInteracted = true;
                    
                    return;
                }
            }
            
            if (doorType is DoorType.SmallDoor) {
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().lockedSmallDoorSound, transform.position);
                PlayerController.Instance.interact.StartUsingLockedDoor();
                    
                if(baseObject.failedDialogue is not { oneTime: true, alreadyInteracted: true }) {
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.failedDialogue.alreadyInteracted =  true;
                }
                
                return;
            }
            
            if (doorType is DoorType.BigDoor) {
                if (PlayerController.Instance.interact.HasObject) {
                    PlayerController.Instance.interact.triggerFailedDrop = true;
                    if (baseObject.failedDialogue is not { oneTime: true, alreadyInteracted: true }) {
                        HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                        baseObject.cantInteractDialogue.alreadyInteracted = true;
                    }
                    return;
                }
                if (sceneToLoad == null) return;
                hasBeenInteracted = true;
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().openBigDoorSound, transform.position);
                PlayerController.Instance.interact.TriggerBigDoor(sceneToLoad, transform.position);
                doorAnimator.SetBool("CanBeInteract", true);
            }

            // if (!linkedDoor.GetBaseObject().GetCollider().enabled) {
            //     GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().lockedSmallDoorSound, transform.position);
            // }
        }

        public void Tick(float deltaTime) {
            if (doorType is DoorType.SmallDoor) {
                if (!baseObject.CanBeInteractedWith() || !linkedDoor.baseObject.CanBeInteractedWith()) {
                    SetDoor(false);
                    return;
                }
                
                if (baseObject.GetGlass) {
                    switch (baseObject.GetGlassInteract.IsVisible) {
                        case false when baseObject.CanBeInteractedWith():
                            baseObject.SetInteract(false);
                            break;
                        case true when !baseObject.CanBeInteractedWith():
                            baseObject.SetInteract(true);
                            break;
                    }
                }
                
                var mask = LayerMask.GetMask("Player");
                var size = Physics.OverlapBoxNonAlloc(triggerPoint.position, new Vector3(3f,4.5f,1), cols, transform.rotation, mask);

                if (size > 0) {
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
                    if (baseObject.GetGlass && linkedDoor.baseObject.GetGlass) {
                        if(baseObject.GetGlassInteract.IsVisible && linkedDoor.baseObject.GetGlassInteract.IsVisible) 
                            SetDoor(true);
                        else
                            SetDoor(false);
                    }
                    else if (baseObject.GetGlass && !linkedDoor.baseObject.GetGlass) {
                        if(baseObject.GetGlassInteract.IsVisible) 
                            SetDoor(true);
                        else
                            SetDoor(false);
                    }
                    else if (!baseObject.GetGlass && linkedDoor.baseObject.GetGlass) {
                        if(linkedDoor.baseObject.GetGlassInteract.IsVisible) 
                            SetDoor(true);
                        else
                            SetDoor(false);
                    }
                }
            }
        }

        public void Dispose() {
            
        }

        private void SetDoor(bool canBeUsed) {
            doorAnimator.SetBool("CanBeInteract", canBeUsed);
            baseObject.SetCollider(!canBeUsed);
        }
        
        public void CompleteObject() {
        }

        public void ResetObject() {
            if(key) key.ResetObject();
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