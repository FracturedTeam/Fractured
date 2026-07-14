using _Project.Scripts.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Glass Text", menuName = "ScriptableObjects/GlassText")]
public class GlassTextScriptableObject : ScriptableObject
{
    public string baseText;
    public string fragAText;
    public string fragBText;
    public string bothText;
}
