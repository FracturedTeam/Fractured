using System;
using _Project.Scripts.Inputs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    private InputsBrain inputsBrain;
    Rigidbody rb;

    [Header("Move Settings")]
    public float moveSpeed = 8f;
    public float moveMult = .1f;
    public float airMoveMult = .2f;
    public float accel = 1f;
    public float decel = 1f;
    public float rotationSpeed = 1f;

    [Header("Ground Settings")] 
    public float maxFallSpeed = 40;
    public float fallSpeedAccel = 35;
    public float maxSlopeAngle = 40;
    public LayerMask groundLayer;
    public Transform feetPosition;
    public Vector3 feetSize;
    
    [Header("Drag Setting")]
    public float groundDrag = 4f;
    public float airDrag = 0.2f;
    
    [Header("Camera Settings")]
    public Camera cam;

    private float currentFallSpeed;
    private float currentAcceleration;
    
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
        
        currentAcceleration = moveDir.magnitude > 0.01f ? accel : decel;

        if (IsGrounded())
            currentFallSpeed = 0;
        else 
            currentFallSpeed = Mathf.SmoothStep(currentFallSpeed, maxFallSpeed, fallSpeedAccel * Time.deltaTime);
        
        rb.useGravity = !IsOnSlope();
    }

    private void UpdateDrag() =>
        rb.linearDamping = IsGrounded() ? groundDrag : airDrag;

    #region ApplyMovementForces

    public void HandleMovement() {
        if (!IsGrounded())
            rb.AddForce(Vector3.down * currentFallSpeed, ForceMode.Acceleration);
        
        PlayerMove();
    }
    
    private void PlayerMove() {
        if (!IsGrounded())
            rb.AddForce(moveDir.normalized * (moveSpeed * moveMult * currentAcceleration * airMoveMult), ForceMode.Acceleration);
        else if (!IsOnSlope())
            rb.AddForce(moveDir.normalized * (moveSpeed * moveMult * currentAcceleration), ForceMode.Acceleration);
        else if(IsGrounded() && IsOnSlope())
            rb.AddForce(slopeNormal.normalized * (moveSpeed * moveMult * currentAcceleration), ForceMode.Acceleration);
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
        
        return Physics.CheckBox(feetPosition.position, feetSize, Quaternion.identity, groundLayer) && angle <= maxSlopeAngle;
    }

    private bool IsOnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Mathf.Infinity, groundLayer)) {
            if (slopeHit.normal != Vector3.up) {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle <= maxSlopeAngle && angle != 0;
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
