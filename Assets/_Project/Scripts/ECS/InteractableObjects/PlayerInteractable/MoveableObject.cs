using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.InteractableObjects.PlayerInteractable {
    public class MoveableObject : PlayerInteractable
    {
        private readonly bool isGrabbed;
        
        internal override void OnInteract(ObjectInteraction interaction)
        {
            base.OnInteract(interaction);
            
            switch (interaction)
            {
                //Grab case
                case ObjectInteraction.Grab:
                    if (InteractableObject.CanBeInteractedWith)
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

        private void GrabObject()
        {
            if(InteractableObject != null) 
                return;
            
            InteractableObject.CanBeInteractedWith = false;
            InteractableObject.SetCollider(false);
            transform.SetParent(PlayerController.Instance.transform);
            transform.localPosition = Vector3.zero + new Vector3(0, 2, 0);
            transform.localRotation = Quaternion.identity;

            Debug.Log("[PlayerInteract] Grab object");
        }

        private void DropObject()
        {
            if(InteractableObject != null) 
                return;
            
            InteractableObject.CanBeInteractedWith = true;
            InteractableObject.SetCollider(true);
            transform.localPosition = Vector3.zero - new Vector3(0, 0.5f, 0) +
                                      PlayerController.Instance.movement.previousMoveDir * 1.5f;
            transform.localRotation = Quaternion.identity;
            transform.SetParent(OriginalParent);

            Debug.Log("[PlayerInteract] Drop object");
        }
    }
}