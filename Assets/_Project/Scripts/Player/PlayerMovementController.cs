using System;
using System.Threading.Tasks;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Inputs;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

namespace _Project.Scripts.Player {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        // private InputsBrain inputsBrain;
        private Rigidbody rb;

        [SerializeField] public PlayerConfiguration playerConfig;

        [Header("Mesh")] 
        [SerializeField] public Transform mesh;
    
        [Header("Ground Settings")] 
        [SerializeField] LayerMask groundLayer;
        [SerializeField] public Transform feetPosition;
        [SerializeField] public Vector3 feetSize;
    
        [Header("Step Settings")]
        [SerializeField] private float lowerHit = 0.1f;
        [SerializeField] private float upperHit = 0.2f;
        [SerializeField] private float stepHeight = 0.2f;
        [SerializeField] private float stepHeigtSmoothing = 2f;
        
        [Header("Camera Settings")]
        [SerializeField] UnityEngine.Camera cam;
        [SerializeField] private bool alternateCameraDirection;
        [SerializeField, Range(0f, 1f)] private float amountOfAlternateDirection = 0f;
        [SerializeField] private float timeToSwitchToNewDir = 2f;
    
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
        
        private Vector3 rawMoveDir; // Inputs joueur de direction
        private Vector3 forwardDir, rightDir; // Direction par rapport à l'angle de la caméra
        private Vector3 newForwardDir, newRightDir;
        private bool newCamDirBuffer;
    
        private RaycastHit slopeHit; // Pour check si le joueur est sur une slope

        private const float LerpTime = 1f;
        private float lerpTimer = 0f;
        private float currentDrag = 0f;

        private float lerpCameraDirTime;
        
        public void Awake() {
        
            // Get every component needed
            // if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
            // else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
            if(TryGetComponent(out Rigidbody _rb)) rb = _rb;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
            if(TryGetComponent(out PlayerController _player)) player = _player;
            else Debug.LogWarning("[PlayerController] No PlayerController found");
        
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void OnEnable() {
            InputsBrain.Instance.OnPlayerMove += SetDir;
        }

        private void OnDisable() {
            InputsBrain.Instance.OnPlayerMove -= SetDir;
        }

        private void SetDir(Vector2 moveInput) {
            moveDir = moveInput.x * rightDir +  moveInput.y * forwardDir;
            rawMoveDir = moveInput;
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

            HandleCamera();
        }

        private void HandleCamera() {
            UpdateMoveDir();

            var forwardAngle = Vector3.Dot(newForwardDir, Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized);
            var rightAngle = Vector3.Dot(newRightDir, Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized);
            
            if ((!Mathf.Approximately(forwardAngle, 1) || !Mathf.Approximately(rightAngle, 1)) && lerpCameraDirTime <= 0) 
                UpdateCameraDir();
        }

        private void UpdateMoveDir() {
            if(!newCamDirBuffer && !alternateCameraDirection) return;
            
            lerpCameraDirTime -= Time.deltaTime;
            if(lerpCameraDirTime < 0) newCamDirBuffer = false;

            var lerpTime = lerpCameraDirTime / timeToSwitchToNewDir;
            
            var alternateForward = new Vector3();
            if (alternateCameraDirection) {
                var camToPlayerDir = transform.position - cam.transform.position;
                alternateForward = Vector3.ProjectOnPlane(camToPlayerDir, Vector3.up).normalized;
                alternateForward = Vector3.Lerp(newForwardDir, alternateForward, amountOfAlternateDirection);
            }

            if (moveDir != Vector3.zero) {
                forwardDir = Vector3.Lerp(alternateCameraDirection ? alternateForward : newForwardDir, forwardDir, lerpTime);
                rightDir = Vector3.Lerp(newRightDir, rightDir, lerpTime);
            }
            else {
                forwardDir = alternateCameraDirection ? alternateForward : newForwardDir;
                rightDir = newRightDir;
                newCamDirBuffer = false;
                lerpCameraDirTime = -1;
            }
        }

        private void UpdateCameraDir() {
            newForwardDir = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
            newRightDir =  Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;

            newCamDirBuffer = true;
            lerpCameraDirTime = timeToSwitchToNewDir;
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

            currentSlopeMult = Mathf.Lerp(1, playerConfig.maxSlopeDecreaseSpeed, CurrentSlopeAngle / playerConfig.maxSlopeAngle);
            // if (IsClimbingSlope())
            //     currentSlopeMult = Mathf.Lerp(1, playerConfig.maxSlopeDecreaseSpeed, CurrentSlopeAngle / playerConfig.maxSlopeAngle);
            // else 
            //     currentSlopeMult = 1 + Mathf.Lerp(0, 1 - playerConfig.maxSlopeDecreaseSpeed, CurrentSlopeAngle / playerConfig.maxSlopeAngle);
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

            if(moveDir.magnitude > 0)
                StepStairs();
            
            if (!IsGrounded())
                rb.AddForce(Vector3.down * CurrentFallSpeed, ForceMode.Acceleration);
        
            PlayerMove();
        }

        private void StepStairs() {
            if (Physics.Raycast(feetPosition.position + Vector3.up * 0.1f, mesh.forward, lowerHit)) {
                if (!Physics.Raycast(feetPosition.position + Vector3.up * stepHeight, mesh.forward, upperHit)) {
                    rb.position -= new Vector3(0f, -stepHeigtSmoothing * Time.fixedDeltaTime, 0f);
                }
            }
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

        // private void OnDrawGizmos() {
        //     Gizmos.matrix = Matrix4x4.identity;
        //     
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(feetPosition.position, feetPosition.position + mesh.forward * lowerHit);
        //     Gizmos.DrawLine(feetPosition.position + Vector3.up * stepHeight, feetPosition.position + Vector3.up * stepHeight + mesh.forward * upperHit);
        // }

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
            var dir = moveDir.normalized;
            if (IsOnSlope()) dir = slopeMoveDir;
            
            var groundLayer = LayerMask.GetMask("Walkable");
            
            Physics.Raycast(feetPosition.position + new Vector3(0,.1f,0), dir, out var hit, 0.6f, ~groundLayer);

            if (!hit.collider) return false;
            if(hit.collider.isTrigger) return false;
            
            var wallNormal = hit.normal;
            return Vector3.Dot(wallNormal, moveDir.normalized) < -0.1f;
        }
    
        #endregion
    }
}
