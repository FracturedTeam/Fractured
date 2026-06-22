using _Project.Scripts.GameServices;
using FMODUnity;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class UnlockedDoor : KeyInteractable {
        [SerializeField] private EventReference unlockedDoorEvent;
        
        protected override void ResolvePuzzle() {
            base.ResolvePuzzle();
            
            GetBaseObject().SetInteract(true);
            GameInitializer.Instance.PlaySound3D(unlockedDoorEvent, transform.position);
        }
    }
}