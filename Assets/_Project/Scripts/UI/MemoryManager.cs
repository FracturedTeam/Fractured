using UnityEngine;

namespace _Project.Scripts.UI
{
   public class MemoryManager : MonoBehaviour
   {
      private static readonly int ActiveMemory = Animator.StringToHash("ActiveMemory");
      [SerializeField] Animator animator;
      [SerializeField] ParticleSystemRenderer memory;

      public static MemoryManager instance;

      private void Awake()
      {
         if(!instance)  instance = this;
         else Destroy(this);
      }

      public void SetMemory(bool isOn, Sprite sprite = null)
      {
         if(sprite)
         {
            var texture = TextureFromSprite(sprite);
            memory.material.mainTexture = texture;
         }
         animator.SetBool(ActiveMemory, isOn);
      }

      private static Texture2D TextureFromSprite(Sprite sprite)
      {
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
   }
}
