using System;
using _Project.Scripts.Systems.Singletons;
using Discord;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class DiscordRichPresence : PersistentSingleton<DiscordRichPresence> {
        private long applicationID = 1521130383679426591;
        [Space] 
        public string details = "Chapter";
        public string state = "Playing Solo";

        [Space] 
        public string largeImage = "";
        public string largeText = "Fractured";
        
        private Discord.Discord discord;
        private bool isInitialized = false;
        
        private long startTimestamp;
        
        void Start() {
            Initialize();   
        }

        private void Initialize() {
            try {
                discord = new Discord.Discord(applicationID, (ulong)CreateFlags.NoRequireDiscord);
                isInitialized = true;
                startTimestamp = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                UpdateStatus();
            }
            catch (Exception e) {
                isInitialized = false;
            }
        }
        
        private void OnDisable() {
            if(!isInitialized) return;
            discord.Dispose();
            isInitialized = false;
        }

        private void Update() {
            if(!isInitialized) return;
            
            try {
                discord.RunCallbacks();
            }
            catch {
                isInitialized = false;
            }
        }

        private void UpdateStatus() {
            if(!isInitialized) return;
            
            try {
                var activityManager = discord.GetActivityManager();
                var activity = new Discord.Activity {
                    Type = ActivityType.Playing,
                    Details = details,
                    State = state,
                    Assets = {
                        LargeImage = largeImage,
                        LargeText = largeText
                    },
                    Timestamps = new ActivityTimestamps {
                        Start = startTimestamp,
                    }
                };
                
                activityManager.UpdateActivity(activity, (res) => {
                    // if(res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord");
                });
            }
            catch {
                isInitialized = false;
            }
        }

        public void UpdateRichPresence(string details, string state) {
            if(!isInitialized) return;
            
            if(details != "")
                this.details = details;
            if(state != "")
                this.state = state;
            
            UpdateStatus();
        }
    }
}