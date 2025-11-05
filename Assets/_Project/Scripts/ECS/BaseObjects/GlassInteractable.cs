using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.Systems.HashSetUtil;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.ECS.BaseObjects
{
    public class GlassInteractable : MonoBehaviour
    {
        public float GetRadius => radius2D;
        
        private BaseObject baseObject;
        private Camera mainCamera;
        private ObservableHashSet<Glass> shardsOnTop;
        
        [Header("Object Color")]
        public ColorEnum objectColor;

        [Header("Behaviour")] 
        [Tooltip("If true, when the object is under a shard, it will transit into a new object that can be interacted with")]
        [SerializeField] private bool swapObject = false;
        [SerializeField] private GameObject alternateObjectMesh;
        
        [Header("Debug on UI")]
        [SerializeField] internal Vector2 pos2D;
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        
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
                
                shardsOnTop = new ObservableHashSet<Glass>();
                shardsOnTop.onUpdate += UpdateShards;
            }
            
            initialized = true;
            
            underRed = 0;
            underBlue = 0;
            
            baseObject!.SetRenderer(objectColor != ColorEnum.Both);
            baseObject!.SetCollider(objectColor != ColorEnum.Both);

            if (swapObject) {
                if(alternateObjectMesh == null) Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
                alternateObjectMesh?.SetActive(false);
            }
            
            
            SetUp();
        }

        void OnDisable() {
            shardsOnTop.onUpdate -= UpdateShards;
        }

        internal void OnInteract(bool isUnder, Glass shard) {
            if(!baseObject)
                return;
            
            if (isUnder) 
                shardsOnTop.Add(shard);
            else if(shardsOnTop.Contains(shard))
                shardsOnTop.Remove(shard);
        }
        
        private void Update() { //Le problème vient d'ici -> Le reset de underRed et underBlue a 0 cause le reset constant l'interactable
            pos2D = mainCamera!.WorldToScreenPoint(transform.position);
        }

        private void UpdateShards() {
            Debug.Log("[GlassInteractable] Updating shards");
            
            underBlue = 0;
            underRed = 0;

            foreach (var shard in shardsOnTop.Items)
                switch (shard.GetColor) {
                    case ColorEnum.Blue:
                        underBlue++;
                        break;
                    case ColorEnum.Red:
                        underRed++;
                        break;
                    case ColorEnum.Both:
                        underBlue++;
                        underRed++;
                        break;
                    default:
                        Debug.LogWarning($"[GlassInteractable] Unknown shard color {shard.GetColor}");
                        break;
                }
            
            if(swapObject) 
                SwapObject();
            else
                SetVisibility();
        }
        
        private void SetVisibility()
        {
            switch (objectColor) {
                case ColorEnum.Both:
                    baseObject.SetRenderer(underRed > 0 && underBlue > 0);
                    baseObject.SetCollider(underRed > 0 && underBlue > 0);
                    baseObject.SetInteract(underRed > 0 && underBlue > 0);
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
            switch (objectColor) {
                case ColorEnum.Both:
                    baseObject.SetRenderer(!(underRed > 0 && underBlue > 0));
                    alternateObjectMesh?.SetActive(underRed > 0 && underBlue > 0);
                    baseObject.SetInteract(underRed > 0 && underBlue > 0);
                    break;
                case ColorEnum.Blue:
                    baseObject.SetRenderer(underBlue < 1);
                    alternateObjectMesh?.SetActive(underBlue > 0);
                    baseObject.SetInteract(underBlue > 0);
                    break;
                case ColorEnum.Red:
                    baseObject.SetRenderer(underRed < 1);
                    alternateObjectMesh?.SetActive(underRed > 0);
                    baseObject.SetInteract(underRed > 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public void ResetObject() {
            underRed = 0;
            underBlue = 0;
            shardsOnTop.Clear();
            
            baseObject!.SetRenderer(true);
            baseObject!.SetCollider(true);

            if (!swapObject) return;
            
            if(alternateObjectMesh == null) Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
            alternateObjectMesh?.SetActive(false);
        }

        public bool UnderGlass() {
            return objectColor switch {
                ColorEnum.Red => underRed > 0,
                ColorEnum.Blue => underBlue > 0,
                ColorEnum.Both => underRed > 0 && underBlue > 0,
                _ => false
            };
        }

        ///Draw The Gizmos of the collider, only in Editor
        private void OnDrawGizmos() {
            if(!showColliders)
                return;
      
            Gizmos.color = objectColor switch {
                ColorEnum.Blue => Color.dodgerBlue,
                ColorEnum.Red => Color.crimson,
                _ => Color.darkOrchid
            };
            Gizmos.DrawSphere(pos2D, GetRadius);
        }

        ///Auto Setup the collision
        [ContextMenu("SetUp")]
        private void SetUp() {
            var meshRenderer = GetComponent<MeshRenderer>();
            if(mainCamera == null) mainCamera = Camera.main;
        
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
