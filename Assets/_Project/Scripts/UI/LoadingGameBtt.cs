using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class LoadingGameBtt : MonoBehaviour {
        private void Start() {
            gameObject.SetActive(GameSaveSystem.Instance.ExistingSave());
        }
    }
}