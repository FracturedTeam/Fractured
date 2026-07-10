using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.ECS {
    [RequireComponent(typeof(BaseObject))]
    public class MemoryFrameMaster : MonoBehaviour, IInteractable {
        private BaseObject baseObject;
        // Il va me falloir un collider avec lequel interagir
        // Raccrocher ça au baseObject ? Sinon il faut que je fasse des modifs dans le PlayerInteract
        
        // Changement de caméra sur les tableaux
        // Moyen de déplacer les tableaux à la souris
        // Comment détecter la souris ? screenPos to worldPos ?
        // Avoir une sorte de snap pour les tableaux
        
        // Comment checker quel ordre est le bon ?

        [Header("Memory Frame")]
        [SerializeField] private CinemachineCamera frameCamera;
        [SerializeField] private Transform[] frameSlots;
        [SerializeField] private MemoryFrame[] frames;
        
        private bool isInitialized;
        private bool isUsingMemoryFrame;
        
        public void Initialize() {
            if (!isInitialized) {
                if (TryGetComponent(out BaseObject component)) baseObject = component;
                else throw new ArgumentNullException($"[MemoryFrame] Base object not found");

                baseObject.GetObjectType = ObjectType.MemoryFrame;

                for (var i = 0; i < frameSlots.Length; i++) {
                    frames[i].SetCurrentPosition(i);
                }
                
                baseObject.SetInteract(true);
                
                isInitialized = true;
            }
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {
            if (interaction is ObjectInteraction.Contextual) {
                UseMemoryFrame();
            }
        }

        private void UseMemoryFrame() {
            isUsingMemoryFrame = !isUsingMemoryFrame;
            
            if (isUsingMemoryFrame) {
                frameCamera.Priority = 2;
                PlayerController.Instance.interact.SetIsFocus(true, baseObject);
                PlayerController.Instance.FreezeController(true);

                foreach (var frame in frames) {
                    frame.CanBeInteracted(true);
                }
            }
            else {
                frameCamera.Priority = 0;
                PlayerController.Instance.interact.SetIsFocus(false);
                PlayerController.Instance.FreezeController(false);
                
                foreach (var frame in frames) {
                    frame.CanBeInteracted(false);
                }
            }
        }

        public void Tick(float deltaTime) {
            
        }

        public void Dispose() {
            
        }

        public void CompleteObject() {
            
        }

        public void ResetObject() {
            
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}