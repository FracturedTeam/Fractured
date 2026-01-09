using _Project.Scripts.Systems.Singletons;
using FMODUnity;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class AudioManager : PersistentSingleton<AudioManager> {
        
        [Header("Glass Sounds")]
        [SerializeField] private EventReference grabGlassSound;
        [SerializeField] private EventReference dropGlassSound;
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
        [SerializeField] private EventReference enterMemorySound;
        [SerializeField] private EventReference reconstructMemorySound;
        
        [Header("Pressure Plate Sounds")]
        [SerializeField] private EventReference pressurePlateActiveSound;
        [SerializeField] private EventReference pressurePlateInactiveSound;
        
        public void PlayOneShot(EventReference sound, Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(sound, worldPosition);
        }

        #region Glass
        public void PlayGrabGlassSound() {
            RuntimeManager.PlayOneShot(grabGlassSound);
        }
        
        public void PlayDropGlassSound() {
            RuntimeManager.PlayOneShot(dropGlassSound);
        }
        
        public void PlayRevealObjectSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(revealSound, worldPosition);
        }
        
        public void PlayHideObjectSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(hideSound, worldPosition);
        }

        public void PlayBreakGlassSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(breakGlassSound, worldPosition);
        }
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
        
        public void PlayEnterMemorySound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(enterMemorySound, worldPosition);
        } 
        
        public void PlayReconstructMemorySound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(reconstructMemorySound, worldPosition);
        } 
        
        public void PlayPlateActiveSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(enterMemorySound, worldPosition);
        } 
        
        public void PlayPlateInactiveSound(Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(reconstructMemorySound, worldPosition);
        } 
    }
}