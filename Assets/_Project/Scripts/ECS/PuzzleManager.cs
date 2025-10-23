using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private List<InteractableObject> gamePlayElements = new();
    [SerializeField] private List<Glass> glassShards = new();
    
    private readonly List<InteractableObject> glassInteractables = new();

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

    private void SetState(InteractableObject block)
    {
        foreach (var glass in glassShards)
            block.OnShardInteract(glass.CheckCollision(block.GetGlassInteract), glass.GetColor);
    }
}
