using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ECS.InteractableObjects {
    public class MoveableObject : InteractableObject {
        private bool isGrabbed = false;
        private Transform originalParent;

        internal override void Start() {
            base.Start();
            originalParent = transform.parent;
            canBeGrabbed = true;
        }

        internal override void OnInteract(ObjectInteraction interaction) {
            switch (interaction) {
                //Grab case
                case ObjectInteraction.Grab:
                    if(CanBeGrabbed())
                        GrabObject();
                    else
                        Debug.LogWarning("[MoveableObject] Can't grab object!");
                    break;
                //Drop case
                case ObjectInteraction.Drop:
                    if(CanBeDropped())
                        DropObject();
                    else
                        Debug.Log("[PlayerInteract] No object to interact with...");
                    break;
                //Other case
                default:
                    Debug.LogWarning($"[MoveableObject] {interaction} Interaction is not supported");
                    break;
            }
        }

        void GrabObject() {
            SetGrabbed(true);
            objectCollider.enabled = false;
            transform.SetParent(PlayerController.Instance.transform);
            transform.localPosition = Vector3.zero + new Vector3(0,2,0);
            transform.localRotation = Quaternion.identity;
            
            Debug.Log("[PlayerInteract] Grab object");
        }

        void DropObject() {
            SetGrabbed(false);
            objectCollider.enabled = true;
            transform.localPosition = Vector3.zero -  new Vector3(0,0.5f,0) + PlayerController.Instance.movement.previousMoveDir * 1.5f;
            transform.localRotation = Quaternion.identity;
            transform.SetParent(originalParent);
            
            Debug.Log("[PlayerInteract] Drop object");
        }

        void ResetObject() { //Todo implémenter le reset correctement
            SetGrabbed(false);
            objectCollider.enabled = true;
            
            Debug.Log("[PlayerInteract] Reset object");
        }
        
        void SetGrabbed(bool grabbed) {
            isGrabbed = grabbed;
        }
        
        public bool CanBeGrabbed() {
            return !isGrabbed;
        }

        public bool CanBeDropped() {
            return isGrabbed;
        }
    }
}