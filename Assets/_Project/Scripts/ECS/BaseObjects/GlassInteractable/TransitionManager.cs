using System;
using _Project.Scripts.Player;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private TransitionPosition pos1;
    [SerializeField] private TransitionPosition pos2;
    [SerializeField] private Material mat;

    private void Update()
    {
        if(pos1 && pos2)
        {
            var dist = Vector3.Distance(pos1.transform.position, pos2.transform.position);
            var i = Mathf.Clamp(PlayerController.Instance.transform.position.z, pos1.transform.position.z,
                pos2.transform.position.z);
            
            print(-(i/dist));
            mat.SetFloat("_ActGlobalTransition", Mathf.Clamp(-(i/dist), 0, 1));
            
        }
    }
}
