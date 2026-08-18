using System;
using System.Collections;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Player;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.UI.Gameplay {
    public class ObjectInteractionUI : MonoBehaviour {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite closeSprite;
        [SerializeField] private float constantCameraScale = 1;
        [SerializeField] private float maxScale = 0.5f, minScale = 0.25f;
        [SerializeField] private float distanceToBeVisible = 8;
        [SerializeField] private float maxScaleMinimumDistance = 1;
        [SerializeField] private Ease easeType;

        private float offset;
        private MeshRenderer parentMesh;
        
        private Vector3 distanceScale;
        private Vector3 cameraScale;
        private float distanceToPlayer;
        private float distanceToCamera;
        private Tweener tween;

        private void Start() {
            StartCoroutine(UpdatePosition());
        }

        private void OnEnable() {
            CinemachineCore.CameraActivatedEvent.AddListener(OnCameraUpdated);
        }

        private void OnDisable() {
            CinemachineCore.CameraActivatedEvent.RemoveListener(OnCameraUpdated);
            tween?.Kill();
        }
        
        private void OnCameraUpdated(ICinemachineCamera.ActivationEventParams camUpdate) {
            StartCoroutine(UpdatePosition());
        }

        private IEnumerator UpdatePosition() {
            yield return null;
            if (parentMesh == null) {
                yield break;
            }

            var outPutCamera = CinemachineBrain.GetActiveBrain(0).OutputCamera;
            var dirToCam = (outPutCamera.transform.position - parentMesh.bounds.center).normalized;
            
            var bounds = parentMesh.bounds;
            var extents = bounds.extents;
            var projectedSize = MathF.Abs(Vector3.Dot(extents, dirToCam));
            
            transform.position = parentMesh.bounds.center + dirToCam * (projectedSize + 0.1f + offset);
        }
        
        public void RegisterComponents(MeshRenderer meshRenderer, float offset) {
            parentMesh = meshRenderer;
            this.offset = offset;
        }
        
        private void LateUpdate() {
            distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
            distanceToCamera = Vector3.Distance(transform.position, CinemachineBrain.GetActiveBrain(0).OutputCamera.transform.position);

            // A modifier pour être call qu'une fois lorsque la distance change
            if (distanceToPlayer > distanceToBeVisible) {
                tween = spriteRenderer.DOFade(0f, 0.25f).SetEase(easeType);
            }
            else {
                tween = spriteRenderer.DOFade(1f, 0.25f).SetEase(easeType);
            }
            
            transform.LookAt(CinemachineBrain.GetActiveBrain(0).OutputCamera.transform);
            distanceScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, maxScaleMinimumDistance / distanceToPlayer);
            cameraScale = distanceScale * distanceToCamera / constantCameraScale;
            
            if(distanceToPlayer < maxScaleMinimumDistance)
                spriteRenderer.sprite = closeSprite;
            else
                spriteRenderer.sprite = normalSprite;
            
            transform.localScale = cameraScale;
        }
    }
}