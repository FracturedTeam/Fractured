using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.Timers;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.ECS
{
    public class Glass : MonoBehaviour, IDragHandler {
        [SerializeField, HideInInspector] private FragmentData data;
        
        public void Bind(FragmentData data) {
            this.data = data;
        }
        
        public void SaveData() {
            data.position = transform.position;
            data.spawned = spawned;
        }
        
        public void LoadData() {
            transform.position = data.position;
            spawned = data.spawned;
            Set3DShard();
        }
        
        public ColorEnum GetColor => color2D;

        [Header("Settings")] [SerializeField] private ColorEnum color2D;
        [SerializeField] private bool canEditAnywhere = false;
        [SerializeField] private bool spawned = false;
        [SerializeField] private Fragment shard;
        [HideInInspector] public Fragment visualShard;
        [HideInInspector] public ParticleSystem visualParticles;

        private Camera mainCamera;
        private Image shardSprite;
        private PolygonCollider2D polygonCollider2D;
        private Vector2 mousePosition;

        private bool isHeld;
        private bool isOnTop;


        private bool initialized = false;
        private bool canInteract = true;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!initialized) {
                mainCamera = PlayerController.Instance.cinemachineBrain.OutputCamera;

                if (mainCamera == null)
                    Debug.LogError($"[Glass] Camera not tagged as MainCamera, Camera could not been acquired !");

                if (TryGetComponent(typeof(Image), out var img))
                    shardSprite = img as Image;

                if (TryGetComponent(typeof(PolygonCollider2D), out var col))
                    polygonCollider2D = col as PolygonCollider2D;
                
                if(shard) 
                {
                    if (shardSprite) 
                        shardSprite.color = Color.clear;
                    InstantiateShard();
                }
            }
            initialized = true;
        }

        private void InstantiateShard() {
            var sh = Instantiate(shard);
            shard = sh;
                
            shard.SetColor(color2D);
            if (!spawned)
            {
                shard.gameObject.SetActive(false);
                HudManager.Instance.ShardSpawn(this);
                spawned = true;
                SaveData();
            }
            Set3DShard();
        }

        public void SetUp3dShard()
        {
            shard.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData) {
            if(!canInteract) return;
            if(!isHeld) return;
            if(MemoryManager.Instance.isInMemory) return;

            if (!GameInitializer.Instance.InEditableArea() && !canEditAnywhere) {
                if(color2D is ColorEnum.Blue && !GameInitializer.Instance.InBlueEditableArea()) return;
                if(color2D is ColorEnum.Red && !GameInitializer.Instance.InRedEditableArea()) return;
            }

            transform.position += (Vector3)eventData.delta; 
            
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, 0 + shardSprite.rectTransform.sizeDelta.x/2, Screen.width - shardSprite.rectTransform.sizeDelta.x/2),
                Mathf.Clamp(transform.position.y, 0 + shardSprite.rectTransform.sizeDelta.y/2, Screen.height  - shardSprite.rectTransform.sizeDelta.y/2));

            Set3DShard();
        }
        
        private void Set3DShard() {
            if(polygonCollider2D == null)
                if (TryGetComponent(typeof(PolygonCollider2D), out var col))
                    polygonCollider2D = col as PolygonCollider2D;
            
            if(mainCamera == null)
                mainCamera = PlayerController.Instance.cinemachineBrain.OutputCamera;
            
            if (!shard) 
                return;
            
            List<Vector3> cornersPos = new ();
            foreach (var points in polygonCollider2D.points)
                cornersPos.Add( mainCamera.ScreenToWorldPoint(new Vector3(transform.position.x + points.x * Screen.height/1080 + polygonCollider2D.offset.x, 
                    transform.position.y + points.y  * Screen.height/1080+ polygonCollider2D.offset.y, 10)));
                
            shard.Setup(cornersPos);
            
            if(visualShard) 
                visualShard.Setup(cornersPos);
        }

        private void OnEnable() {
            shard.gameObject.SetActive(true);
        }

        private void OnDisable() {
            shard?.gameObject.SetActive(false);
        }

        void OnDestroy() {
            if(shard) Destroy(shard.gameObject);
        }
        
        internal void ChangeHoldingState(bool isOn) {
            if (!canInteract) return;
            if (!canEditAnywhere) {
                if (!GameInitializer.Instance.InEditableArea() && isOn) {
                    AudioManager.Instance.PlayGrabGlassFailedSound();
                    return;
                }
            }

            isHeld = isOn;
            if (isOn) AudioManager.Instance.PlayGrabGlassSound();
        }
        
        
        ///Get if an object is colliding with any the colliders 2D
        internal bool IsColliding(Vector3 position)
        {
            if (!mainCamera)
                mainCamera = PlayerController.Instance.cinemachineBrain.OutputCamera;
            if (mainCamera == null)
                return false;

            Vector3 closest = polygonCollider2D.ClosestPoint(position);
            return closest == position;
        }

        internal bool IsColliding(Vector3[] positions)
        {
            if (!mainCamera)
                return false;
            
            foreach (var position in positions)
            {
                Vector3 closest = polygonCollider2D.ClosestPoint(position);
                if(closest != position)
                    return false;
            }
            return true;
        }

        public void SetEditAnywhere(bool editAnywhere) {
            canEditAnywhere = editAnywhere;
        }

        public void SetInFront(bool setOnTop)
        {
            if(!GameInitializer.Instance.InEditableArea())
                return;
                
            var vector3 = shard.transform.position;
            vector3.z = setOnTop ? 0 : -0.0001f;
            shard.transform.position = vector3;
        }
    }
}