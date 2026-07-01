using System;
using _Project.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

public class GlassText : MonoBehaviour
{
   [SerializeField] private GlassTextScriptableObject currentTextScriptableObject;

   [SerializeField] private TMP_Text baseText;
   [SerializeField] private TMP_Text fragAText;
   [SerializeField] private TMP_Text fragBText;
   [SerializeField] private TMP_Text bothText;

   private void Start()
   {
      if(currentTextScriptableObject)
         Setup();
   }

   public void Setup()
   {
      baseText.text = currentTextScriptableObject.baseText;
      fragAText.text = currentTextScriptableObject.fragAText;
      fragBText.text = currentTextScriptableObject.fragBText;
      bothText.text = currentTextScriptableObject.bothText;
   }
}
