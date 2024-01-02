using UnityEngine;

public class E1_DeadState : EnemyDeadState
{
    private Enemy1 enemy;
    public E1_DeadState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyDeadStateData stateData, Enemy1 enemy) : base(enemyBase, stateMachine, animName, stateData)
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
        enemy.transform.position.Set(enemy.transform.position.x, enemy.transform.position.y - Time.deltaTime, enemy.transform.position.z);
        if (isDeadtimeOver)
        {
            enemy.DestroyGameObject();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
