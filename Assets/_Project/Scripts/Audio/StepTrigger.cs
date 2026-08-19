using _Project.Scripts.GameServices;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Audio {
    public class StepTrigger : MonoBehaviour {
        public void StepSound() {
            if(GameInitializer.HasInstance)
                GameInitializer.Instance?.PlaySound3D(GameInitializer.Instance.GetBank().avatar_Walking_Neutral, transform.position);
        }
    }
}