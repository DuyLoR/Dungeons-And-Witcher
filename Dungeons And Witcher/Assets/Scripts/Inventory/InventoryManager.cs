using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    public WeaponData[] startWeapons;
    [SerializeField]
    public OrbData[] startOrbs;
    [SerializeField]
    public WeaponInventorySlot[] weaponInventorySlots;
    [SerializeField]
    public OrbInventorySlot[] orbInventorySlots;

    [SerializeField]
    public GameObject weaponSlotPrefab;
    [SerializeField]
    public GameObject orbSlotPrefab;
    [SerializeField]
    public GameObject weaponItemPrefab;
    [SerializeField]
    public GameObject orbItemPrefab;

    public GameObject mainInventory;
    private Transform weaponInventory;
    private Transform orbInventory;

    private int selectedWeapon = -1;
    private int selectedOrb = -1;

    public bool isChangeInventory = true;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        weaponInventorySlots = new WeaponInventorySlot[3];
        orbInventorySlots = new OrbInventorySlot[15];
        mainInventory = GameObject.Find("MainInventory");
        weaponInventory = GameObject.Find("WeaponInventory").transform;
        orbInventory = GameObject.Find("OrbInventory").transform;

        GenerateWeaponSlot();
        GenerateOrbSlot();
        for (int i = 0; i < startWeapons.Length; i++)
        {
            AddWeapon(startWeapons[i]);
        }
        for (int i = 0; i < startOrbs.Length; i++)
        {
            AddOrb(startOrbs[i]);
        }
        ChangeSelectedWeaponSlot(0);
        mainInventory.SetActive(false);
    }

    private void OnEnable()
    {
        Orb.OnOrbCollected += AddOrbCollected;
    }
    private void OnDisable()
    {
        Orb.OnOrbCollected -= AddOrbCollected;
    }

    public void ChangeSelectedWeaponSlot(int newValue)
    {
        if (selectedWeapon >= 0)
        {
            weaponInventorySlots[selectedWeapon].Deselect();
        }
        weaponInventorySlots[newValue].Select();
        selectedWeapon = newValue;
    }
    private void GenerateOrbSlot()
    {
        for (int i = 0; i < orbInventorySlots.Length; i++)
        {
            orbInventorySlots[i] = Instantiate(orbSlotPrefab, orbInventory.transform).GetComponent<OrbInventorySlot>();
        }
    }

    private void GenerateWeaponSlot()
    {
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            weaponInventorySlots[i] = Instantiate(weaponSlotPrefab, weaponInventory.transform).GetComponent<WeaponInventorySlot>();
        }
    }

    public bool AddWeapon(WeaponData weaponData)
    {
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            WeaponInventorySlot weaponSlot = weaponInventorySlots[i];
            WeaponItem weaponItemInSlot = weaponSlot.GetComponentInChildren<WeaponItem>();
            if (weaponItemInSlot == null)
            {
                SpawnNewWeaponItem(weaponData, weaponSlot);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewWeaponItem(WeaponData weaponData, WeaponInventorySlot weaponSlot)
    {
        GameObject newWeaponItem = Instantiate(weaponItemPrefab, weaponSlot.transform);
        WeaponItem weaponItem = newWeaponItem.GetComponent<WeaponItem>();
        weaponItem.InitializeWeaponItem(weaponData);
    }

    public void RemoveWeapon(WeaponData weaponData)
    {

    }
    public WeaponData GetSeletedWeapon()
    {
        WeaponInventorySlot weaponSlot = weaponInventorySlots[selectedWeapon];
        WeaponItem weaponItemInSlot = weaponSlot.GetComponentInChildren<WeaponItem>();
        if (weaponItemInSlot != null)
        {
            return weaponItemInSlot.weaponData;
        }
        return null;
    }
    public int GetSeletedSlot()
    {
        return selectedWeapon;
    }

    public void UpdateInventory()
    {
        UpdateWeaponInventory();
        UpdateOrbInventory();
    }


    private void UpdateWeaponInventory()
    {
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            WeaponInventorySlot weaponSlot = weaponInventorySlots[i];
            WeaponItem weaponItemInSlot = weaponSlot.GetComponentInChildren<WeaponItem>();
            GameObject currentWeapon = weaponInventory.GetChild(i).gameObject;
            if (weaponItemInSlot != null)
            {
                Debug.Log(i + " " + currentWeapon.GetComponent<WeaponItem>().weaponData);
                weaponItemInSlot.InitalizeWeaponData(currentWeapon.GetComponent<WeaponItem>().weaponData);
            }
            else
            {
                SpawnNewWeaponItem(currentWeapon.GetComponent<WeaponItem>().weaponData, weaponSlot);
            }
        }
    }

    private void UpdateOrbInventory()
    {
        throw new NotImplementedException();
    }
    public void AddOrbCollected(OrbData orbData)
    {
        for (int i = 0; i < orbInventorySlots.Length; i++)
        {
            OrbInventorySlot orbSlot = orbInventorySlots[i];
            OrbItem orbItemInSlot = orbSlot.GetComponentInChildren<OrbItem>();
            if (orbItemInSlot == null)
            {
                SpawnNewOrbItem(orbData, orbSlot);
                return;
            }
        }
    }
    public bool AddOrb(OrbData orbData)
    {
        for (int i = 0; i < orbInventorySlots.Length; i++)
        {
            OrbInventorySlot orbSlot = orbInventorySlots[i];
            OrbItem orbItemInSlot = orbSlot.GetComponentInChildren<OrbItem>();
            if (orbItemInSlot == null)
            {
                SpawnNewOrbItem(orbData, orbSlot);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewOrbItem(OrbData orbData, OrbInventorySlot orbSlot)
    {
        GameObject newOrbItem = Instantiate(orbItemPrefab, orbSlot.transform);
        OrbItem orbItem = newOrbItem.GetComponent<OrbItem>();
        orbItem.InitialiseOrbItem(orbData);
    }

    public void RemoveOrb(OrbData orbData)
    {
    }
    public OrbData GetSelectedOrb()
    {
        OrbInventorySlot orbSlot = orbInventorySlots[selectedOrb];
        return null;
    }
}
