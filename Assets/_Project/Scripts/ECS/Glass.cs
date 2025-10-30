using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Scripts.ECS {
    public class Glass : MonoBehaviour {
        public ColorEnum GetColor => color2D;
    
        [Header("Settings")]
        [SerializeField] private ColorEnum color2D;
        [SerializeField] private bool canEditAnywhere = false;
        
        private Camera mainCamera;
        private Image shardSprite;
        private PolygonCollider2D polygonCollider2D;
        private float GetWindowHeight => mainCamera.pixelHeight/1080f ;
        private Vector2 mousePosition;
        
        
        private bool isHeld;
        private bool isActivated;

        private void Start() {
            mainCamera = Camera.main;
            
            if(mainCamera == null)
                Debug.LogError($"[Glass] Camera not tagged as MainCamera, Camera could not been acquired !");
            
            if (TryGetComponent(typeof(Image), out var img)) 
                shardSprite = img as Image;
            
            if (TryGetComponent(typeof(PolygonCollider2D), out var col)) 
                polygonCollider2D = col as PolygonCollider2D;
        }

        private void Update() {
            if(isHeld && (GameInitializer.Instance.InEditableArea() || canEditAnywhere))
            {
                transform.position = Mouse.current.position.ReadValue();
            }
            
            InputsProcessing();
        }

        private void InputsProcessing()
        {
            if(!polygonCollider2D.bounds.Contains(Mouse.current.position.ReadValue()))
                return;
            
            mousePosition =  Mouse.current.position.ReadValue();
                
            if (Mouse.current.leftButton.wasPressedThisFrame)
                ChangeHoldingState(true);
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
                ChangeHoldingState(false);
            else if (Mouse.current.rightButton.wasPressedThisFrame)
                ChangeStateActivation(!isActivated);
        }

        private void ChangeHoldingState(bool isOn)
        {
            isHeld = isOn;
            if (isOn)
                ChangeStateActivation(false);
        }
        private void ChangeStateActivation(bool isOn)
        {
            isActivated = isOn;
            
            if(!shardSprite)
                return;
            
            //Will be replaced by the shader
            shardSprite.color = isOn ? new Color(1,1,1,0.7f) : new Color(1,1,1,0.4f);
        }

        ///Get if an object is colliding with any the colliders 2D
        internal bool IsColliding(Vector3 position)
        {
            if(!mainCamera || !isActivated)
                return false;
            
            Vector3 closest = polygonCollider2D.ClosestPoint(position);
            // Because closest=point if inside - not clear from docs I feel
            return closest == position;
        
            return  polygonCollider2D.bounds.Contains(position);;
        }
    }
}