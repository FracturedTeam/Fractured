using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Timers;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace _Project.Scripts.GameServices.Services {
    public class AudioService : IGameSystem {
        private AudioBank bank;

        public float masterVolume = 1;
        public float sfxVolume = 1;
        public float musicVolume = 1;
        
        private Bus masterBus;
        private Bus sfxBus;
        private Bus musicBus;
        
        private EventInstance memorySoundInstance;
        private EventInstance ambientZone1Instance;
        private EventInstance ambientTutorialInstance;
        private EventInstance ambientCoffinInstance;
        private EventInstance menuInstance;
        private EventInstance editableInstance;
        private EventInstance creditsInstance;
        
        private readonly CountdownTimer revealObjectTimer = new CountdownTimer(1f);
        private readonly CountdownTimer hideObjectTimer = new CountdownTimer(1f);

        public AudioService(AudioBank _bank) {
            bank = _bank;
        }
        
        public void Initialize() {
            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Ambiance");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");
            
            //Main ambient loop
            ambientCoffinInstance = CreateInstance(bank.ambient_CoffinRoom_Loop);
            ambientTutorialInstance = CreateInstance(bank.ambient_TutorialRooms_Loop);
            ambientZone1Instance = CreateInstance(bank.ambient_Act1_Loop);
            
            //Gameplay dependent ambient loop
            memorySoundInstance = CreateInstance(bank.ambient_Memory_Loop);
            editableInstance = CreateInstance(bank.ambient_GlassEditable_Loop);
            
            //Others ambient loop
            menuInstance = CreateInstance(bank.ambient_MainMenu_Loop);
            creditsInstance = CreateInstance(bank.ambient_Credits_Loop);
        }

        public void Tick() {
            masterBus.setVolume(masterVolume);
            musicBus.setVolume(musicVolume);
            sfxBus.setVolume(sfxVolume);
        }
        
        public void Dispose() {
        }
        
        public void PlayOneShot3D(EventReference sound, Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(sound, worldPosition);
        }

        public void PlayOneShot2D(EventReference sound) {
            RuntimeManager.PlayOneShot(sound);
        }

        public void PlayRevealObjectSound(Vector3 worldPosition) {
             if(revealObjectTimer.IsRunning) return;
             RuntimeManager.PlayOneShot(bank.revealSound, worldPosition);
             revealObjectTimer.Start();
        }

        public void PlayHideObjectSound(Vector3 worldPosition) {
             if(hideObjectTimer.IsRunning) return;
             RuntimeManager.PlayOneShot(bank.hideSound, worldPosition);
             hideObjectTimer.Start();
        }
        
        public void PlayEditableSoundLoop(bool doPlay) {
            if (doPlay) {
                editableInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    editableInstance.start();
                }
            }
            else {
                editableInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.PLAYING)) {
                    editableInstance.stop(STOP_MODE.ALLOWFADEOUT);
                }
            }
        }
        
        public void UpdateAmbientLoop(int index) {
            PLAYBACK_STATE playbackState;
            
            if (index == 2) {
                ambientCoffinInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) ambientCoffinInstance.start();
                FadeLoop(ref ambientCoffinInstance);
            }
            else if (index is > 2 and < 8) {
                ambientTutorialInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) ambientTutorialInstance.start();
                FadeLoop(ref ambientTutorialInstance);
            }
            else if (index is > 7 and < 12) {
                ambientZone1Instance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) ambientZone1Instance.start();
                FadeLoop(ref ambientZone1Instance);
            }
            else if (index is 12) {
                creditsInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) creditsInstance.start();
                FadeLoop(ref creditsInstance);
            }
            else if (index is 0 or 1) {
                menuInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) menuInstance.start();
                FadeLoop(ref menuInstance);
            }
        }
        
        public void UpdateMemory(bool inMemory) {
            if (inMemory) {
                memorySoundInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    memorySoundInstance.start();
                }
            }
            else
                memorySoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
        
        public EventInstance CreateInstance(EventReference reference) {
            if (reference.IsNull) {
                Debug.LogError($"[AudioService] Instance Creation Failed : Missing event reference {reference}, Please verify Audio Bank References");
                return new EventInstance();
            }
            return RuntimeManager.CreateInstance(reference);
        }

        private void FadeLoop(ref EventInstance e) {
            if(e.handle != ambientCoffinInstance.handle) ambientCoffinInstance.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != ambientTutorialInstance.handle) ambientTutorialInstance.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != ambientZone1Instance.handle) ambientZone1Instance.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != menuInstance.handle) menuInstance.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != creditsInstance.handle) creditsInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}