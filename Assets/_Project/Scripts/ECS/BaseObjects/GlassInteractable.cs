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
        
        [Header("Debug on UI")]
        [SerializeField] private Vector2 pos2D;
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        
        private Camera cam; 
        
        public  void Initialize() {
            cam = Camera.main;
            
            if(TryGetComponent(typeof(BaseObject), out var component))
                baseObject = component as BaseObject;
            else return;
            
            baseObject!.SetRenderer(false);
            baseObject!.SetCollider(false);
        }
        
        internal void OnInteract(bool isOn, ColorEnum glassColor)
        {
            if(!baseObject)
                return;

            if (glassColor == color) 
                return;
            
            baseObject.SetRenderer(isOn);
            baseObject.SetCollider(isOn);
        }
    
        ///Draw The Gizmos of the collider, only in Editor
        private void OnDrawGizmos()
        {
            if(!showColliders)
                return;
      
            Gizmos.color = color == ColorEnum.Blue  ? Color.dodgerBlue : Color.crimson;
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
