using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class UnlockedDoor : KeyInteractable {
        private DoorInteractable doorInteractable;

        private bool init = false;
        public override void Initialize() {
            if (!init) {
                base.Initialize();
                if(TryGetComponent(out DoorInteractable door))
                    doorInteractable = door;
                else
                    Debug.LogWarning("[UnlockedDoor] DoorInteractable not found");
            }
            
            init = true;
            doorInteractable.SetUnlocked(completed);
        }
        
        protected override void ResolvePuzzle() {
            base.ResolvePuzzle();
            
            GetBaseObject().SetInteract(true);
            doorInteractable.SetUnlocked(true);
        }
        
        public override void ResetObject() {
            base.ResetObject();
        }
    }
}