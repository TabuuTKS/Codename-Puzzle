using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerStateMashine player)
    {
    }

    public override void FixedUpdateState(PlayerStateMashine player)
    {
        player.rigidbody2D.linearVelocity = player.direction.normalized * player.speed * 100f * Time.fixedDeltaTime;
    }

    public override void UpdateState(PlayerStateMashine player)
    {
        player.speed = player.MoveSpeed;
        player.direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
