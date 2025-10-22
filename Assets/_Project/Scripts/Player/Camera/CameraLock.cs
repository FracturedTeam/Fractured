using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player.Camera {
    [ExecuteAlways, SaveDuringPlay, AddComponentMenu("Cinemachine/Extensions/Camera Lock")]
    public class CameraLock : CinemachineExtension
    {
        [Header("Lock Settings")]
        public bool lockY = false;
        public float lockYRotation = 0f;
        public bool lockX = false;
        public float lockXRotation = 0f;
        
        [Header("Clamp Settings")]
        public bool clampX = false;
        [Tooltip("Minimum X rotation angle")]
        public float minX = -30f;
        [Tooltip("Maximum X rotation angle")]
        public float maxX = 60f;


        [HideInInspector] public float currentRotation;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
            if (stage != CinemachineCore.Stage.Aim) return;

            if (clampX) {
                var euler = state.RawOrientation.eulerAngles;
                if (euler.x > 180f) euler.x -= 360f;

                currentRotation = euler.x;

                euler.x = Mathf.Clamp(euler.x, minX, maxX);
                state.RawOrientation = Quaternion.Euler(euler);
            }

            if (lockY) {
                var euler = state.RawOrientation.eulerAngles;

                euler.y = lockYRotation;
                state.RawOrientation = Quaternion.Euler(euler);
            }
            
            if (lockX) {
                var euler = state.RawOrientation.eulerAngles;

                euler.x = lockXRotation;
                state.RawOrientation = Quaternion.Euler(euler);
            }
        }
    }
}
