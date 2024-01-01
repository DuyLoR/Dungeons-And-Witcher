public class EnemyDeadState : EnemyState
{
    protected EnemyDeadStateData stateData;
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
        enemyBase.gameObject.SetActive(false);
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
