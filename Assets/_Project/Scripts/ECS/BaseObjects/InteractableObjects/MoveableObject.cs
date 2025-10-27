using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class MoveableObject : InteractableObject
    {
        internal override void Start() {
            base.Start();
            
            canBeGrab = true;
        }

        internal override void OnInteract(ObjectInteraction interaction)
        {
            base.OnInteract(interaction);
            
            switch (interaction)
            {
                //Grab case
                case ObjectInteraction.Grab:
                    if (BaseObject.CanBeInteractedWith)
                        GrabObject();
                    else
                        Debug.LogWarning("[MoveableObject] Can't grab object!");
                    break;
                //Drop case
                case ObjectInteraction.Drop:
                    if (isGrabbed)
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

        public override void ConsumeObject() {
            DropObject();
            
        }

        private void GrabObject()
        {
            if(BaseObject == null) return;
            
            BaseObject.CanBeInteractedWith = false;
            BaseObject.SetCollider(false);
            isGrabbed = true;
            transform.SetParent(PlayerController.Instance.transform);
            transform.localPosition = Vector3.zero + new Vector3(0, 2, 0);
            transform.localRotation = Quaternion.identity;

            Debug.Log("[PlayerInteract] Grab object");
        }

        private void DropObject()
        {
            if(BaseObject == null) return;
            
            BaseObject.CanBeInteractedWith = true;
            isGrabbed = false;
            BaseObject.SetCollider(true);
            transform.localPosition = Vector3.zero - new Vector3(0, 0.5f, 0) +
                                      PlayerController.Instance.movement.previousMoveDir * 1.5f;
            transform.localRotation = Quaternion.identity;
            transform.SetParent(OriginalParent);

            Debug.Log("[PlayerInteract] Drop object");
        }
    }
}