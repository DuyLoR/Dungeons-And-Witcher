public class E1_IdleState : EnemyIdleState
{
    private Enemy1 enemy1;
    public E1_IdleState(Enemy enemy, EnemyStateMachine stateMachine, string animName, EnemyIdleStateData stateData, Enemy1 enemy1) : base(enemy, stateMachine, animName, stateData)
    {
        this.enemy1 = enemy1;
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

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy1.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
