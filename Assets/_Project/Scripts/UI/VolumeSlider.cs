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
                VolumeType.Master => AudioManager.Instance.masterVolume,
                VolumeType.Sfx => AudioManager.Instance.sfxVolume,
                VolumeType.Music => AudioManager.Instance.musicVolume,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void OnSliderValueChanged() {
            switch (volumeType) {
                case VolumeType.Master:
                    AudioManager.Instance.masterVolume = slider.value;
                    break;
                case VolumeType.Sfx:
                    AudioManager.Instance.sfxVolume = slider.value;
                    break;
                case VolumeType.Music:
                    AudioManager.Instance.musicVolume = slider.value;
                    break;
                default:
                    break;
            }
        }
    }
}