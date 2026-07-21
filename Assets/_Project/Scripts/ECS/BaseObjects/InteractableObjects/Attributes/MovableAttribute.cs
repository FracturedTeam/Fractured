using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class MovableAttribute : MonoBehaviour, IInteractable, IMoveable {
        private BaseObject baseObject;
        private Transform originalParent;
        private Vector3 originalPosition;
        
        private Vector3 boundExtent;
        private Vector3 boundCenter;
        
        [Header("Particles")]
        [SerializeField] private ParticleSystem particles;
        
        [Header("Dissolve")]
        [SerializeField] private MeshRenderer dissolve;
        
        private bool canBeGrab = false;
        private bool isGrabbed = false;

        private Tweener tween;
        private Tweener matTween;
        private CountdownTimer colTimer = null;
        
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[MoveableObject] Cannot find {nameof(BaseObject)} in {nameof(MovableAttribute)}");
                
                originalPosition = transform.position;
                
                baseObject.GetObjectType = ObjectType.Moveable;
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
                //Grab case
                case ObjectInteraction.Grab:
                    if (baseObject.CanBeInteractedWith())
                        OnGrab();
                    else
                        Debug.LogWarning("[MoveableObject] Can't grab object !");
                    break;
                //Drop case
                case ObjectInteraction.Drop:
                    //if (isGrabbed)
                        OnDrop(other);
                    /*else
                        Debug.Log("[MoveableObject] Cannot drop object !");*/
                    break;
                case ObjectInteraction.DropNoTimer:
                    if (isGrabbed)
                        OnDropNoTimer(other);
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
            if (!PlayerController.HasInstance || !PlayerController.Instance.interact || PlayerController.Instance.interact.GetCurrentInteractable() == null) 
                return;  //C'est infame mais je sais pas ce qui cause une null ref

            if (PlayerController.Instance.interact.GetCurrentInteractable().GetInteract as MovableAttribute == this && !isGrabbed) {
                OnGrab();
            }
        }

        public void Dispose() {
            tween?.Kill();
            matTween?.Kill();
        }

        public void CompleteObject() {
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
                
            if(particles) particles.Stop();
            if(dissolve) dissolve.material.SetFloat("_Progression", 1f);
        }

        public void ResetObject() {
            tween?.Pause();
            tween?.Kill();
            
            colTimer.Pause();
            
            baseObject.SetInteract(true);
            baseObject.SetCollider(true);
            
            isGrabbed = false;
            
            transform.SetParent(originalParent);
            transform.position = originalPosition;
            
            PlayerController.Instance.interact.SetDropObject();
            baseObject.GetGlassInteract?.ResetObject();
            
            if(baseObject.HasSceneElement())
                baseObject.TriggerSceneElement();
            
            Debug.Log("[MoveableObject] Reset object");
        }

        public void OnGrab(IInteractable other = null) {
            PlayerController.Instance.interact.SetGrabbedObject(baseObject);
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
            baseObject.SetRenderer(true);
            
            isGrabbed = true;
            
            if(baseObject.HasSceneElement())
                baseObject.UnValidSceneElement();
            
            transform.SetParent(PlayerController.Instance.interact.objectPos);
            TweenObjectOnPlayer();

            //Call audio
            GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().pickUpKeySound, transform.position);
        }

        public void OnDrop(IInteractable other) {
            if (other == null) {
                
                if(ObstructedSpace()) {
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
                
            }
            
            isGrabbed = false;
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
            }
            
            isGrabbed = false;
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
            var dir = PlayerController.Instance.movement.mesh.forward;

            var maxDistance = 0.8f + boundCenter.z * 2f;
            
            Physics.Raycast(playerPos, dir,  out var hit, maxDistance);
            return hit.collider;
        }
        
        private Vector3 GetGroundPos() {
            var playerPos = PlayerController.Instance.transform.position + new Vector3(0,1,0);
            var dir = PlayerController.Instance.movement.mesh.forward;

            var ignoreLayer = LayerMask.NameToLayer("ShardEditableArea");
            var mask = ~(1 << ignoreLayer);
            
            Physics.Raycast(playerPos + dir, Vector3.down, out var groundLevel, 3, mask); 
                
            var pos = playerPos + dir.normalized * (boundExtent.z * 2 + 0.4f);
            //pos.y = groundLevel.point.y + Mathf.Abs(boundExtent.y) - Mathf.Abs(boundCenter.y);
            pos.y = groundLevel.point.y;
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
            return isGrabbed;
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