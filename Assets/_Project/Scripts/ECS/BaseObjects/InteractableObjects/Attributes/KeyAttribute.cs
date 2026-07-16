using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(CollectableAttribute))]
    public class KeyAttribute : MonoBehaviour {
        [Header("KeyAttribute")] 
        public int ID = 0;
        public bool oneTimeUse = false;
        public Sprite keySprite;
    }
}