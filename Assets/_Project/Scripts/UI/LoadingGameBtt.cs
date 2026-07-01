using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class LoadingGameBtt : MonoBehaviour {
        private void Start() { // Script à la con pour check si le menu affiche ou non le load
            if(GameInitializer.HasInstance)
                gameObject.SetActive(GameInitializer.Instance.ExistingSave());
        }
    }
}