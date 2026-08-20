using _Project.Scripts.GameServices;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Audio {
    public class StepTrigger : MonoBehaviour {
        public void StepSound() {
            if(GameInitializer.HasInstance)
                GameInitializer.Instance?.PlayPlayerFootstep(transform.position);
        }
    }
}