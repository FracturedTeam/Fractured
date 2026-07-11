using UnityEngine;
using UnityEngine.UI;

public class GlassDocument : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private GlassText glassText;

    public void SetUp(GlassDocumentScriptableObject data)
    {
        glassText.Setup(data);
        background.sprite = data.sprite;
    }
}