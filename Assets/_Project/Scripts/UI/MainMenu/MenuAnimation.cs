using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MenuAnimation : MonoBehaviour {
        [Header("Menu")]
        [SerializeField] public MenuAnimation PreviousMenu;
        [SerializeField] public CurrentMenu menuType;
        
        [Header("Switching Time")]
        [SerializeField] private float openingTime = 2;
        [SerializeField] private float closingTime = 1;

        private CanvasGroup canvasGroup;
        private Tweener menuTween;
        
        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup.alpha == 0) {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }
        }

        // private void OnEnable() {
        //     menuTween = canvasGroup.DOFade(1, openingTime).SetUpdate(true);
        // }

        public void Close() {
            menuTween =  canvasGroup.DOFade(0, closingTime).SetUpdate(true);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        public void Open() {
            menuTween =  canvasGroup.DOFade(1, openingTime).SetUpdate(true);
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        
        private void OnDisable() {
            menuTween?.Kill();
        }

        private void OnDestroy() {
            menuTween?.Kill();
        }
    }
}
