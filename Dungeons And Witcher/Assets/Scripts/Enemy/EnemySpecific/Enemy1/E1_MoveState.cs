public class E1_MoveState : EnemyMoveState
{
    private Enemy1 enemy1;

    public E1_MoveState(Enemy enemy, EnemyStateMachine stateMachine, string animName, EnemyMoveStateData stateData, Enemy1 enemy1) : base(enemy, stateMachine, animName, stateData)
    {
        this.enemy1 = enemy1;
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

        //Add if
        enemy1.idleState.SetFlipAfterIdle(true);
        stateMachine.ChangeState(enemy1.idleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
