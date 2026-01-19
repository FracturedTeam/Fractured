using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class LoadMenuCreditScene : MonoBehaviour {
        public void LoadMenu() {
            GameSceneLoaderSystem.Instance.LoadMenu();
        }
    }
}