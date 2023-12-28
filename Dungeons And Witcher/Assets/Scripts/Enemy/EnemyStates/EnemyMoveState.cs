public class EnemyMoveState : EnemyState
{
    protected EnemyMoveStateData stateData;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animName, EnemyMoveStateData stateData) : base(enemy, stateMachine, animName)
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
        enemy.SetVelocity(stateData.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
