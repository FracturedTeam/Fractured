using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class PadlockAttribute : LockedAttribute {
        [Header("Padlock Attribute")]
        [SerializeField] private int requiredCode;
        [SerializeField] private bool doInteractImmediately;

        private readonly CountdownTimer timerUp = new(0.1f);
        private readonly CountdownTimer timerRight = new(0.1f);
        
        private bool isUsingLock;

        private int firstDigit = 2;
        private int secondDigit = 3;
        private int thirdDigit = 4;
        private int fourthDigit = 5;

        private int selectedDigit = 0;

        public override void Initialize() {
            base.Initialize();
            
            firstDigit = Random.Range(0, 10);
            secondDigit = Random.Range(0, 10);
            thirdDigit = Random.Range(0, 10);
            fourthDigit = Random.Range(0, 10);
        }
        
        public override void OnInteract(IInteractable interactable) {
            UseLock();
        }

        private void UseLock() {
            isUsingLock = !isUsingLock;
            
            GameInitializer.Instance.SetShardsOnOff(!isUsingLock);

            PlayerController.Instance.interact.SetIsFocus(isUsingLock, baseObject);
            PlayerController.Instance.FreezeController(isUsingLock);
            
            if(isUsingLock) BindInputs();
            else UnbindInputs();
        }

        private void TryUnlock() {
            var currentCode = firstDigit * 1000 + secondDigit * 100 + thirdDigit * 10 + fourthDigit;

            if (currentCode == requiredCode) {
                isUsingLock = false;
            
                GameInitializer.Instance.SetShardsOnOff(!isUsingLock);

                PlayerController.Instance.interact.SetIsFocus(isUsingLock, baseObject);
                PlayerController.Instance.FreezeController(isUsingLock);
                
                UnbindInputs();
                
                baseObject.GetTrigger?.OnFunction(baseObject.GetTrigger?.OnInteractSuccess);
                baseObject.GetLockState = LockedState.Unlocked;

                if (!doInteractImmediately) return;
                switch (baseObject.GetObjectType) {
                    case ObjectType.Collectable or ObjectType.Moveable:
                        baseObject.OnInteract(ObjectInteraction.Grab);
                        break;
                    case ObjectType.Usable:
                        baseObject.OnInteract(ObjectInteraction.Contextual);
                        break;
                    default:
                        Debug.LogWarning($"[BlockedAttribute] Interactable type {baseObject.GetObjectType} not supported");
                        break;
                }
            }
        }
        
        private void ProcessInputUp(float input) {
            if(!isUsingLock || timerUp.IsRunning) return;
            
            timerUp.Start();
            
            var add = input > 0.25f ? 1 : input < -0.25f ? -1 : 0;
            
            switch (selectedDigit) {
                case 0:
                    firstDigit += add;
                    if(firstDigit > 9) firstDigit = 0;
                    if (firstDigit < 0) firstDigit = 9;
                    break;
                case 1:
                    secondDigit += add;
                    if(secondDigit > 9) secondDigit = 0;
                    if (secondDigit < 0) secondDigit = 9;
                    break;
                case 2:
                    thirdDigit += add;
                    if(thirdDigit > 9) thirdDigit = 0;
                    if (thirdDigit < 0) thirdDigit = 9;
                    break;
                case 3:
                    fourthDigit += add;
                    if(fourthDigit > 9) fourthDigit = 0;
                    if (fourthDigit < 0) fourthDigit = 9;
                    break;
            }
            
            TryUnlock();
        }
        
        private void ProcessInputRight(float input) {
            if(!isUsingLock || timerRight.IsRunning) return;
            
            timerRight.Start();
            
            var select = input > 0.25f ? 1 : input < -0.25f ? -1 : 0;
            selectedDigit += select;
            
            if(selectedDigit > 3) selectedDigit = 0;
            if(selectedDigit < 0) selectedDigit = 3;
        }
        
        private void BindInputs() {
            InputsBrain.Instance.OnLockUp += ProcessInputUp;
            InputsBrain.Instance.OnLockRight += ProcessInputRight;
        }

        private void UnbindInputs() {
            InputsBrain.Instance.OnLockUp -= ProcessInputUp;
            InputsBrain.Instance.OnLockRight -= ProcessInputRight;
        }

        private void OnGUI() {
            if(!isUsingLock) return;
            
            var screenSize = new Vector2(720, 360);
            var screenPos = new Vector2((Screen.width - screenSize.x) / 2, (Screen.height - screenSize.y) / 2);
            var screen = new Rect(screenPos, screenSize);
            
            GUILayout.BeginArea(screen, "Lock");
            GUILayout.BeginHorizontal("box");

            var style = new GUIStyle(GUI.skin.label) {
                fontSize = 24,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = {
                    textColor = Color.white
                }
            };
            
            var selectedStyle = new GUIStyle(GUI.skin.label) {
                fontSize = 24,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = {
                    textColor = Color.indianRed
                }
            };
            
            GUILayout.Label($"{firstDigit}", selectedDigit == 0 ? selectedStyle : style, GUILayout.Width(10));
            GUILayout.Label($"{secondDigit}", selectedDigit == 1 ? selectedStyle : style,GUILayout.Width(10));
            GUILayout.Label($"{thirdDigit}", selectedDigit == 2 ? selectedStyle : style,GUILayout.Width(10));
            GUILayout.Label($"{fourthDigit}", selectedDigit == 3 ? selectedStyle : style,GUILayout.Width(10));
            
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        
    }
}