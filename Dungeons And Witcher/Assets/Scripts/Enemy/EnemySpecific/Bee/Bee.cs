using TMPro;
using UnityEngine;

public class Bee : EnemyBase, IDamageable
{
    public Bee_IdleState idleState { get; private set; }
    public Bee_MoveState moveState { get; private set; }
    public Bee_AttackState attackState { get; private set; }
    public Bee_TakendameState takendameState { get; private set; }
    public Bee_DeadState deadState { get; private set; }

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
        idleState = new Bee_IdleState(this, stateMachine, "idle", enemyIdleStateData, this);
        moveState = new Bee_MoveState(this, stateMachine, "move", enemyMoveStateData, this);
        attackState = new Bee_AttackState(this, stateMachine, "attack", enemyAttackStateData, this);
        takendameState = new Bee_TakendameState(this, stateMachine, "takendame", takendameStateData, this);
        deadState = new Bee_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(idleState);
        currentHeal = enemyBaseData.maxHeal;
    }

    public void Damage(int amount)
    {
        currentHeal -= amount;
        stateMachine.ChangeState(takendameState);
        GameObject popUp = Instantiate(enemyBaseData.popupPrefabs, transform.position, Quaternion.identity, transform);
        TextMeshPro txt = popUp.GetComponent<TextMeshPro>();
        txt.text = amount.ToString();

        if (currentHeal <= 0)
        {
            stateMachine.ChangeState(deadState);
        }
    }
}
