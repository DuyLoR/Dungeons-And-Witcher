public class E1_TakendameState : EnemyTakendameState
{
    private Enemy1 enemy;
    public E1_TakendameState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyTakendameStateData stateData, Enemy1 enemy) : base(enemyBase, stateMachine, animName, stateData)
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
        if (isStunTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
