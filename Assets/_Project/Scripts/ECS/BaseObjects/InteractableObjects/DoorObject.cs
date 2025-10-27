using _Project.Scripts.Enums;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class DoorObject : InteractableObject {

        [SerializeField] private InteractableObject keyObject;
        
        internal override void OnInteract(ObjectInteraction interaction) {
            base.OnInteract(interaction);

            switch (interaction) {
                case ObjectInteraction.Use:
                    CheckForKeyObject();
                    break;
                default:
                    Debug.Log($"[DoorObject] {interaction} not supported]");
                    break;
            }
        }

        void CheckForKeyObject() {
            if(!PlayerController.Instance.interact.hasObject) return;
            
            if(PlayerController.Instance.interact.GetInteractableObject() != keyObject) return;
            
            Use();
        }

        public override void Use() {
            //Open the door
            
            keyObject.ConsumeObject();
        }
    }
}