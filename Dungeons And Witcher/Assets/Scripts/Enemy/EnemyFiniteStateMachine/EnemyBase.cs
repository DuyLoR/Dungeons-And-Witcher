using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public EnemyBaseData enemyBaseData;
    public Room room { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public NavMeshAgent agent { get; private set; }
    public Transform player { get; private set; }

    public int facingDirection { get; private set; }

    private Vector2 velocityWorkspace;
    private float timer;
    private bool isDamageToPlayer;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        stateMachine = new EnemyStateMachine();
        agent.updateUpAxis = false;
        agent.updateRotation = false;

        isDamageToPlayer = false;
        facingDirection = 1;
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
    public void CheckIfShouldFlip(Vector2 distance)
    {
        Vector2 direction = distance - (Vector2)transform.position;
        int xInput = (int)(direction * Vector2.right).normalized.x;
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && !isDamageToPlayer)
        {
            if (collision.CompareTag("Player"))
            {
                isDamageToPlayer = true;
                timer = Time.time;
                damageable.Damage(enemyBaseData.damage);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (Time.time >= timer + Player.Instance.playerData.takendameDelay)
        {
            isDamageToPlayer = false;
        }
        if (damageable != null && !isDamageToPlayer)
        {
            if (collision.CompareTag("Player"))
            {
                isDamageToPlayer = true;
                damageable.Damage(enemyBaseData.damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (collision.CompareTag("Player"))
            {
                isDamageToPlayer = false;
            }
        }
    }

    public void DestroyGameObject()
    {
        //TODO: Spawn item after destroy
        room.EnemiesInTheRoom.Remove(gameObject);
        Destroy(gameObject);
    }
    public void SetRoom(Room room)
    {
        this.room = room;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyBaseData.targetRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyBaseData.attackRange);
        Gizmos.color = Color.cyan;
    }
}
