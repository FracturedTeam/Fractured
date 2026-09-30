using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class DemoLoadScreen : MonoBehaviour {
        public void LoadMenu() { 
            Time.timeScale = 1f;
            GameSceneLoaderSystem.Instance.LoadMenu();
        }
    }
}