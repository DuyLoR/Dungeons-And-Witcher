using UnityEngine;

public class PlayerDashState : PlayerState
{
    public bool canDash { get; private set; }

    private float lastDashTime;
    private Vector2 dashDirection;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }
    public override void Enter()
    {
        base.Enter();
        canDash = false;
        player.SetVelocity(playerData.dashVelocity, dashDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        PlaceAfterImage();
        if (Time.time >= startTime + playerData.dashTime && !isExitingState)
        {
            isDashDone = true;
            lastDashTime = Time.time;
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
    }

    public void DetermineDashDirection(Vector2 velocity)
    {
        dashDirection = velocity;
    }
    public void DetermineDashDirection()
    {
        dashDirection = new Vector2(player.facingDirection, 0f);
    }
    public bool CheckIfCanDash()
    {
        return canDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }
    public void ResetCanDash()
    {
        canDash = true;
    }
}
