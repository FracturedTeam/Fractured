using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.InteractableObjects.PlayerInteractable {
    public class PlayerInteractable : MonoBehaviour
    {
        internal Transform OriginalParent;
        internal InteractableObject InteractableObject;

        internal virtual void Start()
        {
            OriginalParent = transform.parent;
            
            if(TryGetComponent(typeof(InteractableObject), out var component))
                InteractableObject = component as InteractableObject;
        }

        internal virtual void OnInteract(ObjectInteraction interaction)
        {
            if(!InteractableObject) 
                return;
        }

        internal virtual void ResetObject()
        {
            //Todo implémenter le reset correctement
            InteractableObject.CanBeInteractedWith = true;
            
            InteractableObject.SetCollider(true);

            Debug.Log("[PlayerInteract] Reset object");
        }
        
    }
}