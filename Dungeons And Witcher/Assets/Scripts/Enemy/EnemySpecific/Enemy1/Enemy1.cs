using UnityEngine;

public class Enemy1 : EnemyBase
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }

    [SerializeField]
    private EnemyIdleStateData enemyIdleStateData;

    [SerializeField]
    private EnemyMoveStateData enemyMoveStateData;

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", enemyMoveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", enemyIdleStateData, this);

        stateMachine.Initialize(moveState);
    }
}
