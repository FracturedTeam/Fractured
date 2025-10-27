using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.InteractableObjects
{
    public class InteractableObject : MonoBehaviour
    {
        public bool GetGlass =>  GetGlassInteract != null;
        public GlassInteractable.GlassInteractable GetGlassInteract { get; private set; }
        private PlayerInteractable.PlayerInteractable GetPlayerInteract  { get; set; }
        
        private MeshRenderer meshRenderer;
        private Collider objectCollider;

        private bool initialized = false;
        internal bool CanBeInteractedWith { get; set;}

        internal virtual void Start()
        {
            if(TryGetComponent(typeof(GlassInteractable.GlassInteractable), out var g))
                GetGlassInteract = g as GlassInteractable.GlassInteractable;

            if(TryGetComponent(typeof(PlayerInteractable.PlayerInteractable), out var p))
                GetPlayerInteract = p as PlayerInteractable.PlayerInteractable;
        
            if(TryGetComponent(typeof(MeshRenderer), out var m))
                meshRenderer = m as MeshRenderer;
            else
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain MeshRenderer component");
        
            if(TryGetComponent(typeof(Collider), out var c))
                objectCollider = c as Collider;
            else
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain Collider component");
        
            initialized = true;
        }

        public void Initialize() {
            if(initialized) return;
        
            if(TryGetComponent(typeof(GlassInteractable.GlassInteractable), out var g))
                GetGlassInteract = g as GlassInteractable.GlassInteractable;
            else 
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain GlassInteractable component");
        
            if(TryGetComponent(typeof(MeshRenderer), out var m))
                meshRenderer = m as MeshRenderer;
            else
                Debug.LogError($"[InteractableObject] {nameof(InteractableObject)} does not contain MeshRenderer component");
        
            initialized = true;
        }

        internal void OnInteract(ObjectInteraction interaction)
        {
           GetPlayerInteract.OnInteract(interaction);
        }

        internal void OnShardInteract(bool isOn, ColorEnum glassColor)
        {  
            GetGlassInteract.OnInteract(isOn, glassColor);
        }

        public void SetCollider(bool isOn)
        {
            objectCollider.enabled = isOn;
        }
        
        public void SetRenderer(bool isOn)
        {
            meshRenderer.enabled = isOn;
        }
    }
}
