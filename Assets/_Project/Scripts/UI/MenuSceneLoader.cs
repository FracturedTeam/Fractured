using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class MenuSceneLoader : MonoBehaviour {
        public void Quit() {
            Application.Quit();
        }

        public void NewGame() {
            GameInitializer.Instance.NewGame();
        }

        public void LoadGame() {
            GameInitializer.Instance.LoadGame();
        }
    }
}
