using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
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
        [Tooltip("If true, when the object is under a shard, it will transit into a the object that is contain within")]
        [SerializeField] private bool objectInside = false;
        [SerializeField] private MoveableObject interactableInBox;
        
        [Header("Debug on UI")]
        [SerializeField] internal Vector2 pos2D;
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        
        private int underRed;
        private int underBlue;
        
        private bool initialized = false;
        private bool objectOut = false;
        
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

            if (objectInside) {
                if (interactableInBox != null) {
                    interactableInBox.gameObject.SetActive(false);
                    interactableInBox.transform.position = transform.position;
                }
                else
                    Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
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
        
        private void Update() { //Bien de voir pour dégager les updates - Pour le moment elle n'est pas couteuse donc c'est fine
            pos2D = mainCamera!.WorldToScreenPoint(transform.position);

            if (objectInside) {
                if (objectOut) return;
                if (interactableInBox.IsGrabbed()) objectOut = true;
            }
        }

        private void UpdateShards() {
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
            
            SetVisibility();
        }
        
        private void SetVisibility() {
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

            if (objectInside && !objectOut) {
                SwapObject();
            }
        }

        private void SwapObject() { 
            switch (objectColor) {
                case ColorEnum.Both:
                    interactableInBox?.gameObject.SetActive(underRed > 0 && underBlue > 0);
                    break;
                case ColorEnum.Blue:
                    interactableInBox?.gameObject.SetActive(underBlue > 0);
                    break;
                case ColorEnum.Red:
                    interactableInBox?.gameObject.SetActive(underRed > 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ResetObject() { 
            //Todo A modifier pour l'état en boite
            
            underRed = 0;
            underBlue = 0;
            shardsOnTop.Clear();
            
            baseObject!.SetRenderer(true);
            baseObject!.SetCollider(true);

            if (!objectInside) return;
            
            if(interactableInBox?.gameObject == null) Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
            interactableInBox?.gameObject.SetActive(false);
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
