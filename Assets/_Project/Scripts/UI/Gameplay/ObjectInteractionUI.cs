using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Player;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay {
    public class ObjectInteractionUI : MonoBehaviour {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float maxScale = 0.5f, minScale = 0.25f;
        [SerializeField] private float distanceToBeVisible = 8;
        [SerializeField] private float maxScaleMinimumDistance = 1;
        [SerializeField] private Ease easeType;

        private float distanceToPlayer;
        private Tweener tween;
        
        // Scale - Rotation - Position

        private void LateUpdate() {
            distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

            // A modifier pour être call qu'une fois lorsque la distance change
            if (distanceToPlayer > distanceToBeVisible) {
                tween = spriteRenderer.DOFade(0f, 0.25f).SetEase(easeType);
            }
            else {
                tween = spriteRenderer.DOFade(1f, 0.25f).SetEase(easeType);
            }
            
            transform.LookAt(Camera.main.transform);
            transform.localScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, maxScaleMinimumDistance / distanceToPlayer);
        }

        private void OnDisable() {
            tween?.Kill();
        }
    }
}