using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class MenuAnimation : MonoBehaviour
    {
        List<Image> images = new ();
        List<TMP_Text> texts = new ();
        [SerializeField] private float openingTime = 2;
        [SerializeField] private float closingTime = 1;
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if(child.TryGetComponent(typeof(Image), out var img))
                    images.Add(img as Image);
                if(child.TryGetComponent(typeof(TMP_Text), out var txt))
                    texts.Add(txt as TMP_Text);
            }
            foreach (var image in  images)
            {
                image.DOFade(0, 0);
            }
            foreach (var text in  texts)
            {
                text.DOFade(0, 0);
            }
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            foreach (var image in  images)
            {
                image.DOFade(1, openingTime);
            }
            foreach (var text in  texts)
            {
                text.DOFade(1, openingTime);
            }
        }

        public void Close()
        {
            foreach (var image in  images)
            {
                image.DOFade(0, closingTime);
            }
            foreach (var text in  texts)
            {
                text.DOFade(0, closingTime);
            }
            Invoke(nameof(Closed), closingTime);
        }

        private void Closed() => gameObject.SetActive(false);
    }
}
