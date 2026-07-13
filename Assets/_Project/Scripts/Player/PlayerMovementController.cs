using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
using UnityEngine;

namespace _Project.Scripts.Player {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        private InputsBrain inputsBrain;
        private Rigidbody rb;

        [SerializeField] public PlayerConfiguration playerConfig;

        [Header("Mesh")] 
        [SerializeField] public Transform mesh;
    
        [Header("Ground Settings")] 
        [SerializeField] LayerMask groundLayer;
        [SerializeField] public Transform feetPosition;
        [SerializeField] public Vector3 feetSize;
    
        [Header("Camera Settings")]
        [SerializeField] UnityEngine.Camera cam;
    
        private PlayerController player;
    
        public float CurrentMaxSpeed { get; private set; }
        public float CurrentSpeed { get; private set; }
        public float CurrentFallSpeed { get; private set; }
        private float currentTimeToFall;
        
        private float currentSlopeMult;
        public float CurrentSlopeAngle { get; private set; }

        public float AccelTime { get; private set; }
        public float DecelTime { get; private set; }

        public float TimeBeforeMoving { get; private set; }
        private float timeBeforeMovingReset;
    
        private Vector3 moveDir; // Inputs joueur de direction
        public Vector3 PreviousMoveDir { get; private set; } // Keep last inputs joueur de direction
    
        private Vector3 slopeMoveDir; // Si le joueur est sur une slope
        private Vector3 forwardDir, rightDir; // Direction par rapport à l'angle de la caméra
        private Vector3 previousForwardDir;
    
        private RaycastHit slopeHit; // Pour check si le joueur est sur une slope

        private const float LerpTime = 1f;
        private float lerpTimer = 0f;
        private float currentDrag = 0f;
    
        public void Awake() {
        
            // Get every component needed
            if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
            if(TryGetComponent(out Rigidbody _rb)) rb = _rb;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
            if(TryGetComponent(out PlayerController _player)) player = _player;
            else Debug.LogWarning("[PlayerController] No PlayerController found");
        
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        
            UpdateCameraDir();
        }

        private void OnEnable() {
            inputsBrain.OnPlayerMove += SetDir;
        }

        private void OnDisable() {
            inputsBrain.OnPlayerMove -= SetDir;
        }

        private void UpdateCameraDir() {
            forwardDir = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
            rightDir = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up);

            if (forwardDir.sqrMagnitude < 0.0001f)
                forwardDir = previousForwardDir;
            else
                previousForwardDir = forwardDir;
        
