using System;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Singletons;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class HudManager : Singleton<HudManager>
    {
        [SerializeField] private GlassText glassText;
        private DialogueScriptableObject currentDialogue;
        
        private CountdownTimer textTimer;

        private void Start()
        {
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop  += ResetText;
        }

        public void SetText(DialogueScriptableObject newDialogue)
        {
            if(!glassText || !newDialogue)
                return;
            
            currentDialogue = newDialogue;
            
            glassText.Setup(currentDialogue);
            
            if (currentDialogue.time <= 0)
                return;
            
            textTimer.Reset(currentDialogue.time);
            textTimer.Start();
        }
        
        private void ResetText()
        {
            if(currentDialogue && currentDialogue.next)
                SetText(currentDialogue.next);
            else
                glassText.Setup(null);
        }
        
        
    }
}
