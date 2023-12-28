using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    protected float startTime;

    private string animName;
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animName)
    {

        this.animName = animName;
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        DoCheck();
        startTime = Time.time;
        enemy.animator.Play(animName);
    }

    public virtual void Exit()
    {
        enemy.animator.StopPlayback();
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
