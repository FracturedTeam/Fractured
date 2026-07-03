using System;
using System.Collections;
using _Project.Scripts.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GlassText : MonoBehaviour
{
   [SerializeField] private GlassTextScriptableObject currentTextScriptableObject;

   [SerializeField] private TMP_Text baseText;
   [SerializeField] private TMP_Text fragAText;
   [SerializeField] private TMP_Text fragBText;
   [SerializeField] private TMP_Text bothText;
   [SerializeField] private CanvasGroup canvasGroup;

   private void Start()
   {
      if(currentTextScriptableObject)
         Setup(currentTextScriptableObject);

      canvasGroup.alpha = 0;
   }

   public void Setup(GlassTextScriptableObject newData)
   {
      canvasGroup.DOFade(0, 0);
      currentTextScriptableObject = newData;
      baseText.text = currentTextScriptableObject.baseText;
      fragAText.text = currentTextScriptableObject.fragAText;
      fragBText.text = currentTextScriptableObject.fragBText;
      bothText.text = currentTextScriptableObject.bothText;
      canvasGroup.DOFade(1, 1);
   }
   
}
