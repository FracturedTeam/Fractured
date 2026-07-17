using _Project.Scripts.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Glass Document", menuName = "ScriptableObjects/GlassDoc")]
public class GlassDocumentScriptableObject : GlassTextScriptableObject
{
    public Material material;
    public DocumentTypes type; 
}

public enum DocumentTypes
{
    square,
    landscape,
    portrait
}
