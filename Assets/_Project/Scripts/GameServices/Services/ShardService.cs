using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameServices.Services {
    public class ShardService : IGameSystem {
        private List<InteractableObject> interactables = new List<InteractableObject>();
        private List<Glass> shards = new List<Glass>();
        
        private readonly List<InteractableObject> shardsInteractable = new List<InteractableObject>();
        
        public void Initialize() { //Initialize the service
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

        public void PopulateService(InteractableObject[] _interactable,  Glass[] _shards) {//Clear and populate interactable and shards
            interactables.Clear();
            shards.Clear();
            shardsInteractable.Clear();
            
            interactables.AddRange(_interactable);
            shards.AddRange(_shards);
            
            Debug.Log($"[GlassShardService] Populating {interactables.Count} interactable");
            Debug.Log($"[GlassShardService] Populating {shards.Count} shards");
            
            UpdateInteractableObjects();
        }
        
        public void Tick() {
            UpdateGlassInteraction();
        }

        private void UpdateGlassInteraction() {
            foreach (var glassInteractable in shardsInteractable)
                SetState(glassInteractable);
        }

        private void SetState(InteractableObject glassInteractable) {
            foreach (var shard in shards) {
                glassInteractable.OnShardInteract(shard.CheckCollision(glassInteractable.GetGlassInteract), shard.GetColor);
            }
        }
        
        public void Dispose() {
            shardsInteractable.Clear();
        }
        
        public int ShardCount => shards.Count;
        public int InteractableCount => interactables.Count;
    }
}