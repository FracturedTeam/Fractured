using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Camera Ref")]
        [SerializeField] private Animation animatedCamera;
    
        public static MenuManager Instance;

        private void Awake()
        {
            if(Instance == null) Instance =  this;
            else Destroy(this);
        }

        public void ChangeTarget(string anim)
        {
            animatedCamera.Play(anim);
        }
    }
}
