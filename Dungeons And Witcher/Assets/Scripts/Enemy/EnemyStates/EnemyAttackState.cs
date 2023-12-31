using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected EnemyAttackStateData stateData;
    protected bool isAttackTimeOver;

    public EnemyAttackState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyAttackStateData stateData) : base(enemyBase, stateMachine, animName)
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
        isAttackTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.timeToAttack)
        {
            isAttackTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
