using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.HashSetUtil;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    [RequireComponent(typeof(BaseObject))]
    public class GlassInteractable : MonoBehaviour
    {
        private float GetRadius => radius2D;
        
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
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        
        internal Vector3[] BoundingBox;
        private MoveableObject selfMoveable;
        
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

                if (TryGetComponent(out MoveableObject m))
                    selfMoveable = m;
                
                shardsOnTop = new ObservableHashSet<Glass>();
                shardsOnTop.onUpdate += UpdateShards;

                /*switch (objectColor) { //Faudra set up ça pour le mesh en enfant OU le mettre a la main
                    case ColorEnum.Blue:
                        gameObject.layer = LayerMask.NameToLayer("Fragment Color 01");
                        break;
                    case ColorEnum.Red:
                        gameObject.layer = LayerMask.NameToLayer("Fragment Color 02");
                        break;
                    case ColorEnum.Both:
                        gameObject.layer = LayerMask.NameToLayer("NoLUT");
                        break;
                    default:
                        Debug.LogWarning($"[GlassInteractable] Unknown color set : {gameObject.name}");
                        break;
                }*/
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
                    Debug.LogError($"[GlassInteractable] {nameof(GlassInteractable)} Does not have an object referenced");
            }
            
            BoundingBox = new Vector3[4];
            for (int i = 0; i < BoundingBox.Length; i++)
            {
                BoundingBox[i] = new Vector3(0, 0, 0);
            }
            SetUp();
        }

        void OnDisable() {
            shardsOnTop.onUpdate -= UpdateShards;
        }

        internal void OnInteract(bool isUnder, Glass shard) {
            if(!baseObject)
                return;

            SetUp();

            if (isUnder) 
                shardsOnTop.Add(shard);
            else if(shardsOnTop.Contains(shard))
                shardsOnTop.Remove(shard);
        }
        
        public void Tick(float deltaTime) { //Bien de voir pour dégager les updates - Pour le moment elle n'est pas couteuse donc c'est fine

            if (!objectInside) return;
            if (objectOut) return;
            interactableInBox.transform.position = transform.position;
            if (interactableInBox.IsGrabbed()) objectOut = true;
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

            switch (objectColor) {
                case ColorEnum.Both:
                    SetVisibility(underRed > 0 && underBlue > 0);
                    break;
                case ColorEnum.Red:
                    SetVisibility(underRed < 1 || underBlue > 0);
                    break;
                case ColorEnum.Blue:
                    SetVisibility(underBlue < 1 || underRed > 0);
                    break;
                default:
                    Debug.LogWarning($"[GlassInteractable] Unsupported color set : {gameObject.name}");
                    break;
            }
        }
        
        private void SetVisibility(bool isUnder) {
            baseObject.SetRenderer(isUnder);
            baseObject.SetCollider(isUnder);
            if (baseObject.TryGetComponent(out MoveableObject move) && !move.IsGrabbed()) 
                baseObject.SetInteract(isUnder);
            else
                baseObject.SetInteract(isUnder);
            
            if (objectInside && !objectOut) {
                ActivateObjectInside(!isUnder);
            }
        }
        
        private void ActivateObjectInside(bool isUnder) { 
            interactableInBox?.gameObject.SetActive(isUnder);

            if(!selfMoveable) return;
            if (!selfMoveable.IsGrabbed()) return;
            
            selfMoveable.OnInteract(ObjectInteraction.Drop);
            PlayerController.Instance.interact.SetGrabObject(interactableInBox?.GetBaseObject());
            objectOut = true;
        }

        public void ResetObject() {
            underRed = 0;
            underBlue = 0;
            shardsOnTop.Clear();
            
            baseObject!.SetRenderer(true);
            baseObject!.SetCollider(true);

            if (!objectInside || objectOut) return;
            
            if(interactableInBox?.gameObject == null) Debug.LogError($"[GlassInteractable] {gameObject.name} Does not have alternateObjectMesh");
            interactableInBox?.gameObject.SetActive(false);
        }

        public bool UnderGlass() {
            return objectColor switch {
                ColorEnum.Red => underRed > 0 && underBlue < 1,
                ColorEnum.Blue => underBlue > 0 && underRed < 1,
                ColorEnum.Both => underRed > 0 && underBlue > 0,
                _ => false
            };
        }

        ///Draw The Gizmos of the collider, only in Editor
        private void OnDrawGizmos() {
            Gizmos.color = objectColor switch {
                ColorEnum.Blue => Color.dodgerBlue,
                ColorEnum.Red => Color.crimson,
                _ => Color.darkOrchid
            };
            
            if(!mainCamera)
                return;
            
            foreach (var pos in BoundingBox)
            {
                Gizmos.DrawSphere(pos, 10);
            }
            
        }

        ///Auto Setup the collision
        private void SetUp() {
            var points = GetComponent<MeshFilter>().sharedMesh.vertices;
            HashSet<Vector3> pointsHashSet = points.ToHashSet();
            

            Vector3 pMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var pMax = new Vector3(-float.MaxValue, -float.MaxValue, -float.MaxValue);

            foreach (var point in pointsHashSet)
            {
                var current =  transform.TransformPoint(point);
                current = mainCamera.WorldToScreenPoint(current);
                
                if (pMin.x > current.x)
                    pMin.x = current.x;
                if (pMin.y > current.y)
                    pMin.y = current.y;
                if (pMax.x < current.x)
                    pMax.x = current.x;
                if (pMax.y < current.y)
                    pMax.y = current.y;
            } 
            BoundingBox[0] =  new Vector3(pMin.x, pMin.y);
            BoundingBox[1] =  new Vector3(pMin.x, pMax.y);
            BoundingBox[2] =  new Vector3(pMax.x, pMax.y);
            BoundingBox[3] =  new Vector3(pMax.x, pMin.y);
        }
    }
}
