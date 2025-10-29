using System;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class GlassInteractable : MonoBehaviour
    {
        public float GetRadius => radius2D;
        public ColorEnum color;
    
        [SerializeField] private Vector2 pos2D;
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
    
        private Camera cam; 
        private BaseObject baseObject;
        private bool underRed;
        private bool underBlue;
        private void Start()
        {
            cam = Camera.main;
            
            if(TryGetComponent(typeof(BaseObject), out var component))
                baseObject = component as BaseObject;
            else 
                baseObject = gameObject.AddComponent<BaseObject>();
            
            baseObject!.SetRenderer(true);
            baseObject!.SetCollider(true);
        }
        
        internal void OnInteract(bool isOn, ColorEnum glassColor)
        {
            if(!baseObject)
                return;

            switch (glassColor)
            {
                case ColorEnum.Red:
                    underRed = isOn;
                    break;
                case ColorEnum.Blue:
                    underBlue = isOn;
                    break;
                case ColorEnum.Both: 
                    underRed = isOn; 
                    underBlue = isOn;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(glassColor), glassColor, null);
            }

            if (color == ColorEnum.Both )
            {
                baseObject.SetRenderer(underRed && underBlue);
                baseObject.SetCollider(underRed && underBlue);
                return;
            }
            
            if (glassColor != color) 
                return;
            
            baseObject.SetRenderer(!isOn);
            baseObject.SetCollider(!isOn);
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
        [ContextMenu("SetUp")]
        private void SetUp()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            cam = Camera.main;
        
            Vector3 min = meshRenderer.bounds.min;
            Vector3 max = meshRenderer.bounds.max;
            Vector3 screenMin = Camera.main!.WorldToScreenPoint(min);
            Vector3 screenMax = Camera.main!.WorldToScreenPoint(max);
            pos2D = cam!.WorldToScreenPoint(transform.position);
            radius2D = ((((screenMax-screenMin).magnitude *  -transform.position.z) + (screenMax-screenMin).magnitude)) /4;
        }
    }
}
