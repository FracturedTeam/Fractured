using UnityEngine;

namespace _Project.Scripts.ScriptableObjects {
    
    [CreateAssetMenu(fileName = "Transition Text", menuName = "ScriptableObjects/Transition Text")]
    public class TransitionTextSO : ScriptableObject {
        [TextArea]
        public string title;
        
        [TextArea]
        public string description;
    }
}