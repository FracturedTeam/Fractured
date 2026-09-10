using _Project.Scripts.Systems.Singletons;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.UI
{
   public class MemoryManager : Singleton<MemoryManager> {
      private static readonly int ActiveMemory = Animator.StringToHash("ActiveMemory");
      
      [SerializeField] Animator animator;
      [SerializeField] Material memoryMat;
      [SerializeField] Material brokenScreenMat;
      
      public float targetScreenFraction = 0.3f;
      public float meshHeightAtScaleOne = 1f;

      public bool isInMemory { get; private set; }
      
      public void SetMemory(bool isOn, Sprite sprite = null, Sprite sprite2 = null) {
         if (!memoryMat)
              return;
         
         isInMemory = isOn;
         
         if(sprite) {
             memoryMat.SetTexture("_MemoryTexture", TextureFromSprite(sprite));
             brokenScreenMat.SetTexture("_MemoryTextureCOLOR", TextureFromSprite(sprite));
         }
         if(sprite2)
            brokenScreenMat.SetTexture("_MemoryTextureLINE", TextureFromSprite(sprite2));
         
         animator.SetBool(ActiveMemory, isOn);
         if(isOn) UpdateMemoryScale();
      }

      private void UpdateMemoryScale() {
         var cam = CinemachineBrain.GetActiveBrain(0).OutputCamera;
         
         var distance = Vector3.Dot(transform.position - cam.transform.position, cam.transform.forward);

         var fov = cam.fieldOfView;
         var visibleHeight= 2f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
         var targetWorld= visibleHeight * targetScreenFraction;
         var scale= targetWorld / meshHeightAtScaleOne;

         transform.localScale = Vector3.one * scale;
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
   }
}
