#if UNITY_EDITOR || DEVELOPMENT_BUILD
using System;
using UnityEngine;

namespace _Project.Scripts.DebugSystems {
    public class DebugSystemInitializer : MonoBehaviour {
        internal DebugSystem debugSystem;

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
    }
}
#endif