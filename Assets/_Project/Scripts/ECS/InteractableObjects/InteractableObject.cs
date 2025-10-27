using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.InteractableObjects
{
    public class InteractableObject : MonoBehaviour
    {
        public bool GetGlass =>  GetGlassInteract != null;
        public GlassInteractable GetGlassInteract { get; private set; }
        private MeshRenderer meshRenderer;
        internal Collider objectCollider;
    
        bool Initialized = false;
        internal bool canBeGrabbed = false;

        internal virtual void Start()
        {
            if(TryGetComponent(typeof(GlassInteractable), out var g))
                GetGlassInteract = g as GlassInteractable;
            else 
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain GlassInteractable component");
        
            if(TryGetComponent(typeof(MeshRenderer), out var m))
                meshRenderer = m as MeshRenderer;
            else
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain MeshRenderer component");
        
            if(TryGetComponent(typeof(Collider), out var c))
                objectCollider = c as Collider;
            else
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain Collider component");
        
            Initialized = true;
        }

        public void Initialize() {
            if(Initialized) return;
        
            if(TryGetComponent(typeof(GlassInteractable), out var g))
                GetGlassInteract = g as GlassInteractable;
            else 
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain GlassInteractable component");
        
            if(TryGetComponent(typeof(MeshRenderer), out var m))
                meshRenderer = m as MeshRenderer;
            else
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain MeshRenderer component");
        
            Initialized = true;
        }

        internal virtual void OnInteract(ObjectInteraction interaction)
        {
            //Insert here interaction with the player
        }

        internal virtual void OnShardInteract(bool isOn, ColorEnum glassColor)
        {
            //Insert here interaction with the glass
            meshRenderer.enabled = isOn && glassColor == GetGlassInteract.color;
        }
    }
}
