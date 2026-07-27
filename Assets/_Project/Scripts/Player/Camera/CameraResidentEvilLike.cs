using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Player.Camera {
    [ExecuteAlways, SaveDuringPlay, AddComponentMenu("Cinemachine/Extensions/Resident Evil Like Camera")]
    public class CameraResidentEvilLike : CinemachineExtension {
        [Header("Camera Rotation")]
        [SerializeField] private float maxYawRight;
        [SerializeField] private float maxYawLeft;
        [Space]
        [SerializeField] private float maxPitchUp;
        [SerializeField] private float maxPitchDown;
        
        [Header("Tampon Size")]
        [SerializeField, Range(0,1)] private float tamponSizeX;
        [SerializeField, Range(0,1)] private float tamponSizeY;
        
        [Header("Rotation Speed")]
        [SerializeField] private float rotationSpeedX;
        [SerializeField] private float rotationSpeedY;
        
        [Header("Return Speed")]
        [SerializeField] private float returnSpeed;

        private float initialYaw;
        private float initialPitch;
        
        private float xLerpTime = 0;
        private float yLerpTime = 0;
        
        private float deadZoneX;
        private float deadZoneY;
        private float screenPosX;
        private float screenPosY;
        
        private float offsetX;
        private float offsetY;
        
        private bool lerpX = false;
        private bool lerpY = false;
        
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

            deadZoneX = composer.Composition.DeadZone.Size.x;
            deadZoneY = composer.Composition.DeadZone.Size.y;
            
            var brain = CinemachineCore.FindPotentialTargetBrain(vcam);
            var outputCam = brain.OutputCamera;
            
            var screenPos = outputCam.WorldToScreenPoint(followTarget.position);
            
            var behindCam = screenPos.z < 0f;

            var x = behindCam ? 0f : (screenPos.x - Screen.width * 0.5f) / (Screen.width * 0.5f);
            var y = behindCam ? 0f : (screenPos.y - Screen.height * 0.5f) / (Screen.height * 0.5f);

            CalculateOffset(x, y, composer, deltaTime);
            
            state.RawOrientation = Quaternion.Euler(initialPitch + offsetY, initialYaw + offsetX, 0);
        }

        private void CalculateOffset(float x, float y, CinemachineRotationComposer composer, float deltaTime) {
            if (y > deadZoneY || y < -deadZoneY) {
                lerpY = true;
            }
            else if (y > -deadZoneY + tamponSizeY &&  y < deadZoneY - tamponSizeY) {
                lerpY = false;
            }
            
            if (x > deadZoneX || x < -deadZoneX) {
                lerpX = true;
            }
            else if (x > -deadZoneX + tamponSizeX &&  x < deadZoneX - tamponSizeX) {
                lerpX = false;
            }

            xLerpTime = lerpX ? xLerpTime + deltaTime * rotationSpeedX : xLerpTime - deltaTime * returnSpeed;
            xLerpTime = Mathf.Clamp(xLerpTime, 0, 1);
            
            yLerpTime = lerpY ? yLerpTime +  deltaTime * rotationSpeedY : yLerpTime - deltaTime * returnSpeed;
            yLerpTime = Mathf.Clamp(yLerpTime, 0, 1);
            
            offsetX = Mathf.Lerp(0,  x > 0 ? maxYawRight : -maxYawLeft, xLerpTime);
            offsetY =  Mathf.Lerp(0,  y < 0 ? maxPitchUp : -maxPitchDown, yLerpTime);
        }
    }
}