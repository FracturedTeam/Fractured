using UnityEngine;

namespace _Project.Scripts.UI
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField] private Transform nextCameraPos;
        [SerializeField] private Transform previousCameraPos;
        [SerializeField] private GameObject openPanel;
        private bool returning;

        public void OnTrigger()
        {
           
            if(openPanel)
                openPanel.SetActive(true);
            returning = !returning;
        }
    }
}
