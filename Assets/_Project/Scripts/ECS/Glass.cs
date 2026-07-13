using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.ECS
{
    public class Glass : MonoBehaviour, IDragHandler {
        
        #region Save/Load
        
        [SerializeField, HideInInspector] public FragmentData data;
        
        [field:SerializeField] public string Guid { get; set; }
        
        private System.Guid _guid;

        public System.Guid guid {
            get {
                if(_guid == System.Guid.Empty && !System.String.IsNullOrEmpty(Guid))
                {
                    _guid = new System.Guid(Guid);
                }

                return _guid;
            }
        }
        
        #if UNITY_EDITOR

        [ContextMenu("Generate Unique ID")]
        public void GenerateGuid() {
            _guid = System.Guid.NewGuid();
            Guid = _guid.ToString();
            EditorUtility.SetDirty(this);
        }
        
        #endif
        
        public void Bind(FragmentData data) {
            this.data = data;
            if (String.IsNullOrEmpty(Guid)) {
                Debug.LogError($"[Glass] {gameObject.name} does not have Guid, please generate it");
                return;
            }
            data.Guid = Guid;
        }
        
        public void SaveData() {
            if(data.Guid != Guid) return;
            
            data.position = transform.position;
            data.spawned = spawned;
        }
        
        public void LoadData() {
            if(data.Guid != Guid) return;
            
            transform.position = data.position;
            spawned = data.spawned;
            Set3DShard();
        }

        #endregion
        
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

            // if (!GameInitializer.Instance.InEditableArea() && !canEditAnywhere) {
            //     if(color2D is ColorEnum.Blue && !GameInitializer.Instance.InBlueEditableArea()) return;
            //     if(color2D is ColorEnum.Red && !GameInitializer.Instance.InRedEditableArea()) return;
            // }

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
            shard?.gameObject.SetActive(true);
        }

        /*private void OnDisable() {
            shard?.gameObject.SetActive(false);
        }*/

        void OnDestroy() {
            if(shard) Destroy(shard.gameObject);
        }
        
        internal void ChangeHoldingState(bool isOn) {
            if (!canInteract) return;
            // if (!canEditAnywhere) {
            //     if (!GameInitializer.Instance.InEditableArea() && isOn) {
            //         GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().grabGlassFailedSound);
            //         return;
            //     }
            // }

            isHeld = isOn;
            if (isOn) GameInitializer.Instance.PlaySound2D(GameInitializer.Instance.GetBank().grabGlassSound);
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
    
    [Serializable]
    public class FragmentData {
        [field: SerializeField] public string Guid { get; set; }
        public Vector3 position;
        public bool spawned;
    }
}