using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Player {
    public class FootIK : MonoBehaviour {
        private Animator animator;
        private LayerMask groundLayer;

        public float footBoneDistanceToGround;
        
        private void Start() {
            animator = GetComponent<Animator>();
            groundLayer = LayerMask.GetMask("Walkable");
        }

        private void OnAnimatorIK(int layerIndex) {
            if(animator == null) return;
            
            SetIk(AvatarIKGoal.LeftFoot);
            SetIk(AvatarIKGoal.RightFoot);
        }

        private void SetIk(AvatarIKGoal goal) {
            animator.SetIKPositionWeight(goal, 1f);
            animator.SetIKRotationWeight(goal, 1f);

            if (Physics.Raycast(animator.GetIKPosition(goal) + Vector3.up, Vector3.down, out RaycastHit hit, footBoneDistanceToGround + 1f,  groundLayer)) {
                Debug.Log(hit.transform.name);
                
                var footPos = hit.point;
                footPos.y += footBoneDistanceToGround;
                animator.SetIKPosition(goal, footPos);
                animator.SetIKRotation(goal, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}