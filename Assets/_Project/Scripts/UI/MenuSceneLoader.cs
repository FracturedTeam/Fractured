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
            GameSceneLoaderSystem.Instance.NewGame();
        }
    }
}
