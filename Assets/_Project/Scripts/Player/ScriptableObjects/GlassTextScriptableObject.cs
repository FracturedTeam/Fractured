using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Glass Text", menuName = "ScriptableObjects/GlassText")]
    public class GlassTextScriptableObject : ScriptableObject
    {
        [TextArea(5,20)]
        public string baseText;
        
        [TextArea(5,20)]
        public string fragAText;      
        
        [TextArea(5,20)]
        public string fragBText;   
        
        [TextArea(5,20)]
        public string bothText;
        
    }
}
