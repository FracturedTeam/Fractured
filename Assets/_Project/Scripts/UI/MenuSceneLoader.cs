using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI {
    public class MenuSceneLoader : MonoBehaviour
    {
        [SerializeField] SceneField[] scenesToLoad;
    
        public void Quit() {
            Application.Quit();
        }

        public void NewGame() {
            var newGame = LoadNewGameAsync();
        }
        
        private async Task LoadNewGameAsync() {
            for (var i = 0; 0 < scenesToLoad.Length; i++) {
                var toLoad = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);
                while (toLoad is { isDone: false }) {
                    await Task.Yield();
                }
            }

            GameSaveSystem.Instance.NewGame("Game Main Menu");
            GameSceneLoaderSystem.Instance.SetSceneToLoad(scenesToLoad);
            var load = GameSceneLoaderSystem.Instance.UnloadSceneAsync();
            
            Debug.Log($"Load new Game successfully");
        }
    }
}
