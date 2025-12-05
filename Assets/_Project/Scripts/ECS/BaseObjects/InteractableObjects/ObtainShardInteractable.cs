using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class ObtainShardInteractable : MonoBehaviour, IInteractable{
        private BaseObject baseObject;
        
        [Header("Settings")]
        [SerializeField] public Glass[] shards;
        
        private bool initialized = false;
        
        public void Initialize() { //To-do save l'obtention pour insantier a nouveau le fragment
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component)) baseObject = component as BaseObject;
                else Debug.LogError($"[ObtainShardInteractable] Cannot find {nameof(BaseObject)} in {nameof(ObtainShardInteractable)}");
                
                baseObject.GetInteractionType = ObjectType.Shard;
                baseObject.GetCompletion = InteractionCompletion.NotCompleted;
                baseObject?.SetInteract(true);
            }

            initialized = true;

            if(shards.Length == 0)
                Debug.LogError($"[ObtainShardInteractable] {gameObject.name} No shards referenced !");
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            Debug.Log($"[ObtainShardInteractable] {gameObject.name} Enter Interact");
            if(baseObject.GetCompletion is InteractionCompletion.Completed)
            {
                if (baseObject.failedDialogue is { oneTime: true, alreadyInteracted: true })
                    return;

                HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                baseObject.failedDialogue.alreadyInteracted = true;
                return;
            }
            
            if (baseObject.successDialogue is { oneTime: true, alreadyInteracted: true })
                return;

            HudManager.Instance.SetText(baseObject.successDialogue.dialogue);
            baseObject.successDialogue.alreadyInteracted = true;
            
            if (interaction is ObjectInteraction.Contextual) {
                ObtainShard();
            }
        }

        public void Tick(float deltaTime) {
        }

        public void CompleteObject() {
            ObtainShard();
        }

        void ObtainShard() {
            baseObject.GetCompletion = InteractionCompletion.Completed;
            
            GameInitializer.Instance.AddShards(shards);
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