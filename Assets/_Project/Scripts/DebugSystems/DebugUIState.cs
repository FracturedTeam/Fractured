using System.Collections.Generic;

namespace _Project.Scripts.DebugSystems {
    public class DebugUIState {
        private readonly Dictionary<string, bool> states = new Dictionary<string, bool>();

        public void SetVisible(string key, bool visible) {
            states[key] = visible;
        }

        public void Toggle(string key) {
            if (!states.ContainsKey(key))
                states[key] = true;
            else
                states[key] = !states[key];
        }

        public bool IsVisible(string key) {
            return states.TryGetValue(key, out var visible) && visible;;
        }
    }
}