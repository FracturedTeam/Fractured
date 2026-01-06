using _Project.Scripts.GameServices;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class PedestalInteractable : KeyInteractable {
        protected override void ResolvePuzzle() {
            base.ResolvePuzzle();
            
            GetBaseObject().SetInteract(true);
            AudioManager.Instance.PlayReconstructMemorySound(transform.position);
        }
    }
}