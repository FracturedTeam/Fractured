using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.GameServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace _Project.Scripts.ECS
{
    public class Glass : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public ColorEnum GetColor => color2D;
    
        [SerializeField] private ColorEnum color2D;
        [SerializeField] internal List<InternColliders> colliders = new List<InternColliders>();
        private Camera cam;
        private float GetWindowHeight => cam.pixelHeight/1080f ;
        
        private bool isHeld;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if(isHeld && GameInitializer.Instance.InEditableArea())
                transform.position = Mouse.current.position.ReadValue();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
           isHeld = true;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            isHeld = false;
        }
    
        public bool CheckCollision(GlassInteractable block)
        {
            foreach (var internColliders in colliders)
            {
                if (!IsColliding(block, internColliders)) 
                    continue;

                return true;
            }
            return false;
        }

        ///Get if the 3D object is colliding with any the colliders 2D
        private bool IsColliding(GlassInteractable block, InternColliders internCollider)
        {
            var screenPos = cam.WorldToScreenPoint(block.transform.position);
            var ab = screenPos - (transform.position + new Vector3(internCollider.pos.x, internCollider.pos.y) * GetWindowHeight);
        
            var radiusSum = internCollider.radius * GetWindowHeight + block.GetRadius;
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
            var color = element!.GetColor == ColorEnum.Blue  ? Color.dodgerBlue : Color.crimson;
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