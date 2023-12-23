using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public InventoryData InventoryData;
    [SerializeField]
    public WeaponInventorySlot[] weaponInventorySlots;
    [SerializeField]
    public OrbInventorySlot[] orbInventorySlots;

    [SerializeField]
    public Dictionary<int, WeaponData> weaponData;

    public GameObject mainInventory;
    private Transform weaponInventory;
    private Transform orbInventory;

    private int selectedWeapon = -1;
    private int selectedOrb = -1;

    public bool isChangeInventory = true;
    private void Awake()
    {
        Instance = this;

        mainInventory = GameObject.Find("MainInventory");
        weaponInventory = GameObject.Find("WeaponInventory").transform;
        orbInventory = GameObject.Find("OrbInventory").transform;

        weaponInventorySlots = new WeaponInventorySlot[InventoryData.maxWeaponIS];
        orbInventorySlots = new OrbInventorySlot[InventoryData.maxOrbIS];
    }
    private void Start()
    {
        GenerateWeaponSlot();
        GenerateOrbSlot();
        for (int i = 0; i < InventoryData.startWeaponDatas.Length; i++)
        {
            AddWeapon(InventoryData.startWeaponDatas[i]);
        }
        for (int i = 0; i < InventoryData.startOrbDatas.Length; i++)
        {
            AddOrb(InventoryData.startOrbDatas[i]);
        }
        ChangeSelectedWeaponSlot(0);
        mainInventory.SetActive(false);
    }

    private void OnEnable()
    {
        WeaponInventorySlot.OnSlotChanged += WeaponInventorySlot_OnSlotChanged;
        Orb.OnOrbCollected += AddOrbCollected;
    }

    private void WeaponInventorySlot_OnSlotChanged(GameObject obj, int index)
    {
        weaponInventorySlots[index] = obj.GetComponent<WeaponInventorySlot>();
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
            orbInventorySlots[i] = Instantiate(InventoryData.orbSlotPrefab, orbInventory.transform).GetComponent<OrbInventorySlot>();
        }
    }

    private void GenerateWeaponSlot()
    {
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            weaponInventorySlots[i] = Instantiate(InventoryData.weaponSlotPrefab, weaponInventory.transform).GetComponent<WeaponInventorySlot>();
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
        GameObject newWeaponItem = Instantiate(InventoryData.weaponItemPrefab, weaponSlot.transform);
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
    public int GetSlotIndex(WeaponInventorySlot slot)
    {
        return Array.IndexOf(weaponInventorySlots, slot);
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
        GameObject newOrbItem = Instantiate(InventoryData.orbItemPrefab, orbSlot.transform);
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
