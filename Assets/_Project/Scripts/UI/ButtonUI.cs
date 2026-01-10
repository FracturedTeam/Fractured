using System;
using System.Collections;
using _Project.Scripts.Systems.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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
        public UnityEvent onClickPostTimer;
        
        Tweener tweener;
        private Image backgroundImage;
        
        private void Awake()
        {
            scale = transform.localScale;
            if(TryGetComponent(typeof(Image), out var bgImage))
                backgroundImage = (Image)bgImage;
        }

        public void OnHover(bool hovering)
        {
            tweener = transform.DOScale(hovering ? scale * multiplicator : scale, time).SetUpdate(true);
            
            if(backgroundImage)
                backgroundImage.sprite = hovering ? hoverSprite: baseSprite;

        }
        public void OnClicked()
        {
            
            if(backgroundImage)
                backgroundImage.sprite = clickedSprite;
            
            tweener = transform.DOScale(transform.localScale * multiplicator, time).SetUpdate(true);
            StartCoroutine(CallClickPostTimer());
        }
        
        private IEnumerator CallClickPostTimer()
        { 
            yield return new WaitForSecondsRealtime(time);
           onClickPostTimer?.Invoke();
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
