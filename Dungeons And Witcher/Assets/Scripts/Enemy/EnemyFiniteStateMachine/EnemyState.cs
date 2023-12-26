using UnityEngine;

public class EnemyState
{
    protected Entity entity;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    protected float startTime;

    private string animName;
    public EnemyState(Entity entity, EnemyStateMachine stateMachine, EnemyData enemyData, string animName)
    {
        this.animName = animName;
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        DoCheck();
        startTime = Time.time;
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck()
    {

    }
}
