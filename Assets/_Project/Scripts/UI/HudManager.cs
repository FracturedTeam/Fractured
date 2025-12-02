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
        
        private CountdownTimer textTimer;

        private void Start()
        {
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop  += ResetText;
        }

        public void SetText(DialogueScriptableObject newDialogue)
        {
            if(!glassText)
                return;
            
            glassText.Setup(newDialogue);
            
            if (newDialogue.time <= 0)
                return;
            
            textTimer.Reset(newDialogue.time);
            textTimer.Start();
        }
        
        private void ResetText()
        {
            glassText.Setup(null);
        }
        
        
    }
}
