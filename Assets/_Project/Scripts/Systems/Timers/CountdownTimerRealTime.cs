using UnityEngine;

namespace _Project.Scripts.Systems.Timers {
    public class CountdownTimerRealTime : Timer {
        public CountdownTimerRealTime(float value) : base(value) { }

        public override void Tick() {
            if(IsRunning && CurrentTime > 0) {
                CurrentTime -= Time.unscaledDeltaTime;
            }

            if(IsRunning && CurrentTime <= 0) {
                Stop();
            } 
        }

        public override bool IsFinished => CurrentTime <= 0f;
    }
}