using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class SliderImage : MonoBehaviour
    {
        [SerializeField] private List<Sprite> imageList;
        [SerializeField] private Slider slider;
        private Image image;

        private void Start()
        {
            image =  GetComponent<Image>();
            NewValue();
        }

        public void NewValue()
        {
            if (imageList.Count > 0 && slider)
                image.sprite = imageList[Mathf.CeilToInt(slider.value * (imageList.Count-1))];
        }
    }
}
