using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "ScriptableObjects/PlayerConfiguration", order = 1)]
public class PlayerConfiguration : ScriptableObject
{
    [Header("Movement Settings")]
    [Tooltip("Base movement speed of the player")]
    public float normalMoveSpeed = 8f;
    [Tooltip("Base movement multiplier")]
    public float moveMult = 4f;
    [Tooltip("Air movement multiplier when the player is not on ground (this value is multiplied to the moveSpeed and the moveMult)")]
    public float airMoveMult = .2f;
    [Tooltip("Acceleration Rate")]
    public float accel = 1f;
    [Tooltip("Deceleration Rate")]
    public float decel = 1f;
    
    [Header("Slope Settings")] 
    [Tooltip("Max slope angle that the player can walk on")]
    public float maxSlopeAngle = 40;
    
    [Header("Fall Settings")] 
    [Tooltip("Max fall speed of the player")]
    public float maxFallSpeed = 40;
    [Tooltip("Acceleration rate of fall speed over time")]
    public float fallSpeedAccel = 35;
    
    [Header("Drag Setting")]
    [Tooltip("Player resistance/friction when moving on ground (higher value mean more friction)")]
    public float groundDrag = 4f;
    [Tooltip("Player resistance/friction when falling (higher value mean more friction)")]
    public float airDrag = 0.2f;
}
