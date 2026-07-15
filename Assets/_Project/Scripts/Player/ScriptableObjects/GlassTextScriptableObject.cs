using _Project.Scripts.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Glass Text", menuName = "ScriptableObjects/GlassText")]
public class GlassTextScriptableObject : ScriptableObject
{
    [TextArea]
    public string baseText;
    [TextArea]
    public string fragAText;
    [TextArea]
    public string fragBText;
    [TextArea]
    public string bothText;
}
