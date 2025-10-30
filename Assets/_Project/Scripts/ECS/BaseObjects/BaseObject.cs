using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class BaseObject : MonoBehaviour
    {
        public bool GetGlass =>  GetGlassInteract != null;
        public GlassInteractable GetGlassInteract { get; private set; }
        public IInteractable GetInteract  { get; set; }
        
        private MeshRenderer meshRenderer;
        private Collider objectCollider;

        private bool initialized = false;
        private bool canBeInteractedWith;

        private void Awake() {
            Initialize();
        }

        public void Initialize() {
            if(!initialized) {
                if(TryGetComponent(typeof(GlassInteractable), out var g))
                    GetGlassInteract = g as GlassInteractable;
                if(TryGetComponent(typeof(IInteractable), out var p))
                    GetInteract = p as IInteractable;
            
                if(TryGetComponent(typeof(MeshRenderer), out var m)) meshRenderer = m as MeshRenderer;
                else Debug.LogError($"[BaseObject] {nameof(BaseObject)} does not contain MeshRenderer component");
        
                if(TryGetComponent(typeof(Collider), out var c)) objectCollider = c as Collider;
                else Debug.LogError($"[BaseObject] {nameof(BaseObject)} does not contain Collider component");
        
                gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        
            GetInteract?.Initialize();
            GetGlassInteract?.Initialize();
            
            initialized = true;
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable interactable = null) {
           GetInteract.OnInteract(interaction, interactable);
        }

        public void OnShardInteract(bool isOn, ColorEnum glassColor, Glass shard) {  
            GetGlassInteract.OnInteract(isOn, glassColor, shard);
        }

        public void SetInteract(bool canInteract) {
            canBeInteractedWith = canInteract;
        }
        
        public void SetCollider(bool isOn) {
            if (!objectCollider) return;
            objectCollider.enabled = isOn;
        }
        
        public void SetRenderer(bool isOn) {
            if(!meshRenderer) return;
            meshRenderer.enabled = isOn;
        }

        public bool CanBeInteractedWith() {
            return canBeInteractedWith;
        }
    }
}
