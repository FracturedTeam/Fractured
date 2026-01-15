using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Inputs;
using _Project.Scripts.Player;
using UnityEngine;

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
    [SerializeField] Camera cam;
    
    private PlayerController player;
    
    public float currentMaxSpeed { get; private set; }
    public float currentSpeed { get; private set; }
    
    public float currentFallSpeed { get; private set; }
    private float currentTimeToFall;
    
    private float currentSlopeMult;
    public float currentSlopeAngle { get; private set; }

    public float accelTime { get; private set; }
    public float decelTime { get; private set; }

    public float timeBeforeMoving { get; private set; }
    private float timeBeforeMovingReset;
    
    private Vector3 moveDir;//Inputs joueur de direction
    public Vector3 previousMoveDir { get; private set; }//Keep last inputs joueur de direction
    
    private Vector3 slopeMoveDir;//Si le joueur est sur une slope
    private Vector3 forwardDir, rightDir;//Par rapport à la caméra
    private Vector3 previousForwardDir;
    
    private RaycastHit slopeHit;//Pour check si le joueur est sur une slope

    private float lerpTime = 1f;
    private float lerpTimer = 0f;

    public void Awake() {
        
        //Get every component needed
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
                currentMaxSpeed = playerConfig.normalMoveSpeed;
                break;
            case PlayerSpeedEnum.None :
                currentMaxSpeed = 0;
                break;
        }
    }
    
    public void HandleUpdate() {
        if(rb.isKinematic) return;
        
        MeshRotation();
        CheckMethods();
        UpdateDrag();
        
        if(forwardDir != cam.transform.forward || rightDir != cam.transform.right)
            UpdateCameraDir();
    }

    private void MeshRotation() {
        if (player.interact.UsingDoor()) return;
        if(moveDir == Vector3.zero) return;
        
        var angle = Mathf.Atan2(previousMoveDir.x, previousMoveDir.z) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(0, angle, 0);
        mesh.rotation = Quaternion.Slerp(mesh.rotation, targetRotation, playerConfig.rotationSpeed * Time.deltaTime);
    }

    private void CheckMethods() {
        slopeMoveDir = Vector3.ProjectOnPlane(previousMoveDir, slopeHit.normal);
        
        if(moveDir != Vector3.zero)
            previousMoveDir = moveDir;
        
        HandleTimeBeforeMoving();
        HandleAcceleration();
        HandlingSlope();

        if (IsGrounded()) {
            currentFallSpeed = 0;
            currentTimeToFall = 0;
        }
        else {
            currentTimeToFall += Time.deltaTime;
            if(currentTimeToFall >= playerConfig.timeBeforeApplyingFallSpeed)
                currentFallSpeed = Mathf.SmoothStep(currentFallSpeed, playerConfig.maxFallSpeed, playerConfig.fallSpeedAccel * Time.deltaTime);
        }
    }

    private void HandleTimeBeforeMoving() {
        //Handling player time before moving when he start to press a move key
        timeBeforeMoving = moveDir.magnitude > 0 ? 
            timeBeforeMoving += Time.deltaTime : 
            timeBeforeMovingReset <= 0 ?
                timeBeforeMoving -= Time.deltaTime : 
                timeBeforeMoving = timeBeforeMoving;

        if (rb.linearVelocity == Vector3.zero && moveDir == Vector3.zero && timeBeforeMovingReset <= 0)
            timeBeforeMoving = 0;
        
        if(moveDir == Vector3.zero)
            timeBeforeMovingReset -= Time.deltaTime;

        if (timeBeforeMoving >= playerConfig.timeBeforeMoving && moveDir != Vector3.zero)
            timeBeforeMovingReset = playerConfig.timeBeforeMovingReset;
        
        timeBeforeMoving = Mathf.Clamp(timeBeforeMoving, 0, playerConfig.timeBeforeMoving);
    }

    private void HandleAcceleration() {
        if (moveDir.magnitude > 0 && timeBeforeMoving >= playerConfig.timeBeforeMoving) {
            accelTime += Time.deltaTime;
            decelTime -= Time.deltaTime;
            
            if(currentSpeed >= currentMaxSpeed - 0.1f)
                decelTime = 0;
            
            currentSpeed = Mathf.Lerp(0, currentMaxSpeed, playerConfig.accelCurve.Evaluate(accelTime / playerConfig.accelTime));
            
        }
        else {
            decelTime += Time.deltaTime;
            accelTime -= Time.deltaTime;
            
            if(currentSpeed <= 0.1f)
                accelTime = 0;
            
            currentSpeed = Mathf.Lerp(currentMaxSpeed, 0, playerConfig.decelCurve.Evaluate(decelTime / playerConfig.decelTime));
        }
        
        decelTime = Mathf.Clamp(decelTime, 0, playerConfig.decelTime);
        accelTime = Mathf.Clamp(accelTime, 0, playerConfig.accelTime);
    }

    private void HandlingSlope() {
        rb.useGravity = !IsOnSlope();

        if (!IsOnSlope()) {
            currentSlopeMult = 1;
            return;
        }

        if (IsClimbingSlope())
            currentSlopeMult = Mathf.Lerp(1, playerConfig.maxSlopeDecreaseSpeed, currentSlopeAngle / playerConfig.maxSlopeAngle);
        else 
            currentSlopeMult = 1 + Mathf.Lerp(0, 1 - playerConfig.maxSlopeDecreaseSpeed, currentSlopeAngle / playerConfig.maxSlopeAngle);
    }
    
    private void UpdateDrag() =>
        rb.linearDamping = IsGrounded() ? playerConfig.groundDrag : playerConfig.airDrag;

    #region ApplyMovementForces

    public void HandleMovement() {
        if (!IsGrounded())
            rb.AddForce(Vector3.down * currentFallSpeed, ForceMode.Acceleration);
        
        if (player.interact.UsingDoor()) return;
        
        PlayerMove();
    }
    
    private void PlayerMove() {
        if (!IsGrounded())
            rb.AddForce(previousMoveDir.normalized * (currentSpeed * playerConfig.moveMult * playerConfig.airMoveMult), ForceMode.Acceleration);
        else if (!IsOnSlope())
            rb.AddForce(previousMoveDir.normalized * (currentSpeed * playerConfig.moveMult), ForceMode.Acceleration);
        else if(IsGrounded() && IsOnSlope())
            rb.AddForce(slopeMoveDir.normalized * (currentSpeed * currentSlopeMult * playerConfig.moveMult), ForceMode.Acceleration);
    }
    
    #endregion

    public void SetPosition(Vector3 position, Direction dir) {
        FreezeController();
        rb.position = position;
        Physics.SyncTransforms();
        mesh.eulerAngles = dir switch {
            Direction.Right => new Vector3(0, 90, 0),
            Direction.Left => new Vector3(0, -90, 0),
            Direction.Up => new Vector3(0, 0, 0),
            Direction.Down => new Vector3(0, 180, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
        UnfreezeController();
    }

    public void FreezeController() {
        rb.isKinematic = true;
    }

    public void UnfreezeController() {
        rb.isKinematic = false;
    }

    public float GetSpeedRatio() {
        if (player.interact.UsingDoor()) return 0;
        return currentSpeed / currentMaxSpeed;
    }

    public float SetAnimatorSpeed() {
        if (moveDir.magnitude > 0) 
          return lerpTimer = Mathf.Clamp(lerpTimer + Time.deltaTime * 3f, 0, lerpTime);
        
        return lerpTimer = Mathf.Clamp(lerpTimer - Time.deltaTime * 4f, 0, lerpTime);
    }
    
    internal bool IsPlayerFrozen() {
        return rb.isKinematic;
    }
    
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
                
                if(angle <= playerConfig.maxSlopeAngle) currentSlopeAngle = angle;
                
                return angle <= playerConfig.maxSlopeAngle && angle != 0;
            }
        }

        currentSlopeAngle = 0;
        return false;
    }

    private bool IsClimbingSlope() {
        var rbDir = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).normalized;
        var verticalComponent = Vector3.Dot(rbDir, slopeHit.normal);

        return verticalComponent < -0.1f;
    }
    
    #endregion
}
