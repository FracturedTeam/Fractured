using System;
using System.Collections;
using _Project.Scripts.GameServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
        private EventTrigger button;
        
        Tweener tweener;
        private Image backgroundImage;
        
        private void Awake()
        {
            scale = transform.localScale;
            if(TryGetComponent(typeof(Image), out var bgImage))
                backgroundImage = (Image)bgImage;
            if(TryGetComponent(typeof(EventTrigger), out var btn))
                button = (EventTrigger)btn;
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
            
            AudioManager.Instance.PlayBttClikedSound();
            
            button.enabled = false;
            
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

        private void OnEnable()
        {
            //sometimes the OnHover false of the disable doesn't work, this fixes it 
            tweener = transform.DOScale(scale, 0).SetUpdate(true);
            if(backgroundImage)
                backgroundImage.sprite = baseSprite;
            
            button.enabled = true;
        }

        private void OnDisable()
        {
            OnHover(false);
            tweener.Kill();
            tweener = null;
        }
    }
}
