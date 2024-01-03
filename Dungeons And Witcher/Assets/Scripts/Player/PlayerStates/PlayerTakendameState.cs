using UnityEngine;

public class PlayerTakendameState : PlayerState
{
    public PlayerTakendameState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocity(playerData.movementVelocity * input);
        player.CheckIfShouldFlip(mousePos);
        if (Time.time >= startTime + player.playerData.takendameDelay)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
