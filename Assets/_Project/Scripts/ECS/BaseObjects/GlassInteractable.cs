using System;
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
        [SerializeField] private Vector2 pos2D;
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        
        private Camera mainCamera; 
        private bool underRed;
        private bool underBlue;
        
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
            
            underRed = false;
            underBlue = false;
            
            baseObject!.SetRenderer(true);
            baseObject!.SetCollider(true);

            if (swapObject) {
                if(alternateObjectMesh == null) Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
                alternateObjectMesh?.SetActive(false);
            }
            
            SetUp();
        }
        
        internal void OnInteract(bool isUnder, ColorEnum glassColor) {
            if(!baseObject)
                return;
            
            switch (glassColor) {
                case ColorEnum.Red:
                    underRed = isUnder;
                    break;
                case ColorEnum.Blue:
                    underBlue = isUnder;
                    break;
                case ColorEnum.Both: 
                    underRed = isUnder; 
                    underBlue = isUnder;
                    break;
                default:
                    Debug.LogError($"[GlassInteractable] {gameObject.name} Have an unsupported color !");
                    throw new ArgumentOutOfRangeException(nameof(glassColor), glassColor, null);
            }
            
            if (glassColor != color) 
                return;
            
            if(swapObject)
                SwapObject(isUnder);
            else
                SetVisibility(isUnder);
        }

        private void SetVisibility(bool isUnder) {
            if (color == ColorEnum.Both) {
                baseObject.SetRenderer(underRed && underBlue);
                baseObject.SetCollider(underRed && underBlue);
                baseObject.SetInteract(underRed && underBlue);
            }
            else {
                baseObject.SetRenderer(!isUnder);
                baseObject.SetCollider(!isUnder);
                baseObject.SetInteract(false);
            }
        }

        private void SwapObject(bool isUnder) {
            if (color == ColorEnum.Both) {
                baseObject.SetRenderer(!underRed && !underBlue);
                alternateObjectMesh?.SetActive(underRed && underBlue);
                baseObject.SetInteract(underRed && underBlue);
            }
            else {
                baseObject.SetRenderer(!isUnder);
                alternateObjectMesh?.SetActive(isUnder);
                baseObject.SetInteract(isUnder);
            }
        }

        private void Update() {
            pos2D = mainCamera!.WorldToScreenPoint(transform.position);
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
