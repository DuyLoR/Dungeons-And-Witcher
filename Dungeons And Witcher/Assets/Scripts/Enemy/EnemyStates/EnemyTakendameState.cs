using UnityEngine;

public class EnemyTakendameState : EnemyState
{
    protected EnemyTakendameStateData stateData;

    protected bool isStunTimeOver;
    public EnemyTakendameState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyTakendameStateData stateData) : base(enemyBase, stateMachine, animName)
    {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
