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
    private float currentFallSpeed;
    
    private Vector3 moveDir;//Inputs joueur de direction
    
    private Vector3 slopeNormal;//Si le joueur est sur une slope
    private Vector3 forwardDir, rightDir;//Par rapport à la caméra
    
    private RaycastHit slopeHit;//Pour check si le joueur est sur une slope
    
    private void Awake() {
        if(TryGetComponent(out InputsBrain _input)) inputsBrain = _input;
        else Debug.LogWarning("No InputsBrain found");
        
        if(TryGetComponent(out Rigidbody _rb)) rb = _rb;
        else Debug.LogWarning("No InputsBrain found");
        
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
        slopeNormal = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);
        
        //currentAcceleration = moveDir.magnitude > 0.01f ? playerConfig.accel : playerConfig.decel;

        if (IsGrounded())
            currentFallSpeed = 0;
        else 
            currentFallSpeed = Mathf.SmoothStep(currentFallSpeed, playerConfig.maxFallSpeed, playerConfig.fallSpeedAccel * Time.deltaTime);
        
        rb.useGravity = !IsOnSlope();
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
            rb.AddForce(moveDir.normalized * (currentMaxSpeed * playerConfig.moveMult * playerConfig.airMoveMult), ForceMode.Acceleration);
        else if (!IsOnSlope())
            rb.AddForce(moveDir.normalized * (currentMaxSpeed * playerConfig.moveMult), ForceMode.Acceleration);
        else if(IsGrounded() && IsOnSlope())
            rb.AddForce(slopeNormal.normalized * (currentMaxSpeed * playerConfig.moveMult), ForceMode.Acceleration);
        
        /*if (!IsGrounded())
            rb.linearVelocity = TargetVelocity(moveDir, playerConfig.moveSpeed * playerConfig.moveMult * playerConfig.airMoveMult, false);
        else {
            if (!IsOnSlope())
                rb.linearVelocity = TargetVelocity(moveDir, playerConfig.moveSpeed * playerConfig.moveMult);
            else if(IsOnSlope())
                rb.linearVelocity = TargetVelocity(slopeNormal, playerConfig.moveSpeed * playerConfig.moveMult);
        }*/
        
        /*if (!IsGrounded())
            rb.linearVelocity = TargetVelocity(moveDir, currentMaxSpeed * playerConfig.airMoveMult, false);
        else {
            if (!IsOnSlope())
                rb.linearVelocity = TargetVelocity(moveDir, currentMaxSpeed);
            else if(IsOnSlope())
                rb.linearVelocity = TargetVelocity(slopeNormal, currentMaxSpeed);
        }*/
    }

    Vector3 TargetVelocity(Vector3 dir, float forces, bool grounded = true) {
        Vector3 targetVel = dir * forces;
        Vector3 currentVel = rb.linearVelocity;
        
        Vector3 newVel = Vector3.MoveTowards(currentVel, targetVel, 
            (targetVel.magnitude > currentVel.magnitude ? playerConfig.accel : playerConfig.decel) * Time.deltaTime);
       
        return grounded ? 
            newVel : new Vector3(newVel.x, rb.linearVelocity.y, newVel.z);
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
        if(slopeHit.normal != Vector3.up)
            angle = Vector3.Angle(Vector3.up, slopeHit.normal);
        
        return Physics.CheckBox(feetPosition.position, feetSize, Quaternion.identity, groundLayer) && angle <= playerConfig.maxSlopeAngle;
    }

    private bool IsOnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Mathf.Infinity, groundLayer)) {
            if (slopeHit.normal != Vector3.up) {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle <= playerConfig.maxSlopeAngle && angle != 0;
            }
        }
        return false;
    }

    #endregion
    
    private void OnDrawGizmos() {
        //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        
        //Draw la box de détection des pieds
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetPosition.position, feetSize);
    }
}
