using System;
using UnityEngine;

public class Weapon : MonoBehaviour, ICollectible
{
    public static event Action<WeaponData> OnWeaponCollected;
    public WeaponData weaponData { get; private set; }
    [SerializeField]
    public OrbData[] weaponOrbDatas;
    public GameObject currentWeapon { get; private set; }
    public GameObject currentWeaponOrb { get; private set; }
    public Vector2 weaponDirection { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Transform orbSpawnPoint;

    private int currentWeaponOrbIndex = 0;
    private int facingDirection;
    private float startTime;
    private float timeDelay = 0.2f;

    private bool isAttack;
    public bool isSetStartWeapon = false;

    private void Awake()
    {
        facingDirection = 1;
        startTime = Time.time;
    }

    private void Update()
    {
        RotationAngleOfWeapon();
        if (isAttack && Time.time >= startTime + timeDelay)
        {
            startTime = Time.time;
            if (weaponData != null)
            {
                AddNewOrbInPool(weaponData.orbDatas);
                FireOrb();
            }
        }
    }

    #region Set Funciton
    public void SetWeaponMouseTarget(Vector2 position)
    {
        weaponDirection = position - (Vector2)transform.position;
    }

    public void SetWeaponData(WeaponData newWeaponData)
    {
        if (weaponData == newWeaponData) return;
        if (!isSetStartWeapon)
        {
            Instantiate(newWeaponData.weaponPrefab, transform);
            isSetStartWeapon = true;
        }
        else
        {
            if (newWeaponData == null)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                return;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(newWeaponData.weaponPrefab, transform);

        weaponData = newWeaponData;
        currentWeapon = transform.GetChild(0).gameObject;
        currentWeapon.SetActive(weaponData != null);
        currentWeapon.GetComponent<WeaponItem>().InitalizeWeaponData(weaponData);
        spriteRenderer = currentWeapon?.GetComponent<SpriteRenderer>();
        orbSpawnPoint = currentWeapon?.transform.Find("OrbSpawnPoint").transform;
    }
    #endregion

    #region Check Function
    public void CheckIfAttack(bool AttackInput)
    {
        isAttack = AttackInput;
    }
    public void CheckIfShouldFlip(int direction)
    {
        if (facingDirection != direction)
        {
            Flip();
        }
    }
    #endregion

    #region Orther Function
    private void ChangeDataCurrentOrb(Orb weaponOrb)
    {
        weaponOrb.transform.position = orbSpawnPoint.position;
        weaponOrb.transform.up = transform.up;
        weaponOrb.GetComponent<Orb>().SetVelocity(weaponDirection);
    }
    public void Flip()
    {
        facingDirection *= -1;
        spriteRenderer = weaponData.weaponPrefab.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.flipX = facingDirection == 1 ? true : false;
    }
    private void AddNewOrbInPool(OrbData[] newOrbDatas)
    {
        if (newOrbDatas.Length == 0) { return; }
        for (int i = 0; i < newOrbDatas.Length; i++)
        {
            if (!OrbSpawnPool.Instance.CheckIfOrbInPool(newOrbDatas[i].orbPrefab))
            {
                OrbSpawnPool.Instance.AddOrbToOrbPrefabs(newOrbDatas[i].orbPrefab);
            }
        }
    }
    private void FireOrb()
    {
        var orb = OrbSpawnPool.Instance.GetFromPool(currentWeaponOrb).GetComponent<Orb>();
        if (orb == null) return;
        ChangeDataCurrentOrb(orb);
        currentWeaponOrbIndex = (currentWeaponOrbIndex + 1) % weaponData.orbDatas.Length;
        currentWeaponOrb = weaponData.orbDatas[currentWeaponOrbIndex].orbPrefab;
    }
    private void RotationAngleOfWeapon()
    {
        var angle = Mathf.Atan2(weaponDirection.y, weaponDirection.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
        if (currentWeapon != null) currentWeapon.transform.localRotation = transform.rotation;
    }

    /// <summary>
    /// Fix position weapon when player animation run
    /// </summary>
    /// <param name="newPosition"></param>
    public void ChangePosition(Vector3 newPosition)
    {
        transform.localPosition = newPosition;
    }
    public void Collect()
    {
        Destroy(gameObject);
        OnWeaponCollected?.Invoke(weaponData);
    }
    #endregion
}
