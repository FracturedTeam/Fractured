using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Audio {
    public class StepTrigger : MonoBehaviour {

        public void StepSound() {
            AudioManager.Instance.PlayFootStepSound(transform.position);
        }
        
    }
}