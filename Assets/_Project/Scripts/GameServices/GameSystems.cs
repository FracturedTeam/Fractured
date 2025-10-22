using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class GameSystems : IDisposable {
        private readonly Dictionary<Type, IGameSystem> systems = new();

        public void Register<T>(T system) where T : class, IGameSystem {
            systems[typeof(T)] = system ?? throw new ArgumentNullException(nameof(system));
        }

        public T Get<T>() where T : class, IGameSystem {
            systems.TryGetValue(typeof(T), out var system);
            return system as T;
        }

        public void Initialize() {
            try {
                foreach (var system in systems.Values)
                    system.Initialize();
                Debug.Log($"[GameSystems] {systems.Count} GameSystems initialized");
            }
            catch (Exception ex) {
                Debug.LogError($"[GameSystems] Systems initialization failed: {ex.Message}");
                foreach (var binding in systems) {
                    Debug.LogError(binding.Value == null
                        ? $"{binding.Key.Name} is null"
                        : $"{binding.Key.Name} is initialized");
                }

                throw;
            }
        }

        public void Tick() {
            foreach (var system in systems.Values)
                system.Tick();
        }
        
        public void Dispose() {
            foreach (var system in systems.Values)
                system.Dispose();
            systems.Clear();
        }
    }
}