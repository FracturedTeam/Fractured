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

        // public void SetHandTarget(Transform target, Bounds bounds, Transform leftTarget, Transform rightTarget) {
        //     PlaceHandTarget(target, bounds, leftTarget, leftHandLocalPos);
        //     PlaceHandTarget(target, bounds, rightTarget, rightHandLocalPos);
        // }
        //
        // private void PlaceHandTarget(Transform target, Bounds bounds, Transform handTarget, Vector3 localHandPos) {
        //     var frontFaceCenter = bounds.center - target.forward * bounds.extents.z;
        //     var worldHandPos = frontFaceCenter + target.TransformDirection(localHandPos);
        //     
        //     handTarget.position = worldHandPos;
        //
        //     var rayOrigin = worldHandPos + target.forward * 0.2f;
        //     var rayDir = -target.forward;
        //
        //     if (Physics.Raycast(rayOrigin, rayDir, out var hit, 0.5f)) {
        //         handTarget.rotation = Quaternion.LookRotation(-hit.normal, target.up);
        //     }
        //     else {
        //         handTarget.rotation = Quaternion.LookRotation(target.forward, target.up);
        //     }
        // }
        
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