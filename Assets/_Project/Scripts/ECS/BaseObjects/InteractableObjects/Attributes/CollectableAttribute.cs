using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class CollectableAttribute : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        private Transform originalParent;
        private Vector3 originalPosition;
        private Vector3 originalScale;
        
        private Vector3 boundExtent;
        private Vector3 boundCenter;
        
        [Header("Edges")] 
        [SerializeField] public Transform leftEdge;
        
        [Header("items")]
        public Sprite itemSprite;

        [Header("Particles")]
        [SerializeField] private ParticleSystem particles;
        
        [Header("Key Settings")]
        [SerializeField] private bool isAKey;
        [SerializeField] public bool isOneTimeUse;
        [SerializeField] public int keyID;

        private bool keyHasBeenUse;
        
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
                
                originalPosition = transform.position;
                originalScale = transform.localScale;
                
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
                    // if (baseObject.CanBeInteractedWith())
                        OnPickedUp();
                    // else
                    //     Debug.LogWarning("[Collectable] Can't grab object !");
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
            
            PlayerController.Instance.Interact.SetDropObject(false);
            baseObject.GetGlassInteract?.ResetObject();
            
            if(baseObject.HasSceneElement())
                baseObject.TriggerSceneElement();
            
            Debug.Log("[Collectable] Reset object");
        }

        private void OnPickedUp() {
            SetInInventory();
            PlayerController.Instance.Interact.pickUpObjectYPos = baseObject.GetRendered().bounds.center.y;

            GameInitializer.Instance.PlaySound3D(
                isAKey
                    ? GameInitializer.Instance.GetBank().avatar_Taking_Key
                    : GameInitializer.Instance.GetBank().avatar_Taking_Object, transform.position);
        }

        private void HoldObject() {
            isHeld = true;
            
            var attachPoint = PlayerController.Instance.Interact.objectPos;

            transform.SetParent(attachPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            Physics.SyncTransforms();
            
            var bounds = GetCombineBounds();
            var heightOffset = new Vector3(0, -bounds.extents.y, 0);
            if(bounds.extents.y > 0.5f) heightOffset -= new Vector3(0, 0.5f, 0);
            
            var depthOffset = new Vector3(0, 0, bounds.extents.z);
            var targetLocalPos = heightOffset + depthOffset;
            
            transform.localPosition = targetLocalPos;
            transform.localScale = Vector3.zero;
            
            baseObject.gameObject.SetActive(true);
            transform.DOScale(originalScale, 0.5f).OnComplete(() => transform.localScale = originalScale);
            
            PlayerController.Instance.Interact.HoldObject(true, GetBaseObject());
        }
        
        private Bounds GetCombineBounds() {
            var render = GetComponent<MeshFilter>();
            if(render != null)
                return render.sharedMesh.bounds;
            
            return new Bounds(Vector3.zero, Vector3.one);
        }
        
        private void StopHolding() {
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => baseObject.gameObject.SetActive(false));
            
            isHeld = false;
            PlayerController.Instance.Interact.HoldObject(false);
        }

        private void OnDrop(IInteractable other) {
            if (other == null) {
                
                if(ObstructedSpace()) {
                    Debug.Log("Space is Obstructed");
                    PlayerController.Instance.Interact.triggerFailedDrop = true;
                    return;
                }

                var pos = GetGroundPos();
                PlayerController.Instance.Interact.pickUpObjectYPos = pos.y + baseObject.GetRendered().bounds.extents.y;
                transform.SetParent(originalParent);
                TweenObjectDrop(pos, transform.eulerAngles);
                transform.localScale = Vector3.one;
                
                baseObject.SetInteract(true);
                colTimer.Start();
                
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().avatar_Drops_Object, transform.position);
            }
            
            if (isInInventory) {
                isInInventory = false;
                PlayerController.Instance.Inventory.OnItemDropped(baseObject);
            }
            
            isHeld = false;
            PlayerController.Instance.Interact.SetDropObject(false);
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
                baseObject.SetInteract(true);
                
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().avatar_Drops_Object, transform.position);
            }
            
            if (isInInventory) {
                isInInventory = false;
                PlayerController.Instance.Inventory.OnItemDropped(baseObject);
            }
            
            isHeld = false;
            PlayerController.Instance.Interact.SetDropObject(false);
        }

        public void SetInInventory() {
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => baseObject.gameObject.SetActive(false));
            
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
            
            isHeld = false;
            isInInventory = true;
            
            if(baseObject.HasSceneElement())
                baseObject.UnValidSceneElement();
            
            if(isAKey)
                PlayerController.Instance.Inventory.OnKeyPickUp(this);
            else
                PlayerController.Instance.Inventory.OnItemPickedUp(this);
        }

        public void SetHasBeenUse() {
            keyHasBeenUse = true;
        }

        public bool KeyHasBeenUsed() {
            return keyHasBeenUse;
        }
        
        #region OtherMethods
        
        private void TweenObjectDrop(Vector3 pos, Vector3 rot) {
            tween.Kill();
            tween = transform.DOMove(pos, 0.5f).OnComplete(IsColliding);
            tween = transform.DORotate(new Vector3(0,rot.y,0), 0.5f);
            tween.onComplete += TriggerSceneElement;
        }

        private void TriggerSceneElement() {
            if(baseObject.HasSceneElement())
                baseObject.TriggerSceneElement();
        }
        
        private void ActiveCollision() {
            baseObject.SetCollider(true);
        }

        private bool ObstructedSpace() {
            var playerPos = PlayerController.Instance.transform.position + new Vector3(0,1,0);
            var dir = PlayerController.Instance.Movement.mesh.forward;

            var maxDistance = 0.8f + boundCenter.z * 2f;
            
            Physics.Raycast(playerPos, dir,  out var hit, maxDistance);
            return hit.collider;
        }
        
        private Vector3 GetGroundPos() {
            var playerPos = PlayerController.Instance.transform.position + new Vector3(0,1,0);
            var dir = PlayerController.Instance.Movement.mesh.forward;

            var ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
            var mask = ~(1 << ignoreLayer);
            
            Physics.Raycast(playerPos + dir, Vector3.down, out var groundLevel, 3, mask);

            var dist = boundExtent.z * 2 + 0.4f;
            
            if(dist < 1.4f) dist = 1.4f;
            
            var pos = playerPos + dir.normalized * dist;
            pos.y = groundLevel.point.y;
            return pos;
        }

        private static readonly Collider[] Hits = new Collider[16];
        
        private void IsColliding() {
            var myCol = baseObject.GetCollider();
            if (!myCol || !myCol.enabled) return;

            var mask = LayerMask.GetMask(
                "Interactable",
                "InteractableNoLUT",
                "Wall",
                "Walkable",
                "Default"
            );
            
            
            var yPos = transform.position.y;
            var toPlayer = (PlayerController.Instance.transform.position - transform.position).normalized;
            toPlayer.y = 0;

            var resolvedPosition = transform.position;

            const int maxIteration = 10;

            for (var iteration = 0; iteration < maxIteration; iteration++) {
                var boundsCenter = resolvedPosition + (myCol.bounds.center - myCol.transform.position);
                
                var count = Physics.OverlapBoxNonAlloc(
                   boundsCenter,
                    myCol.bounds.extents,
                    Hits,
                    myCol.transform.rotation,
                    mask,
                    QueryTriggerInteraction.Ignore
                );
                
                var hadOverlap = false;
            
                for (var i = 0; i < count; i++)
                {
                    var other = Hits[i];
                    if (!other || other == myCol) continue;

                    if (Physics.ComputePenetration(
                            myCol, myCol.transform.position, myCol.transform.rotation,
                            other, other.transform.position, other.transform.rotation,
                            out Vector3 dir, out var distance)) {
                        hadOverlap = true;
                    
                        var correction = dir * (distance + 0.001f);
                        correction.y = 0f;
                    
                        var dot = Vector3.Dot(correction.normalized, toPlayer);
                        if (dot > 0f) {
                            resolvedPosition += Vector3.Project(correction, toPlayer);
                        }
                        else {
                            resolvedPosition -= correction * 0.05f;
                        }
                    }
                }
                
                if(!hadOverlap) break;
            }

            resolvedPosition.y = yPos;
            transform.DOMove(resolvedPosition, 0.1f);
        }

        public bool CanBeGrab() {
            return canBeGrab;
        }

        public bool IsHeld() {
            return isHeld;
        }

        public bool IsInInventory() {
            return isInInventory;
        }
        
        public bool IsKey() {
            return isAKey;
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