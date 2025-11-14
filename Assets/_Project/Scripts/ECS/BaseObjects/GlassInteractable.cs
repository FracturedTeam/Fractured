using System;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.HashSetUtil;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects
{
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
        [SerializeField] internal Vector2 pos2D;
        [SerializeField] private float radius2D;
        [SerializeField] private bool showColliders;

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
        
        public void Tick(float deltaTime) { //Bien de voir pour dégager les updates - Pour le moment elle n'est pas couteuse donc c'est fine
            pos2D = mainCamera!.WorldToScreenPoint(transform.position);

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
                    SetVisibility(underRed < 1);
                    break;
                case ColorEnum.Blue:
                    SetVisibility(underBlue < 1);
                    break;
                default:
                    Debug.LogWarning($"[GlassInteractable] Unsupported color set : {gameObject.name}");
                    break;
            }
        }
        
        private void SetVisibility(bool isUnder) {
            baseObject.SetRenderer(isUnder);
            baseObject.SetCollider(isUnder);
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
