using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class MenuAnimation : MonoBehaviour
    {
        [SerializeField] private float openingTime = 2;
        [SerializeField] private float closingTime = 1;

        private CanvasGroup canvasGroup;
        private Tweener menuTween;
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            menuTween = canvasGroup.DOFade(1, openingTime).SetUpdate(true);
        }

        public void Close()
        {
            menuTween =  canvasGroup.DOFade(0, closingTime).SetUpdate(true);
            Invoke(nameof(Closed), closingTime);
        }

        private void Closed() => gameObject.SetActive(false);

        private void OnDisable() {
            menuTween?.Kill();
        }

        private void OnDestroy() {
            menuTween?.Kill();
        }
    }
}
