using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateMashine player)
    {

    }

    public override void UpdateState(PlayerStateMashine player)
    {
        player.speed = player.RunSpeed;
        player.direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GameUI.instance.StaminaBar();
    }
}
