using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class InteractionPopUp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactionText;
        public CanvasGroup GetGroup { get; private set; }
        public TextMeshProUGUI GetInteractionText { get; private set; }
        
        private void Awake()
        {
            GetGroup = GetComponent<CanvasGroup>();
            GetInteractionText = interactionText;
        }

        public void SetData(string text)
        {
            interactionText.text = text;
        }
    }
}
