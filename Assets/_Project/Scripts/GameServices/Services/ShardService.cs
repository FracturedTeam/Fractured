using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.InteractableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.GameServices.Services {
    public class ShardService : IGameSystem {
        public List<BaseObject> interactables { get; private set; }
        public List<GlassText> glassTexts {get; private set;}
        public List<Glass> shards {get; private set;}
        private Glass currentGlass;
        
        private readonly List<BaseObject> shardsInteractable = new List<BaseObject>();

        public bool PlayerInEditableArea {get; private set;}
        
        public void Initialize() { //Initialize the service
            interactables = new List<BaseObject>();
            shards = new List<Glass>();
            glassTexts = new List<GlassText>();
            PlayerInEditableArea = false;
            UpdateInteractableObjects();
        }

        void UpdateInteractableObjects() { //Update the shards interactable List and Initialize its components
            if(interactables.Count == 0) return;
            
            foreach (var interactable in interactables) {
                interactable.Initialize();
                if (interactable.GetGlass) {
                    shardsInteractable.Add(interactable);
                }
            }
        }

        public void PopulateService(BaseObject[] _interactable,  Glass[] _shards, GlassText[] _texts) {//Clear and populate interactable and shards
            interactables.Clear();
            shards.Clear();
            shardsInteractable.Clear();
            glassTexts.Clear();
            
            interactables.AddRange(_interactable);
            shards.AddRange(_shards);
            glassTexts.AddRange(_texts);
            
            Debug.Log($"[GlassShardService] Populating {interactables.Count} interactable");
            Debug.Log($"[GlassShardService] Populating {shards.Count} shards");
            Debug.Log($"[GlassShardService] Populating {glassTexts.Count} texts");
            
            UpdateInteractableObjects();
        }
        
        
        public void Tick() {
            HandleShardMovement();
            UpdateGlassInteraction(); //Expensive methods
        }

        private void UpdateGlassInteraction() { //Pas opti du tout ça la double boucle de for avec SetShardState
            foreach (var glassInteractable in shardsInteractable)
                SetShardState(glassInteractable);
            foreach (var glassText in glassTexts)
              SetGlassTextState(glassText);
        }
        
        private void SetShardState(BaseObject glassBase) {
            foreach (var shard in shards) {
                glassBase.OnShardInteract(shard.IsColliding(glassBase.GetGlassInteract.pos2D), shard);
            }
        }
        
        private void SetGlassTextState(GlassText text) {
            foreach (var shard in shards) {
                foreach (var pos in text.TagPositions)
                {
                    text.OnInteract(shard.IsColliding(pos), shard);
                }

            }
        }
        
        ///Handles player input on the shards & grab priority
        private void HandleShardMovement() { //Input is gather here and movement is handle here - So if the shard is not reference, it can't be moved or activate
            foreach (var shard in shards) {
                if (Mouse.current.leftButton.wasPressedThisFrame && !currentGlass) {
                    if (!shard.IsColliding(Mouse.current.position.ReadValue(), true))
                        continue;
                    
                    currentGlass = shard;
                    currentGlass.ChangeHoldingState(true);

                    if (!shards.Contains(currentGlass)) 
                        return;
                    
                    shards.Remove(currentGlass);
                    shards.Insert(0, currentGlass);

                    return;
                }
                
                if (Mouse.current.leftButton.wasReleasedThisFrame && currentGlass) {
                    currentGlass.ChangeHoldingState(false);
                    currentGlass = null;
                    return;
                }
               
                if (Mouse.current.rightButton.wasPressedThisFrame && !currentGlass) {
                    if (!shard.IsColliding(Mouse.current.position.ReadValue(), true))
                        continue;
                    
                    shard.ChangeStateActivation(!shard.IsActivated);
                    return;
                }
            }
        }
        
        public void SetEditableArea(bool inArea) {
            PlayerInEditableArea = inArea;
        }
        
        public void Dispose() {
            shardsInteractable.Clear();
        }
        
        public int ShardCount => shards.Count;
        public int InteractableCount => interactables.Count;
    }
}