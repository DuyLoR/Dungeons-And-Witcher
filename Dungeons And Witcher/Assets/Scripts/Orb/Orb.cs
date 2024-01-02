using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb { get; private set; }

    [SerializeField]
    public OrbData orbData;

    private float orbActiveTimer = 0;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        orbActiveTimer += Time.deltaTime;
        if (orbActiveTimer >= orbData.timeActive)
        {
            orbActiveTimer = 0;
            OrbSpawnPool.Instance.AddToPool(gameObject);
        }
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = (velocity).normalized * orbData.orbSpeed;
    }
    public void SetOrbData(OrbData orbData)
    {
        this.orbData = orbData;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(orbData.damage);
        }
    }
}
