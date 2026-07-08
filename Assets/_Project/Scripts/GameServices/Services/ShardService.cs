using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.InteractableObjects;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.GameServices.Services {
    public class ShardService : IGameSystem {
        public List<BaseObject> interactables { get; private set; }
        public List<GlassText> glassTexts {get; private set;}
        public List<Glass> shards {get; private set;}
        private Glass currentGlass;
        private Glass onTopGlass;
        
        private readonly List<BaseObject> shardsInteractable = new List<BaseObject>();

        public bool PlayerInEditableArea {get; private set;}
        public bool PlayerInRedEditableArea {get; private set;}
        public bool PlayerInBlueEditableArea {get; private set;}
        
        public void Initialize() { //Initialize the service
            interactables = new List<BaseObject>();
            shards = new List<Glass>();
            glassTexts = new List<GlassText>();
            PlayerInEditableArea = false;
            UpdateInteractableObjects();
        }

        private void UpdateInteractableObjects() { //Update the shards interactable List and Initialize its components
            if(interactables.Count != 0)
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
            if(glassTexts.Count != 0)
            {
                foreach (var text in glassTexts)
                {
                    text.Initialize();
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
            
            Debug.Log($"[GlassShardService] Populating {interactables.Count} interactable | Populating {shards.Count} shards | Populating {glassTexts.Count} texts");
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
                glassBase.OnShardInteract(shard.IsColliding(glassBase.GetGlassInteract.BoundingBox), shard);
            }
        }
        
        private void SetGlassTextState(GlassText text) {
            foreach (var shard in shards) {
                text.OnInteract(shard.IsColliding(text.transform.position), shard);
            }
        }
        
        ///Handles player input on the shards & grab priority
        private void HandleShardMovement()
        {
            if(shards.Count>0 && onTopGlass==null)
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

        public void RepopulateBaseObjet(BaseObject[] obj, GlassText[] texts) {
            interactables.Clear();
            interactables.AddRange(obj);
            
            glassTexts.Clear();
            glassTexts.AddRange(texts);
            
            UpdateInteractableObjects();
        }
        
        public void AddShards(Glass[] newShards) {
            shards.AddRange(newShards);
        }
        
        public void SetEditableArea(bool inArea) {
            PlayerInEditableArea = inArea;
        }
        
        public void SetRedEditableArea(bool inArea) {
            PlayerInRedEditableArea = inArea;
        }

        public void SetBlueEditableArea(bool inArea) {
            PlayerInBlueEditableArea = inArea;
        }

        public void ClearAll() {
            shards.Clear();
            shardsInteractable.Clear();
            interactables.Clear();
        }
        
        public void Dispose() {
            shardsInteractable.Clear();
            shards.Clear();
        }
        
        public int ShardCount => shards.Count;
        public int InteractableCount => interactables.Count;

        
    }
}