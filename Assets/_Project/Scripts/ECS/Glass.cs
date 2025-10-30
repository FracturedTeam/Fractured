using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Scripts.ECS
{
    public class Glass : MonoBehaviour
    {
        public ColorEnum GetColor => color2D;
    
        [SerializeField] private ColorEnum color2D;
        [SerializeField] internal List<InternColliders> colliders = new List<InternColliders>();
        [SerializeField] private bool canEditAnywhere = false;
        private Camera cam;
        private Image image;
        private float GetWindowHeight => cam.pixelHeight/1080f ;
        private Vector2 mousePosition;
        
        private bool isHeld;
        private bool isActivated;
        

        private void Start()
        {
            cam = Camera.main;
            
            if (TryGetComponent(typeof(Image), out var img)) 
                image = img as Image;
        }

        private void Update()
        {
            if(isHeld && (GameInitializer.Instance.InEditableArea() || canEditAnywhere))
            {
                transform.position = Mouse.current.position.ReadValue();
            }
            
            InputsProcessing();
        }

        private void InputsProcessing()
        {
            var isInZone = false;
            foreach (var internCollider in colliders)
            {
                var ab = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y) 
                         - (transform.position + new Vector3(internCollider.pos.x, internCollider.pos.y) 
                             * GetWindowHeight);
        
                var radiusSum = internCollider.radius * GetWindowHeight + 1;
        
                if(ab.magnitude <= radiusSum)
                    isInZone = true;
            }
                
            if(!isInZone)
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
            
            if(!image)
                return;
            
            //Will be replaced by the shader
            image.color = isOn ? new Color(1,1,1,0.5f) : Color.grey ;
        }
    
        public bool CheckCollision(GlassInteractable block)
        {
            foreach (var internColliders in colliders)
            {
                if (!IsColliding(cam.WorldToScreenPoint(block.transform.position), internColliders, block.GetRadius))
                    continue;

                return true;
            }
            return false;
        }

        ///Get if an object is colliding with any the colliders 2D
        private bool IsColliding(Vector3 position, InternColliders internCollider, float radius = 1)
        {
            if(!cam || !isActivated)
                return false;
            
            var ab = position - (transform.position + new Vector3(internCollider.pos.x, internCollider.pos.y) * GetWindowHeight);
        
            var radiusSum = internCollider.radius * GetWindowHeight + radius;
            var isColliding = ab.magnitude <= radiusSum; 
        
            return isColliding;
        }
    
        [Serializable]
        internal struct InternColliders
        {
            public Vector2 pos;
            public float radius;
        }
    }

    ///Show The colliders 2d/3d of the glass shard
    [CustomEditor(typeof(Glass))]
    public class ShowColliders : Editor
    {
        public void OnSceneGUI()
        {
            var element = target as Glass;
            var color = element!.GetColor switch
            {
                ColorEnum.Blue => Color.dodgerBlue,
                ColorEnum.Red => Color.crimson,
                _ => Color.darkOrchid
            };
            var size = Camera.main!.pixelHeight / 1080f;
            Handles.color = color;
        
            foreach (var collider in element!.colliders)
            {
                var pos = element.transform.position + new Vector3(collider.pos.x, collider.pos.y) * size;
                Handles.DrawWireDisc(pos, Vector3.forward, collider.radius * size);
                GUI.color = color;
                Handles.Label(pos, collider.pos.ToString());
            }
        }
    }
}