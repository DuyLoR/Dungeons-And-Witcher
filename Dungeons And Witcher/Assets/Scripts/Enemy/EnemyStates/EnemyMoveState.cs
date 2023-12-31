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
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        int vertexIndex = Random.Range(0, triangulation.vertices.Length);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return enemyBase.transform.position;
        }
    }
}
