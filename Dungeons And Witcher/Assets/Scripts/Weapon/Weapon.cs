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

    private int currentWeaponMana;
    private int currentWeaponOrbIndex;
    private int facingDirection;
    private float startTime;
    private float manaRegenTimer = 0;

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
        if (weaponData != null && weaponData.orbDatas.Length >= 0)
        {
            manaRegenTimer += Time.deltaTime;
            if (manaRegenTimer >= 1)
            {
                manaRegenTimer = 0f;

                if (currentWeaponMana < weaponData.maxMana)
                {
                    currentWeaponMana += weaponData.manaRegen;

                    currentWeaponMana = Mathf.Min(currentWeaponMana, weaponData.maxMana);
                }
            }

            float timeRecharge = currentWeaponOrbIndex == weaponData.orbDatas.Length - 1 ? weaponData.rechargeTime : 0;
            if (Time.time >= startTime + weaponData.castDelay + weaponData.orbDatas[currentWeaponOrbIndex].castDelay + timeRecharge && isAttack)
            {
                startTime = Time.time;
                if (currentWeaponMana >= 0 && weaponData.orbDatas[currentWeaponOrbIndex].manaUse <= currentWeaponMana)
                {
                    AddNewOrbInPool(weaponData.orbDatas[currentWeaponOrbIndex]);
                    FireOrb();
                }
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
        currentWeaponOrbIndex = 0;
        currentWeaponMana = weaponData.maxMana;
    }
    public void SetWeaponData(WeaponData newWeaponData)
    {
        if (weaponData == newWeaponData) return;
        weaponData = newWeaponData;
        WeaponInfo.instance.SetWeaponData(weaponData);
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
    private void AddNewOrbInPool(OrbData newOrbData)
    {
        if (!OrbSpawnPool.Instance.CheckIfOrbInPool(newOrbData.orbPrefab))
        {
            OrbSpawnPool.Instance.AddOrbToOrbPrefabs(newOrbData.orbPrefab);
        }
    }
    private void FireOrb()
    {
        currentWeaponOrb = weaponData.orbDatas[currentWeaponOrbIndex].orbPrefab;
        Orb currentOrb = OrbSpawnPool.Instance.GetFromPool(currentWeaponOrb).GetComponent<Orb>();
        ChangeDataCurrentOrb(currentOrb);
        currentWeaponMana -= currentOrb.orbData.manaUse;
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
