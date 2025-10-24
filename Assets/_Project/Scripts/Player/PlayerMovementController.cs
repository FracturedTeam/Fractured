using System;
using _Project.Scripts.Inputs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    private InputsBrain inputsBrain;
    private Rigidbody rb;

    [SerializeField] public PlayerConfiguration playerConfig;

    [Header("Mesh")] 
    [SerializeField] Transform mesh;
    
    [Header("Ground Settings")] 
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform feetPosition;
    [SerializeField] Vector3 feetSize;
    
    [Header("Camera Settings")]
    [SerializeField] Camera cam;


    internal float currentMaxSpeed;
    internal float currentSpeed;
    
    internal float currentFallSpeed;
    private float currentTimeToFall;
    
    private float currentSlopeMult;
    internal float currentSlopeAngle;

    internal float accelTime;
    internal float decelTime;

    internal float timeBeforeMoving;
    private float timeBeforeMovingReset;
    
    private Vector3 moveDir;//Inputs joueur de direction
    internal Vector3 previousMoveDir;//Keep last inputs joueur de direction
    
    private Vector3 slopeMoveDir;//Si le joueur est sur une slope
    private Vector3 forwardDir, rightDir;//Par rapport à la caméra
    
    private RaycastHit slopeHit;//Pour check si le joueur est sur une slope

    public void Awake() {
        
        //Get every component needed
        if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
        else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
        if(TryGetComponent(out Rigidbody _rb)) rb = _rb;
        else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
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
        forwardDir = cam.transform.forward;
        rightDir = cam.transform.right;
        
        forwardDir.y = 0;
        rightDir.y = 0;
        
        forwardDir.Normalize();
        rightDir.Normalize();
    }
    
    private void SetDir(Vector2 moveInput) =>
        moveDir = moveInput.x * rightDir +  moveInput.y * forwardDir;

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
        MeshRotation();
        CheckMethods();
        UpdateDrag();
        
        if(forwardDir != cam.transform.forward)
            UpdateCameraDir();
        else if(rightDir != cam.transform.right)
            UpdateCameraDir();
    }

    private void MeshRotation() {
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

    public void FreezeController() {
        rb.isKinematic = true;
    }

    public void UnfreezeController() {
        rb.isKinematic = false;
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

    internal bool IsOnSlope() {
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
    
    private void OnDrawGizmos() {
        //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        
        //Draw la box de détection des pieds
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetPosition.position, feetSize);
    }
}
