using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class MoveableObject : MonoBehaviour, IInteractable, IMoveable {
        private BaseObject baseObject;
        private Transform originalParent;
        private Vector3 originalPosition;
        
        private Vector3 boundExtent;
        private Vector3 boundCenter;
        
        [Header("Key Settings")]
        [Tooltip("The object location where he must be put to resolve the puzzle")]
        [SerializeField] private KeyInteractable keyObjectNeeded;
        [Tooltip("Set the object type, will be used for knowing what object it is for the UI or other thing")]
        [SerializeField] private MoveableType moveableType;
        
        private bool canBeGrab = false;
        private bool isGrabbed = false;
        
        private Tweener tween;
        private CountdownTimer colTimer = null;
        
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else Debug.LogError($"[MoveableObject] Cannot find {nameof(BaseObject)} in {nameof(MoveableObject)}");
                
                originalPosition = transform.position;
                
                baseObject.GetInteractionType = ObjectType.Moveable;
                baseObject.GetCompletion = keyObjectNeeded ? InteractionCompletion.NotCompleted : InteractionCompletion.None;
                
                baseObject?.SetInteract(true);
                
                if(keyObjectNeeded == null)
                    Debug.LogWarning("[MoveableObject] ResolveLocation is null");

                colTimer = new CountdownTimer(0.5f);
                colTimer.OnTimerStop += ActiveCollision;
                
                keyObjectNeeded?.Initialize();
            
                //Set resolve location object
                keyObjectNeeded?.GetBaseObject().SetInteract(true);
                keyObjectNeeded?.GetBaseObject().SetCollider(true);
                keyObjectNeeded?.SetKeyObject(GetBaseObject());
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

        public void Tick(float deltaTime) {
            IsColliding();
            
            if (!baseObject.GetGlass) return;

            if (!isGrabbed || !baseObject.GetGlassInteract.UnderGlass()) return;
            DropUnderShard();
        }

        public void CompleteObject() {
            if (keyObjectNeeded) {
                transform.SetParent(originalParent);
                TweenObjectDrop(keyObjectNeeded.transform);
                    
                baseObject.SetInteract(false);
                baseObject.SetCollider(false);
                    
                keyObjectNeeded.OnInteract(ObjectInteraction.Drop, this);
            }
        }
        
        private void DropUnderShard() {
            tween?.Pause();
            tween?.Kill();
            DOTween.Kill(transform);
            
            colTimer.Pause();
            
            baseObject.SetInteract(true);
            baseObject.SetCollider(true);
            
            isGrabbed = false;
            
            transform.SetParent(originalParent);
            var pos = GetGroundPos();
            transform.position = pos;
            
            PlayerController.Instance.interact.SetDropObject();
            baseObject.GetGlassInteract?.ResetObjectUnderShard();
            
            Debug.Log("[MoveableObject] Drop under shard");
        }

        public void ResetObject() {
            baseObject.GetCompletion = keyObjectNeeded ? InteractionCompletion.NotCompleted : InteractionCompletion.None;
            
            tween?.Pause();
            tween?.Kill();
            DOTween.Kill(transform);
            
            colTimer.Pause();
            
            baseObject.SetInteract(true);
            baseObject.SetCollider(true);
            
            isGrabbed = false;
            
            transform.SetParent(originalParent);
            transform.position = originalPosition;
            
            PlayerController.Instance.interact.SetDropObject();
            baseObject.GetGlassInteract?.ResetObject();
            
            Debug.Log("[MoveableObject] Reset object");
        }

        public void OnGrab(IInteractable other = null) {
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
            
            
            isGrabbed = true;
            
            transform.SetParent(PlayerController.Instance.transform);
            TweenObjectOnPlayer();
            
            if (baseObject.successDialogue is not{ oneTime: true, alreadyInteracted: true })
            {
                HudManager.Instance.SetText(baseObject.successDialogue.dialogue);
                baseObject.successDialogue.alreadyInteracted = true;
            }
            
            Debug.Log("[MoveableObject] Grab object");
        }

        public void OnDrop(IInteractable other) {
            if (other == null) {
                
                if(ObstructedSpace())
                {
                    if (baseObject.cantInteractDialogue is not{ oneTime: true, alreadyInteracted: true })
                    {
                        HudManager.Instance.SetText(baseObject.cantInteractDialogue.dialogue);
                        baseObject.cantInteractDialogue.alreadyInteracted = true;
                    }
                    return;
                }

                var pos = GetGroundPos();
                
                transform.SetParent(originalParent);
                TweenObjectDrop(pos, transform.eulerAngles);
                
                baseObject.SetInteract(true);
                colTimer.Start();
                
                if (baseObject.failedDialogue is not{ oneTime: true, alreadyInteracted: true })
                {
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.failedDialogue.alreadyInteracted = true;
                }
                
                Debug.Log("[MoveableObject] Drop on ground");
            }
            else {
                if (!other.GetBaseObject().TryGetComponent(out KeyInteractable keyObject)) {
                    Debug.LogError("[MoveableObject] Not a key location !");
                    return;
                }

                if (keyObject == keyObjectNeeded) {
                    transform.SetParent(originalParent);
                    TweenObjectDrop(keyObjectNeeded.transform);
                    
                    baseObject.SetInteract(false);
                    baseObject.SetCollider(false);
                    
                    keyObject.OnInteract(ObjectInteraction.Drop, this);
                    baseObject.GetCompletion = InteractionCompletion.Completed;
                    Debug.Log("[MoveableObject] key location");
                }
                else {
                    Debug.Log("[MoveableObject] key is not for this object");
                     
                    if (baseObject.failedDialogue is { oneTime: true, alreadyInteracted: true })
                        return;
                    
                    HudManager.Instance.SetText( other.GetBaseObject().failedDialogue.dialogue);
                    other.GetBaseObject().failedDialogue.alreadyInteracted = true;
                    
                    return;
                }
            }
            
            isGrabbed = false;
            PlayerController.Instance.interact.SetDropObject();
        }

        private void TweenObjectOnPlayer() {
            tween.Kill();
            tween = transform.DOLocalMove(Vector3.zero + new Vector3(0, 2, 0), 0.5f);
            tween = transform.DOLocalRotate(Vector3.zero, 0.5f);
        }
        
        private void TweenObjectDrop(Transform t) {
            tween.Kill();
            TweenObjectDrop(t.position, t.eulerAngles);
        }
        
        private void TweenObjectDrop(Vector3 pos, Vector3 rot) {
            tween.Kill();
            tween = transform.DOMove(pos, 0.5f);
            tween = transform.DORotate(rot, 0.5f);
        }

        private void ActiveCollision() {
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

            var ignoreLayer = LayerMask.NameToLayer("ShardEditableArea");
            var mask = ~(1 << ignoreLayer);
            
            Physics.Raycast(playerPos + dir, Vector3.down, out var groundLevel, 3, mask); 
                
            var pos = playerPos + dir.normalized * (boundExtent.x * 3);
            pos.y = groundLevel.point.y + Mathf.Abs(boundExtent.y) - Mathf.Abs(boundCenter.y);
            
            return pos;
        }

        private static readonly Collider[] _hits = new Collider[16];
        
        private bool IsColliding() {
            /*var inObjects = new Collider[4];
            var layer = LayerMask.GetMask("Interactable", "InteractableNoLUT");

            if (!baseObject.GetCollider().enabled) return false;
            var size = Physics.OverlapBoxNonAlloc(transform.position, boundExtent, inObjects, transform.rotation, layer);

            if (size <= 0) return false;
            
            var dir = (inObjects[0].transform.position - transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * (boundExtent.magnitude * 2.5f);
            return true;*/
            var myCol = baseObject.GetCollider();
            if (!myCol || !myCol.enabled)
                return false;

            var mask = LayerMask.GetMask(
                "Interactable",
                "InteractableNoLUT",
                "Default",
                "Walkable"
            );

            var count = Physics.OverlapBoxNonAlloc(
                myCol.bounds.center,
                myCol.bounds.extents,
                _hits,
                myCol.transform.rotation,
                mask,
                QueryTriggerInteraction.Ignore
            );

            var resolved = false;

            for (var i = 0; i < count; i++)
            {
                var other = _hits[i];
                if (!other || other == myCol)
                    continue;

                if (Physics.ComputePenetration(
                        myCol, myCol.transform.position, myCol.transform.rotation,
                        other, other.transform.position, other.transform.rotation,
                        out Vector3 dir,
                        out float distance))
                {
                    // Push OUT of collision
                    transform.position += dir * (distance + 0.001f);
                    resolved = true;
                }
            }

            return resolved;
        }
        
		public MoveableType GetObjectType(){
            return moveableType;
        }

        public bool CanBeGrab() {
            return canBeGrab;
        }

        public bool IsGrabbed() {
            return isGrabbed;
        }
        
        public BaseObject GetBaseObject() {
            if (baseObject is not null) return baseObject;
            
            TryGetComponent(out baseObject);
            baseObject.Initialize();
            return baseObject;
        }
    }
}