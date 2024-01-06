using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : EnemyState
{
    protected EnemyMoveStateData stateData;

    protected Vector3 randomRoaningPos;

    protected float distance;


    public EnemyMoveState(EnemyBase enemyBase, EnemyStateMachine stateMachine, string animName, EnemyMoveStateData stateData) : base(enemyBase, stateMachine, animName)
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

        enemyBase.agent.isStopped = false;
        enemyBase.agent.speed = stateData.movementSpeed;
        randomRoaningPos = GetRoaningPos();
        enemyBase.agent.SetDestination(randomRoaningPos);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        NavMeshPath path = new NavMeshPath();
        if (Vector2.Distance(enemyBase.transform.position, enemyBase.player.position) > enemyBase.enemyBaseData.targetRange)
        {
            enemyBase.agent.CalculatePath(randomRoaningPos, path);
        }
        else
        {
            enemyBase.agent.CalculatePath(enemyBase.player.position, path);
        }
        if (path.corners.Length > 0)
        {
            enemyBase.CheckIfShouldFlip(path.corners[path.corners.Length - 1]);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public Vector3 GetRoaningPos()
    {
        var index = Random.Range(0, enemyBase.room.PositionsAccessibleFromPath.Count - 1);
        return (Vector2)enemyBase.room.PositionsAccessibleFromPath[index];
    }
}
