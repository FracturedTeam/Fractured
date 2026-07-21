using System;
using System.Collections.Generic;
using _Project.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;


public class TransitionManager : MonoBehaviour
{
    [SerializeField] private TransitionPosition pos1;
    [SerializeField] private TransitionPosition pos2;
    [SerializeField] private List<Material> mat;
    private bool secondColor;

    private void Update()
    {
        
        if(pos1 && pos2)
        {
            var dist = Vector3.Distance(pos1.transform.position, pos2.transform.position);
            
            var current = ((pos2.transform.position.z -PlayerController.Instance.transform.position.z)/dist);
            
            switch (current)
            {
                case > .5f when !secondColor:
                {
                    secondColor = true;
                    foreach (var material in mat)
                        material.SetFloat("_CurrentAct", pos1.act+1);
                    break;
                }
                case < .5f when secondColor:
                {
                    secondColor = false;
                    foreach (var material in mat)
                        material.SetFloat("_CurrentAct", pos2.act+1);
                    break;
                }
            }
            
            foreach (var material in mat)
                material.SetFloat("_ActGlobalTransition", Mathf.Clamp(current < .5f ? current * 2 : 2 * (1-current) , 0 ,  1 ));
           
            
        }
    }
}
