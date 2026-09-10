using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _Project.Scripts.Player {
    public class PlayerIK : MonoBehaviour {

        [Header("Arms")] 
        [SerializeField] private Rig armsRig;
        [SerializeField] private TwoBoneIKConstraint rightArm;
        [SerializeField] private TwoBoneIKConstraint leftArm;
        [SerializeField] private float handOffset = 0.2f;
        [SerializeField] private MultiRotationConstraint rightHand;
        
        private Transform rightEdge;
        private Transform leftEdge;

        private bool isHolding;
        private float lerp;
        
        private void Update() {
            lerp = isHolding ? Mathf.Min(lerp + Time.deltaTime * 4f, 1) : Mathf.Max(lerp - Time.deltaTime * 4f, 0);
            
            armsRig.weight = lerp;
            
            if(!isHolding) return;

            if (rightEdge == null) {
                rightArm.weight = 0;
                rightHand.weight = 0;
            }
            else {
                rightArm.weight = 1;
                rightHand.weight = 1;
            }
            
            if(rightEdge != null) rightArm.data.target.position = rightEdge.position - rightEdge.forward * handOffset;
            if(leftEdge != null) leftArm.data.target.position = leftEdge.position - leftEdge.forward * handOffset;
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