using _Project.Scripts.Systems.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

namespace _Project.Scripts.GameServices.Services {
    public class RumbleService : IGameSystem {
        private Gamepad pad;
        private bool hasGamepad => pad != null;
        
        private readonly CountdownTimer rumbleDuration;
        private readonly CountdownTimer coloredRumbleDuration;
        
        public RumbleService(Gamepad pad) {
            this.pad = pad;
            rumbleDuration = new CountdownTimer(1f);
            coloredRumbleDuration = new CountdownTimer(1f);
        }
        
        public void Initialize() {
            if(!hasGamepad) Debug.Log("RumbleService does not have a gamepad");

            rumbleDuration.OnTimerStop += StopRumble;
            coloredRumbleDuration.OnTimerStop += StopColoredRumble;
            SetGamepadColor(GameInitializer.Instance.GetCurrentChapterColor());
        }

        public void RumblePulse(float lowFrequency, float highFrequency, float duration) {
            rumbleDuration.Reset(duration);
            
            if (!hasGamepad) return;

            pad.SetMotorSpeeds(lowFrequency, highFrequency);
            rumbleDuration.Start();
            
        }

        public void RumblePulseAndColor(float lowFrequency, float highFrequency, float duration, Color color) {
            coloredRumbleDuration.Reset(duration);
            if (!hasGamepad) return;

            if (pad is DualShock4GamepadHID dual) {
                dual.SetMotorSpeedsAndLightBarColor(lowFrequency, highFrequency, Color.chartreuse); 
            }
            else
                pad.SetMotorSpeeds(lowFrequency, highFrequency);
            
            coloredRumbleDuration.Start();
        }
        
        private void StopRumble() {
            if (!hasGamepad) return; 
            
            pad.SetMotorSpeeds(0, 0);
        }

        private void StopColoredRumble() {
            if (!hasGamepad) return; 
            
            if (pad is DualShock4GamepadHID dual) {
                dual.SetMotorSpeedsAndLightBarColor(0, 0, GameInitializer.Instance.GetCurrentChapterColor());
            }
            else {
                pad.SetMotorSpeeds(0, 0);
            }
        }

        private void PauseRumble() {
            if (!hasGamepad) return; 
            
            pad.PauseHaptics();
        }

        private void ResumeRumble() {
            if (!hasGamepad) return; 
            
            pad.ResumeHaptics();
        }

        public void SetGamepadColor(Color color) {
            if(!hasGamepad) return;
            
            if (pad is DualShockGamepad dual) {
                dual.SetLightBarColor(color);
            }
        }
        
        public void Tick() {
            
        }
        
        public void Dispose() {
            rumbleDuration.OnTimerStop -= StopRumble;
        }
    }
}