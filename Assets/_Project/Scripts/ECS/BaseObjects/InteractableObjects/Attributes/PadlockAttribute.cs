using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.EventBus;
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
        private int selectedDigit = 0;

        private int firstDigit;
        private int secondDigit;
        private int thirdDigit;
        private int fourthDigit;

        public override void Initialize() {
            base.Initialize();

            firstDigit = Random.Range(0,10);
            secondDigit = Random.Range(0,10);
            thirdDigit = Random.Range(0,10);
            fourthDigit = Random.Range(0,10);
            
            var actualCode = firstDigit * 1000 + secondDigit * 100 + thirdDigit * 10 + fourthDigit;

            if (actualCode == requiredCode) {
                firstDigit = Random.Range(0,10);
                secondDigit = Random.Range(0,10);
                thirdDigit = Random.Range(0,10);
                fourthDigit = Random.Range(0,10);
            }
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
            
            if(isUsingLock) BindInputs();
            else UnbindInputs();
            
            EventBus<PadlockEvent>.Raise(new PadlockEvent {
                doShow = isUsingLock,
                firstDigit = firstDigit,
                secondDigit = secondDigit,
                thirdDigit = thirdDigit,
                fourthDigit = fourthDigit,
                selectedDigit = selectedDigit,
                currentLock = this
            });
        }

        private void TryUnlock() {
            var actualCode = firstDigit * 1000 + secondDigit * 100 + thirdDigit * 10 + fourthDigit;
            
            if (actualCode == requiredCode) {
                isUsingLock = false;
            
                GameInitializer.Instance.SetShardsOnOff(!isUsingLock);
                GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().lock_Unlocked);
                GameInitializer.Instance.rumbleService.RumblePulse(0.3f, 0.5f, 0.4f);
                
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
                    // default:
                    //     Debug.LogWarning($"[BlockedAttribute] Interactable type {baseObject.GetObjectType} not supported");
                    //     break;
                }
                
                EventBus<PadlockEvent>.Raise(new PadlockEvent {
                    doShow = isUsingLock,
                    firstDigit = firstDigit,
                    secondDigit = secondDigit,
                    thirdDigit = thirdDigit,
                    fourthDigit = fourthDigit,
                    selectedDigit = selectedDigit,
                    currentLock = this
                });
            }
        }
        
        private void ProcessInputUp(float input) {
            if(!isUsingLock || timerUp.IsRunning) return;
            
            timerUp.Start();
            
            var add = input > 0.25f ? 1 : input < -0.25f ? -1 : 0;
            
            if(add == 0) return;
            
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
            
            EventBus<PadlockEvent>.Raise(new PadlockEvent {
                doShow = isUsingLock,
                firstDigit = firstDigit,
                secondDigit = secondDigit,
                thirdDigit = thirdDigit,
                fourthDigit = fourthDigit,
                selectedDigit = selectedDigit,
                currentLock = this
            });
            
            TryUnlock();
        }
        
        private void ProcessInputRight(float input) {
            if(!isUsingLock || timerRight.IsRunning) return;
            
            timerRight.Start();
            
            var select = input > 0.25f ? 1 : input < -0.25f ? -1 : 0;
            selectedDigit += select;
            
            if(selectedDigit > 3) selectedDigit = 0;
            if(selectedDigit < 0) selectedDigit = 3;
            
            EventBus<PadlockEvent>.Raise(new PadlockEvent {
                doShow = isUsingLock,
                firstDigit = firstDigit,
                secondDigit = secondDigit,
                thirdDigit = thirdDigit,
                fourthDigit = fourthDigit,
                selectedDigit = selectedDigit,
                currentLock = this
            });
        }

        public void UpdateLockDigit(int firstDigit, int secondDigit, int thirdDigit, int fourthDigit) {
            this.firstDigit = firstDigit;
            this.secondDigit = secondDigit;
            this.thirdDigit = thirdDigit;
            this.fourthDigit = fourthDigit;
            
            TryUnlock();
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
    
    public struct PadlockEvent : IEvent {
        public bool doShow;
        public int firstDigit;
        public int secondDigit;
        public int thirdDigit;
        public int fourthDigit;
        public int selectedDigit;
        public PadlockAttribute currentLock;
    }
}