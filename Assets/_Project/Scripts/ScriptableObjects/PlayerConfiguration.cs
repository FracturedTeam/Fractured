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
    [Tooltip("Time to get to full speed")]
    public float accelTime = 1f;
    [Tooltip("Time to stop")]
    public float decelTime = 1f;
    
    [Header("Slope Settings")] 
    [Tooltip("Max slope angle that the player can walk on")]
    public float maxSlopeAngle = 40;
    [Tooltip("When player walk on a slope, his speed decrease progressively as the slope get steeper // 1 will not change the speed on slope"), Range(0.1f, 1f)]
    public float maxSlopeDecreaseSpeed = 0.5f;
    
    [Header("Fall Settings")] 
    [Tooltip("Max fall speed of the player")]
    public float maxFallSpeed = 40;
    [Tooltip("Acceleration rate of fall speed over time")]
    public float fallSpeedAccel = 35;
    [Tooltip("Time to wait before applying the fall force on the player")]
    public float timeBeforeApplyingFallSpeed = 0.5f;
    
    [Header("Drag Setting")]
    [Tooltip("Player resistance/friction when moving on ground (higher value mean more friction)")]
    public float groundDrag = 4f;
    [Tooltip("Player resistance/friction when falling (higher value mean more friction)")]
    public float airDrag = 0.2f;
}
