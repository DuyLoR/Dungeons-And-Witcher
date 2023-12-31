using UnityEngine;

public class Enemy1 : EnemyBase
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_AttackState attackState { get; private set; }

    [SerializeField]
    private EnemyIdleStateData enemyIdleStateData;

    [SerializeField]
    private EnemyMoveStateData enemyMoveStateData;
    [SerializeField]
    private EnemyAttackStateData enemyAttackStateData;

    public override void Start()
    {
        base.Start();

        idleState = new E1_IdleState(this, stateMachine, "idle", enemyIdleStateData, this);
        moveState = new E1_MoveState(this, stateMachine, "move", enemyMoveStateData, this);
        attackState = new E1_AttackState(this, stateMachine, "attack", enemyAttackStateData, this);

        stateMachine.Initialize(moveState);
    }
}
