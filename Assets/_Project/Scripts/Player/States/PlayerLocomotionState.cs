using UnityEngine;

public class PlayerLocomotionState : PlayerBaseState
{
    public PlayerLocomotionState(PlayerController player) : base(player) {
    }

    public override void OnEnter() {
        player.movement.SetSpeed(PlayerSpeedEnum.Normal);
    }

    public override void OnUpdate() {
        player.movement.HandleUpdate();
    }

    public override void OnFixedUpdate() {
        player.movement.HandleMovement();
    }

    public override void OnExit() {
        
    }
}
