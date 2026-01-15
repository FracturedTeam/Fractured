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
        }
        
        public void LoadData() {
            transform.position = data.position;
            Set3DShard();
        }
        
        public ColorEnum GetColor => color2D;

        [Header("Settings")] [SerializeField] private ColorEnum color2D;
        [SerializeField] private bool canEditAnywhere = false;
        [SerializeField] private Fragment shard;
        public Fragment VisualShard;

        private Camera mainCamera;
        private Image shardSprite;
        private PolygonCollider2D polygonCollider2D;
        private Vector2 mousePosition;

        private bool isHeld;

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
                
                if(shard) {
                    if (shardSprite) shardSprite.color = Color.clear;
                    InstantiateShard();
                }
            }
            initialized = true;
        }

        private void InstantiateShard() {
            var sh = Instantiate(shard);
            shard = sh;
                
            shard.SetColor(color2D);
            shard.gameObject.SetActive(false);
            HudManager.Instance.ShardSpawn(this);
            Set3DShard();
        }

        public void SetUp3dShard()
        {
            shard.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData) {
            if (!canInteract) return;
            if(!isHeld) return;

            if (!GameInitializer.Instance.InEditableArea() && !canEditAnywhere) {
                if(color2D is ColorEnum.Blue && !GameInitializer.Instance.InBlueEditableArea() && !MemoryManager.Instance.isInMemory)
                    return;
                if(color2D is ColorEnum.Red && !GameInitializer.Instance.InRedEditableArea() && !MemoryManager.Instance.isInMemory)
                    return;
            }

            transform.position += (Vector3)eventData.delta; 
            
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, 0 + shardSprite.rectTransform.sizeDelta.x/2, 1920 - shardSprite.rectTransform.sizeDelta.x/2),
                Mathf.Clamp(transform.position.y, 0 + shardSprite.rectTransform.sizeDelta.y/2, 1080 - shardSprite.rectTransform.sizeDelta.y/2));

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
                cornersPos.Add( mainCamera.ScreenToWorldPoint(new Vector3(transform.position.x + points.x + polygonCollider2D.offset.x, 
                    transform.position.y + points.y + polygonCollider2D.offset.y, 10)));
                
            shard.Setup(cornersPos);
            
            if(VisualShard) 
                VisualShard.Setup(cornersPos);
        }

        private void OnEnable()
        {
            if (shard) 
                shard.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if(shard)
                shard?.gameObject.SetActive(false);
        }

        void OnDestroy() {
            if(shard) Destroy(shard.gameObject);
        }
        
        internal void ChangeHoldingState(bool isOn) {
            if (!canInteract) return;
            if (!GameInitializer.Instance.InEditableArea()) {
                AudioManager.Instance.PlayGrabGlassFailedSound();
                return;
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

        private void SetInteract(bool canInteract) {
            this.canInteract = canInteract;
        }

        public void SetEditAnywhere(bool editAnywhere) {
            canEditAnywhere = editAnywhere;
        }
    }
}