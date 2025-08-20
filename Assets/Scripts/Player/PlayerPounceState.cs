using UnityEngine;

public class PlayerPounceState : PlayerBaseState
{
    private Vector2 PounceForceVector;
    public override void EnterState(PlayerStateMashine player)
    {
        PounceForceVector = player.direction * player.PounceForce;
    }

    public override void FixedUpdateState(PlayerStateMashine player)
    {
        if (Time.time - player.jumpPressedTime <= player.jumpBuffer)
        {
            player.rigidbody2D.AddForce(PounceForceVector, ForceMode2D.Impulse);
            player.SwitchState(player.idleState);
        }
    }

    public override void UpdateState(PlayerStateMashine player)
    {
        
    }
}
