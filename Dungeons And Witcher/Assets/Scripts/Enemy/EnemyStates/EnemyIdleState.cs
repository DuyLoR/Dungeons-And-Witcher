using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected EnemyIdleStateData stateData;

    protected float idleTime;

    protected bool isIdleTimeOver;

    public EnemyIdleState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyIdleStateData stateData) : base(enemyBase, stateMachine, animName)
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
        enemyBase.agent.isStopped = true;
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
