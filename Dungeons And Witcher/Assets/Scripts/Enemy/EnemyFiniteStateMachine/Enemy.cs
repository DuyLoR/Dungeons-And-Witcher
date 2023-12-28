using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public EnemyData enemyData;

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public GameObject aliveGO { get; private set; }

    public int facingDirection { get; private set; }

    private Vector2 velocityWorkspace;

    public virtual void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        animator = aliveGO.GetComponent<Animator>();

        stateMachine = new EnemyStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(velocity * facingDirection, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0.0f, 180f, 0.0f);
    }
}
