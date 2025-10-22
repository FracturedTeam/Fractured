<<<<<<< HEAD
using System;
using UnityEngine;

namespace _Project.Scripts.Systems.Timers {
    public class FrequencyTimer : Timer {
        public float TicksPerSecond { get; private set; }
        public Action OnTick { get; private set; }

        private float timeThreshold;
        
        public FrequencyTimer(float ticksPerSecond) : base(0) {
            CalculateTimeTreshold(ticksPerSecond);
        }

        public override void Tick() {
            if (IsRunning && CurrentTime >= timeThreshold) {
                CurrentTime -= timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < timeThreshold) {
                CurrentTime += Time.deltaTime;
            }
        }

        public override bool IsFinished => !IsRunning;

        public override void Reset() {
            CurrentTime = 0;
        }
        
        public override void Reset(float newTicksPerSecond) {
            CalculateTimeTreshold(newTicksPerSecond);
            Reset();
        }

        void CalculateTimeTreshold(float ticksPerSecond) {
            TicksPerSecond = ticksPerSecond;
            timeThreshold = 1f / TicksPerSecond;
        }
=======
namespace _Project.Scripts.Systems.Timers {
    public class FrequencyTimer {
        
>>>>>>> origin/feature/SystemUtility
    }
}