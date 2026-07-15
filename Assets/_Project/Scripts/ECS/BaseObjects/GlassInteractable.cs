using System;
using System.Linq;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.HashSetUtil;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
    [RequireComponent(typeof(BaseObject))]
    public class GlassInteractable : MonoBehaviour {
        private float GetRadius => radius2D;
        
        private BaseObject baseObject;
        private Camera mainCamera;
        private ObservableHashSet<Glass> shardsOnTop;
        private MeshFilter meshFilter;
        
        [Header("Object Color")]
        public ColorEnum objectColor;
        public bool isOn = true;

        [Header("Behaviour")] 
        [Tooltip("If true, when the object is under a shard, it will transit into a the object that is contain within")]
        [SerializeField] private bool objectInside = false;
        [SerializeField] private MovableAttribute interactableInBox;
        
        [Header("Invisible Walls")]
        [SerializeField] private MeshRenderer[] wallRenderer;
        [SerializeField] private Material visibleWallMat;
        [SerializeField] private Material invisibleWallMat;
        
        [Header("Debug on UI")]
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        [SerializeField, Range(0 ,2)] private float scaleModificator = 1;
        
        internal Vector3[] BoundingBox;
        private MovableAttribute moveableComponent;
        private readonly FrequencyTimer updateShardVisual = new (1.0f);
        
        private int underRed;
        private int underBlue;
        
        private bool isInitialized;
        public bool objectOut { get; set; }
        
        public bool IsVisible { get; private set; }
        
        public  void Initialize() {
            mainCamera = PlayerController.Instance.cinemachineBrain.OutputCamera;
            
            if (!isInitialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[GlassInteractable] BaseObject on {gameObject.name} could not be found !");

                if(TryGetComponent(out MeshFilter mf)) meshFilter = mf;
                else Debug.LogWarning($"[BaseObject] {gameObject.name} does not contain MeshFilter component");

                if (baseObject.GetObjectType is ObjectType.Moveable) {
                    moveableComponent = baseObject.GetInteract as MovableAttribute;
                }
                
                baseObject.SetGlassInteract(true);
                
                shardsOnTop = new ObservableHashSet<Glass>();
                shardsOnTop.onUpdate += UpdateShards;
                
                updateShardVisual.OnTick += Set2DPoints;
                updateShardVisual.Start();
                
                gameObject.layer = LayerMask.NameToLayer("InteractableNoLUT");
                
                IsVisible = true;
                
                if (objectColor is ColorEnum.Both) {
                    SetVisibility(false);
                    
                    baseObject.SetRenderer(false);
                    for (var i = 0; i < transform.childCount; i++) {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                    
                    /*
                    if (baseObject.locked && !MemoryManager.Instance.IsUnlockedMemory(baseObject.memoryId)) {
                        if(wallRenderer.Length > 0)
                            foreach (var wall in wallRenderer) {
                                wall.material = visibleWallMat;
                            }
                    }
                    */
                }
                
                underRed = 0;
                underBlue = 0;
                
                baseObject.SetCollider(objectColor != ColorEnum.Both);
                
                if (objectInside) {
                    if (interactableInBox != null)
                        SetObjectInside();
                    else
                        Debug.LogError($"[GlassInteractable] {nameof(GlassInteractable)} Does not have an object referenced");
                }
                
                isInitialized = true;
            }
            
            BoundingBox = new Vector3[4];
            for (int i = 0; i < BoundingBox.Length; i++) {
                BoundingBox[i] = new Vector3(0, 0, 0);
            }
            Set2DPoints();
            
        }

        void SetObjectInside() {
            SetInteractableInBox(false);
            interactableInBox.transform.position = transform.position;
        }

        internal void OnShardUpdated(bool isUnder, Glass shard) {
            Set2DPoints();

            if (isUnder) 
                shardsOnTop.Add(shard);
            else if(shardsOnTop.Contains(shard))
                shardsOnTop.Remove(shard);
        }
        
        public void Tick(float deltaTime) {
            if (!objectInside || objectOut) return;
            
            if (interactableInBox.IsGrabbed() && !objectOut) {
                objectOut = true;
            }
        }

        public void CompleteObject() {
            if (objectInside && interactableInBox) {
                objectOut = true;
                SetInteractableInBox(true);
            }
        }
        
        void OnDestroy() {
            updateShardVisual.OnTick -= Set2DPoints;
            updateShardVisual.Stop();
            updateShardVisual.Dispose();

            // if (shardsOnTop == null) 
            //     return;
            
            shardsOnTop.onUpdate -= UpdateShards;
            shardsOnTop.Clear();
        }

        private void UpdateShards() {
            if (!isOn)
                return;
            
            if (!baseObject.GetRendered().enabled) {
                baseObject.SetRenderer(true);
                for (var i = 0; i < transform.childCount; i++) {
                    transform.GetChild(i).gameObject.SetActive(true);
                }

                if (wallRenderer.Length > 0) {
                    foreach (var wall in wallRenderer) {
                        wall.material = invisibleWallMat;
                    }
                }
            }
            
            underBlue = 0;
            underRed = 0;

            foreach (var shard in shardsOnTop.Items)
                switch (shard.GetColor) {
                    case ColorEnum.ColorA:
                        underBlue++;
                        break;
                    case ColorEnum.ColorB:
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
                case ColorEnum.ColorB:
                    SetVisibility(underRed < 1 || underBlue > 0);
                    break;
                case ColorEnum.ColorA:
                    SetVisibility(underBlue < 1 || underRed > 0);
                    break;
                default:
                    Debug.LogWarning($"[GlassInteractable] Unsupported color set : {gameObject.name}");
                    break;
            }
        }
        
        private void SetVisibility(bool isUnder) {
            if (objectColor == ColorEnum.Both) IsVisible = !isUnder;
            else IsVisible = isUnder;
            baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger?.OnHideReveal);
            
            if (baseObject.GetObjectType is ObjectType.Moveable) {
                if (!moveableComponent.IsGrabbed()) {
                    baseObject.SetCollider(isUnder);
                    baseObject.SetInteract(isUnder);
                }
                else if (moveableComponent.IsGrabbed() && !objectInside && !isUnder) {
                    moveableComponent.OnInteract(ObjectInteraction.DropNoTimer);
                }
            }
            else {
                baseObject.SetCollider(isUnder);
                baseObject.SetInteract(isUnder);
            }
            
            if (objectInside && !objectOut)
                ActivateObjectInside(!isUnder);
            
            if(isUnder) GameInitializer.Instance.PlayHideSound(transform.position);
            else GameInitializer.Instance.PlayRevealSound(transform.position);
            
            if(baseObject.HasSceneElement())
                baseObject.TriggerSceneElement();
        }
        
        private void ActivateObjectInside(bool isUnder) {
            interactableInBox.transform.position = transform.position;
            
            SetInteractableInBox(isUnder);

            if(!moveableComponent) return;
            if (!moveableComponent.IsGrabbed()) return;
            
            if(!IsInteractableInBoxActive()) return;
            
            moveableComponent.OnInteract(ObjectInteraction.DropNoTimer);
            interactableInBox?.GetBaseObject().OnInteract(ObjectInteraction.Grab);
            
            objectOut = true;
        }

        public void ResetObject() {
            underRed = 0;
            underBlue = 0;
            shardsOnTop.Clear();
            
            baseObject!.SetCollider(true);

            if (!objectInside) return;

            objectOut = false;
            SetInteractableInBox(false);
        }
        
        public void SetInteractableInBox(bool revealed) {
            if(interactableInBox == null) return;
            
            var inBoxObject = interactableInBox.GetBaseObject();
            if (!inBoxObject.IsInitialized) {
                inBoxObject.Initialize();
            }

            if (inBoxObject.GetLockState is LockedState.Locked) {
                inBoxObject.SetInteract(revealed);
            }
            
            inBoxObject.SetCollider(revealed);
            inBoxObject.SetRenderer(revealed);
        }

        bool IsInteractableInBoxActive() {
            var inBoxObject = interactableInBox.GetBaseObject();
            return inBoxObject.GetCollider().enabled &&
                   inBoxObject.CanBeInteractedWith() &&
                   inBoxObject.GetRendered().enabled;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = objectColor switch {
                ColorEnum.ColorA => Color.dodgerBlue,
                ColorEnum.ColorB => Color.crimson,
                _ => Color.darkOrchid
            };
            
            if(!mainCamera)
                return;
            
            foreach (var pos in BoundingBox) {
                Gizmos.DrawSphere(pos, 10);
            }
        }
        #endif
        
        ///Auto Setup the collision
        private void Set2DPoints() {
            var points = meshFilter.sharedMesh.vertices;
            var pointsHashSet = points.ToHashSet();
            
            var pMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var pMax = new Vector3(-float.MaxValue, -float.MaxValue, -float.MaxValue);

            foreach (var point in pointsHashSet) {
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

            var middle = new Vector3((pMin.x + pMax.x)/ 2, (pMin.y + pMax.y)/ 2);

            BoundingBox[0] = Vector3.LerpUnclamped(middle, new Vector3(pMin.x, pMin.y), scaleModificator );
            BoundingBox[1] = Vector3.LerpUnclamped(middle, new Vector3(pMin.x, pMax.y), scaleModificator );  
            BoundingBox[2] = Vector3.LerpUnclamped(middle, new Vector3(pMax.x, pMax.y), scaleModificator ); 
            BoundingBox[3] = Vector3.LerpUnclamped(middle, new Vector3(pMax.x, pMin.y), scaleModificator ); 
        }
    }
}
