using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateMashine player)
    {
        player.direction = Vector2.zero;
    }

    public override void FixedUpdateState(PlayerStateMashine player)
    {
        player.rigidbody2D.linearVelocity = Vector2.zero;
    }

    public override void UpdateState(PlayerStateMashine player)
    {
        player.speed = 0;
    }
}
