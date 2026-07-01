using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueScriptableObject")]
    public class DialogueScriptableObject : ScriptableObject
    {
        [TextArea(15,20)]
        public string dialogue;

        public DialogueScriptableObject next;
        public float time;
    }
}