            forwardDir.Normalize();
            rightDir.Normalize();
        }

        private void SetDir(Vector2 moveInput) {
            moveDir = moveInput.x * rightDir +  moveInput.y * forwardDir;
        }

        public void SetSpeed(PlayerSpeedEnum speed) {
            switch (speed) {
                case PlayerSpeedEnum.Normal :
                    CurrentMaxSpeed = playerConfig.normalMoveSpeed;
                    break;
                case PlayerSpeedEnum.None :
                    CurrentMaxSpeed = 0;
                    break;
            }
        }
    
        public void HandleUpdate() {
            if(rb.isKinematic) return;
        
            MeshRotation();
            CheckMethods();
            UpdateDrag();
        
            // TODO a voir pour déplacer cette fonction pour être appeler uniquement lors d'un changement de salle
            if(forwardDir != cam.transform.forward || rightDir != cam.transform.right)
                UpdateCameraDir();
        }

        private void MeshRotation() {
            if (player.IsUsingDoor()) return;
            if (moveDir == Vector3.zero) return;
        
            var angle = Mathf.Atan2(PreviousMoveDir.x, PreviousMoveDir.z) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(0, angle, 0);
            mesh.rotation = Quaternion.Slerp(mesh.rotation, targetRotation, playerConfig.rotationSpeed * Time.deltaTime);
        }

        private void CheckMethods() {
            slopeMoveDir = Vector3.ProjectOnPlane(PreviousMoveDir, slopeHit.normal);
        
            if(moveDir != Vector3.zero)
                PreviousMoveDir = moveDir;
        
            HandleTimeBeforeMoving();
            HandleAcceleration();
            HandlingSlope();

            if (IsGrounded()) {
                CurrentFallSpeed = 0;
                currentTimeToFall = 0;
            }
            else {
                currentTimeToFall += Time.deltaTime;
                if(currentTimeToFall >= playerConfig.timeBeforeApplyingFallSpeed)
                    CurrentFallSpeed = Mathf.SmoothStep(CurrentFallSpeed, playerConfig.maxFallSpeed, playerConfig.fallSpeedAccel * Time.deltaTime);
            }
        }

        private void HandleTimeBeforeMoving() {
            if (IsAgainstWall()) {
                TimeBeforeMoving = 0;
                return;
            }
        
            //Handling player time before moving when he start to press a move key
            TimeBeforeMoving = moveDir.magnitude > 0 ? 
                TimeBeforeMoving += Time.deltaTime : 
                timeBeforeMovingReset <= 0 ?
                    TimeBeforeMoving -= Time.deltaTime : 
                    TimeBeforeMoving = TimeBeforeMoving;

            if (rb.linearVelocity == Vector3.zero && moveDir == Vector3.zero && timeBeforeMovingReset <= 0)
                TimeBeforeMoving = 0;
        
            if(moveDir == Vector3.zero)
                timeBeforeMovingReset -= Time.deltaTime;

            if (TimeBeforeMoving >= playerConfig.timeBeforeMoving && moveDir != Vector3.zero)
                timeBeforeMovingReset = playerConfig.timeBeforeMovingReset;
        
            TimeBeforeMoving = Mathf.Clamp(TimeBeforeMoving, 0, playerConfig.timeBeforeMoving);
        }

        private void HandleAcceleration() {
            if (moveDir.magnitude > 0 && TimeBeforeMoving >= playerConfig.timeBeforeMoving && !IsAgainstWall()) {
                AccelTime += Time.deltaTime;
                DecelTime -= Time.deltaTime;
            
                if(CurrentSpeed >= CurrentMaxSpeed - 0.1f)
                    DecelTime = 0;
            
                CurrentSpeed = Mathf.Lerp(0, CurrentMaxSpeed, playerConfig.accelCurve.Evaluate(AccelTime / playerConfig.accelTime));
            
            }
            else {
                DecelTime += Time.deltaTime;
                AccelTime -= Time.deltaTime;
            
                if(CurrentSpeed <= 0.1f)
                    AccelTime = 0;
            
                CurrentSpeed = Mathf.Lerp(CurrentMaxSpeed, 0, playerConfig.decelCurve.Evaluate(DecelTime / playerConfig.decelTime));
            }
        
            DecelTime = Mathf.Clamp(DecelTime, 0, playerConfig.decelTime);
            AccelTime = Mathf.Clamp(AccelTime, 0, playerConfig.accelTime);
        }

        private void HandlingSlope() {
            rb.useGravity = !IsOnSlope();

            if (!IsOnSlope()) {
                currentSlopeMult = 1;
                return;
            }

            if (IsClimbingSlope())
                currentSlopeMult = Mathf.Lerp(1, playerConfig.maxSlopeDecreaseSpeed, CurrentSlopeAngle / playerConfig.maxSlopeAngle);
            else 
                currentSlopeMult = 1 + Mathf.Lerp(0, 1 - playerConfig.maxSlopeDecreaseSpeed, CurrentSlopeAngle / playerConfig.maxSlopeAngle);
        }

        private void UpdateDrag() {
            if(IsGrounded() && !Mathf.Approximately(currentDrag, playerConfig.groundDrag)) {
                currentDrag = playerConfig.groundDrag;
                rb.linearDamping = currentDrag;
            }
            else if (!IsGrounded() && !Mathf.Approximately(currentDrag, playerConfig.airDrag)) {
                currentDrag = playerConfig.airDrag;
                rb.linearDamping = playerConfig.airDrag;
            }
        }

        #region ApplyMovementForces

        public void HandleFixedUpdate() {
            if(rb.isKinematic) return;
            
            if (player.IsUsingDoor()) return;
            
            if (!IsGrounded())
                rb.AddForce(Vector3.down * CurrentFallSpeed, ForceMode.Acceleration);
        
            PlayerMove();
        }
    
        private void PlayerMove() {
            if (!IsGrounded())
                rb.AddForce(PreviousMoveDir.normalized * (CurrentSpeed * playerConfig.moveMult * playerConfig.airMoveMult), ForceMode.Acceleration);
            else if (!IsOnSlope())
                rb.AddForce(PreviousMoveDir.normalized * (CurrentSpeed * playerConfig.moveMult), ForceMode.Acceleration);
            else if(IsGrounded() && IsOnSlope())
                rb.AddForce(slopeMoveDir.normalized * (CurrentSpeed * currentSlopeMult * playerConfig.moveMult), ForceMode.Acceleration);
        }
    
        #endregion

        #region Settes/Helpers

        public void SetPosition(Vector3 position, Direction dir) {
            SetKinematic(true);
            rb.interpolation = RigidbodyInterpolation.None;
        
            rb.Move(position, Quaternion.identity);
            transform.position = position;
            Physics.SyncTransforms();
        
            mesh.eulerAngles = dir switch {
                Direction.Right => new Vector3(0, 90, 0),
                Direction.Left => new Vector3(0, -90, 0),
                Direction.Up => new Vector3(0, 0, 0),
                Direction.Down => new Vector3(0, 180, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            SetKinematic(false);
        }

        public void SetKinematic(bool doFreeze) {
            rb.isKinematic = doFreeze;
        }

        public float GetSpeedRatio() {
            if (player.IsUsingDoor()) return 0;
            return CurrentSpeed / CurrentMaxSpeed;
        }

        public float SetAnimatorSpeed() {
            if(rb.isKinematic || player.GetFailedDrop()) return lerpTimer = Mathf.Clamp(lerpTimer - Time.deltaTime * 6f, 0, LerpTime);
        
            if (moveDir.magnitude > 0 && !IsAgainstWall()) 
                return lerpTimer = Mathf.Clamp(lerpTimer + Time.deltaTime * 3f, 0, LerpTime);
        
            return lerpTimer = Mathf.Clamp(lerpTimer - Time.deltaTime * 4f, 0, LerpTime);
        }
    
        public bool IsPlayerFrozen() {
            return rb.isKinematic;
        }

        public Rigidbody GetRigidbody() {
            return rb;
        }

        #endregion
    
        #region Boolean
    
        public bool IsGrounded() {
            float angle = 0;
            if (slopeHit.normal != Vector3.up)
                angle = Vector3.Angle(Vector3.up, slopeHit.normal);
        
            return Physics.CheckBox(feetPosition.position, feetSize, Quaternion.identity, groundLayer) && angle <= playerConfig.maxSlopeAngle;
        }

        public bool IsOnSlope() {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Mathf.Infinity, groundLayer)) {
                if (slopeHit.normal != Vector3.up) {
                    float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                
                    if(angle <= playerConfig.maxSlopeAngle) CurrentSlopeAngle = angle;
                
                    return angle <= playerConfig.maxSlopeAngle && angle != 0;
                }
            }

            CurrentSlopeAngle = 0;
            return false;
        }

        private bool IsClimbingSlope() {
            var rbDir = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).normalized;
            var verticalComponent = Vector3.Dot(rbDir, slopeHit.normal);

            return verticalComponent < -0.1f;
        }
        
        private bool IsAgainstWall() {
            Physics.Raycast(feetPosition.position + new Vector3(0,.1f,0), moveDir.normalized, out var hit, 0.6f);

            if (!hit.collider) return false;
            if(hit.collider.isTrigger) return false;
            
            var wallNormal = hit.normal;
            return Vector3.Dot(wallNormal, moveDir.normalized) < -0.1f;
        }
    
        #endregion
    }
}
