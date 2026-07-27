using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSprite : MonoBehaviour
{
   [SerializeField] private List<Sprite> sprites;
   private Image image;

   private void Start()
   {
       image = GetComponent<Image>();
   }


   public void Swap(int number)
   { 
       if(image && sprites.Count >= number) 
           image.sprite = sprites[number];
   }
}
