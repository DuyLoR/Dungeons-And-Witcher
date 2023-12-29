using UnityEngine;

public class EnemyState
{
    protected EnemyBase enemyBase;
    protected EnemyStateMachine stateMachine;

    protected float startTime;

    private string animName;
    public EnemyState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName)
    {

        this.animName = animName;
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        DoCheck();
        startTime = Time.time;
        enemyBase.animator.Play(animName);
    }

    public virtual void Exit()
    {
        enemyBase.animator.StopPlayback();
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
