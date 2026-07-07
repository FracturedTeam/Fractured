using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Structs;
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
        [SerializeField] internal Dialogue specialDialogue;
        
        [Header("Particles")]
        [SerializeField] private ParticleSystem particles;
        
        [Header("Dissolve")]
        [SerializeField] private MeshRenderer dissolve;
        
        private bool canBeGrab = false;
        private bool isGrabbed = false;

        private PressurePlate pressurePlateOn;
        private Tweener tween;
        private Tweener matTween;
        private CountdownTimer colTimer = null;
        
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(out BaseObject component)) baseObject = component;
                else Debug.LogError($"[MoveableObject] Cannot find {nameof(BaseObject)} in {nameof(MoveableObject)}");
                
                originalPosition = transform.position;
                
                baseObject.GetObjectType = ObjectType.Moveable;
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

            if (PlayerController.Instance.interact.GetCurrentInteractable().GetInteract as MoveableObject == this && !isGrabbed) {
                OnGrab();
            }
        }

        public void Dispose() {
            tween?.Kill();
            matTween?.Kill();
        }

        public void CompleteObject() {
            if (!keyObjectNeeded) 
                return;
            
            if (keyObjectNeeded.keyObjectPos != null) {
                transform.SetParent(keyObjectNeeded.keyObjectPos);
                transform.position = keyObjectNeeded.keyObjectPos.position;
                transform.rotation = keyObjectNeeded.keyObjectPos.rotation;
            }
            else {
                transform.SetParent(keyObjectNeeded.transform);
                transform.position = keyObjectNeeded.transform.position;
            }
                    
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
                    
            keyObjectNeeded.OnInteract(ObjectInteraction.Drop, this);
                
            if(particles) particles.Stop();
            if(dissolve) dissolve.material.SetFloat("_Progression", 1f);
            Debug.LogWarning("[MoveableObject] Complete object");
        }

        public void ResetObject() {
            baseObject.GetCompletion = keyObjectNeeded ? InteractionCompletion.NotCompleted : InteractionCompletion.None;
            
            tween?.Pause();
            tween?.Kill();
            
            colTimer.Pause();
            
            pressurePlateOn = null;
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
            baseObject.SetRenderer(true);
            
            isGrabbed = true;
            
            transform.SetParent(PlayerController.Instance.interact.objectPos);
            TweenObjectOnPlayer();

            //Call audio
            if(keyObjectNeeded == null)
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().pickUpObjectSound, transform.position);
            else
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().pickUpKeySound, transform.position);
            
            var dialogue = baseObject.GetGlassInteract && baseObject.GetGlassInteract.objectOut
                ? specialDialogue
                : baseObject.successDialogue;
            
            if (dialogue is not{ oneTime: true, alreadyInteracted: true }) {
                HudManager.Instance.SetText(dialogue.dialogue);
                dialogue.alreadyInteracted = true;
            }
            
            Debug.Log("[MoveableObject] Grab object");
        }

        public void OnDrop(IInteractable other) {
            if (other == null) {
                
                if(ObstructedSpace())
                {
                    PlayerController.Instance.interact.triggerFailedDrop = true;
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
                transform.localScale = Vector3.one;
                IsColliding();
                
                baseObject.SetInteract(true);
                colTimer.Start();
                
                GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().dropObjectSound, transform.position);
                
                if (baseObject.failedDialogue is not{ oneTime: true, alreadyInteracted: true })
                {
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.failedDialogue.alreadyInteracted = true;
                }
                
                Debug.Log("[MoveableObject] Drop on ground");
            }
            else {
                if (other.GetBaseObject().GetInteract as PressurePlate) {
                    var p = other.GetBaseObject().GetInteract as PressurePlate;
                    if (p.objectPosition == null) {
                        transform.SetParent(originalParent);
                        TweenObjectDrop(p.GetBaseObject().transform);
                    }
                    else {
                        transform.SetParent(p.transform);
                        TweenObjectDrop(p.objectPosition);
                    }
                    
                    baseObject.SetInteract(false);
                    baseObject.SetCollider(false);
                    
                    GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().dropObjectSound, transform.position);
                    
                    isGrabbed = false;
                    PlayerController.Instance.interact.SetDropObject();
                    
                    Debug.Log("[MoveableObject] Pressure Plate Location");
                    return;
                }
                
                if (!other.GetBaseObject().TryGetComponent(out KeyInteractable keyObject)) {
                    Debug.LogError("[MoveableObject] Not a key location !");
                    return;
                }

                if (keyObject == keyObjectNeeded) {
                    if (keyObject.keyObjectPos == null) {
                        transform.SetParent(originalParent);
                        TweenObjectDrop(keyObjectNeeded.transform);
                    }
                    else {
                        transform.SetParent(keyObject.keyObjectPos);
                        TweenObjectDrop(keyObject.keyObjectPos);
                    }
                    
                    baseObject.SetInteract(false);
                    baseObject.SetCollider(false);
                    
                    //Ici pour mettre l'objet sur le bon endroit
                    keyObject.OnInteract(ObjectInteraction.Drop, this);
                    baseObject.GetCompletion = InteractionCompletion.Completed;
                    
                    GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().dropObjectSound, transform.position);
                    if(particles) particles.Stop();
                    if(dissolve) matTween = dissolve.material.DOFloat(1f, "_Progression", 1f).SetEase(Ease.InQuad);
                    
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
                
                if (baseObject.failedDialogue is not{ oneTime: true, alreadyInteracted: true })
                {
                    HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                    baseObject.failedDialogue.alreadyInteracted = true;
                }
                
                Debug.Log("[MoveableObject] Drop on ground");
            }
            
            isGrabbed = false;
            PlayerController.Instance.interact.SetDropObject();
        }

        public void SetPressurePlateOn(PressurePlate plate) => pressurePlateOn = plate;
        public PressurePlate GetPressurePlateOn() => pressurePlateOn;
        
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
                Debug.Log("[MoveableObject] Something in the way");
                
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
        #endregion
    }
}