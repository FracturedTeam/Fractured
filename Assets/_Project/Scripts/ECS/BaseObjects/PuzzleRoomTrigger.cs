using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.GameServices;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.InteractableObjects {
    public class PuzzleRoomTrigger : MonoBehaviour {
        [Header("Puzzle Room Objects")]
        [Tooltip("Interactable Objects in the room")]
        public BaseObject[] interactable;
        [Tooltip("Shards to solve the room")]
        public Glass[] shards;
        [Tooltip("Texts")]
        public GlassTextLink[] texts;

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                GameInitializer.Instance.UpdatePuzzleRoom(interactable, shards, texts);
            }
        }
        
    }
}