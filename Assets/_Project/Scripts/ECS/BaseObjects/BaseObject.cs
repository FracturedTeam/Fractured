using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class BaseObject : MonoBehaviour
    {
        public bool GetGlass =>  GetGlassInteract != null;
        public GlassInteractable GetGlassInteract { get; private set; }
        private InteractableObject GetInteract  { get; set; }
        
        private MeshRenderer meshRenderer;
        private Collider objectCollider;

        private bool initialized = false;
        internal bool CanBeInteractedWith { get; set;}

        internal virtual void Start()
        {
            if(TryGetComponent(typeof(GlassInteractable), out var g))
                GetGlassInteract = g as GlassInteractable;

            if(TryGetComponent(typeof(InteractableObject), out var p))
                GetInteract = p as InteractableObject;
        
            if(TryGetComponent(typeof(MeshRenderer), out var m))
                meshRenderer = m as MeshRenderer;
            else
                Debug.LogError($"[BaseObject] {nameof(BaseObject)} does not contain MeshRenderer component");
        
            if(TryGetComponent(typeof(Collider), out var c))
                objectCollider = c as Collider;
            else
                Debug.LogError($"[BaseObject] {nameof(BaseObject)} does not contain Collider component");
        
            initialized = true;
        }

        public void Initialize() {
            if(initialized) return;
        
            if(TryGetComponent(typeof(GlassInteractable), out var g))
                GetGlassInteract = g as GlassInteractable;
            else 
                Debug.LogError($"[BaseObject] {nameof(BaseObject)} does not contain GlassInteractable component");
        
            if(TryGetComponent(typeof(MeshRenderer), out var m))
                meshRenderer = m as MeshRenderer;
            else
                Debug.LogError($"[BaseObject] {nameof(BaseObject)} does not contain MeshRenderer component");
        
            initialized = true;
        }

        internal void OnInteract(ObjectInteraction interaction)
        {
           GetInteract.OnInteract(interaction);
        }

        internal void OnShardInteract(bool isOn, ColorEnum glassColor)
        {  
            GetGlassInteract.OnInteract(isOn, glassColor);
        }

        public void SetCollider(bool isOn)
        {
            if(objectCollider)
                objectCollider.enabled = isOn;
        }
        
        public void SetRenderer(bool isOn)
        {
            if(meshRenderer) 
                meshRenderer.enabled = isOn;
        }
    }
}
