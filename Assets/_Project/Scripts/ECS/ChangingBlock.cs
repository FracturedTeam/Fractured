using System;
using UnityEngine;

public class ChangingBlock : MonoBehaviour
{
   
   MeshRenderer meshRenderer;
   private Camera cam; 
   [SerializeField] Vector2 pos2D;
   [SerializeField] float radius2D;
   [SerializeField] bool showColliders;

   private void Start()
   {
      cam = Camera.main;
      meshRenderer = GetComponent<MeshRenderer>();
   }

   [ContextMenu("SetUp")]
   private void Calcul()
   {
      meshRenderer = GetComponent<MeshRenderer>();
      Vector3 min = meshRenderer.bounds.min;
      Vector3 max = meshRenderer.bounds.max;
      Vector3 screenMin = Camera.main!.WorldToScreenPoint(min);
      Vector3 screenMax = Camera.main!.WorldToScreenPoint(max);
      pos2D = Camera.main!.WorldToScreenPoint(transform.position);
      radius2D = ((((screenMax-screenMin).magnitude *  -transform.position.z) + (screenMax-screenMin).magnitude)) /4;
   }
   
   private void OnDrawGizmos()
   {
      if(!showColliders)
         return;
      
      Gizmos.color = new Color(1f, 0f, 0f, 1);
      Gizmos.DrawSphere(pos2D, GetRadius);
   }
   
   public void ChangeState(bool isOn)
   {
      meshRenderer.enabled = isOn;
   }
   
   public float GetRadius => radius2D;
}
