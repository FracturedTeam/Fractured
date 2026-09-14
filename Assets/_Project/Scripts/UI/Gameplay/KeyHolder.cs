using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay {
    public class KeyHolder : MonoBehaviour {
        public int ID = -1;
        public Image icon;
        
        public void ResetKey() {
            ID = -1;
            icon = null;
        }
        
        public void SetKey(Key key) {
            icon.sprite = key.keySprite;
            ID = key.ID;
        }

    }
}