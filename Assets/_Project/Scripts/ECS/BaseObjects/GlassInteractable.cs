using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class GlassInteractable : MonoBehaviour
    {
        public float GetRadius => radius2D;
        public ColorEnum color;
        private BaseObject baseObject;

        [Header("Behaviour")] 
        [Tooltip("If true, when the object is under a shard, it will transit into a new object that can be interacted with")]
        [SerializeField] private bool swapObject = false;
        [SerializeField] private GameObject alternateObjectMesh;
        
        [Header("Debug on UI")]
        [SerializeField] internal Vector2 pos2D;
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        
        private Camera mainCamera; 
        private int underRed;
        private int underBlue;
        
        private bool initialized = false;
        
        public  void Initialize() {
            mainCamera = Camera.main;

            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component))
                    baseObject = component as BaseObject;
                else
                    Debug.LogError($"[GlassInteractable] BaseObject on {gameObject.name} could not be found !");
            }

            initialized = true;
            
            underRed = 0;
            underBlue = 0;
            
            baseObject!.SetRenderer(color != ColorEnum.Both);
            baseObject!.SetCollider(color != ColorEnum.Both);

            if (swapObject) {
                if(alternateObjectMesh == null) Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
                alternateObjectMesh?.SetActive(false);
            }
            
            
            SetUp();
        }
        
        internal void OnInteract(bool isUnder, ColorEnum colorEnum) {
            if(!baseObject)
                return;
            
            if(isUnder)
            {
                switch (colorEnum)
                {
                    case ColorEnum.Red:
                        underRed++;
                        break;
                    case ColorEnum.Blue:
                        underBlue++;
                        break;
                    case ColorEnum.Both:
                        underBlue++;
                        underRed++;
                        break;
                }
            }

            if (color == ColorEnum.Both)
            {
                if(swapObject)
                    SwapObject();
                else
                    SetVisibility();
                return;
            }
            
            if (colorEnum != color) 
                return;
            
            if(swapObject)
                SwapObject();
            else
                SetVisibility();
        }
        

        private void SetVisibility()
        {
            switch (color)
            {
                case ColorEnum.Both:
                    baseObject.SetRenderer(underRed>0 && underBlue>0);
                    baseObject.SetCollider(underRed>0 && underBlue>0);
                    baseObject.SetInteract(underRed>0 && underBlue>0);
                    break;
                case ColorEnum.Red:
                    baseObject.SetRenderer(underRed < 1);
                    baseObject.SetCollider(underRed < 1);
                    baseObject.SetInteract(underRed < 1);
                    break;
                case ColorEnum.Blue:
                    baseObject.SetRenderer(underBlue < 1);
                    baseObject.SetCollider(underBlue < 1);
                    baseObject.SetInteract(underBlue < 1);
                    break;
            }
        }

        private void SwapObject() {
            if (color == ColorEnum.Both) {
                baseObject.SetRenderer(!(underRed>0 && underBlue>0));
                alternateObjectMesh?.SetActive(underRed>0 && underBlue>0);
                baseObject.SetInteract(underRed>0 && underBlue>0);
            }
            else {
                baseObject.SetRenderer(!(underRed>0 && underBlue>0));
                alternateObjectMesh?.SetActive(underRed>0 && underBlue>0);
                baseObject.SetInteract(underRed>0 && underBlue>0);
            }
        }

        public void ResetObject() {
            underRed = 0;
            underBlue = 0;
            
            baseObject!.SetRenderer(true);
            baseObject!.SetCollider(true);

            if (swapObject) {
                if(alternateObjectMesh == null) Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
                alternateObjectMesh?.SetActive(false);
            }
        }

        private void Update() {
            pos2D = mainCamera!.WorldToScreenPoint(transform.position);
            underBlue = 0;
            underRed = 0;
        }

        public bool UnderGlass()
        {
            return color switch
            {
                ColorEnum.Red => underRed > 0,
                ColorEnum.Blue => underBlue > 0,
                ColorEnum.Both => underRed > 0 && underBlue > 0,
                _ => false
            };
        }

        ///Draw The Gizmos of the collider, only in Editor
        private void OnDrawGizmos()
        {
            if(!showColliders)
                return;
      
            Gizmos.color = color switch
            {
                ColorEnum.Blue => Color.dodgerBlue,
                ColorEnum.Red => Color.crimson,
                _ => Color.darkOrchid
            };
            Gizmos.DrawSphere(pos2D, GetRadius);
        }
   
        ///Auto Setup the collision, only called in Editor
        /// Now is also called on Initialisation
        [ContextMenu("SetUp")]
        private void SetUp() {
            var meshRenderer = GetComponent<MeshRenderer>();
            
            if(mainCamera == null)
                mainCamera = Camera.main;
        
            var min = meshRenderer.bounds.min;
            var max = meshRenderer.bounds.max;
            var screenMin = Camera.main!.WorldToScreenPoint(min);
            var screenMax = Camera.main!.WorldToScreenPoint(max);
            var mag = (transform.position + mainCamera!.transform.position).magnitude;
            
            pos2D = mainCamera!.WorldToScreenPoint(transform.position);
            radius2D = Mathf.Abs((screenMax-screenMin).magnitude *  -transform.position.z + (screenMax-screenMin).magnitude) / mag * 4;
        }
    }
}
