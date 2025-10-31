#if UNITY_EDITOR || DEVELOPMENT_BUILD
using UnityEngine;

namespace _Project.Scripts.DebugSystems {
    public class DebugSystemInitializer : MonoBehaviour {
        private DebugSystem debugSystem;

        private void OnGUI() {
            debugSystem?.DrawDebugGUI();
        }

        private void Update() {
            debugSystem?.Tick();
        }

        private void OnDrawGizmos() {
            debugSystem?.DrawDebugGizmos();
        }

        private void OnDestroy() {
            debugSystem?.Dispose();
        }

        public void SetDebugSystem(DebugSystem debugSystem) {
            this.debugSystem = debugSystem;
        }
    }
}
#endif