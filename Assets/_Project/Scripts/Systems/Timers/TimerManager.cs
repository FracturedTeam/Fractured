using System.Collections.Generic;

namespace _Project.Scripts.Systems.Timers {
    public static class TimerManager {
        private static readonly List<Timer> timers = new();

        public static void RegisterTimer(Timer timer) =>  timers.Add(timer);
        public static void DeregisterTimer(Timer timer) =>  timers.Remove(timer);

        public static void UpdateTimers() {
            foreach (var timer in new List<Timer>(timers)) {
                timer.Tick();
            }
        }
        
        public static void Clear() => timers.Clear();
    }
}