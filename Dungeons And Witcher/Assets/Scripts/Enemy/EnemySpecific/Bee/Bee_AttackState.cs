using UnityEngine.AI;

public class Bee_AttackState : EnemyAttackState
{
    private Bee enemy;
    public Bee_AttackState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyAttackStateData stateData, Bee enemy) : base(enemyBase, stateMachine, animName, stateData)
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
        enemy.agent.speed = stateData.attackSpeed;
        enemy.agent.SetDestination(enemy.player.position);

        NavMeshPath path = new NavMeshPath();
        enemyBase.agent.CalculatePath(enemyBase.player.position, path);
        if (path.corners.Length > 0)
        {
            enemyBase.CheckIfShouldFlip(path.corners[path.corners.Length - 1]);
        }

        if (isAttackTimeOver)
        {
            enemy.SetVelocity(0f);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
