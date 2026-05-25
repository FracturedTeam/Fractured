using _Project.Scripts.GameServices;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class PedestalInteractable : KeyInteractable {
        protected override void ResolvePuzzle() {
            base.ResolvePuzzle();
            
            GetBaseObject().SetInteract(true);
            GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().reconstructMemorySound, transform.position);
        }
    }
}