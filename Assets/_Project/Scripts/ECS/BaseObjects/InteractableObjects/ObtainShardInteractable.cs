using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Interfaces;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Scripts.ECS.BaseObjects.InteractableObjects {
    [RequireComponent(typeof(BaseObject))]
    public class ObtainShardInteractable : MonoBehaviour, IInteractable{
        private BaseObject baseObject;
        
        [Header("Settings")]
        [SerializeField] public Glass[] shards;

        [SerializeField] private ParticleSystem shardParticle;
        [SerializeField] private Material baseMat; 
        [SerializeField] private Material brokenMat; 

        private bool initialized = false;
        private MeshRenderer glassRenderer;

        public void Initialize() { //To-do save l'obtention pour insantier a nouveau le fragment
            if (!initialized) {
                if(TryGetComponent(typeof(BaseObject), out var component)) baseObject = component as BaseObject;
                else Debug.LogError($"[ObtainShardInteractable] Cannot find {nameof(BaseObject)} in {nameof(ObtainShardInteractable)}");
                
                baseObject.GetInteractionType = ObjectType.Shard;
                baseObject.GetCompletion = InteractionCompletion.NotCompleted;
                baseObject?.SetInteract(true);
                
                //Get materials and particles
                if(baseMat == null)
                    baseMat = Resources.Load<Material>("Materials/M_GlassPicture_01_Base");
                if(brokenMat == null)
                    brokenMat = Resources.Load<Material>("Materials/M_GlassPicture_01_Broken");
                if(shardParticle == null)
                    shardParticle = Resources.Load<ParticleSystem>("VFX/P_MirrorBreakParticles");
                if (transform.Find("Glass")) {
                    glassRenderer = transform.Find("Glass").GetComponent<MeshRenderer>();
                    glassRenderer.material = baseMat; 
                }
            }

            initialized = true;

            if(shards.Length == 0)
                Debug.LogError($"[ObtainShardInteractable] {gameObject.name} No shards referenced !");
        }

        public void OnInteract(ObjectInteraction interaction, IInteractable other = null) {

            if(baseObject.GetCompletion is InteractionCompletion.Completed)
            {
                if (baseObject.failedDialogue is { oneTime: true, alreadyInteracted: true })
                    return;

                HudManager.Instance.SetText(baseObject.failedDialogue.dialogue);
                baseObject.failedDialogue.alreadyInteracted = true;
                return;
            }
            
            if (baseObject.successDialogue is { oneTime: true, alreadyInteracted: false }) {
                HudManager.Instance.SetText(baseObject.successDialogue.dialogue);
                baseObject.successDialogue.alreadyInteracted = true;
            }

            
            if (interaction is ObjectInteraction.Contextual) {
                ObtainShard();
                //AudioManager.Instance.PlayBreakGlassSound(transform.position);
                Instantiate(shardParticle, transform);
            }
        }

        public void Tick(float deltaTime) {
        }

        public void Dispose() {
            
        }

        public void CompleteObject() {
            ObtainShard();
        }

        void ObtainShard() {
            baseObject.GetCompletion = InteractionCompletion.Completed;
            baseObject.SetInteract(false);

            GameInitializer.Instance.AddShards(shards);
            
            if(glassRenderer)
                glassRenderer.material = brokenMat;
            
            Debug.Log($"[ObtainShardInteractable] {gameObject.name} Obtain Shard");
        }

        public void ResetObject() {
            GameObject glassMesh = transform.Find("Glass").gameObject; //ajout� par Paloma
            if (baseMat) { glassMesh.GetComponent<MeshRenderer>().material = baseMat; } //ajout� par Paloma

            baseObject.GetCompletion = InteractionCompletion.NotCompleted;
            baseObject?.SetInteract(true);
            Debug.Log($"[ObtainShardInteractable] {gameObject.name} Reset Object");
        }

        public BaseObject GetBaseObject() {
            return baseObject;
        }
    }
}