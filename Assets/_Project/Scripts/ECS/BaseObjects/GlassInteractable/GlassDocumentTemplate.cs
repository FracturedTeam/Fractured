using UnityEngine;
using UnityEngine.Serialization;

public class GlassDocumentTemplate : MonoBehaviour
{
    [FormerlySerializedAs("renderer")] [SerializeField] private MeshRenderer render;
    [SerializeField] private GlassText text;

    public void SetUp(GlassDocumentScriptableObject data)
    {
        render.material = data.material;
        text.Setup(data);
    }
}
