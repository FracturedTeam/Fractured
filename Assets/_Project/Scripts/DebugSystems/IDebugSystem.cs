using System;

namespace _Project.Scripts.DebugSystems {
    public interface IDebugSystem : IDisposable {
        void Initialize();
        void Tick();
    }

    public interface IDebugGUI {
        void DrawDebugGUI();
    }

    public interface IDebugGizmos {
        void DrawDebugGizmos();
    }
}