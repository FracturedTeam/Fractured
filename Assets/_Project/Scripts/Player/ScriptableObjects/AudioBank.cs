using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.ScriptableObjects {
    [CreateAssetMenu(fileName = "AudioBank", menuName = "ScriptableObjects/AudioBank")]
    public class AudioBank : ScriptableObject {
        [Header("One-Shots")] 
        [Header("Player")]
        public EventReference avatar_Taking_Object;
        public EventReference avatar_Taking_Key;
        public EventReference avatar_PuttingAway_Object;
        public EventReference avatar_Equipping_Object;
        public EventReference avatar_Drops_Object;
        public EventReference avatar_Sight_Sounds;
        public EventReference avatar_Inventory_Sound;
        public EventReference avatar_Walking_Neutral;
        public EventReference avatar_Walking_Wood;
        public EventReference avatar_Walking_Tile;
        public EventReference avatar_Walking_Carpet;
        public EventReference avatar_Walking_Sand;
        
        [Header("Shard")]
        public EventReference shard_Picked;
        public EventReference shard_LetGo;
        public EventReference shard_Hide;
        public EventReference shard_Obtained;
        
        [Header("Door")]
        public EventReference door_Opened;

        [Header("Lock")]
        public EventReference lock_Tick;
        public EventReference lock_Unlocked;
        
        [Header("Interaction")]
        public EventReference environmentText_Appear;
        
        [Header("Frame Complete")]
        public EventReference ld_Solved;
        
        [Header("Memory")]
        public EventReference memory_Interact;
        public EventReference memory_Leave;
        
        [Header("Room")]
        public EventReference room_Enter;
        public EventReference room_Exit;
        
        [Header("UI")]
        public EventReference ui_Clicked;
        public EventReference ui_Back;
        public EventReference ui_Play;
        
        [Space, Header("Loops")] 
        [Header("Act Ambient Loop")] 
        public EventReference act_0_ambient_Loop;
        public EventReference act_1_ambient_Loop;
        public EventReference act_2_ambient_Loop;
        public EventReference act_3_ambient_Loop;
        public EventReference act_4_ambient_Loop;
        public EventReference act_5_ambient_Loop;
        
        [Header("Main Loop")] 
        public EventReference mainMenu_Loop;
        public EventReference credit_Loop;
        public EventReference beach_Loop;
        
        [Header("Shard")]
        public EventReference movingShard_Loop;
        
        [Header("Environmental Text")]
        public EventReference environmentalText_Loop;
        
        [Header("Memory")] 
        public EventReference memorySeeing_Loop;
    }
}