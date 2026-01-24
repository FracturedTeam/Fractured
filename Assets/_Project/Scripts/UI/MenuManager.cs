using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Camera Transform Position")]
        [SerializeField] Transform splashScreenCameraTransform;
        [SerializeField] Transform mainScreenCameraTransform;
        [SerializeField] Transform optionsScreenCameraTransform;
        [SerializeField] Transform creditsScreenCameraTransform;
        
        [Header("Camera Animation Curve")]
        [SerializeField] AnimationCurve cameraMovementCurve;

        [Header("Camera Move Speed")]
        [Range(0, 1), SerializeField] private float speed;
        
        [Header("Camera Ref")]
        [SerializeField] private CinemachineCamera mainCamera;
        private Transform target;
        private Vector3 lastPosition;
        private Quaternion lastRotation;
        private float moveTime = 0.0f;
    
        public static MenuManager Instance;

        private void Awake()
        {
            //mainCamera = Camera.main;
            
            if(Instance == null) Instance =  this;
            else Destroy(this);
            mainCamera.transform.SetPositionAndRotation(splashScreenCameraTransform.position, splashScreenCameraTransform.rotation);
        }

        public void ChangeTarget(Transform newTarget)
        {
            target = newTarget;
            lastPosition = mainCamera.transform.position;
            lastRotation = mainCamera.transform.rotation;
            moveTime = 0;
        }
        private void Update()
        {
            if(target != null)
                ChangeCameraTransform();
        }

        private void ChangeCameraTransform()
        {
            moveTime += Time.deltaTime * speed;
            mainCamera.transform.position = Vector3.Lerp (lastPosition, target.position, cameraMovementCurve.Evaluate(moveTime));
            mainCamera.transform.rotation = Quaternion.Lerp (lastRotation, target.rotation, cameraMovementCurve.Evaluate(moveTime));

            if (!((mainCamera.transform.position - target.position).magnitude <= 0.1f)) 
                return;
            
            if (target.TryGetComponent(typeof(CameraTransition), out var trans))
            {
                var transPos = trans as CameraTransition;
                transPos!.OnTrigger();
            }
            else
                target = null;
        }
    }
}
