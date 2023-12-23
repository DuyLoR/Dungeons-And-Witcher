using UnityEngine;

[CreateAssetMenu(fileName = "BaseInventory", menuName = "Data/ Inventory/ Base Inventory")]
public class InventoryData : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject weaponSlotPrefab;
    public GameObject weaponItemPrefab;
    public GameObject orbSlotPrefab;
    public GameObject orbItemPrefab;

    [Header("Spawn")]
    public WeaponData[] startWeaponDatas;
    public OrbData[] startOrbDatas;

    [Header("Weapon")]
    public int maxWeaponIS;

    [Header("Orb")]
    public int maxOrbIS;
}
