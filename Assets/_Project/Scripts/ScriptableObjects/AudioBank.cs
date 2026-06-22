using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.ScriptableObjects {
    [CreateAssetMenu(fileName = "AudioBank", menuName = "ScriptableObjects/AudioBank")]
    public class AudioBank : ScriptableObject {
        [Header("One Shot Sounds")]
        [Header("Player")]
        public EventReference playerFootstepSound;
        
        [Header("Glass Sounds")]
        public EventReference grabGlassSound;
        public EventReference grabGlassFailedSound;
        public EventReference revealSound;
        public EventReference hideSound;
        public EventReference breakGlassSound;
        
        [Header("Small Doors Sounds")]
        public EventReference lockedSmallDoorSound;
        public EventReference openSmallDoorSound;
        
        [Header("Big Doors Sounds")]
        public EventReference lockedBigDoorSound;
        public EventReference openBigDoorSound;
        
        [Header("Moveable Object Sounds")]
        public EventReference pickUpObjectSound;
        public EventReference pickUpKeySound;
        public EventReference dropObjectSound;
        
        [Header("Memory Sounds")]
        public EventReference reconstructMemorySound;
        public EventReference enterMemorySound;
        public EventReference leaveMemorySound;
        
        [Header("Pressure Plate Sounds")]
        public EventReference pressurePlateActiveSound;
        public EventReference pressurePlateInactiveSound;
        
        [Header("Looping Main Ambient Sounds")]
        public EventReference ambient_CoffinRoom_Loop;
        public EventReference ambient_TutorialRooms_Loop;
        public EventReference ambient_Act1_Loop;
        
        [Header("Looping Gameplay Ambient Sounds")]
        public EventReference ambient_Memory_Loop;
        public EventReference ambient_GlassEditable_Loop;
        
        [Header("Looping Other Ambient Sounds")]
        public EventReference ambient_MainMenu_Loop;
        public EventReference ambient_Credits_Loop;
        
        [Header("UI")]
        public EventReference uiBttClickedSound;
    }
}