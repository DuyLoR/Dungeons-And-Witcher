using UnityEngine;

public class E1_MoveState : EnemyMoveState
{
    private Enemy1 enemy;

    public E1_MoveState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyMoveStateData stateData, Enemy1 enemy) : base(enemyBase, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
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
