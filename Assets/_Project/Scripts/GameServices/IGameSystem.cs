using System;

namespace _Project.Scripts.GameServices {
    public interface IGameSystem : IDisposable{
        void Initialize();

        void Tick();
    }
}