using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Interfaces {
    public interface IInteractable {
        
        public void Initialize();
        
        public void  OnInteract(ObjectInteraction interaction, IInteractable other = null);

        public void Tick(float deltaTime);

        public void Dispose();

        public void CompleteObject();
        
        public void ResetObject();
        
        public BaseObject GetBaseObject();
    }

    public interface IMoveable {
        public void OnGrab(IInteractable other = null);
        public void OnDrop(IInteractable other);
    }
    
}