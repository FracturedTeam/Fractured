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
        
        [Header("Shard")]
        public EventReference shard_Picked;
        public EventReference shard_LetGo;
        public EventReference shard_Hide;
        public EventReference shard_Obtained;
        
        [Header("Door")]
        public EventReference door_Opened;
        public EventReference door_Locked;
        public EventReference door_Unlocked;

        [Header("Lock")]
        public EventReference lock_Tick;
        public EventReference lock_Unlocked;
        
        [Header("Document")]
        public EventReference document_Paper;
        public EventReference document_Book;
        
        [Header("Interaction")]
        public EventReference closet_DoorOpen;
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
        public EventReference Act1_Memory_1_Loop;
        public EventReference Act1_Memory_2_Loop;
        public EventReference Act2_Memory_1_Loop;
        public EventReference Act2_Memory_2_Loop;
        public EventReference Act2_Memory_3_Loop;
        public EventReference Act3_Memory_1_Loop;
        public EventReference Act3_Memory_2_Loop;
        public EventReference Act3_Memory_3_Loop;
        public EventReference Act4_Memory_1_Loop;
        public EventReference Act4_Memory_2_Loop;
        public EventReference Act4_Memory_3_Loop;
        public EventReference Act4_Memory_4_Loop;
        public EventReference Act5_Memory_1_Loop;
        public EventReference Act5_Memory_2_Loop;
        
        // [Header("Player")]
        // public EventReference playerFootstepSound;
        //
        // [Header("Glass Sounds")]
        // public EventReference grabGlassSound;
        // public EventReference grabGlassFailedSound;
        // public EventReference revealSound;
        // public EventReference hideSound;
        // public EventReference breakGlassSound;
        //
        // [Header("Small Doors Sounds")]
        // public EventReference lockedSmallDoorSound;
        // public EventReference openSmallDoorSound;
        //
        // [Header("Big Doors Sounds")]
        // public EventReference lockedBigDoorSound;
        // public EventReference openBigDoorSound;
        //
        // [Header("Moveable Object Sounds")]
        // public EventReference pickUpObjectSound;
        // public EventReference pickUpKeySound;
        // public EventReference dropObjectSound;
        //
        // [Header("Memory Sounds")]
        // public EventReference reconstructMemorySound;
        // public EventReference enterMemorySound;
        // public EventReference leaveMemorySound;
        //
        // [Header("Looping Main Ambient Sounds")]
        // public EventReference ambient_CoffinRoom_Loop;
        // public EventReference ambient_TutorialRooms_Loop;
        // public EventReference ambient_Act1_Loop;
        //
        // [Header("Looping Gameplay Ambient Sounds")]
        // public EventReference ambient_Memory_Loop;
        // public EventReference ambient_GlassEditable_Loop;
        //
        // [Header("Looping Other Ambient Sounds")]
        // public EventReference ambient_MainMenu_Loop;
        // public EventReference ambient_Credits_Loop;
        //
        // [Header("UI")]
        // public EventReference uiBttClickedSound;
    }
}