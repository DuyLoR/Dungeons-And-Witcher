using UnityEngine;

public class Bee_MoveState : EnemyMoveState
{
    private Bee enemy;
    public Bee_MoveState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyMoveStateData stateData, Bee enemy) : base(enemyBase, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
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
        if (Vector2.Distance(enemy.transform.position, enemyBase.player.position) <= enemy.enemyBaseData.targetRange)
        {
            enemy.agent.SetDestination(enemyBase.player.position);
            if (Vector2.Distance(enemy.transform.position, enemyBase.player.position) <= enemy.enemyBaseData.attackRange)
            {
                stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            enemy.agent.SetDestination(randomRoaningPos);
            if (Vector2.Distance(enemy.transform.position, randomRoaningPos) < 0.1f)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
