using UnityEngine;

namespace _Project.Scripts.ECS {
    public class MemoryFrame : MonoBehaviour{
        public int requiredPosition;
        
        private int currentPos;
        private bool isUnlocked;
        private bool canBeInteracted;
        
        public int GetCurrentPosition() => currentPos;
        public void SetCurrentPosition(int newPos) => currentPos = newPos;
        
        public void Unlock() => isUnlocked = true;

        public void CanBeInteracted(bool can) {
            if(isUnlocked) canBeInteracted = can;
        }
        
    }
}