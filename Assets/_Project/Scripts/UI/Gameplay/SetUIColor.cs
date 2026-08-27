using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay {
    public class SetUIColor : MonoBehaviour {
        [SerializeField] private Image[] allColoredSprites;
        [SerializeField] private TextMeshProUGUI[] allColoredTexts;

        public void SetSpriteColor(Color color) {
            foreach (var sprite in allColoredSprites) {
                var alpha = sprite.color.a;
                var newColor = new Color(color.r, color.g, color.b, alpha);
                sprite.color = newColor;
            }

            foreach (var text in allColoredTexts) {
                text.color = new Color(color.r, color.g, color.b, text.color.a);
            }
        }
    }
}