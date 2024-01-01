using UnityEngine;

public class Enemy1 : EnemyBase, IDamageable
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_AttackState attackState { get; private set; }
    public E1_TakendameState takendameState { get; private set; }
    public E1_DeadState deadState { get; private set; }

    [SerializeField]
    private EnemyIdleStateData enemyIdleStateData;
    [SerializeField]
    private EnemyMoveStateData enemyMoveStateData;
    [SerializeField]
    private EnemyAttackStateData enemyAttackStateData;
    [SerializeField]
    private EnemyTakendameStateData takendameStateData;
    [SerializeField]
    private EnemyDeadStateData deadStateData;

    private int currentHeal;

    public override void Start()
    {
        base.Start();
        idleState = new E1_IdleState(this, stateMachine, "idle", enemyIdleStateData, this);
        moveState = new E1_MoveState(this, stateMachine, "move", enemyMoveStateData, this);
        attackState = new E1_AttackState(this, stateMachine, "attack", enemyAttackStateData, this);
        takendameState = new E1_TakendameState(this, stateMachine, "takendame", takendameStateData, this);
        deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);


        stateMachine.Initialize(moveState);
        currentHeal = enemyBaseData.maxHeal;
    }
    public void Damege(int amount)
    {
        currentHeal -= amount;
        stateMachine.ChangeState(takendameState);
        if (currentHeal <= 0)
        {
            stateMachine.ChangeState(deadState);
        }
    }
}
