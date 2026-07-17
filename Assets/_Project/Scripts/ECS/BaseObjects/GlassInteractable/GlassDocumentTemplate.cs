using UnityEngine;

public class GlassDocumentTemplate : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private GlassText text;

    public void SetUp(GlassDocumentScriptableObject data)
    {
        renderer.material = data.material;
        text.Setup(data);
    }
}
