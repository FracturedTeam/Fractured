using System.Collections.Generic;
using _Project.Scripts.ECS;
using _Project.Scripts.Enums;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.HashSetUtil;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.UI
{
    public class SubtitleText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text = new ();
        internal Vector2 TagPositions;
        private DialogueScriptableObject dialogue;
        public CanvasGroup textCanva;
        private string show;
        private string BaseText;

        private Tweener tween;

        private void Start() {
            textCanva.alpha = 0;
        }
    

        public void Setup(DialogueScriptableObject scriptableObject) {
            tween?.Kill();
        
            if (scriptableObject == null) {
                tween = textCanva.DOFade(0, 0.5f);
                textCanva.blocksRaycasts = false;
                textCanva.interactable = false;
            
                //text.text = "";
                dialogue = null;
                return;
            }
        
            tween = textCanva.DOFade(1, 0.5f);
            textCanva.blocksRaycasts = true;
            textCanva.interactable = true;
        
            dialogue = scriptableObject;
            BaseText =  scriptableObject.dialogue;
            text.text = BaseText;
        
            SetText();
        }
    
        private void Update()
        {
            SetText();
        }
        
        private void OnDisable() {
            tween?.Kill();
        }
    
        private void SetText()
        {
            if(!dialogue)
                return;
            text.text = BaseText;
        }
    }
}
