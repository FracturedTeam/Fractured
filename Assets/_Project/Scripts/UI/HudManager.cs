using System;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class HudManager : MonoBehaviour
    {
        public static HudManager hud;
        
        [SerializeField] private GlassText glassText;
        
        private CountdownTimer textTimer;

        private void Awake()
        {
            if(hud == null)
                    hud = this;
            else
                Destroy(this);
            textTimer = new CountdownTimer(0);
            textTimer.OnTimerStop  += ResetText;
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
