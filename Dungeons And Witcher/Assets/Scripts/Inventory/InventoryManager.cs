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
        WeaponItem.OnWeaponCollected += AddWeaponCollected;
        OrbItem.OnOrbCollected += AddOrbCollected;
    }

    private void OnDisable()
    {
        WeaponItem.OnWeaponCollected -= AddWeaponCollected;
        OrbItem.OnOrbCollected -= AddOrbCollected;
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
    private void AddWeaponCollected(WeaponData weaponData)
    {
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            WeaponInventorySlot weaponSlot = weaponInventorySlots[i];
            WeaponItem weaponItemInSlot = weaponSlot.GetComponentInChildren<WeaponItem>();
            if (weaponItemInSlot == null)
            {
                SpawnNewWeaponItem(weaponData, weaponSlot);
                return;
            }
        }
        SpawnNewWeaponItem(weaponData, weaponInventorySlots[selectedWeapon]);
    }

    private void SpawnNewWeaponItem(WeaponData weaponData, WeaponInventorySlot weaponSlot)
    {
        if (weaponSlot.GetComponent<WeaponItem>() != null) weaponSlot.RemoveItemFromInventory();
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
        Debug.Log(Array.IndexOf(weaponInventorySlots, slot));
        return Array.IndexOf(weaponInventorySlots, slot);
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
        SpawnNewOrbItem(orbData, orbInventorySlots[0]);
    }

    private void SpawnNewOrbItem(OrbData orbData, OrbInventorySlot orbSlot)
    {
        if (orbSlot.GetComponent<OrbItem>() != null) orbSlot.RemoveFromInventory();
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
