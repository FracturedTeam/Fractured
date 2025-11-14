using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameServices {
    public class GameSceneLoaderSystem : Singleton<GameSceneLoaderSystem> {

        public List<Scene> loadedScenes { get; private set; }

        public int loadOnStart;

        private void Start() {
            if (loadOnStart != 0) {
                var load = LoadSceneAsync(loadOnStart);
            }
        }

        public async Task LoadSceneAsync(int toLoad, int toUnload = 0) {
            var operation = SceneManager.LoadSceneAsync(toLoad, LoadSceneMode.Additive);

            while (!operation.isDone) {
                await Task.Yield();
            }
            
            //Une fois que c'est fait -> Envoyé les infos de switch de caméra etc.
            
            Debug.Log($"[GameSceneLoaderSystem] Loading scene {toLoad}");
            
            if(toUnload != 0)
                await UnloadSceneAsync(toUnload);

        }

        public async Task UnloadSceneAsync(int toUnload) {
            var operation = SceneManager.UnloadSceneAsync(toUnload);

            while (!operation.isDone) {
                await Task.Yield();
            }
            
            Debug.Log($"[GameSceneLoaderSystem] Unloading scene {toUnload}");
            //euh je sais pas quoi faire mdr
        }
        
    }
}