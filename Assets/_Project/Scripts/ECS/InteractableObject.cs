using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool GetGlass =>  GetGlassInteract != null;
    public GlassInteractable GetGlassInteract { get; private set; }
    private MeshRenderer meshRenderer;

    private void Start()
    {
        if(TryGetComponent(typeof(GlassInteractable), out var g))
            GetGlassInteract = g as GlassInteractable;
        
        if(TryGetComponent(typeof(MeshRenderer), out var m))
            meshRenderer = m as MeshRenderer;
    }

    internal virtual void OnInteract()
    {
        //Insert here interaction with the player
    }

    internal virtual void UnderTheGlass(bool isOn, ColorEnum glassColor)
    {
        //Insert here interaction with the glass
        meshRenderer.enabled = isOn && glassColor == GetGlassInteract.color;
    }
}
