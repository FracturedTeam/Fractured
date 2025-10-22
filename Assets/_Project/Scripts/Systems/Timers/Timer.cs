<<<<<<< HEAD
using System;
using UnityEngine;

namespace _Project.Scripts.Systems.Timers {
    public abstract class Timer : IDisposable {
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }

        protected float initialTime;
        public virtual float Progress => Mathf.Clamp(CurrentTime / initialTime, 0f, 1f);
        
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        public Timer(float value) {
            initialTime = value;
        }

        public void Start() {
            CurrentTime = initialTime;
            if (!IsRunning) {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart.Invoke();
            }
        }

        public void Stop() {
            if (IsRunning) {
                IsRunning = false;
                TimerManager.DeregisterTimer(this);
                OnTimerStop.Invoke();
            }
        }

        public abstract void Tick();
        public abstract bool IsFinished { get; }
        
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public virtual void Reset() => CurrentTime = initialTime;

        public virtual void Reset(float newTime) {
            initialTime = newTime;
            Reset();
        }

        private bool disposed;

        ~Timer() {
            Dispose(false);
        }
        
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(disposed) return;
            
            if(disposing)
                TimerManager.DeregisterTimer(this);
            
            disposed = true;
        }
=======
namespace _Project.Scripts.Systems.Timers {
    public class Timer {
        
>>>>>>> origin/feature/SystemUtility
    }
}