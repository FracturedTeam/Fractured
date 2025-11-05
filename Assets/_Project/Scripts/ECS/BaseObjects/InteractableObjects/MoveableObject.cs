using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class MoveableObject : MonoBehaviour, IInteractable, IMoveable {
        private BaseObject baseObject;
        private Transform originalParent;
        
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private Vector3 boundExtent;
        
        [Header("Object Location Droppable")]
        [Tooltip("The object location where he must be put to resolve the puzzle")]
        [SerializeField] DropInteractableObject resolveLocation;
        
        private bool canBeGrab = false;
        private bool isGrabbed = false;
        
        private Tweener tweener;
        private CountdownTimer colTimer = null;
        
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component))
                    baseObject = component as BaseObject;
                else 
                    Debug.LogError($"[MoveableObject] {gameObject.name} does not have a BaseObject !");
                
                baseObject?.SetInteract(true);
                
                if(resolveLocation == null)
                    Debug.LogWarning("[MoveableObject] ResolveLocation is null");

                colTimer = new CountdownTimer(0.5f);
                colTimer.OnTimerStop += ActiveCollision;
            }

            initialized = true;
            
            resolveLocation?.Initialize();
            
            //Set resolve location object
            resolveLocation?.GetBaseObject().SetInteract(true);
            resolveLocation?.GetBaseObject().SetCollider(true);
            resolveLocation?.SetResolveLocation(true);
            resolveLocation?.SetKeyObject(this);
            
            originalParent = transform.parent;
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            
            boundExtent = baseObject.GetCollider().bounds.extents;
            
            canBeGrab = true;
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            switch (interaction) {
                //Grab case
                case ObjectInteraction.Grab:
                    if (baseObject.CanBeInteractedWith())
                        OnGrab();
                    else
                        Debug.LogWarning("[MoveableObject] Can't grab object !");
                    break;
                //Drop case
                case ObjectInteraction.Drop:
                    if (isGrabbed)
                        OnDrop(other);
                    else
                        Debug.Log("[MoveableObject] Cannot drop object !");
                    break;
                //Reset Object
                case ObjectInteraction.Reset:
                    ResetObject();
                    break;
                //Other case
                default:
                    Debug.LogWarning($"[MoveableObject] {interaction} Interaction is not supported");
                    break;
            }
        }

        public void ResetObject() {
            tweener?.Pause();
            tweener?.Kill();
            DOTween.Kill(transform);
            
            colTimer.Pause();
            
            baseObject.SetInteract(true);
            baseObject.SetCollider(true);
            
            isGrabbed = false;
            
            transform.SetParent(originalParent);
            var pos = GetGroundPos();
            transform.position = pos;
            /*transform.rotation = originalRotation;*/
            
            PlayerController.Instance.interact.SetDropObject();
            baseObject.GetGlassInteract.ResetObject();
            
            Debug.Log("[MoveableObject] Reset object");
        }

        public void OnGrab(IInteractable other = null) {
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
            
            isGrabbed = true;
            
            transform.SetParent(PlayerController.Instance.transform);
            TweenObjectOnPlayer();
            
            Debug.Log("[MoveableObject] Grab object");
        }

        public void OnDrop(IInteractable other) {
            if (other == null) {
                
                if(ObstructedSpace()) return;

                var pos = GetGroundPos();
                
                transform.SetParent(originalParent);
                TweenObjectDrop(pos, transform.eulerAngles);
                
                baseObject.SetInteract(true);
                colTimer.Start();
                //baseObject.SetCollider(true);
                
                Debug.Log("[MoveableObject] Drop on ground");
            }
            else {
                if (!other.GetBaseObject().TryGetComponent(out DropInteractableObject otherDrop)) {
                    Debug.LogError("[MoveableObject] Not a drop location !");
                    return;
                }

                if (otherDrop == resolveLocation) {
                    transform.SetParent(originalParent);
                    TweenObjectDrop(resolveLocation.transform);
                    
                    baseObject.SetInteract(false);
                    baseObject.SetCollider(false);
                    
                    otherDrop.OnInteract(ObjectInteraction.Drop, this);
                    
                    Debug.Log("[MoveableObject] Drop object ResolveLocation");
                }
            }
            
            isGrabbed = false;
            PlayerController.Instance.interact.SetDropObject();
        }

        private void Update() {
            if (!baseObject.GetGlass) return;
            
            if (isGrabbed && baseObject.GetGlassInteract.UnderGlass()) {
                Debug.Log("[MoveableObject] UnderGlass Reset");
                ResetObject();
            }
        }

        private void TweenObjectOnPlayer() {
            tweener.Kill();
            tweener = transform.DOLocalMove(Vector3.zero + new Vector3(0, 2, 0), 0.5f);
            tweener = transform.DOLocalRotate(Vector3.zero, 0.5f);
        }
        
        private void TweenObjectDrop(Transform t) {
            tweener.Kill();
            TweenObjectDrop(t.position, t.eulerAngles);
        }
        
        private void TweenObjectDrop(Vector3 pos, Vector3 rot) {
            tweener.Kill();
            tweener = transform.DOMove(pos, 0.5f);
            tweener = transform.DORotate(rot, 0.5f);
        }

        public void ActiveCollision() {
            baseObject.SetCollider(true);
        }

        private bool ObstructedSpace() {
            var playerPos = PlayerController.Instance.transform.position;
            var dir = PlayerController.Instance.movement.previousMoveDir;
            
            Physics.Raycast(playerPos, dir,  out var hit, 2f);
            if (hit.collider) {
                Debug.Log("[MoveableObject] Something in the way");
                return true;
            }
            return false;
        }
        
        private Vector3 GetGroundPos() {
            var playerPos = PlayerController.Instance.transform.position;
            var dir = PlayerController.Instance.movement.previousMoveDir;

            Physics.Raycast(playerPos, Vector3.down, out var groundLevel);
                
            var pos = playerPos + dir.normalized * (boundExtent.x * 3);
            pos.y = groundLevel.point.y + boundExtent.y;
            
            return pos;
        }
        
        public bool CanBeGrab() {
            return canBeGrab;
        }

        public bool IsGrabbed() {
            return isGrabbed;
        }
        
        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}