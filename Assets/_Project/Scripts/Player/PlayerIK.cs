using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _Project.Scripts.Player {
    public class PlayerIK : MonoBehaviour {

        [Header("Arms")] 
        [SerializeField] private Rig armsRig;
        [SerializeField] private TwoBoneIKConstraint rightArm;
        [SerializeField] private TwoBoneIKConstraint leftArm;

        private Transform rightEdge;
        private Transform leftEdge;

        private bool isHolding;
        private float lerp;
        
        private void Update() {
            lerp = isHolding ? Mathf.Min(lerp + Time.deltaTime, 1) : Mathf.Max(lerp - Time.deltaTime, 0);
            
            armsRig.weight = lerp;
            
            if(!isHolding) return;
            
            rightArm.data.target.position = rightEdge.position;
            leftArm.data.target.position = leftEdge.position;
        }

        public void SetHoldingState(bool holding, Transform rEdge = null, Transform lEdge = null) {
            isHolding = holding;
            rightEdge = rEdge;
            leftEdge = lEdge;
        }
    }
}