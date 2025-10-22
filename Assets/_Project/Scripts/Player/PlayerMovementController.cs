using System;
using _Project.Scripts.Inputs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    private InputsBrain inputsBrain;
    Rigidbody rb;

    [SerializeField] PlayerConfiguration playerConfig;

    [Header("Ground Settings")] 
    public LayerMask groundLayer;
    public Transform feetPosition;
    public Vector3 feetSize;
    
    [Header("Camera Settings")]
    public Camera cam;


    private float currentMaxSpeed;
    private float currentSpeed;
    
    private float currentFallSpeed;
    private float currentTimeToFall;
    
    private float currentSlopeMult;
    private float currentSlopeAngle;

    private float accelTime;
    private float decelTime;
    
    private Vector3 moveDir;//Inputs joueur de direction
    private Vector3 previousMoveDir;//Keep last inputs joueur de direction
    
    private Vector3 slopeMoveDir;//Si le joueur est sur une slope
    private Vector3 forwardDir, rightDir;//Par rapport à la caméra
    
    private RaycastHit slopeHit;//Pour check si le joueur est sur une slope
    
    private void Awake() {
        if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
        else Debug.LogWarning("[PlayerController] No InputsBrain found");
        
        if(TryGetComponent(out Rigidbody _rb)) rb = _rb;
        else Debug.LogWarning("[PlayerController] No RigidBody found");
        
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
        CheckMethods();
        UpdateDrag();
        
        if(forwardDir != cam.transform.forward)
            UpdateCameraDir();
        else if(rightDir != cam.transform.right)
            UpdateCameraDir();
    }

    private void CheckMethods() {
        slopeMoveDir = Vector3.ProjectOnPlane(previousMoveDir, slopeHit.normal);
        
        if(moveDir != Vector3.zero)
            previousMoveDir = moveDir;
        
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

    private void HandleAcceleration() {
        if(decelTime < 0)
            decelTime = 0;
        else if(decelTime > playerConfig.decelTime)
            decelTime = playerConfig.decelTime;
        
        if(accelTime < 0)
            accelTime = 0;
        else if(accelTime > playerConfig.accelTime)
            accelTime = playerConfig.accelTime;
        
        if (moveDir.magnitude > 0) {
            accelTime += Time.deltaTime;
            decelTime -= Time.deltaTime;
            
            if(currentSpeed >= currentMaxSpeed - 0.1f)
                decelTime = 0;
            
            currentSpeed = Mathf.Lerp(0, currentMaxSpeed, accelTime / playerConfig.accelTime);
        }
        else {
            decelTime += Time.deltaTime;
            accelTime -= Time.deltaTime;
            
            if(currentSpeed <= 0.1f)
                accelTime = 0;
            
            currentSpeed = Mathf.Lerp(currentMaxSpeed, 0, decelTime / playerConfig.decelTime);
        }
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
    
    #region Boolean
    
    public bool IsGrounded() {
        float angle = 0;
        if (slopeHit.normal != Vector3.up)
            angle = Vector3.Angle(Vector3.up, slopeHit.normal);
        
        return Physics.CheckBox(feetPosition.position, feetSize, Quaternion.identity, groundLayer) && angle <= playerConfig.maxSlopeAngle;
    }

    private bool IsOnSlope() {
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
