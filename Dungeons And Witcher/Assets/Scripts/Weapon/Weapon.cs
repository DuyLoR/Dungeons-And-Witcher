using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject orb;
    private Orb currentOrb;

    [SerializeField]
    private List<GameObject> orbs;
    public Transform orbSpawnPoint { get; private set; }
    public Vector2 weaponDirection { get; private set; }

    private SpriteRenderer spriteRenderer;

    private int currentOrbIndex = 0;
    private int facingDirection;
    private float startTime;
    private float timeDelay = 0.2f;

    private bool isAttack;

    private void Awake()
    {
        orb = orbs[currentOrbIndex];
        facingDirection = 1;
    }
    private void Start()
    {
        orbSpawnPoint = GameObject.Find("OrbSpawnPoint").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        startTime = Time.time;
    }

    private void Update()
    {
        RotationAngleOfWeapon();
        if (!OrbSpawnPool.Instance.CheckIfOrbInPool(orb))
        {
            OrbSpawnPool.Instance.AddOrbToOrbPrefabs(orb);
        }
        if (isAttack && Time.time >= startTime + timeDelay)
        {
            startTime = Time.time;
            currentOrb = OrbSpawnPool.Instance.GetFromPool(orb).GetComponent<Orb>();
            ChangeDataCurrentOrb(currentOrb);

            currentOrbIndex = (currentOrbIndex + 1) % orbs.Count;
            orb = orbs[currentOrbIndex];
        }
    }

    private void ChangeDataCurrentOrb(Orb currentOrb)
    {
        currentOrb.transform.position = orbSpawnPoint.position;
        currentOrb.transform.up = transform.up;
        currentOrb.SetVelocity(weaponDirection);
    }

    public void SetWeaponMouseTarget(Vector2 position)
    {
        weaponDirection = position - (Vector2)transform.position;
    }

    private void RotationAngleOfWeapon()
    {
        var angle = Mathf.Atan2(weaponDirection.y, weaponDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
    public void CheckIfAttack(bool AttackInput)
    {
        isAttack = AttackInput;
    }
    public void ChangePosition(Vector3 newPosition)
    {
        transform.localPosition = newPosition;
    }
    public void CheckIfShouldFlip(int direction)
    {
        if (facingDirection != direction)
        {
            Flip();
        }
    }
    public void Flip()
    {
        facingDirection *= -1;
        spriteRenderer.flipX = facingDirection == -1 ? true : false;
    }
}
