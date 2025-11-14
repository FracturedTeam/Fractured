using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class ObtainShardInteractable : MonoBehaviour, IInteractable{
        private BaseObject baseObject;
        
        [Header("Settings")]
        [SerializeField] private Glass[] shard;
        
        private bool initialized = false;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component)) baseObject = component as BaseObject;
                else Debug.LogError($"[ObtainShardInteractable] Cannot find {nameof(BaseObject)} in {nameof(ObtainShardInteractable)}");
                
                baseObject.GetInteractionType = ObjectType.Shard;
                baseObject.GetCompletion = InteractionCompletion.NotCompleted;
                baseObject?.SetInteract(true);
            }

            initialized = true;

            if(shard.Length == 0)
                Debug.LogError($"[ObtainShardInteractable] {gameObject.name} No shards referenced !");
            
            foreach (var s in shard) {
                s.gameObject.SetActive(false);
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(baseObject.GetCompletion is InteractionCompletion.Completed) return;
            
            if (interaction is ObjectInteraction.Contextual) {
                ObtainShard();
            }
        }

        public void Tick(float deltaTime) {
        }

        void ObtainShard() {
            baseObject.GetCompletion = InteractionCompletion.Completed;
            
            foreach (var s in shard) {
                s.gameObject.SetActive(true);
                s.Initialize();
            }
            
            baseObject.SetInteract(false);
            
            Debug.Log($"[ObtainShardInteractable] {gameObject.name} Obtain Shard");
        }

        public void ResetObject() {
            Debug.Log($"[ObtainShardInteractable] {gameObject.name} Reset Object");
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}