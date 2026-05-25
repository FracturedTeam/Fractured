// using System;
// using _Project.Scripts.Systems.EventBus;
// using _Project.Scripts.Systems.Singletons;
// using _Project.Scripts.Systems.Timers;
// using FMOD.Studio;
// using FMODUnity;
// using UnityEngine;
// using STOP_MODE = FMOD.Studio.STOP_MODE;
//
// namespace _Project.Scripts.GameServices {
//
//     public enum Loop {
//         ambientZone1,
//         ambientTuto,
//         ambientCoffin,
//         credits,
//         mainMenu
//     }
//     
//     public struct ManageAmbientAudio : IEvent {
//         public Loop loop;
//     }
//
//     public struct MemorySound : IEvent {
//         public bool inMemory;
//     }
//
//     public struct EditableSound : IEvent {
//         public bool inEditable;
//     }
//     
//     public class AudioManager : PersistentSingleton<AudioManager> {
//         [Header("Volumes")] 
//         [Range(0, 1)] public float masterVolume = 1;
//         [Range(0, 1)] public float sfxVolume = 1;
//         [Range(0, 1)] public float musicVolume = 1;
//
//         private Bus masterBus;
//         private Bus sfxBus;
//         private Bus musicBus;
//         
//         private EventInstance memorySoundInstance;
//         private EventInstance ambientZone1Instance;
//         private EventInstance ambientTutorialInstance;
//         private EventInstance ambientCoffinInstance;
//         private EventInstance menuInstance;
//         private EventInstance editableInstance;
//         private EventInstance creditsInstance;
//
//         private EventBinding<MemorySound> memoryEventBinding;
//         private EventBinding<ManageAmbientAudio> ambientEventBinding;
//         private EventBinding<EditableSound> editableEventBinding;
//
//         readonly CountdownTimer revealObjectTimer = new CountdownTimer(1f);
//         readonly CountdownTimer hideObjectTimer = new CountdownTimer(1f);
//             
//         #region OneShot Sounds
//         public void PlayOneShot(EventReference sound, Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(sound, worldPosition);
//         }
//         public void PlayFootStepSound(Vector3 worldPosition) => RuntimeManager.PlayOneShot(playerFootstepSound, worldPosition);
//         public void PlayBttClikedSound() => RuntimeManager.PlayOneShot(uiBttClickedSound);
//
//         #region Glass
//         public void PlayGrabGlassSound() => RuntimeManager.PlayOneShot(grabGlassSound);
//         public void PlayGrabGlassFailedSound() => RuntimeManager.PlayOneShot(grabGlassFailedSound);
//
//         public void PlayRevealObjectSound(Vector3 worldPosition) {
//             if(revealObjectTimer.IsRunning) return;
//             RuntimeManager.PlayOneShot(revealSound, worldPosition);
//             revealObjectTimer.Start();
//         }
//
//         public void PlayHideObjectSound(Vector3 worldPosition) {
//             if(hideObjectTimer.IsRunning) return;
//             RuntimeManager.PlayOneShot(hideSound, worldPosition);
//             hideObjectTimer.Start();
//         }
//
//         public void PlayBreakGlassSound(Vector3 worldPosition) => RuntimeManager.PlayOneShot(breakGlassSound, worldPosition);
//
//         #endregion
//         
//         #region Doors
//         public void PlayLockedSmallSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(lockedSmallDoorSound, worldPosition);
//         }   
//         
//         public void PlayOpenSmallSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(openSmallDoorSound, worldPosition);
//         }
//         
//         public void PlayLockedBigSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(lockedBigDoorSound, worldPosition);
//         }   
//         
//         public void PlayOpenBigSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(openBigDoorSound, worldPosition);
//         }
//         #endregion
//         
//         public void PlayPickUpSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(pickUpObjectSound, worldPosition);
//         }   
//         
//         public void PlayPickUpKeySound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(pickUpKeySound, worldPosition);
//         }  
//         
//         public void PlayDropSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(dropObjectSound, worldPosition);
//         } 
//         
//         public void PlayReconstructMemorySound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(reconstructMemorySound, worldPosition);
//         } 
//         
//         public void PlayEnterMemorySound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(enterMemorySound, worldPosition);
//         } 
//         
//         public void PlayLeaveMemorySound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(leaveMemorySound, worldPosition);
//         } 
//         
//         public void PlayPlateActiveSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(pressurePlateActiveSound, worldPosition);
//         } 
//         
//         public void PlayPlateInactiveSound(Vector3 worldPosition) {
//             RuntimeManager.PlayOneShot(pressurePlateInactiveSound, worldPosition);
//         }
// #endregion
//
//         protected override void Awake() {
//             base.Awake();
//             masterBus = RuntimeManager.GetBus("bus:/");
//             musicBus = RuntimeManager.GetBus("bus:/Ambiance");
//             sfxBus = RuntimeManager.GetBus("bus:/SFX");
//         }
//
//         private void Start() {
//             memorySoundInstance = CreateInstance(memoryLoopSound);
//             ambientZone1Instance = CreateInstance(ambientSoundZone);
//             ambientTutorialInstance = CreateInstance(ambientSoundTutorial);
//             ambientCoffinInstance = CreateInstance(ambientSoundTutorialCoffin);
//             menuInstance = CreateInstance(menuLoop);
//             editableInstance = CreateInstance(editableLoop);
//             creditsInstance = CreateInstance(creditsLoop);
//         }
//
//         private void Update() {
//             masterBus.setVolume(masterVolume);
//             musicBus.setVolume(musicVolume);
//             sfxBus.setVolume(sfxVolume);
//         }
//
//         private void OnEnable() {
//             memoryEventBinding = new EventBinding<MemorySound>(UpdateMemory);
//             EventBus<MemorySound>.Register(memoryEventBinding);
//             ambientEventBinding = new EventBinding<ManageAmbientAudio>(UpdateAmbient);
//             EventBus<ManageAmbientAudio>.Register(ambientEventBinding);
//             editableEventBinding = new EventBinding<EditableSound>(UpdateEditable);
//             EventBus<EditableSound>.Register(editableEventBinding);
//         }
//
//         private void OnDisable() {
//             EventBus<MemorySound>.Deregister(memoryEventBinding);
//             EventBus<ManageAmbientAudio>.Deregister(ambientEventBinding);
//             EventBus<EditableSound>.Deregister(editableEventBinding);
//         }
//         
//         public EventInstance CreateInstance(EventReference reference) {
//             var instance = RuntimeManager.CreateInstance(reference);
//             return instance;
//         }
//         
//         private void UpdateEditable(EditableSound e) {
//             if (e.inEditable) {
//                 editableInstance.getPlaybackState(out var playbackState);
//                 if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
//                     editableInstance.start();
//                 }
//             }
//             else {
//                 editableInstance.getPlaybackState(out var playbackState);
//                 if (playbackState.Equals(PLAYBACK_STATE.PLAYING)) {
//                     editableInstance.stop(STOP_MODE.ALLOWFADEOUT);
//                 }
//             }
//         }
//         
//         private void UpdateMemory(MemorySound m) {
//             if (m.inMemory) {
//                 memorySoundInstance.getPlaybackState(out var playbackState);
//                 if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
//                     memorySoundInstance.start();
//                 }
//             }
//             else
//                 memorySoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
//         }
//         
//         private void UpdateAmbient(ManageAmbientAudio m) {
//             PLAYBACK_STATE playbackState;
//             
//             switch (m.loop) {
//                 case Loop.ambientZone1:
//                     ambientZone1Instance.getPlaybackState(out playbackState);
//                     if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) ambientZone1Instance.start();
//                     FadeLoop(ref ambientZone1Instance);
//                     break;
//                 case Loop.ambientTuto:
//                     ambientTutorialInstance.getPlaybackState(out playbackState);
//                     if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) ambientTutorialInstance.start();
//                     FadeLoop(ref ambientTutorialInstance);
//                     break;
//                 case Loop.ambientCoffin:
//                     ambientCoffinInstance.getPlaybackState(out playbackState);
//                     if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) ambientCoffinInstance.start();
//                     FadeLoop(ref ambientCoffinInstance);
//                     break;
//                 case Loop.credits:
//                     creditsInstance.getPlaybackState(out playbackState);
//                     if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) creditsInstance.start();
//                     FadeLoop(ref creditsInstance);
//                     break;
//                 case Loop.mainMenu:
//                     menuInstance.getPlaybackState(out playbackState);
//                     if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) menuInstance.start();
//                     FadeLoop(ref menuInstance);
//                     break;
//                 default:
//                     throw new ArgumentOutOfRangeException();
//             }
//         }
//
//         void FadeLoop(ref EventInstance e) {
//             if(e.handle != ambientCoffinInstance.handle) ambientCoffinInstance.stop(STOP_MODE.ALLOWFADEOUT);
//             if(e.handle != ambientTutorialInstance.handle) ambientTutorialInstance.stop(STOP_MODE.ALLOWFADEOUT);
//             if(e.handle != ambientZone1Instance.handle) ambientZone1Instance.stop(STOP_MODE.ALLOWFADEOUT);
//             if(e.handle != menuInstance.handle) menuInstance.stop(STOP_MODE.ALLOWFADEOUT);
//             if(e.handle != creditsInstance.handle) creditsInstance.stop(STOP_MODE.ALLOWFADEOUT);
//         }
//     }
// }