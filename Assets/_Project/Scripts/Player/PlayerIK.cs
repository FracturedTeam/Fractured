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
            lerp = isHolding ? Mathf.Min(lerp + Time.deltaTime * 4f, 1) : Mathf.Max(lerp - Time.deltaTime * 4f, 0);
            
            armsRig.weight = lerp;
            
            if(!isHolding) return;

            rightArm.weight = rightEdge == null ? 0 : 1;
            if(rightEdge != null) rightArm.data.target.position = rightEdge.position;
            if(leftEdge != null) leftArm.data.target.position = leftEdge.position;
        }

        public void SetHoldingState(Transform rEdge, Transform lEdge) {
            rightEdge = rEdge;
            leftEdge = lEdge;
        }

        public void SetLightObject(Transform lEdge) {
            leftEdge = lEdge;
        }

        public void SetHolding(bool holding) {
            isHolding = holding;
            
            if(holding) return;
            rightEdge = null;
            leftEdge = null;
        }
    }
}