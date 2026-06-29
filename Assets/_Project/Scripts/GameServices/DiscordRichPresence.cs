using System;
using _Project.Scripts.Systems.Singletons;
using Discord;
using UnityEngine;

namespace _Project.Scripts.GameServices {
    public class DiscordRichPresence : PersistentSingleton<DiscordRichPresence> {
        private long applicationID = 1521130383679426591;
        [Space] 
        public string details = "Playing Fractured";
        public string state = "Remembering why i killed my brother";

        [Space] 
        public string largeImage = "";
        public string largeText = "Fractured";
        
        public Discord.Discord discord;

        void Start() {
            discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

            UpdateStatus();
        }

        private void Update() {
            try {
                discord.RunCallbacks();
            }
            catch {
                Destroy(this);
            }
        }

        void UpdateStatus() {
            try {
                var activityManager = discord.GetActivityManager();
                var activity = new Discord.Activity {
                    Details = details,
                    State = state,
                    Assets = {
                        LargeImage = largeImage,
                        LargeText = largeText
                    }
                };
                activityManager.UpdateActivity(activity, (res) => {
                    if(res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord");
                });
            }
            catch {
                Destroy(this);
            }
        }
    }
}