using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class ShardIndexObtention : MonoBehaviour, IInteractable {
        private BaseObject _baseObject;
        
        private bool initialized = false;
        
        [SerializeField] private int shardIndexToUnlock = 0;
        
        public void Initialize() {
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component)) _baseObject = component as BaseObject;
                else Debug.LogError($"[ShardIndexObtention] Cannot find {nameof(BaseObject)} in {nameof(ShardIndexObtention)}");
                
                _baseObject.GetInteractionType = ObjectType.Shard;
                _baseObject.GetCompletion = InteractionCompletion.NotCompleted;
                _baseObject?.SetInteract(true);
            }
            
            initialized = true;
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if(_baseObject.GetCompletion is InteractionCompletion.Completed)
            {
                if (_baseObject.failedDialogue is { oneTime: true, alreadyInteracted: true })
                    return;

                HudManager.Instance.SetText(_baseObject.failedDialogue.dialogue);
                _baseObject.failedDialogue.alreadyInteracted = true;
                return;
            }
            
            if (_baseObject.successDialogue is { oneTime: true, alreadyInteracted: false }) {
                HudManager.Instance.SetText(_baseObject.successDialogue.dialogue);
                _baseObject.successDialogue.alreadyInteracted = true;
            }

            
            if (interaction is ObjectInteraction.Contextual) {
                ObtainShard();
                AudioManager.Instance.PlayBreakGlassSound(transform.position);
            }
        }

        private void ObtainShard() {
            //Here unlock the index of the shard
            _baseObject.GetCompletion = InteractionCompletion.Completed;
            _baseObject?.SetInteract(false);
            
            PlayerShardsCollection.Instance.UnlockShardAtIndex(shardIndexToUnlock);
        }

        public void Tick(float deltaTime) {
            
        }

        public void Dispose() {
            
        }

        public void CompleteObject() {
            //ObtainShard();
        }

        public void ResetObject() {
            
        }

        public BaseObject GetBaseObject() {
            return _baseObject;
        }
    }
}