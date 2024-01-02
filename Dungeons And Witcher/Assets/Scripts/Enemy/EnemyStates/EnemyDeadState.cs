using UnityEngine;

public class EnemyDeadState : EnemyState
{
    protected EnemyDeadStateData stateData;

    protected bool isDeadtimeOver;
    public EnemyDeadState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyDeadStateData stateData) : base(enemyBase, stateMachine, animName)
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
        isDeadtimeOver = false;
        enemyBase.agent.isStopped = true;
        enemyBase.GetComponent<Collider2D>().enabled = false;
        enemyBase.GetComponent<SpriteRenderer>().sortingLayerName = "Obstacles";
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.deadTime)
        {
            isDeadtimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
