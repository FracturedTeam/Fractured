using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] Transform splashScreenCameraTransform;
        [SerializeField] Transform mainScreenCameraTransform;
        [SerializeField] Transform optionsScreenCameraTransform;
        [SerializeField] Transform creditsScreenCameraTransform;

        [Range(0, 1), SerializeField] private float speed;
        private Camera mainCamera;
        private Transform target;
        private Transform lastPosition;
        private float moveTime = 0.0f;
        
    
        public static MenuManager Instance;

        private void Awake()
        {
            mainCamera = Camera.main;
            
            if(Instance == null) Instance =  this;
            else Destroy(this);
            //mainCamera.transform.SetPositionAndRotation(splashScreenCameraTransform.position, splashScreenCameraTransform.rotation);
        }

        public void ChangeTarget(Transform newTarget)
        {
            target = newTarget;
            lastPosition = mainCamera.transform;
            moveTime = 0;
        }
        private void Update()
        {
            if(target != null)
                ChangeCameraTransform(target);
        }

        private void ChangeCameraTransform(Transform newCameraTransform)
        {
            moveTime += Time.deltaTime * speed;
            mainCamera.transform.position = Vector3.Lerp (lastPosition.transform.position, newCameraTransform.position, moveTime);
            mainCamera. transform.rotation = Quaternion.Lerp (lastPosition.transform.rotation, newCameraTransform.rotation, moveTime);

            if (!((mainCamera.transform.position - target.position).magnitude <= 0.1f)) 
                return;
            
            if (newCameraTransform.TryGetComponent(typeof(CameraTranstion), out var trans))
            {
                var transPos = trans as CameraTranstion;
                transPos!.OnTrigger();
            }
            else
                target = null;
        }
    }
}
