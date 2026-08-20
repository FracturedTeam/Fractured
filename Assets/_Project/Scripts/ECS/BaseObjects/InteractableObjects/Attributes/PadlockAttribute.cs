using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    public class PadlockAttribute : LockedAttribute {
        [Header("Padlock Attribute")]
        [SerializeField] private int requiredCode;
        internal int currentCode = 9876;
        [SerializeField] private bool doInteractImmediately;
        [SerializeField] internal Vector2 offset;

        private readonly CountdownTimer timerUp = new(0.1f);
        private readonly CountdownTimer timerRight = new(0.1f);
        
        private bool isUsingLock;

        private int selectedDigit = 0;

        public override void Initialize() {
            base.Initialize();

            currentCode = Random.Range(0, 10000);

            if (currentCode == requiredCode)
                currentCode = 2713;
        }
        
        public override void OnInteract(IInteractable interactable) {
            UseLock();
        }

        private void UseLock() {
            isUsingLock = !isUsingLock;
            
            GameInitializer.Instance.SetShardsOnOff(!isUsingLock);

            PlayerController.Instance.Interact.SetIsFocus(isUsingLock, baseObject);
            PlayerController.Instance.Interact.SetGlassInteraction(!isUsingLock);
            PlayerController.Instance.FreezeController(isUsingLock);
            
            HudManager.Instance.padLock.SetCurrent(isUsingLock ? this : null);
            HudManager.Instance.padLock.SetSelected(selectedDigit);
            
            if(isUsingLock) BindInputs();
            else UnbindInputs();
        }

        private void TryUnlock() {

            if (currentCode == requiredCode) {
                isUsingLock = false;
            
                GameInitializer.Instance.SetShardsOnOff(!isUsingLock);
                GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().lock_Unlocked);
                
                HudManager.Instance.padLock.SetCurrent(null);

                PlayerController.Instance.Interact.SetIsFocus(isUsingLock, baseObject);
                PlayerController.Instance.Interact.SetGlassInteraction(!isUsingLock);
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
            
           var firstDigit = currentCode / 1000;
           var secondDigit = ((currentCode % 1000) / 100);
           var thirdDigit = (((currentCode % 1000) % 100) / 10);
           var fourthDigit = ((((currentCode % 1000) % 100) % 10));
            
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
            GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().lock_Tick);
            
            currentCode = firstDigit * 1000 + secondDigit * 100 + thirdDigit * 10 + fourthDigit;
            HudManager.Instance.padLock.UpdateCode();
            TryUnlock();
        }

        public void ForceSetInput(int newCode, int newSelectedDigit)
        {
            currentCode = newCode;
            selectedDigit = newSelectedDigit;
            HudManager.Instance.padLock.UpdateCode();
            HudManager.Instance.padLock.SetSelected(selectedDigit);
        }
        
        private void ProcessInputRight(float input) {
            if(!isUsingLock || timerRight.IsRunning) return;
            
            timerRight.Start();
            
            var select = input > 0.25f ? 1 : input < -0.25f ? -1 : 0;
            selectedDigit += select;
            
            if(selectedDigit > 3) selectedDigit = 0;
            if(selectedDigit < 0) selectedDigit = 3;
            
            HudManager.Instance.padLock.SetSelected(selectedDigit);
        }
        
        private void BindInputs() {
            InputsBrain.Instance.OnLockUp += ProcessInputUp;
            InputsBrain.Instance.OnLockRight += ProcessInputRight;
        }

        private void UnbindInputs() {
            InputsBrain.Instance.OnLockUp -= ProcessInputUp;
            InputsBrain.Instance.OnLockRight -= ProcessInputRight;
        }
    }
}