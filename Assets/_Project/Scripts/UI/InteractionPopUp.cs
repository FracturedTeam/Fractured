using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class InteractionPopUp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactionText;
        [SerializeField] private Image interactionImage;
        public CanvasGroup GetGroup { get; private set; }
        public Image GetInteractionImage { get; private set; }
        public TextMeshProUGUI GetInteractionText { get; private set; }
        
        private void Awake()
        {
            GetGroup = GetComponent<CanvasGroup>();
            GetInteractionText = interactionText;
            GetInteractionImage =  interactionImage;
        }

        public void SetData(string text)
        {
            interactionText.text = text;
        }
        
        public void SetData(Sprite image)
        {
            interactionImage.sprite = image;
        }

    }
}
