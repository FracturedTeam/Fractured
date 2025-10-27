using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects {
    public class InteractableObject : MonoBehaviour
    {
        internal Transform OriginalParent;
        internal BaseObject BaseObject;

        internal virtual void Start()
        {
            OriginalParent = transform.parent;
            
            if(TryGetComponent(typeof(BaseObject), out var component))
                BaseObject = component as BaseObject;
        }

        internal virtual void OnInteract(ObjectInteraction interaction)
        {
            if(!BaseObject) 
                return;
        }

        internal virtual void ResetObject()
        {
            //Todo implémenter le reset correctement
            BaseObject.CanBeInteractedWith = true;
            
            BaseObject.SetCollider(true);

            Debug.Log("[PlayerInteract] Reset object");
        }
        
    }
}