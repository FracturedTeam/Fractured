using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class GlassDocument : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private GlassText glassText;
    [SerializeField] private float distance;
    private Transform cameraTrans;
    
    public void SetUp(GlassDocumentScriptableObject data)
    {
        cameraTrans = PlayerController.Instance.cinemachineBrain.OutputCamera.transform;
        glassText.Setup(data);
        glassText.Appear();
        transform.position = cameraTrans.position + cameraTrans.forward * distance;
        transform.LookAt(cameraTrans);
        transform.eulerAngles = new Vector3(20, 180 + transform.eulerAngles.y, 0);
        //background.sprite = data.sprite;
    }
}