using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.UI {
    public class Fragment : MonoBehaviour {
        [SerializeField] private List<Transform> corners;
        [SerializeField] private GameObject frag;
        [SerializeField] private Renderer render;

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

            switch (color)
            {
                case ColorEnum.Blue:
                    frag.layer = LayerMask.NameToLayer("Fragment Color A");
                    render.material = GameInitializer.Instance.GetCurrentFragmentMaterial(false, 1);
                    break;
                case ColorEnum.Red:
                    frag.layer = LayerMask.NameToLayer("Fragment Color B");
                    render.material = GameInitializer.Instance.GetCurrentFragmentMaterial(true, 1);
                    break;
                case ColorEnum.Both:
                default:
                    frag.layer = LayerMask.NameToLayer("Default");
                    break;
            }
        }
        }
    }
