using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class ColliderEvent : MonoBehaviour
    {
        private Collider coll;
        
        private void Awake()
        {
            coll = GetComponentInChildren<Collider>();
        }

        public void DeactivateCollision()
        {
            coll.enabled = false;
        }
        
        public void ActivateCollision()
        {
            coll.enabled = true;
        }
    }
}