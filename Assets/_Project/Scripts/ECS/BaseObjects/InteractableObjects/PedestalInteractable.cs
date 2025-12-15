namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class PedestalInteractable : KeyInteractable {
        protected override void ResolvePuzzle() {
            base.ResolvePuzzle();
            
            GetBaseObject().SetInteract(true);
        }
    }
}