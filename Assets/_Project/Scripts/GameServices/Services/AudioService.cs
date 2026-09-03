using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Timers;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace _Project.Scripts.GameServices.Services {
    public class AudioService : IGameSystem {
        private AudioBank bank;
        private SaveService saveService;
        
        private Bus masterBus;
        private Bus sfxBus;
        private Bus musicBus;
        
        // Act Ambient
        private EventInstance Act0Ambient;
        private EventInstance Act1Ambient;
        private EventInstance Act2Ambient;
        private EventInstance Act3Ambient;
        private EventInstance Act4Ambient;
        private EventInstance Act5Ambient;
        
        // Main Menu Ambient
        private EventInstance menuInstance;
        // Beach Ambient
        private EventInstance beachInstance;
        // Credit Ambient
        private EventInstance creditsInstance;
        
        private EventInstance memorySeeingInstance;
        
        private EventInstance movingShardInstance;
        
        private readonly CountdownTimer hideObjectTimer = new (1f);

        private SettingData settingData;
        private int currentAmbient = 0;

        public AudioService(AudioBank _bank) {
            bank = _bank;
        }
        
        public void Initialize() {
            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Ambiance");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");
            
            //Main ambient loop
            Act0Ambient = CreateInstance(bank.act_0_ambient_Loop);
            Act1Ambient = CreateInstance(bank.act_1_ambient_Loop);
            Act2Ambient = CreateInstance(bank.act_2_ambient_Loop);
            Act3Ambient = CreateInstance(bank.act_3_ambient_Loop);
            Act4Ambient = CreateInstance(bank.act_4_ambient_Loop);
            Act5Ambient = CreateInstance(bank.act_5_ambient_Loop);
            
            //Others ambient loop
            menuInstance = CreateInstance(bank.mainMenu_Loop);
            beachInstance = CreateInstance(bank.beach_Loop);
            creditsInstance = CreateInstance(bank.credit_Loop);
            
            //Memory
            memorySeeingInstance = CreateInstance(bank.memorySeeing_Loop);
            
            movingShardInstance = CreateInstance(bank.movingShard_Loop);
            
            settingData = GameInitializer.Instance.GetSettings;
        }

        public void Tick() { }

        public void SetSound(int index, float newValue) {
            switch (index) {
                case 0:
                    masterBus.setVolume(newValue);
                    settingData.mainVolume = newValue;
                    break;
                case 1:
                    sfxBus.setVolume(newValue);
                    settingData.sfxVolume = newValue;
                    break;
                case 2:
                    musicBus.setVolume(newValue);
                    settingData.musicVolume = newValue;
                    break;
            }
            
            GameInitializer.Instance.SaveSettings();
        }
        
        public void Dispose() {
            Act0Ambient.stop(STOP_MODE.IMMEDIATE);
            Act1Ambient.stop(STOP_MODE.IMMEDIATE);
            Act2Ambient.stop(STOP_MODE.IMMEDIATE);
            Act3Ambient.stop(STOP_MODE.IMMEDIATE);
            Act4Ambient.stop(STOP_MODE.IMMEDIATE);
            Act5Ambient.stop(STOP_MODE.IMMEDIATE);
            menuInstance.stop(STOP_MODE.IMMEDIATE);
            beachInstance.stop(STOP_MODE.IMMEDIATE);
            creditsInstance.stop(STOP_MODE.IMMEDIATE);
            memorySeeingInstance.stop(STOP_MODE.IMMEDIATE);
            
            Act0Ambient.release();
            Act1Ambient.release();
            Act2Ambient.release();
            Act3Ambient.release();
            Act4Ambient.release();
            Act5Ambient.release();
            menuInstance.release();
            beachInstance.release();
            creditsInstance.release();
            memorySeeingInstance.release();
        }
        
        public void PlayOneShot3D(EventReference sound, Vector3 worldPosition) {
            RuntimeManager.PlayOneShot(sound, worldPosition);
        }

        public void PlayOneShot2D(EventReference sound) {
            RuntimeManager.PlayOneShot(sound);
        }

        public void PlayHideObjectSound(Vector3 worldPosition) {
             if(hideObjectTimer.IsRunning) return;
             RuntimeManager.PlayOneShot(bank.shard_Hide, worldPosition);
             hideObjectTimer.Start();
        }

        public void PlayMovingShardLoop(bool moving) {
            if (moving) {
                movingShardInstance.getPlaybackState(out var playbackSate);
                if (playbackSate.Equals(PLAYBACK_STATE.STOPPED)) movingShardInstance.start();
            }
            else {
                movingShardInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
        
        public void UpdateAmbientLoop(int index) {
            PLAYBACK_STATE playbackState;
            
            if (index is 2) {
                Act0Ambient.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) Act0Ambient.start();
                FadeLoop(ref Act0Ambient);
                currentAmbient = 0;
            }
            else if (index is 3) {
                Act1Ambient.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) Act1Ambient.start();
                FadeLoop(ref Act1Ambient);
                currentAmbient = 1;
            }
            else if (index is 4) {
                Act2Ambient.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) Act2Ambient.start();
                FadeLoop(ref Act2Ambient);
                currentAmbient = 2;
            }
            else if (index is 5) {
                Act3Ambient.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) Act3Ambient.start();
                FadeLoop(ref Act3Ambient);
                currentAmbient = 3;
            }
            else if (index is 6) {
                Act4Ambient.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) Act4Ambient.start();
                FadeLoop(ref Act4Ambient);
                currentAmbient = 4;
            }
            else if (index is 7) {
                Act5Ambient.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) Act5Ambient.start();
                FadeLoop(ref Act5Ambient);
                currentAmbient = 5;
            }
            else if (index is 8) {
                beachInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) beachInstance.start();
                FadeLoop(ref beachInstance);
            }
            else if (index is 0 or 1) {
                menuInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) menuInstance.start();
                FadeLoop(ref menuInstance);
            }
        }
        
        public void UpdateMemory(bool inMemory) {
            SetCurrentAmbientPlayState(currentAmbient, !inMemory);
            
            if (inMemory) {
                memorySeeingInstance.getPlaybackState(out var playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    memorySeeingInstance.start();
                }
            }
            else
                memorySeeingInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }

        private void SetCurrentAmbientPlayState(int currentIndex, bool doPlay) {
            switch (currentIndex) {
                case 0:
                    if(doPlay) Act0Ambient.start();
                    else Act0Ambient.stop(STOP_MODE.ALLOWFADEOUT);
                    break;
                case 1:
                    if(doPlay) Act1Ambient.start();
                    else Act0Ambient.stop(STOP_MODE.ALLOWFADEOUT);
                    break;
                case 2:
                    if(doPlay) Act2Ambient.start();
                    else Act0Ambient.stop(STOP_MODE.ALLOWFADEOUT);
                    break;
                case 3:
                    if(doPlay) Act3Ambient.start();
                    else Act0Ambient.stop(STOP_MODE.ALLOWFADEOUT);
                    break;
                case 4:
                    if(doPlay) Act4Ambient.start();
                    else Act0Ambient.stop(STOP_MODE.ALLOWFADEOUT);
                    break;
                case 5:
                    if(doPlay) Act5Ambient.start();
                    else Act0Ambient.stop(STOP_MODE.ALLOWFADEOUT);
                    break;
            }            
        }
        
        public EventInstance CreateInstance(EventReference reference) {
            if (reference.IsNull) {
                Debug.LogError($"[AudioService] Instance Creation Failed : Missing event reference {reference}, Please verify Audio Bank References");
                return new EventInstance();
            }
            
            return RuntimeManager.CreateInstance(reference);
        }

        private void FadeLoop(ref EventInstance e) {
            if(e.handle != Act0Ambient.handle) Act0Ambient.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != Act1Ambient.handle) Act1Ambient.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != Act2Ambient.handle) Act2Ambient.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != Act3Ambient.handle) Act3Ambient.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != Act4Ambient.handle) Act4Ambient.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != Act5Ambient.handle) Act5Ambient.stop(STOP_MODE.ALLOWFADEOUT);
            
            if(e.handle != menuInstance.handle) menuInstance.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != creditsInstance.handle) creditsInstance.stop(STOP_MODE.ALLOWFADEOUT);
            if(e.handle != beachInstance.handle) beachInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}