using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData { get; private set; }
    [SerializeField]
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

    private void Awake()
    {
        facingDirection = 1;
        startTime = Time.time;
    }

    private void Update()
    {
        if (weaponData != InventoryManager.Instance.GetSeletedWeapon())
        {
            SetWeaponData(InventoryManager.Instance.GetSeletedWeapon());
        }
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

    public void InitializeWeapon(WeaponData data)
    {
        weaponData = data;
        currentWeapon = Instantiate(data.weaponPrefab, transform);
        SetCurrentWeaponData();
    }

    #region Set Funciton
    public void SetWeaponMouseTarget(Vector2 position)
    {
        weaponDirection = position - (Vector2)transform.position;
    }

    public void SetCurrentWeaponData()
    {
        currentWeapon.GetComponent<WeaponItem>().InitalizeWeaponData(weaponData);
        spriteRenderer = currentWeapon.GetComponentInChildren<SpriteRenderer>();
        orbSpawnPoint = currentWeapon.transform.Find("OrbSpawnPoint").transform;
    }
    public void SetWeaponData(WeaponData newWeaponData)
    {
        if (weaponData == newWeaponData) return;
        weaponData = newWeaponData;
        if (weaponData == null)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            return;
        }
        else
        {
            Destroy(currentWeapon);
            InitializeWeapon(weaponData);
        }
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
        spriteRenderer.flipX = facingDirection == -1 ? true : false;
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
        if (weaponData.orbDatas.Length == 0) return;
        currentWeaponOrb = weaponData.orbDatas[0].orbPrefab;
        var orb = OrbSpawnPool.Instance.GetFromPool(currentWeaponOrb)?.AddComponent<Orb>();
        ChangeDataCurrentOrb(orb);
        currentWeaponOrbIndex = (currentWeaponOrbIndex + 1) % weaponData.orbDatas.Length;
        currentWeaponOrb = weaponData.orbDatas[currentWeaponOrbIndex].orbPrefab;
    }
    public void RotationAngleOfWeapon()
    {
        var angle = Mathf.Atan2(weaponDirection.y, weaponDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    /// <summary>
    /// Fix position weapon when player animation run
    /// </summary>
    /// <param name="newPosition"></param>
    public void ChangePosition(Vector3 newPosition)
    {
        transform.localPosition = newPosition;
    }
    #endregion
}
