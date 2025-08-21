using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateMashine player)
    {
        PlayerPrefs.isHidden = true;
    }

    public override void FixedUpdateState(PlayerStateMashine player)
    {
        player.rigidbody2D.linearVelocity = player.direction.normalized * player.speed * 100f * Time.fixedDeltaTime;
    }

    public override void UpdateState(PlayerStateMashine player)
    {
        player.speed = player.WalkSpeed;
        player.direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
