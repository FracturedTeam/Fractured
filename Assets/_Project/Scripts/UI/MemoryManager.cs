using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;

namespace _Project.Scripts.UI
{
   public class MemoryManager : Singleton<MemoryManager> {
      private static readonly int ActiveMemory = Animator.StringToHash("ActiveMemory");
      [SerializeField] Animator animator;
      [SerializeField] Material memoryMat;
      [SerializeField] Material brokenScreenMat;
      
      Dictionary<int, bool> memories = new Dictionary<int, bool>();
      public bool isInMemory { get; private set; }

      public void SetMemory(bool isOn, int id = 0, Sprite sprite = null, Sprite sprite2 = null) {
         if (!memoryMat)
              return;
         
         memories.TryAdd(id, true);
         memories[id] = true;

         isInMemory = isOn;
         
         if(sprite) {
             memoryMat.SetTexture("_MemoryTexture", TextureFromSprite(sprite));
             memoryMat.SetTexture("_MemoryTexture", TextureFromSprite(sprite));
             brokenScreenMat.SetTexture("_MemoryTexture", TextureFromSprite(sprite));
         }
         
         animator.SetBool(ActiveMemory, isOn);
      }

      private static Texture2D TextureFromSprite(Sprite sprite) {
         if (Mathf.Approximately(sprite.rect.width, sprite.texture.width)) 
            return sprite.texture;
         
         var newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
         var newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
            (int)sprite.textureRect.y, 
            (int)sprite.textureRect.width, 
            (int)sprite.textureRect.height );
         newText.SetPixels(newColors);
         newText.Apply();
         
         return newText;
      }

      public bool IsUnlockedMemory(int id)
      {
         if (!memories.ContainsKey(id))
            memories.Add(id, false);
         
         return memories[id];
      }
      
   }
}
