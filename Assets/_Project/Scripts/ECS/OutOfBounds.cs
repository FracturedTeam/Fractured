using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class OutOfBounds : MonoBehaviour {
        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out BaseObject baseObject)) {
                var m = baseObject.GetInteract as MoveableObject;
                if (m) {
                    m.ResetObject();
                }
            }
        }
    }
}