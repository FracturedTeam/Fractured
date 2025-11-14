using System;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class HudManager : MonoBehaviour
    {
        public static HudManager hud;
        
        [SerializeField] private GlassText glassText;

        private void Awake()
        {
            if(hud == null)
                    hud = this;
            else
                Destroy(this);
        }

        private void OnDestroy()
        {
            if(hud == this)
                    hud =  null;
        }

        public void SetText(DialogueScriptableObject newDialogue)
        {
            if(!glassText)
                return;
            
            glassText.Setup(newDialogue);
        }
        
        
    }
}
