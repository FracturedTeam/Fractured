using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Player;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

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

        private Vector3 distanceScale;
        private Vector3 cameraScale;
        private float distanceToPlayer;
        private float distanceToCamera;
        private Tweener tween;

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

        private void OnDisable() {
            tween?.Kill();
        }
    }
}