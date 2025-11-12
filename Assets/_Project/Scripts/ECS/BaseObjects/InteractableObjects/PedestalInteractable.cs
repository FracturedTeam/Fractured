using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class PedestalInteractable : KeyInteractable {
        private MemoryInteractable memoryInteractable;

        private bool init = false;
        public override void Initialize() {
            if (!init) {
                base.Initialize();
                if(TryGetComponent(out MemoryInteractable memory))
                    memoryInteractable = memory;
                else
                    Debug.LogWarning("[PedestalInteractable] MemoryInteractable not found");
            }
            
            init = true;
            memoryInteractable.SetUnlocked(completed);
        }
        
        protected override void ResolvePuzzle() {
            base.ResolvePuzzle();
            
            GetBaseObject().SetInteract(true);
            memoryInteractable.SetUnlocked(true);
        }
        
        public override void ResetObject() {
            base.ResetObject();
        }
    }
}