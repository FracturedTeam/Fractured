using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.GameServices.Services {
    public class ShardService : IGameSystem {
        public List<BaseObject> interactables { get; private set; }
        public List<Glass> shards {get; private set;}
        private Glass AShard;
        private Glass BShard;
        
        private Glass currentGlass;
        private Glass onTopGlass;
        
        private readonly List<BaseObject> shardsInteractable = new List<BaseObject>();

        public bool PlayerInEditableArea {get; private set;}
        public bool PlayerInRedEditableArea {get; private set;}
        public bool PlayerInBlueEditableArea {get; private set;}
        
        public void Initialize() { //Initialize the service
            interactables = new List<BaseObject>();
            shards = new List<Glass>();
            PlayerInEditableArea = false;
            UpdateInteractableObjects();

            InputsBrain.Instance.OnShardA += GrabShardA;
            InputsBrain.Instance.OnShardB += GrabShardB;
        }

        private void UpdateInteractableObjects() { //Update the shards interactable List and Initialize its components
            if (interactables.Count != 0)
            {
                foreach (var interactable in interactables)
                {
                    interactable.Initialize();
                    if (interactable.GetGlass)
                    {
                        shardsInteractable.Add(interactable);
                    }
                }
            }
        }

        public void PopulateService(BaseObject[] _interactable,  Glass[] _shards) {//Clear and populate interactable and shards
            interactables.Clear();
            shards.Clear();
            shardsInteractable.Clear();
            
            interactables.AddRange(_interactable);
            shards.AddRange(_shards);
            
            //Debug.Log($"[GlassShardService] Populating {interactables.Count} interactable | Populating {shards.Count} shards");
            UpdateInteractableObjects();
        }
        
        
        public void Tick() {
            HandleShardMovement();
            UpdateGlassInteraction(); //Expensive methods
        }

        private void UpdateGlassInteraction() { //Pas opti du tout ça la double boucle de for avec SetShardState
            foreach (var glassInteractable in shardsInteractable) {
                SetShardState(glassInteractable);
            }
        }
        
        private void SetShardState(BaseObject glassBase) {
            foreach (var shard in shards) {
                glassBase.OnShardInteract(glassBase.GetTextInteractable ? shard.IsColliding(glassBase.transform.position) : shard.IsColliding(glassBase.GetGlassInteract.BoundingBox), shard);
            }
        }
        
        ///Handles player input on the shards & grab priority
        private void HandleShardMovement()
        {
            if(shards.Count > 0 && onTopGlass ==null)
                onTopGlass = shards.Last();
            
            //Input is gather here and movement is handle here - So if the shard is not reference, it can't be moved or activate
            if (Mouse.current.leftButton.wasPressedThisFrame && !currentGlass)
            {
                UpdateGlassInteraction();
                foreach (var shard in shards)
                {
                    if (!shard.IsColliding(Mouse.current.position.ReadValue())) 
                        continue;
                    
                    currentGlass = shard;
                    currentGlass.ChangeHoldingState(true);

                    if (!shards.Contains(currentGlass))
                        return;

                    shards.Remove(currentGlass);
                    shards.Insert(0, currentGlass);

                    shard.transform.SetAsLastSibling();
                        
                    if(onTopGlass != null)
                    {
                        onTopGlass.SetInFront(false);
                        onTopGlass = shard;
                        onTopGlass.SetInFront(true);
                    }
                    return;
                }
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame && currentGlass) {
                currentGlass.ChangeHoldingState(false);
                currentGlass = null;
            }
        }

        private void GrabShardA(InputAction.CallbackContext ctx) {
            if (ctx.performed && currentGlass != AShard && AShard) {
                if(BShard)
                    BShard.ChangeHoldingState(false);
                AShard.ChangeHoldingState(true);
                currentGlass = AShard;
            }

            if (ctx.canceled && AShard) {
                AShard.ChangeHoldingState(false);
                currentGlass = null;
            }
        }

        private void GrabShardB(InputAction.CallbackContext ctx) {
            if (ctx.performed && currentGlass != BShard && BShard) {
                BShard.ChangeHoldingState(true);
                if(AShard)
                    AShard.ChangeHoldingState(false);
                currentGlass = BShard;
            }

            if (ctx.canceled && BShard) {
                BShard.ChangeHoldingState(false);
                currentGlass = null;
            }
        }
        
        public void RepopulateBaseObjet(BaseObject[] obj) {
            interactables.Clear();
            interactables.AddRange(obj);
            
            UpdateInteractableObjects();
        }
        
        public void AddShards(Glass newShards, bool isA) {
            shards.Add(newShards);
            if(isA) AShard = newShards;
            else BShard = newShards;
        }

        public void ClearAll() {
            shards.Clear();
            AShard = null;
            BShard = null;
            shardsInteractable.Clear();
            interactables.Clear();
        }
        
        public void Dispose() {
            shardsInteractable.Clear();
            shards.Clear();
            InputsBrain.Instance.OnShardA -= GrabShardA;
            InputsBrain.Instance.OnShardB -= GrabShardB;
        }
        
        public int ShardCount => shards.Count;
        public int InteractableCount => interactables.Count;

        
    }
}