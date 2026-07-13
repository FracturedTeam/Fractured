using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI.Gameplay;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class CollectableAttribute : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        private KeyAttribute keyAttribute;
        private Transform originalParent;
        private Vector3 originalPosition;
        
        private Vector3 boundExtent;
        private Vector3 boundCenter;
        
        [Header("items")]
        public Sprite itemSprite;

        [Header("Particles")]
        [SerializeField] private ParticleSystem particles;
        
        private bool canBeGrab = false;
        private bool isHeld = false;
        private bool isInInventory = false;

        private Tweener tween;
        private CountdownTimer colTimer = null;
        
        private bool initialized = false;
        
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[Collectable] Cannot find {nameof(BaseObject)} in {nameof(CollectableAttribute)}");
                
                if(TryGetComponent(out KeyAttribute key)) keyAttribute = key;
                
                originalPosition = transform.position;
                
                baseObject.GetObjectType = ObjectType.Collectable;
                
                baseObject.SetInteract(true);
                
                colTimer = new CountdownTimer(0.5f);
                colTimer.OnTimerStop += ActiveCollision;
            }

            initialized = true;
            
            originalParent = transform.parent;
            if (!baseObject.GetCollider().enabled) {
                baseObject.SetCollider(true);
                boundExtent = baseObject.GetCollider().bounds.extents;
                boundCenter = baseObject.GetCollider().bounds.center - baseObject.transform.position;
                baseObject.SetCollider(false);
            }
            else {
                boundExtent = baseObject.GetCollider().bounds.extents;
                boundCenter = baseObject.GetCollider().bounds.center - baseObject.transform.position;
            }
            
            canBeGrab = true;
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            switch (interaction) {
                case ObjectInteraction.Grab:
                    if (baseObject.CanBeInteractedWith())
                        OnPickedUp();
                    else
                        Debug.LogWarning("[Collectable] Can't grab object !");
                    break;
                case ObjectInteraction.Held:
                    HoldObject();
                    break;
                case ObjectInteraction.StopHeld:
                    StopHolding();
                    break;
                case ObjectInteraction.Drop:
                    OnDrop(other);
                    break;
                case ObjectInteraction.DropNoTimer:
                    if (isHeld)
                        OnDropNoTimer(other);
                    else
                        Debug.Log("[Collectable] Cannot drop object !");
                    break;
                case ObjectInteraction.Reset:
                    ResetObject();
                    break;
                default:
                    Debug.LogWarning($"[Collectable] {interaction} Interaction is not supported");
                    break;
            }
        }

        public void Tick(float deltaTime) {
        }

        public void Dispose() {
            tween?.Kill();
        }

        public void CompleteObject() { //TODO à voir son state complete
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
            
            if(particles) particles.Stop();
            Debug.LogWarning("[Collectable] Complete object");
        }

        public void ResetObject() {
            tween?.Pause();
            tween?.Kill();
            
            colTimer.Pause();
            
            baseObject.SetInteract(true);
            baseObject.SetCollider(true);
            
            isHeld = false;
            
            transform.SetParent(originalParent);
            transform.position = originalPosition;
            
            PlayerController.Instance.interact.SetDropObject();
            baseObject.GetGlassInteract?.ResetObject();
            
            if(baseObject.HasSceneElement())
                baseObject.TriggerSceneElement();
            
            Debug.Log("[Collectable] Reset object");
        }

        private void OnPickedUp() {
            Debug.Log("[Collectable] Picked up object");
            baseObject.gameObject.SetActive(false);
            
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
            
            isHeld = false;
            isInInventory = true;
            
            if(keyAttribute)
                PlayerController.Instance.inventory.OnKeyPickUp(keyAttribute);
            else
                PlayerController.Instance.inventory.OnItemPickedUp(this);

            GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().pickUpKeySound, transform.position);
        }

        private void HoldObject() {
            isHeld = true;
            transform.SetParent(PlayerController.Instance.interact.objectPos);
            transform.localPosition = Vector3.zero;
            baseObject.gameObject.SetActive(true);
            
            PlayerController.Instance.interact.HoldObject(true, GetBaseObject());
        }
        
        private void StopHolding() {
            baseObject.gameObject.SetActive(false);
            isHeld = false;
            PlayerController.Instance.interact.HoldObject(false);
        }

        private void OnDrop(IInteractable other) {
            if (other == null) {
                
                if(ObstructedSpace()) {
                    Debug.Log("Space is Obstructed");
                    PlayerController.Instance.interact.triggerFailedDrop = true;
                    return;
                }

                var pos = GetGroundPos();
                
                transform.SetParent(originalParent);
                TweenObjectDrop(pos, transform.eulerAngles);
                transform.localScale = Vector3.one;
                IsColliding();
                
                baseObject.SetInteract(true);
                colTimer.Start();
                
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().dropObjectSound, transform.position);
                
                if(baseObject.HasSceneElement())
                    baseObject.TriggerSceneElement();
            }

            if (isInInventory) {
                isInInventory = false;
                PlayerController.Instance.inventory.OnItemDropped(baseObject);
            }
            
            isHeld = false;
            PlayerController.Instance.interact.SetDropObject();
        }
        
        private void OnDropNoTimer(IInteractable other) {
            if (other == null) {
                if(ObstructedSpace())
                {
                    ResetObject();
                    return;
                }
                
                transform.SetParent(originalParent);
                TweenObjectDrop(GetGroundPos(), transform.eulerAngles);
                IsColliding();
                baseObject.SetInteract(true);
                
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().dropObjectSound, transform.position);
                
                if(baseObject.HasSceneElement())
                    baseObject.TriggerSceneElement();
            }
            
            if (isInInventory) {
                isInInventory = false;
                PlayerController.Instance.inventory.OnItemDropped(baseObject);
            }
            
            isHeld = false;
            PlayerController.Instance.interact.SetDropObject();
        }
        
        #region OtherMethods
        private void TweenObjectOnPlayer() {
            tween.Kill();
            tween = transform.DOLocalMove(Vector3.zero, 0.5f);
            tween = transform.DOLocalRotate(Vector3.zero, 0.5f);
        }
        
        private void TweenObjectDrop(Transform t) {
            tween.Kill();
            TweenObjectDrop(t.position, t.eulerAngles);
        }
        
        private void TweenObjectDrop(Vector3 pos, Vector3 rot) {
            tween.Kill();
            tween = transform.DOMove(pos, 0.5f);
            tween = transform.DORotate(new Vector3(0,rot.y,0), 0.5f);
        }
        
        private void ActiveCollision() {
            baseObject.SetCollider(true);
        }

        private bool ObstructedSpace() {
            var playerPos = PlayerController.Instance.transform.position;
            var dir = PlayerController.Instance.movement.mesh.forward;
            
            Physics.Raycast(playerPos, dir,  out var hit, 2f);
            if (hit.collider) {
                return true;
            }
            return false;
        }
        
        private Vector3 GetGroundPos() {
            var playerPos = PlayerController.Instance.transform.position;
            var dir = PlayerController.Instance.movement.mesh.forward;

            var ignoreLayer = LayerMask.NameToLayer("ShardEditableArea");
            var mask = ~(1 << ignoreLayer);
            
            Physics.Raycast(playerPos + dir, Vector3.down, out var groundLevel, 3, mask); 
                
            var pos = playerPos + dir.normalized * (boundExtent.x * 3);
            pos.y = groundLevel.point.y + Mathf.Abs(boundExtent.y) - Mathf.Abs(boundCenter.y);
            
            var elementPos = new Vector3();
            if (baseObject.HasSceneElement() && baseObject.GetSceneElementPosition(pos, ref elementPos)) {
                return elementPos;
            }
            
            return pos;
        }

        private static readonly Collider[] Hits = new Collider[16];
        
        private void IsColliding() {
            var myCol = baseObject.GetCollider();
            if (!myCol || !myCol.enabled)
                return;

            var mask = LayerMask.GetMask(
                "Interactable",
                "InteractableNoLUT",
                "Wall",
                "Walkable"
            );

            var count = Physics.OverlapBoxNonAlloc(
                myCol.bounds.center,
                myCol.bounds.extents,
                Hits,
                myCol.transform.rotation,
                mask,
                QueryTriggerInteraction.Ignore
            );

            for (var i = 0; i < count; i++)
            {
                var other = Hits[i];
                if (!other || other == myCol)
                    continue;

                if (Physics.ComputePenetration(
                        myCol, myCol.transform.position, myCol.transform.rotation,
                        other, other.transform.position, other.transform.rotation,
                        out Vector3 dir,
                        out var distance)) {
                    transform.position += dir * (distance + 0.001f);
                }
            }
        }

        public bool CanBeGrab() {
            return canBeGrab;
        }

        public bool IsGrabbed() {
            return isHeld;
        }

        public KeyAttribute GetKey() {
            if(keyAttribute) return keyAttribute;

            return null;
        }
        
        public BaseObject GetBaseObject() {
            if (baseObject is not null) return baseObject;
            
            TryGetComponent(out baseObject);
            baseObject.Initialize();
            return baseObject;
        }
        #endregion
    }
}