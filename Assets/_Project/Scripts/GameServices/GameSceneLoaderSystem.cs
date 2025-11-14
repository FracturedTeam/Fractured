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

        private float targetProcess;
        
        public async Task LoadSceneAsync(SceneData toLoad, SceneData toUnload) {
            var scene = SceneManager.GetSceneByName(toLoad.sceneName);
            var operation = SceneManager.LoadSceneAsync(scene.buildIndex);

            while (!operation.isDone) {
                await Task.Yield();
            }
            
            //Une fois que c'est fait -> Envoyé les infos de switch de caméra etc.
            
            await UnloadSceneAsync();

        }

        public async Task UnloadSceneAsync() {
            
        }
        
    }

    [Serializable]
    public class SceneData {
        public string sceneName;
    }
}