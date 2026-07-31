using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class SettingsBaseUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image image;
        [SerializeField] private Material backgroundHoverMat;
        
        [SerializeField] private Material normalTextMat;
        [SerializeField] private Material hoverTextMat;

        public void OnEnable() {
            text.material = normalTextMat;
            image.material = null;
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            text.material = hoverTextMat;
            image.material = backgroundHoverMat;
        }

        public void OnPointerExit(PointerEventData eventData) {
            text.material = normalTextMat;
            image.material = null;
        }
    }
}