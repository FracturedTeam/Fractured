using System;
using _Project.Scripts.Systems.EventBus;
using _Project.Scripts.Systems.Singletons;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace _Project.Scripts.GameServices {
    public struct ManageAmbientAudio : IEvent {
        public bool ambientSoundZone1;
        public bool ambientSoundCoffin;
        public bool ambientSoundTuto;
    }

    public struct MemorySound : IEvent {
        public bool inMemory;
    }
    
    public class AudioManager : PersistentSingleton<AudioManager> {
        [Header("One Shot Sounds")]
        [Header("Glass Sounds")]
        [SerializeField] private EventReference grabGlassSound;
        [SerializeField] private EventReference grabGlassFailedSound;
        [SerializeField] private EventReference revealSound;
        [SerializeField] private EventReference hideSound;
        [SerializeField] private EventReference breakGlassSound;
        
        [Header("Small Doors Sounds")]
        [SerializeField] private EventReference lockedSmallDoorSound;
        [SerializeField] private EventReference openSmallDoorSound;
        
        [Header("Big Doors Sounds")]
        [SerializeField] private EventReference lockedBigDoorSound;
        [SerializeField] private EventReference openBigDoorSound;
        
        [Header("Moveable Object Sounds")]
        [SerializeField] private EventReference pickUpObjectSound;
        [SerializeField] private EventReference pickUpKeySound;
        [SerializeField] private EventReference dropObjectSound;
        
        [Header("Memory Sounds")]
        [SerializeField] private EventReference reconstructMemorySound;
        [SerializeField] private EventReference enterMemorySound;
        
        [Header("Pressure Plate Sounds")]
        [SerializeField] private EventReference pressurePlateActiveSound;
        [SerializeField] private EventReference pressurePlateInactiveSound;
        
        [Header("Looping Sounds")]
        [SerializeField] private EventReference memoryLoopSound;
        [SerializeField] private EventReference ambientSoundZone;
        [SerializeField] private EventReference ambientSoundTutorial;
        [SerializeField] private EventReference ambientSoundTutorialCoffin;
        
        private EventInstance memorySoundInstance;
        private EventInstance ambientSoundZoneInstance;
        private EventInstance ambientSoundTutorialInstance;
        private EventInstance ambientSoundTutorialCoffinInstance;

        private EventBinding<MemorySound> memoryEventBinding;
        private EventBinding<ManageAmbientAudio> ambientEventBinding;
        
        #region OneShot Sounds
        public void PlayOneShot(EventReference sound, Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(sound, worldPosition);
        }

        #region Glass
        public void PlayGrabGlassSound() => RuntimeManager.PlayOneShot(grabGlassSound);
        public void PlayGrabGlassFailedSound() => RuntimeManager.PlayOneShot(grabGlassFailedSound);
        public void PlayRevealObjectSound(Vector3 worldPosition) => RuntimeManager.PlayOneShot(revealSound, worldPosition);
        public void PlayHideObjectSound(Vector3 worldPosition) => RuntimeManager.PlayOneShot(hideSound, worldPosition);
        public void PlayBreakGlassSound(Vector3 worldPosition) => RuntimeManager.PlayOneShot(breakGlassSound, worldPosition);

        #endregion
        
        #region Doors
        public void PlayLockedSmallSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(lockedSmallDoorSound, worldPosition);
        }   
        
        public void PlayOpenSmallSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(openSmallDoorSound, worldPosition);
        }
        
        public void PlayLockedBigSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(lockedBigDoorSound, worldPosition);
        }   
        
        public void PlayOpenBigSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(openBigDoorSound, worldPosition);
        }
        #endregion
        
        public void PlayPickUpSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(pickUpObjectSound, worldPosition);
        }   
        
        public void PlayPickUpKeySound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(pickUpKeySound, worldPosition);
        }  
        
        public void PlayDropSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(dropObjectSound, worldPosition);
        } 
        
        public void PlayReconstructMemorySound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(reconstructMemorySound, worldPosition);
        } 
        
        public void PlayEnterMemorySound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(enterMemorySound, worldPosition);
        } 
        
        public void PlayPlateActiveSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(pressurePlateActiveSound, worldPosition);
        } 
        
        public void PlayPlateInactiveSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(pressurePlateInactiveSound, worldPosition);
        }
#endregion

        private void Start() {
            memorySoundInstance = CreateInstance(memoryLoopSound);
            ambientSoundZoneInstance = CreateInstance(ambientSoundZone);
            ambientSoundTutorialInstance = CreateInstance(ambientSoundTutorial);
            ambientSoundTutorialCoffinInstance = CreateInstance(ambientSoundTutorialCoffin);
        }

        private void OnEnable() {
            memoryEventBinding = new EventBinding<MemorySound>(UpdateMemory);
            EventBus<MemorySound>.Register(memoryEventBinding);
            ambientEventBinding = new EventBinding<ManageAmbientAudio>(UpdateAmbient);
            EventBus<ManageAmbientAudio>.Register(ambientEventBinding);
        }

        private void OnDisable() {
            EventBus<MemorySound>.Deregister(memoryEventBinding);
            EventBus<ManageAmbientAudio>.Deregister(ambientEventBinding);
        }
        
        public EventInstance CreateInstance(EventReference reference) {
            var instance = RuntimeManager.CreateInstance(reference);
            return instance;
        }

        private void UpdateMemory(MemorySound m) {
            if (m.inMemory) {
                memorySoundInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    memorySoundInstance.start();
                }
            }
            else
                memorySoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
        
        private void UpdateAmbient(ManageAmbientAudio m) {
            if (m.ambientSoundCoffin) {
                ambientSoundTutorialCoffinInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    ambientSoundTutorialCoffinInstance.start();
                }
                ambientSoundTutorialInstance.stop(STOP_MODE.ALLOWFADEOUT);
                ambientSoundZoneInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
            else if (m.ambientSoundTuto) {
                ambientSoundTutorialInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    ambientSoundTutorialInstance.start();
                }
                ambientSoundTutorialCoffinInstance.stop(STOP_MODE.ALLOWFADEOUT);
                ambientSoundZoneInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
            else if (m.ambientSoundZone1){
                ambientSoundZoneInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    ambientSoundZoneInstance.start();
                }
                ambientSoundTutorialCoffinInstance.stop(STOP_MODE.ALLOWFADEOUT);
                ambientSoundTutorialInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
            else {
                ambientSoundTutorialCoffinInstance.stop(STOP_MODE.ALLOWFADEOUT);
                ambientSoundTutorialInstance.stop(STOP_MODE.ALLOWFADEOUT);
                ambientSoundZoneInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}