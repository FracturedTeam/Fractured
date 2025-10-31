#if UNITY_EDITOR || DEVELOPMENT_BUILD
using System.Collections.Generic;

namespace _Project.Scripts.DebugSystems {
    public class DebugSystem {
        private List<IDebugSystem> debugSystems =  new List<IDebugSystem>();

        public void Register(IDebugSystem debugSystem) {
            debugSystems.Add(debugSystem);
            debugSystem.Initialize();
        }

        public void Tick() {
            foreach (var debugSystem in debugSystems) {
                debugSystem.Tick();
            }
        }

        public void DrawDebugGUI() {
            foreach (var debugSystem in debugSystems) {
                if (debugSystem is IDebugGUI debugGUI) {
                    debugGUI.DrawDebugGUI();
                }
            }
        }

        public void DrawDebugGizmos() {
            foreach (var debugSystem in debugSystems) {
                if (debugSystem is IDebugGizmos debugGizmos) {
                    debugGizmos.DrawDebugGizmos();
                }
            }
        }
        
        public void Dispose() {
            foreach (var debugSystem in debugSystems) {
                debugSystem.Dispose();
            }
        }
    }
    
}
#endif
