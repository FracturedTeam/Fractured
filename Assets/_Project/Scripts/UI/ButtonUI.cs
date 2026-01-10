using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private Sprite baseSprite;
        [SerializeField] private Sprite hoverSprite;
        [SerializeField] private Sprite clickedSprite;
        private Vector3 scale;
        [SerializeField] private float time = 0.5f;
        [SerializeField] private  float multiplicator = 1.15f;
        
        Tweener tweener;
        private Image backgroundImage;
        
        private void Awake()
        {
            scale = transform.localScale;
            backgroundImage = GetComponent<Image>();
        }

        public void OnHover(bool hovering)
        {
            //tweener = transform.DOScale(hovering ? scale * multiplicator : scale, time).SetUpdate(true);
            backgroundImage.sprite = hoverSprite;
            
        }
        public void OnClicked()
        {
            //tweener = transform.DOScale(hovering ? scale * multiplicator : scale, time).SetUpdate(true);
            backgroundImage.sprite = clickedSprite;
        }

        private void OnDestroy() {
            tweener.Kill();
            tweener = null;
        }

        private void OnDisable()
        {
            OnHover(false);
            tweener.Kill();
            tweener = null;
        }
    }
}
