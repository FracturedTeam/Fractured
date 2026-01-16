using System.Collections.Generic;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class Fragment : MonoBehaviour {
        [SerializeField] private List<Transform> corners;
        [SerializeField] private GameObject frag;

        public void Setup(List<Vector3> positions) {
            if(positions.Count != corners.Count ){
                Debug.LogError("Wrong number of corners, are you sure the right fragment prefab is selected ?");
                return;
            }

            for (var i = 0; i < corners.Count; i++) { 
                corners[i].position = positions[i];
            }
        }

        public void SetColor(ColorEnum color)
        {
            if (!frag)
                return;
            
            frag.layer = color == ColorEnum.Blue ? LayerMask.NameToLayer("Fragment Color A") : color == ColorEnum.Red ? LayerMask.NameToLayer("Fragment Color B") :  LayerMask.NameToLayer("Default");
        }
        }
    }
