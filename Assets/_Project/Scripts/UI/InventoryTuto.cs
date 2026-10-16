using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class InventoryTuto : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private bool hasAlreadyBeenShowed = false;

        public void ShowInventory()
        {
            if(hasAlreadyBeenShowed) return;
            
            hasAlreadyBeenShowed = true;
            canvasGroup.DOFade(1f, 1f);
        }

        public void HideInventory()
        {
            canvasGroup.DOFade(0f, 1f);
        }

    }
}