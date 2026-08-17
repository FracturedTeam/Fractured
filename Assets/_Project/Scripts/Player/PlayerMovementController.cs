using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player.States;
using _Project.Scripts.Player.States.SubStates;
using Unity.Cinemachine;
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
        
        private Vector3 forwardDir, rightDir; // Direction par rapport à l'angle de la caméra
        private Vector3 newForwardDir, newRightDir;
        private Vector3 rawMoveInput;
        private bool newCamDirBuffer;
    
        private RaycastHit slopeHit; // Pour check si le joueur est sur une slope
        private LayerMask nonWalkableLayer;

        private const float LerpTime = 1f;
        private float lerpTimer = 0f;
        private float currentDrag = 0f;

        private float lerpCameraDirTime;

        private bool isGrounded;
        private bool isOnSlope;
        private bool isAgainstWall;
        private bool camOn90Degrees;
        private bool useAlternateCameraDirection => alternateCameraDirection && !camOn90Degrees;
        private bool hasInput;
        
        private const float GroundRayDistance = 0.15f;
        
        private const float CameraUpdateInterval = 0.066f;
        private float cameraUpdateTimer;

        private bool HasMoveInput => moveDir.sqrMagnitude > 0.001f;

        private Quaternion cachedMeshedRotation;
        private Vector3 lastMeshDir;
        
        public void Awake() {
            if(TryGetComponent(out Rigidbody _rb)) rb = _rb;
            else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
            if(TryGetComponent(out PlayerController _player)) player = _player;
            else Debug.LogWarning("[PlayerController] No PlayerController found");
        
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            nonWalkableLayer = ~LayerMask.GetMask("Walkable");
        }

        private void OnEnable() {
            InputsBrain.Instance.OnPlayerMove += SetDir;
        }

        private void OnDisable() {
            if(InputsBrain.HasInstance)
                InputsBrain.Instance.OnPlayerMove -= SetDir;
        }

        private void SetDir(Vector2 moveInput) {
            rawMoveInput = moveInput;
            hasInput = moveInput.sqrMagnitude > 0.001f;
            
            // if (newCamDirBuffer && hasInput) return;
            //
            // moveDir = moveInput.x * rightDir +  moveInput.y * forwardDir;
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

            HandleCamera();
        }

        private void HandleCamera() {
            cameraUpdateTimer -= Time.deltaTime;
            if(cameraUpdateTimer > 0) return;
            cameraUpdateTimer = CameraUpdateInterval;
            
            var flatCamForward = Vector3.ProjectOnPlane(player.cinemachineBrain.OutputCamera.transform.forward, Vector3.up).normalized;
            var flatCamRight = Vector3.ProjectOnPlane(player.cinemachineBrain.OutputCamera.transform.right, Vector3.up).normalized;
            
            UpdateMoveDir(flatCamForward);

            moveDir = rawMoveInput.x * rightDir + rawMoveInput.y * forwardDir;
            
            var forwardAngle = Vector3.Dot(newForwardDir, flatCamForward);
            var rightAngle = Vector3.Dot(newRightDir, flatCamRight);
            
            if ((!Mathf.Approximately(forwardAngle, 1) || !Mathf.Approximately(rightAngle, 1)) && lerpCameraDirTime <= 0) 
                UpdateCameraDir();
        }

        private void UpdateMoveDir(Vector3 flatCamForward) {
            if(!newCamDirBuffer && !useAlternateCameraDirection) return;
            
            lerpCameraDirTime -= Time.deltaTime * 4;
            if(lerpCameraDirTime < 0) newCamDirBuffer = false;

            var lerpTime = lerpCameraDirTime / timeToSwitchToNewDir;
            
            var alternateForward = new Vector3();
            if (useAlternateCameraDirection) {
                var camToPlayerDir = transform.position - player.cinemachineBrain.OutputCamera.transform.position;
                alternateForward = Vector3.ProjectOnPlane(camToPlayerDir, Vector3.up).normalized;

                var dot = Vector3.Dot(alternateForward, flatCamForward);
                if(dot < 0) alternateForward = -alternateForward;
                
                alternateForward = Vector3.Lerp(newForwardDir, alternateForward, amountOfAlternateDirection);
            }

            if (hasInput) {
                forwardDir = Vector3.Lerp(useAlternateCameraDirection ? alternateForward : newForwardDir, forwardDir, lerpTime);
                rightDir = Vector3.Lerp(newRightDir, rightDir, lerpTime);
            }
            else {
                forwardDir = useAlternateCameraDirection ? alternateForward : newForwardDir;
                rightDir = newRightDir;
                newCamDirBuffer = false;
                lerpCameraDirTime = -1;
            }
        }

        private void UpdateCameraDir() {
            newForwardDir = Vector3.ProjectOnPlane(player.cinemachineBrain.OutputCamera.transform.forward, Vector3.up).normalized;
            newRightDir =  Vector3.ProjectOnPlane(player.cinemachineBrain.OutputCamera.transform.right, Vector3.up).normalized;

            newCamDirBuffer = true;
            lerpCameraDirTime = timeToSwitchToNewDir;

            var threshold = 1f;
            var yRotation = player.cinemachineBrain.OutputCamera.transform.rotation.eulerAngles.y;
            var nearestAngle = Mathf.Round(yRotation / 90f) * 90f;
            var difference = Mathf.Abs(yRotation - nearestAngle);
            
            camOn90Degrees = difference < threshold;
        }

        private void MeshRotation() {
            if (player.IsUsingDoor()) return;
            if (!HasMoveInput) return;
        
            if (PreviousMoveDir != lastMeshDir) {
                lastMeshDir = PreviousMoveDir;
                var angle = Mathf.Atan2(PreviousMoveDir.x, PreviousMoveDir.z) * Mathf.Rad2Deg;
                cachedMeshedRotation = Quaternion.Euler(0, angle, 0);
            }
            
            mesh.rotation = Quaternion.Slerp(mesh.rotation, cachedMeshedRotation, playerConfig.rotationSpeed * Time.deltaTime);
        }

        private void CheckMethods() {
            slopeMoveDir = Vector3.ProjectOnPlane(PreviousMoveDir, slopeHit.normal);
        
            if(HasMoveInput)
                PreviousMoveDir = moveDir;
        
            HandleTimeBeforeMoving();
            HandleAcceleration();
            HandlingSlope();

            if (isGrounded) {
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
            if (isAgainstWall) {
                TimeBeforeMoving = 0;
                return;
            }

            if (HasMoveInput) {
                TimeBeforeMoving += Time.deltaTime;
                timeBeforeMovingReset = playerConfig.timeBeforeMovingReset;
            }
            else {
                timeBeforeMovingReset -= Time.deltaTime;
                if (timeBeforeMovingReset <= 0) {
                    TimeBeforeMoving -= Time.deltaTime;

                    if (rb.linearVelocity.sqrMagnitude < 0.001f) {
                        TimeBeforeMoving = 0;
                    }
                }
            }
        
            TimeBeforeMoving = Mathf.Clamp(TimeBeforeMoving, 0, playerConfig.timeBeforeMoving);
        }

        private void HandleAcceleration() {
            if (HasMoveInput && TimeBeforeMoving >= playerConfig.timeBeforeMoving && !isAgainstWall) {
                AccelTime += Time.deltaTime;
                DecelTime -= Time.deltaTime;
            
                if(CurrentSpeed >= CurrentMaxSpeed - 0.1f)
                    DecelTime = 0;
            
                CurrentSpeed = CurrentMaxSpeed * playerConfig.accelCurve.Evaluate(AccelTime / playerConfig.accelTime);
            
            }
            else {
                DecelTime += Time.deltaTime;
                AccelTime -= Time.deltaTime;
            
                if(CurrentSpeed <= 0.1f)
                    AccelTime = 0;
            
                CurrentSpeed = CurrentMaxSpeed * (1f - playerConfig.accelCurve.Evaluate(DecelTime / playerConfig.decelTime));
            }
        
            DecelTime = Mathf.Clamp(DecelTime, 0, playerConfig.decelTime);
            AccelTime = Mathf.Clamp(AccelTime, 0, playerConfig.accelTime);
        }

        private void HandlingSlope() {
            rb.useGravity = !isOnSlope;

            if (!isOnSlope) {
                currentSlopeMult = 1;
                return;
            }

            currentSlopeMult = Mathf.Lerp(1, playerConfig.maxSlopeDecreaseSpeed, CurrentSlopeAngle / playerConfig.maxSlopeAngle);
        }

        #region ApplyMovementForces

        public void HandleFixedUpdate() {
            if (rb.isKinematic || player.IsUsingDoor()) return;

            isOnSlope = IsOnSlope();
            isGrounded = IsGrounded();
            isAgainstWall = IsAgainstWall();
            
            UpdateDrag();
            StepStairs();
            
            if (!isGrounded)
                rb.AddForce(Vector3.down * CurrentFallSpeed, ForceMode.Acceleration);
        
            PlayerMove();
        }

        private void UpdateDrag() {
            if(isGrounded && currentDrag != playerConfig.groundDrag) {
                currentDrag = playerConfig.groundDrag;
                rb.linearDamping = currentDrag;
            }
            else if (!isGrounded && currentDrag != playerConfig.airDrag) {
                currentDrag = playerConfig.airDrag;
                rb.linearDamping = playerConfig.airDrag;
            }
        }
        
        private void StepStairs() {
            if(!HasMoveInput) return;

            if (!Physics.Raycast(feetPosition.position + Vector3.up * 0.1f, mesh.forward, lowerHit)) return;
            
            if (!Physics.Raycast(feetPosition.position + Vector3.up * stepHeight, mesh.forward, upperHit)) {
                rb.position -= new Vector3(0f, -stepHeigtSmoothing * Time.fixedDeltaTime, 0f);
            }
        }

        private void PlayerMove() {
            if (!isGrounded)
                rb.AddForce(PreviousMoveDir.normalized * (CurrentSpeed * playerConfig.moveMult * playerConfig.airMoveMult), ForceMode.Acceleration);
            else if (!isOnSlope)
                rb.AddForce(PreviousMoveDir.normalized * (CurrentSpeed * playerConfig.moveMult), ForceMode.Acceleration);
            else if(isGrounded && isOnSlope)
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
            if(player.IsCurrentState<GrabObjectState>() || player.IsCurrentState<DropObjectState>() || player.IsCurrentState<TakeItemState>())  return lerpTimer = Mathf.Clamp(lerpTimer - Time.deltaTime * 6f, 0, LerpTime);
            if(rb.isKinematic || player.GetFailedDrop()) return lerpTimer = Mathf.Clamp(lerpTimer - Time.deltaTime * 6f, 0, LerpTime);
        
            if (HasMoveInput && !isAgainstWall) 
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
            return Physics.CheckBox(feetPosition.position, feetSize, Quaternion.identity, groundLayer) && CurrentSlopeAngle <= playerConfig.maxSlopeAngle;
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
            if(!HasMoveInput) return false;
            
            var dir = isOnSlope ? slopeMoveDir : moveDir.normalized;
            
            Physics.Raycast(feetPosition.position + new Vector3(0,.1f,0), dir, out var hit, 0.6f, nonWalkableLayer);
        
            if (!hit.collider) return false;
            if(hit.collider.isTrigger) return false;
            
            var wallNormal = hit.normal;
            return Vector3.Dot(wallNormal, moveDir.normalized) < -0.1f;
        }
    
        #endregion
    }
}
