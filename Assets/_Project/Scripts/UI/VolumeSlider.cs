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
        
        private void Awake() {
            slider = GetComponent<Slider>();

            slider.value = volumeType switch {
                VolumeType.Master => GameInitializer.Instance.GetMasterVolume(),
                VolumeType.Sfx => GameInitializer.Instance.GetSFXVolume(),
                VolumeType.Music => GameInitializer.Instance.GetMusicVolume(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void OnSliderValueChanged() {
            switch (volumeType) {
                case VolumeType.Master:
                    GameInitializer.Instance.SetMasterVolume(slider.value);
                    break;
                case VolumeType.Sfx:
                    GameInitializer.Instance.SetSFXVolume(slider.value);
                    break;
                case VolumeType.Music:
                    GameInitializer.Instance.SetMusicVolume(slider.value);
                    break;
                default:
                    break;
            }
        }
    }
}