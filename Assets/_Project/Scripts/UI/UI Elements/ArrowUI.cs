using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class ArrowUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
        [SerializeField] private Image hoverImage;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite hoverSprite;
        [SerializeField] private bool isRightArrow;
        
        [SerializeField] private DropDownUI parentDropDownUI;
        
        private void OnEnable() {
            hoverImage.sprite = normalSprite;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            hoverImage.sprite = hoverSprite;
        }

        public void OnPointerExit(PointerEventData eventData) {
            hoverImage.sprite = normalSprite;
        }
        
        public void OnPointerDown(PointerEventData eventData) {
            parentDropDownUI.UpdateIndex(isRightArrow);
        }
    }
}