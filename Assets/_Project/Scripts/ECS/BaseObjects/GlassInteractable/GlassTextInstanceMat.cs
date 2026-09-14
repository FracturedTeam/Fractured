using DG.Tweening;
using UnityEngine;

public class GlassTextInstanceMat : MonoBehaviour
{
    Material materialParticle;
    Material materialMesh;
    
    private void Start()
    {
        materialParticle = transform.GetChild(0).GetComponent<Renderer>().material;
        materialParticle.SetFloat("_Opacity", 0f);
        materialParticle.DOFloat(1f, "_Opacity", 1f);
        
        materialMesh = transform.GetChild(1).GetComponent<Renderer>().material;
        materialMesh.SetFloat("_Opacity", 0f);
        materialMesh.DOFloat(1f, "_Opacity", 1f);
    }
}
