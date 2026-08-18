using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Player {
    public class FootIK : MonoBehaviour {
        private Animator animator;
        private LayerMask groundLayer;

        [Header("IK Settings")]
        public float raycastHeightAboveFoot = 0.5f;   // how high above foot bone to start the ray
        public float raycastMaxDistance = 1.5f;   // max distance downward to look for ground
       
        [Header("Foot Offset")]
        public float footHeightOffsetFlat = 0.1f;  // offset on flat ground
        public float footHeightOffsetSlope = 0.05f; // offset on slopes/stairs
        public float slopeAngleThreshold = 10f;   // above this angle = slope mode
        
        [Header("Smoothing")]
        public float positionSmoothSpeed = 15f;   // higher = snappier, lower = smoother
        public float rotationSmoothSpeed = 10f;
        
        // Per foot smoothed state
        private Vector3 _leftFootPos, _rightFootPos;
        private Quaternion _leftFootRot, _rightFootRot;
        
        // Store hit info per foot for gizmos — can't call GetIKPosition outside OnAnimatorIK.
        private RaycastHit _leftHit, _rightHit;
        private Vector3 _leftOrigin, _rightOrigin;
        private bool _leftHitValid, _rightHitValid;
        
        private void Start() {
            animator = GetComponent<Animator>();
            groundLayer = LayerMask.GetMask("Walkable");
            
            _leftFootRot  = transform.rotation;
            _rightFootRot = transform.rotation;
        }

        private void OnAnimatorIK(int layerIndex) {
            if(animator == null) return;
            
            float leftWeight  = animator.GetFloat("LeftFootIKWeight");
            float rightWeight = animator.GetFloat("RightFootIKWeight");

            Debug.Log($"[FootIK] LeftWeight: {leftWeight} | RightWeight: {rightWeight}");
            
            SetIk(AvatarIKGoal.LeftFoot,  "LeftFootIKWeight", ref _leftFootPos, ref _leftFootRot,
                ref _leftOrigin, ref _leftHit, ref _leftHitValid);
            SetIk(AvatarIKGoal.RightFoot, "RightFootIKWeight", ref _rightFootPos, ref _rightFootRot,
                ref _rightOrigin, ref _rightHit, ref _rightHitValid);
        }

        private void SetIk(AvatarIKGoal goal, string weightParam, ref Vector3 smoothPos, ref Quaternion smoothRot,
            ref Vector3 origin, ref RaycastHit hit, ref bool hitValid) 
        {
            
            // Read IK weight from the animator curve — 0 when foot is lifting,
            // 1 when foot is planted. Falls back to 1 if curve is not set up yet.
            float ikWeight = animator.GetFloat(weightParam);
            
            animator.SetIKPositionWeight(goal, ikWeight);
            animator.SetIKRotationWeight(goal, ikWeight);

            // if (Physics.Raycast(animator.GetIKPosition(goal) + Vector3.up, Vector3.down, out RaycastHit hit, footBoneDistanceToGround + 1f,  groundLayer)) {
            //     Debug.Log(hit.transform.name);
            //     
            //     var footPos = hit.point;
            //     footPos.y += footBoneDistanceToGround;
            //     animator.SetIKPosition(goal, footPos);
            //     animator.SetIKRotation(goal, Quaternion.LookRotation(transform.forward, hit.normal));
            // }
            
            // No need to compute IK when weight is essentially zero.
            if (ikWeight < 0.01f) {
                hitValid = false;
                return;
            }

            Vector3 footPos   = animator.GetIKPosition(goal);
            Vector3 rayOrigin = footPos + Vector3.up * raycastHeightAboveFoot;
            float   rayDist   = raycastHeightAboveFoot + raycastMaxDistance;

            origin   = rayOrigin;
            hitValid = Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDist, groundLayer);

            if (hitValid) {
                // Pick offset based on surface angle.
                float slopeAngle  = Vector3.Angle(Vector3.up, hit.normal);
                float heightOffset = slopeAngle > slopeAngleThreshold
                    ? footHeightOffsetSlope
                    : footHeightOffsetFlat;

                Vector3 targetPos = hit.point + Vector3.up * heightOffset;

                Quaternion targetRot = Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(transform.forward, hit.normal),
                    hit.normal
                );

                // Smooth position and rotation to prevent snapping.
                smoothPos = Vector3.Lerp(smoothPos, targetPos,
                    positionSmoothSpeed * Time.deltaTime);
                smoothRot = Quaternion.Slerp(smoothRot, targetRot,
                    rotationSmoothSpeed * Time.deltaTime);

                animator.SetIKPosition(goal, smoothPos);
                animator.SetIKRotation(goal, smoothRot);
            } else {
                // No ground found — smoothly return to animated position.
                smoothPos = Vector3.Lerp(smoothPos, footPos,
                    positionSmoothSpeed * Time.deltaTime);
                smoothRot = Quaternion.Slerp(smoothRot, transform.rotation,
                    rotationSmoothSpeed * Time.deltaTime);
            }
        }
        
        private void OnDrawGizmos() {
            if (animator == null) return;

            DrawFootGizmo(_leftOrigin,  _leftHit,  _leftHitValid,  "L");
            DrawFootGizmo(_rightOrigin, _rightHit, _rightHitValid, "R");
        }

        private void DrawFootGizmo(Vector3 origin, RaycastHit hit, bool hitValid, string label) {
            float rayDist = raycastHeightAboveFoot + raycastMaxDistance;

            if (hitValid) {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(origin, hit.point);
                Gizmos.DrawSphere(hit.point, 0.03f);

                Gizmos.color = Color.grey;
                Gizmos.DrawLine(hit.point, origin + Vector3.down * rayDist);

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(hit.point + Vector3.up * footHeightOffsetFlat, 0.04f);

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(hit.point, hit.normal * 0.2f);

#if UNITY_EDITOR
                UnityEditor.Handles.Label(hit.point + Vector3.right * 0.1f,
                    $"{label} | angle: {Vector3.Angle(Vector3.up, hit.normal):F1}°");
#endif
            } else {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, origin + Vector3.down * rayDist);
            }
        }
    }
}