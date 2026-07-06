using System;
using System.Collections;
using _Project.Scripts.Player;
using _Project.Scripts.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GlassText : MonoBehaviour
{
   [SerializeField] private GlassTextScriptableObject currentTextScriptableObject;

   [SerializeField] private TMP_Text baseText;
   [SerializeField] private TMP_Text fragAText;
   [SerializeField] private TMP_Text fragBText;
   [SerializeField] private TMP_Text bothText;
   [SerializeField] private CanvasGroup canvasGroup;
   int lastIndex = 0;
   private Camera camera;

   private void Start()
   {
      if(currentTextScriptableObject)
         Setup(currentTextScriptableObject);

      canvasGroup.alpha = 0;
      camera = PlayerController.Instance.cinemachineBrain.OutputCamera;
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

   private void Update()
   {
      if (Vector2.Distance(camera.WorldToScreenPoint(Mouse.current.position.value),  camera.WorldToScreenPoint(baseText.transform.position)) < 5)
      {
         Check();
      }
   }

   public void Check()
   {
      Debug.Log("Entered");
      var newIndex = TMP_TextUtilities.FindIntersectingLink(baseText,Mouse.current.position.value, camera);
      if (newIndex == -1)
         return;

      newIndex = TMP_TextUtilities.FindIntersectingWord(baseText,Mouse.current.position.value, camera);
      if (lastIndex == newIndex)
         return;
      lastIndex = newIndex;

      TMP_WordInfo wordInfo = baseText.textInfo.wordInfo[lastIndex];
      var w = wordInfo.textComponent.text.Substring(wordInfo.firstCharacterIndex, wordInfo.characterCount);
      var nw = string.Format("<u><b>{0}</b></u>", w);
      baseText.text = baseText.text.Replace(w, nw);
      Debug.Log(w);
   }

}
