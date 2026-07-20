using _Project.Scripts.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Memory", menuName = "ScriptableObjects/MemoryFrame")]
public class MemoryFrameScriptable : ScriptableObject
{
    public Material material;
    [TextArea(15,20)]
    public string infoText; 
}

