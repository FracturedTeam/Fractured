using _Project.Scripts.GameServices;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Audio {
    public class StepTrigger : MonoBehaviour {
        public void StepSound() {
            GameInitializer.Instance.PlaySound(GameInitializer.Instance.GetBank().playerFootstepSound, transform.position);
        }
        
    }
}