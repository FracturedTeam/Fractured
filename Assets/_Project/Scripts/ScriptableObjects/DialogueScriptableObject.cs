using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueScriptableObject")]
    public class DialogueScriptableObject : ScriptableObject
    {
        public string dialogue;
        
        public string variableName;
        public string basic;
        public string red;
        public string blue;
        public string both;
        
        public int letters;
        public float time;
    }
}
