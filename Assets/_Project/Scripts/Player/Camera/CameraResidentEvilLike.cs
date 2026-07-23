using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player.Camera {
    [ExecuteAlways, SaveDuringPlay, AddComponentMenu("Cinemachine/Extensions/Resident Evil Like Camera")]
    public class CameraResidentEvilLike : CinemachineExtension {
        [Header("Max Rotation Offset Per Side (degrees)")]
        public float maxYawLeft    = 15f;
        public float maxYawRight   = 15f;
        public float maxPitchUp    = 10f;
        public float maxPitchDown  = 10f;

        [Header("Smoothing")]
        [Tooltip("How fast the camera rotates toward the target offset.")]
        public float rotationSpeed = 5f;
        [Tooltip("How fast the camera returns to rest when player re-enters deadzone.")]
        public float returnSpeed   = 3f;

        private float initialYaw;
        private float initialPitch;

        private float lerpTime = 0;
        
        protected override void Awake() {
            base.Awake();
            
            initialYaw = transform.eulerAngles.y;
            initialPitch = transform.eulerAngles.x;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
            if(stage != CinemachineCore.Stage.Aim) return;
            if(deltaTime < 0f) return;
            
            var followTarget = vcam.Follow;
            if(followTarget == null) return;

            var composer = vcam.GetComponent<CinemachineRotationComposer>();
            if(composer == null) return;
            
            var brain = CinemachineCore.FindPotentialTargetBrain(vcam);
            var outputCam = brain.OutputCamera;
            
            var screenPos = outputCam.WorldToScreenPoint(followTarget.position);
            
            var behindCam = screenPos.z < 0f;

            var offsetX = behindCam ? 0f : (screenPos.x - Screen.width * 0.5f) / (Screen.width * 0.5f);
            var offsetY = behindCam ? 0f : (screenPos.y - Screen.height * 0.5f) / (Screen.height * 0.5f);
            
            // Move Screen Position in compensation of being outside the deadzone
            // Move the camera also to turn it
            
            // I think there is something to find for having a good result but will need some work
            
            state.RawOrientation = Quaternion.Euler(initialPitch, initialYaw, 0);
        }
    }
}