using System;
using _Project.Scripts.GameServices;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI {
    public class VolumeSlider : MonoBehaviour {
        private Slider slider;

        private enum VolumeType {
            Master,
            Sfx,
            Music
        }
        
        [SerializeField] private VolumeType volumeType;
        
        private void Start() {
            slider = GetComponent<Slider>();

            slider.value = GameInitializer.Instance.GetVolume((int)volumeType);
        }

        public void OnSliderValueChanged() {
            
            GameInitializer.Instance.SetVolume((int)volumeType, slider.value);
        }
    }
}