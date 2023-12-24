using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb { get; private set; }

    [SerializeField]
    public OrbData orbData;
    private float maxDistance = 10f;

    private Vector2 startingPos;
    private bool isActive;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            startingPos = transform.position;
            isActive = false;
        }
        if (Vector2.Distance(startingPos, rb.position) >= maxDistance)
        {
            OrbSpawnPool.Instance.AddToPool(gameObject);
        }
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = (velocity).normalized * 30f;
    }
}
