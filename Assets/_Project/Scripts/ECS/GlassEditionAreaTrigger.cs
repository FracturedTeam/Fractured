    using System;
    using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.ECS {
    public class GlassEditionAreaTrigger : MonoBehaviour {
        [SerializeField] private ColorEnum colorEdition;
        [SerializeField] private Material screenEffectMat;

        private float fadeTime = 1.0f;
        private float fadeTimer = 0.0f;
        private bool inZone = false;
        
        void OnEnable() {
            screenEffectMat.SetFloat("_Progression", 0f);
        }
        
        void OnDisable() {
            screenEffectMat.SetFloat("_Progression", 0f);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                GameInitializer.Instance.SetEditableArea(true, colorEdition);
                inZone = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                GameInitializer.Instance.SetEditableArea(false, colorEdition);
                inZone = false;
            }
        }

        private void Update() {
            fadeTimer = inZone ? Mathf.Clamp(fadeTimer + Time.deltaTime, 0, fadeTime):
            fadeTimer = Mathf.Clamp(fadeTimer - Time.deltaTime, 0, fadeTime);
            
            screenEffectMat.SetFloat("_Progression", fadeTimer);
        }
    }
}