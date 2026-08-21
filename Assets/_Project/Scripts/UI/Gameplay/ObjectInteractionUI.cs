using System;
using System.Collections;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.UI.Gameplay {
    public class ObjectInteractionUI : MonoBehaviour {
        [Header("Sprites")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite closedSpriteBlue;
        [SerializeField] private Sprite closedSpriteRed;
        [SerializeField] private Sprite closedSpriteYellow;
        
        [Header("UI Settings")]
        [SerializeField] private float constantCameraScale = 1;
        [SerializeField] private float maxScale = 0.5f, minScale = 0.25f;
        [SerializeField] private float distanceToBeVisible = 8;
        [SerializeField] private float maxScaleMinimumDistance = 1;
        [SerializeField] private Ease easeType;

        private Sprite closeSprite;
        private Vector3 offset;
        private MeshRenderer parentMesh;
        private Collider parentCollider;
        
        private Vector3 distanceScale;
        private Vector3 cameraScale;
        private float distanceToPlayer;
        private float distanceToCamera;
        private Tweener tween;

        private void Start() {
            closeSprite = GameInitializer.Instance.CurrentChapter switch {
                1 => closedSpriteRed,
                2 => closedSpriteBlue,
                3 => closedSpriteYellow,
                _ => throw new ArgumentOutOfRangeException()
            };
            
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
            
            if (parentMesh == null && parentCollider == null) {
                yield break;
            }
            
            var center = parentMesh ? parentMesh.bounds.center : parentCollider.bounds.center;

            var outPutCamera = CinemachineBrain.GetActiveBrain(0).OutputCamera;
            var dirToCam = (outPutCamera.transform.position - center).normalized;
            
            var bounds = parentMesh ? parentMesh.bounds : parentCollider.bounds;
            var extents = bounds.extents;
            var projectedSize = MathF.Abs(Vector3.Dot(extents, dirToCam));
            
            transform.position = center + dirToCam + offset * (projectedSize + 0.1f);
        }
        
        public void RegisterComponents(MeshRenderer meshRenderer, Collider col, Vector3 offset) {
            parentMesh = meshRenderer;
            parentCollider = col;
            this.offset = offset;
            StartCoroutine(UpdatePosition());
        }

        public void ManualPositionUpdate(Vector3 offset) {
            this.offset = offset;
            StartCoroutine(UpdatePosition());
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