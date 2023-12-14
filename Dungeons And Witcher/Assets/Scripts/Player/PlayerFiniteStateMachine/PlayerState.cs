using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;
    protected Vector2 input;
    protected Vector2 mousePos;

    protected bool dashInput;
    protected bool isDashDone;
    protected bool isExitingState;

    private string animName;


    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animName = animName;
    }
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        isExitingState = false;
        DoChecks();
        if (animName != null)
        {
            player.animator.Play(animName);
        }
        isDashDone = false;
        player.dashState.ResetCanDash();
    }
    public virtual void Exit()
    {
        if (animName != null)
        {
            player.animator.StopPlayback();
        }
        isExitingState = true;
    }
    public virtual void LogicUpdate()
    {
        input = player.inputHandle.rawMovementInput;
        mousePos = player.inputHandle.mousePos;
        dashInput = player.inputHandle.dashInput;

        if (dashInput && player.dashState.CheckIfCanDash())
        {
            if (input != Vector2.zero)
            {
                player.dashState.DetermineDashDirection(input);
            }
            else
            {
                player.dashState.DetermineDashDirection();
            }
            player.stateMachine.ChangeState(player.dashState);
        }
        if (isDashDone)
        {
            if (input != Vector2.zero)
            {
                player.stateMachine.ChangeState(player.moveState);
            }
            else
            {
                player.stateMachine.ChangeState(player.idleState);
            }
        }
    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}
