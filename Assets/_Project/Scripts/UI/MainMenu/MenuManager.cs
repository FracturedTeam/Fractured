using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Camera Ref")]
        [SerializeField] private Animation animatedCamera;

        [Header("Button Ref")]
        [SerializeField] private GameObject loadGameBtt;
        
        // public static MenuManager Instance;

        // private void Awake()
        // {
        //     if(Instance == null) Instance =  this;
        //     else Destroy(this);
        // }

        private void Start() {
            if(GameInitializer.HasInstance)
                loadGameBtt.SetActive(GameInitializer.Instance.ExistingSave());
        }
        
        public void ChangeTarget(string anim)
        {
            animatedCamera.Play(anim);
        }

        public void QuitGame() {
            Application.Quit();
        }
        
        public void NewGame() {
            GameSceneLoaderSystem.Instance.NewGame();
        }

        public void LoadGame() {
            GameSceneLoaderSystem.Instance.LoadGame();
        }
    }
}
