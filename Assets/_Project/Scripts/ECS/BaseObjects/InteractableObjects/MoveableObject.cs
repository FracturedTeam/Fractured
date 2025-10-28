using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class MoveableObject : MonoBehaviour, IInteractable, IMoveable {
        private BaseObject baseObject;
        private Transform originalParent;
        
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        
        [Header("Object Location Droppable")]
        [Tooltip("The object location where he is initially at the start")]
        [SerializeField] DropInteractableObject startLocation;
        [Tooltip("The object location where he must be put to resolve the puzzle")]
        [SerializeField] DropInteractableObject resolveLocation;
        
        private bool canBeGrab = false;
        private bool isGrabbed = false;
        
        protected void Start() {
            Initialize();
        }

        public void Initialize() {
            if(TryGetComponent(typeof(BaseObject), out var component))
                baseObject = component as BaseObject;
            else 
                Debug.LogError($"[MoveableObject] {gameObject.name} does not have a BaseObject !");
            baseObject?.SetInteract(true);
            
            if(startLocation == null)
                Debug.LogWarning("[MoveableObject] StartLocation is null");
            if(resolveLocation == null)
                Debug.LogWarning("[MoveableObject] ResolveLocation is null");
            
            SetPositionOnStart();
            
            originalParent = transform.parent;
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            canBeGrab = true;
            
            startLocation?.Initialize();
            resolveLocation?.Initialize();
            
            //Set base location object
            startLocation?.GetBaseObject().SetInteract(false);
            startLocation?.GetBaseObject().SetCollider(false);
            startLocation?.SetResolveLocation(false);
            startLocation?.SetKeyObject(this);
            
            //Set resolve location object
            resolveLocation?.GetBaseObject().SetInteract(true);
            resolveLocation?.GetBaseObject().SetCollider(true);
            resolveLocation?.SetResolveLocation(true);
            resolveLocation?.SetKeyObject(this);
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
            baseObject.SetInteract(true);
            baseObject.SetCollider(true);
            
            isGrabbed = false;
            
            transform.SetParent(originalParent);
            transform.position = startLocation.transform.position;
            transform.rotation = startLocation.transform.rotation;
            
            startLocation?.GetBaseObject().SetInteract(false);
            startLocation?.GetBaseObject().SetCollider(false);
            
            Debug.Log("[MoveableObject] Reset object");
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }

        public void OnGrab(IInteractable other = null) {
            baseObject.SetInteract(false);
            baseObject.SetCollider(false);
            
            isGrabbed = true;
            
            transform.SetParent(PlayerController.Instance.transform);
            transform.localPosition = Vector3.zero + new Vector3(0, 2, 0);
            transform.localRotation = Quaternion.identity;
            
            startLocation.GetBaseObject().SetInteract(true);
            startLocation.GetBaseObject().SetCollider(true);
            
            Debug.Log("[MoveableObject] Grab object");
        }

        public void OnDrop(IInteractable other) {
            if (other == null) {
                Debug.LogError($"[MoveableObject] Other is null !");
                return;
            }

            if (!other.GetBaseObject().TryGetComponent(out DropInteractableObject otherDrop)) {
                Debug.LogError("[MoveableObject] Not a drop location !");
                return;
            }

            if (otherDrop == startLocation) { //Player can still intereact with the object
                transform.SetParent(originalParent);
                transform.position = startLocation.transform.position;
                transform.rotation = startLocation.transform.rotation;
                
                baseObject.SetInteract(true);
                baseObject.SetCollider(true);
                
                otherDrop.OnInteract(ObjectInteraction.Drop, this);
                
                Debug.Log("[MoveableObject] Drop object StartLocation");
            }
            else if (otherDrop == resolveLocation) { //Player can't interact anymore with the object
                transform.SetParent(originalParent);
                transform.position = resolveLocation.transform.position;
                transform.rotation = resolveLocation.transform.rotation;
                
                baseObject.SetInteract(false);
                baseObject.SetCollider(false);
                
                otherDrop.OnInteract(ObjectInteraction.Drop, this);
                
                startLocation.SetInteract(false);
                
                Debug.Log("[MoveableObject] Drop object ResolveLocation");
            }
            else {
                Debug.LogWarning("[MoveableObject] Drop object Location is not the intended one");
                return;
            }
            
            isGrabbed = false;
            PlayerController.Instance.interact.SetDropObject();
        }

        private void SetPositionOnStart() {
            transform.position = startLocation.transform.position;
            transform.rotation = startLocation.transform.rotation;
        }
        
        public bool CanBeGrab() {
            return canBeGrab;
        }

        public bool IsGrabbed() {
            return isGrabbed;
        }
    }
}