using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.ECS.InteractableObjects;
using UnityEngine;

namespace _Project.Scripts.ECS
{
    public class PuzzleManager : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private List<BaseObject> gamePlayElements = new();
        [SerializeField] private List<Glass> glassShards = new();
    
        private readonly List<BaseObject> glassInteractables = new();

        private void Start()
        {
            foreach (var interactableObject in gamePlayElements)
            {
                if (interactableObject.GetGlass)
                    glassInteractables.Add(interactableObject);
            }
        }

        private void Update()
        {
            UpdateGlassElements();
        }

        private void UpdateGlassElements()
        {
            foreach (var block in glassInteractables)
                SetState(block);
        }

        private void SetState(BaseObject block)
        {
            //foreach (var glass in glassShards)
                //block.OnShardInteract(glass.CheckCollision(block.GetGlassInteract), glass.GetColor);
        }
    }
}
