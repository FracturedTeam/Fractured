using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class ButtonUI : MonoBehaviour
    {
        private Vector3 scale;
        [SerializeField] private float time = 0.5f;
        [SerializeField] private  float multiplicator = 1.15f;
        
        Tweener tweener;
        
        private void Awake()
        {
            scale = transform.localScale;
        }

        public void OnHover(bool hovering)
        {
            tweener = transform.DOScale(hovering ? scale * multiplicator : scale, time).SetUpdate(true);
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
