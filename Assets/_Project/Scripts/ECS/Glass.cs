using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Scripts.ECS
{
    public class Glass : MonoBehaviour, IDragHandler
    {
        public ColorEnum GetColor => color2D;

        [Header("Settings")] [SerializeField] private ColorEnum color2D;
        [SerializeField] private bool canEditAnywhere = false;

        private Camera mainCamera;
        private Image shardSprite;
        private PolygonCollider2D polygonCollider2D;
        private Vector2 mousePosition;

        private bool isHeld;

        private bool initialized = false;
        private bool canInteract = true;

        private void Start()
        {
            if (!initialized)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
                Debug.LogError($"[Glass] Camera not tagged as MainCamera, Camera could not been acquired !");

            if (TryGetComponent(typeof(Image), out var img))
                shardSprite = img as Image;

            if (TryGetComponent(typeof(PolygonCollider2D), out var col))
                polygonCollider2D = col as PolygonCollider2D;

            initialized = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!canInteract)
                return;

            if (!isHeld || (!GameInitializer.Instance.InEditableArea() && !canEditAnywhere))
                return;

            transform.position += (Vector3)eventData.delta;
            transform.position = new Vector2(
                Math.Clamp(transform.position.x, 0 + shardSprite.rectTransform.sizeDelta.x / 2,
                    mainCamera.pixelWidth - shardSprite.rectTransform.sizeDelta.x / 2),
                Mathf.Clamp(transform.position.y, 0 + shardSprite.rectTransform.sizeDelta.y / 2,
                    mainCamera.pixelHeight - shardSprite.rectTransform.sizeDelta.y / 2));
        }

        internal void ChangeHoldingState(bool isOn)
        {
            if (!canInteract) return;

            isHeld = isOn;
        }

        internal void ChangeStateActivation(bool isOn)
        {
            if (!shardSprite)
                return;

            //Will be replaced by the shader
            //shardSprite.color = isOn ? new Color(1,1,1,0.7f) : new Color(1,1,1,0.4f);
        }
        
        
        ///Get if an object is colliding with any the colliders 2D
        internal bool IsColliding(Vector3 position)
        {
            if (!mainCamera)
                return false;

            Vector3 closest = polygonCollider2D.ClosestPoint(position);
            return closest == position;
        }

        internal bool IsColliding(Vector3[] positions)
        {
            if (!mainCamera)
                return false;
            
            foreach (var position in positions)
            {
                Vector3 closest = polygonCollider2D.ClosestPoint(position);
                if(closest != position)
                    return false;
            }
            return true;
        }
        

        private void SetInteract(bool canInteract) {
            this.canInteract = canInteract;
        }

        public void SetEditAnywhere(bool editAnywhere) {
            canEditAnywhere = editAnywhere;
        }
    }
}