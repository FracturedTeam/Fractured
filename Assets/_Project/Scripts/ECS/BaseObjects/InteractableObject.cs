using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects {
    [RequireComponent(typeof(BaseObject))]
    public class InteractableObject : MonoBehaviour
    {
        internal Transform OriginalParent;
        internal BaseObject BaseObject;

        protected bool canBeGrab = false;
        protected bool isGrabbed = false;

        internal virtual void Start()
        {
            OriginalParent = transform.parent;
            
            if(TryGetComponent(typeof(BaseObject), out var component))
                BaseObject = component as BaseObject;
            
            BaseObject?.SetInteract(true);
        }

        internal virtual void OnInteract(ObjectInteraction interaction)
        {
            if(!BaseObject) 
                return;
        }

        public virtual void Use() {
            
        }
        
        internal virtual void ResetObject()
        {
            //Todo implémenter le reset correctement
            BaseObject.CanBeInteractedWith = true;
            BaseObject.SetCollider(true);

            Debug.Log("[PlayerInteract] Reset object");
        }

        public virtual void ConsumeObject() {
        }

        public bool GetCanGrab() {
            return canBeGrab;
        }
        
    }
}