using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class ObtainShardInteractable : MonoBehaviour, IInteractable{
        private BaseObject baseObject;
        
        [Header("Settings")]
        [SerializeField] private Glass shard;
        
        private bool initialized = false;
        private bool shardObtained = false;
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component))
                    baseObject = component as BaseObject;
                else 
                    Debug.LogError($"[ObtainShardInteractable] {gameObject.name} does not have a BaseObject !");
                
                baseObject?.SetInteract(true);
            }

            initialized = true;
            
            shard.gameObject.SetActive(false);
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(shardObtained) return;
            
            if (interaction is ObjectInteraction.Contextual) {
                ObtainShard();
            }
        }

        void ObtainShard() {
            Debug.Log($"[ObtainShardInteractable] {gameObject.name} Obtain Shard");
            shardObtained = true;
            shard.gameObject.SetActive(true);
            shard.Initialize();
            baseObject.SetInteract(false);
        }

        public void ResetObject() {
            Debug.Log($"[ObtainShardInteractable] {gameObject.name} Reset Object");
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}