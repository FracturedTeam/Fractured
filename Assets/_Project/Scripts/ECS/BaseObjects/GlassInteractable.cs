using System.Collections.Generic;
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
        
        [Header("Object Color")]
        public ColorEnum objectColor;

        [Header("Behaviour")] 
        [Tooltip("If true, when the object is under a shard, it will transit into a the object that is contain within")]
        [SerializeField] private bool objectInside = false;
        [SerializeField] private MoveableObject interactableInBox;
        
        [Header("Invisible Walls")]
        [SerializeField] private MeshRenderer[] wallRenderer;
        [SerializeField] private Material visibleWallMat;
        [SerializeField] private Material invisibleWallMat;
        
        [Header("Debug on UI")]
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;
        [SerializeField, Range(0 ,2)] private float scaleModificator = 1;
        
        internal Vector3[] BoundingBox;
        private MoveableObject selfMoveable;
        private FrequencyTimer updatePos = new (1.0f);
        private CountdownTimer setObjectInsideTimer = new (.1f);
        
        private int underRed;
        private int underBlue;
        
        private bool initialized = false;
        public bool objectOut { get; set; }
        
        public bool IsVisible { get; private set; }
        
        public  void Initialize() {
            mainCamera = PlayerController.Instance.cinemachineBrain.OutputCamera;
            
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component))
                    baseObject = component as BaseObject;
                else
                    Debug.LogError($"[GlassInteractable] BaseObject on {gameObject.name} could not be found !");

                if (TryGetComponent(out MoveableObject m))
                    selfMoveable = m;
                
                shardsOnTop = new ObservableHashSet<Glass>();
                shardsOnTop.onUpdate += UpdateShards;
                
                updatePos.OnTick += Set2DPoints;
                updatePos.Start();
                
                gameObject.layer = LayerMask.NameToLayer("InteractableNoLUT");
                
                if (objectColor is ColorEnum.Both) {
                    SetVisibility(false);
                    if (baseObject.locked && !MemoryManager.Instance.IsUnlockedMemory(baseObject.memoryId)) {
                        baseObject.SetRenderer(false);
                        for (var i = 0; i < transform.childCount; i++) {
                            transform.GetChild(i).gameObject.SetActive(false);
                        }

                        if(wallRenderer.Length > 0)
                            foreach (var wall in wallRenderer) {
                                wall.material = visibleWallMat;
                            }
                    }
                }
                
                underRed = 0;
                underBlue = 0;
                
                baseObject!.SetCollider(objectColor != ColorEnum.Both);
                
                if (objectInside) {
                    if (interactableInBox != null) {
                        SetObjectInside();
                    
                        //setObjectInsideTimer.OnTimerStop += SetObjectInside;
                        //setObjectInsideTimer.Start();
                    }
                    else
                        Debug.LogError($"[GlassInteractable] {nameof(GlassInteractable)} Does not have an object referenced");
                }
            }
            
            BoundingBox = new Vector3[4];
            for (int i = 0; i < BoundingBox.Length; i++) {
                BoundingBox[i] = new Vector3(0, 0, 0);
            }
            Set2DPoints();
            
            initialized = true;
        }

        void SetObjectInside() {
            SetInteractableInBox(false);
            interactableInBox.transform.position = transform.position;
        }

        internal void OnInteract(bool isUnder, Glass shard) {
            if(!baseObject) return;

            Set2DPoints();

            if (isUnder) 
                shardsOnTop.Add(shard);
            else if(shardsOnTop.Contains(shard))
                shardsOnTop.Remove(shard);
        }
        
        public void Tick(float deltaTime) { //Bien de voir pour dégager les updates - Pour le moment elle n'est pas couteuse donc c'est fine
            if (!objectInside) return;
            if (!objectOut) {
                if(objectColor == ColorEnum.Blue && underBlue <= 0 || objectColor == ColorEnum.Red && underRed <= 0)
                    ActivateObjectInside(false);
            }
            if (objectOut) return;
            
            interactableInBox.transform.position = transform.position; //C'est ça qui entre en conflit avec la save
            if (interactableInBox.IsGrabbed() && !objectOut) {
                objectOut = true;
                UpdateShards();
            }
        }

        public void CompleteObject() {
            if (objectInside && interactableInBox) {
                objectOut = true;
                SetInteractableInBox(true);
            }
        }
        
        void OnDestroy() {
            updatePos.OnTick -= Set2DPoints;
            updatePos.Stop();
            updatePos.Dispose();
        }

        private void UpdateShards() {
            if (baseObject.locked && !MemoryManager.Instance.IsUnlockedMemory(baseObject.memoryId)) {
                baseObject.SetRenderer(false);
                for (var i = 0; i < transform.childCount; i++) {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                return;
            }
            
            if (!baseObject.locked){
                if (!baseObject.GetRendered().enabled) {
                    baseObject.SetRenderer(true);
                    for (var i = 0; i < transform.childCount; i++) {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    
                    if(wallRenderer.Length > 0)
                        foreach (var wall in wallRenderer) {
                            wall.material = invisibleWallMat;
                        }
                }
            }
            
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
            if (objectColor == ColorEnum.Both) IsVisible = isUnder;
            else IsVisible = !isUnder;
            
            if (selfMoveable) {
                if (baseObject.IsOnPressurePlate()) {
                    if (IsVisible && objectInside) {
                        if (!objectOut) selfMoveable.GetPressurePlateOn().SetActivation(!isUnder);
                        else selfMoveable.GetPressurePlateOn().SetActivation(isUnder);
                    }
                    else {
                        selfMoveable.GetPressurePlateOn().SetActivation(isUnder);
                    }

                    baseObject.SetCollider(false);
                    baseObject.SetInteract(false);
                    selfMoveable.GetPressurePlateOn().GetBaseObject().SetInteract(isUnder);
                }
                else if (!selfMoveable.IsGrabbed()) {
                    baseObject.SetCollider(isUnder);
                    baseObject.SetInteract(isUnder);
                }else if (selfMoveable.IsGrabbed() && !objectInside && !isUnder) {
                    selfMoveable.OnInteract(ObjectInteraction.DropNoTimer);
                }
            }
            else {
                baseObject.SetCollider(isUnder);
                baseObject.SetInteract(isUnder);
            }
            
            if (objectInside && !objectOut)
                ActivateObjectInside(!isUnder);
            
            if(isUnder) AudioManager.Instance.PlayHideObjectSound(transform.position);
            else AudioManager.Instance.PlayRevealObjectSound(transform.position);
        }
        
        private void ActivateObjectInside(bool isUnder) {
            SetInteractableInBox(isUnder);

            if(!selfMoveable) return;
            if (!selfMoveable.IsGrabbed()) return;
            
            if(!InteractableInBoxActive()) return;
            
            selfMoveable.OnInteract(ObjectInteraction.DropNoTimer);
            PlayerController.Instance.interact.SetGrabObject(interactableInBox?.GetBaseObject());
            objectOut = true;
            Debug.Log("Activate Object Inside");
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
            if(!interactableInBox.GetBaseObject().initialized) interactableInBox.GetBaseObject().Initialize();
            
            interactableInBox?.GetBaseObject().SetInteract(revealed);
            interactableInBox?.GetBaseObject().SetCollider(revealed);
            interactableInBox?.GetBaseObject().SetRenderer(revealed);
        }

        bool InteractableInBoxActive() {
            return interactableInBox.GetBaseObject().GetCollider().enabled &&
                   interactableInBox.GetBaseObject().CanBeInteractedWith() &&
                   interactableInBox.GetBaseObject().GetRendered().enabled;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = objectColor switch {
                ColorEnum.Blue => Color.dodgerBlue,
                ColorEnum.Red => Color.crimson,
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
            var points = baseObject.meshFilter.sharedMesh.vertices;
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
