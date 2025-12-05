namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class UnlockedDoor : KeyInteractable {
        protected override void ResolvePuzzle() {
            base.ResolvePuzzle();
            
            GetBaseObject().SetInteract(true);
        }
        
        public override void ResetObject() {
            base.ResetObject();
        }
    }
}