using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<WeaponInventory> weapons;
    public List<OrbInventory> orbs;

    public Dictionary<WeaponData, WeaponInventory> weaponDictionary;
    public Dictionary<OrbData, OrbInventory> orbDictionary;

    private void Awake()
    {
        weapons = new List<WeaponInventory>();
        orbs = new List<OrbInventory>();
        weaponDictionary = new Dictionary<WeaponData, WeaponInventory>();
        orbDictionary = new Dictionary<OrbData, OrbInventory>();
    }
    private void OnEnable()
    {
        OrbItem.OnOrbCollected += AddOrb;
        WeaponItem.OnWeaponCollected += AddWeapon;
    }
    private void OnDisable()
    {
        OrbItem.OnOrbCollected -= AddOrb;
        WeaponItem.OnWeaponCollected -= AddWeapon;
    }
    public void AddWeapon(WeaponData weaponData)
    {
        WeaponInventory newWeapon = new WeaponInventory(weaponData);
        weapons.Add(newWeapon);
        weaponDictionary.Add(weaponData, newWeapon);
    }
    public void RemoveWeapon(WeaponData weaponData)
    {
        if (weaponDictionary.TryGetValue(weaponData, out WeaponInventory weapon))
        {
            weapon.RemoveFromInventory();
            weaponDictionary.Remove(weaponData);
        }
    }
    public void AddOrb(OrbData orbData)
    {
        OrbInventory newOrb = new OrbInventory(orbData);
        orbs.Add(newOrb);
        orbDictionary.Add(orbData, newOrb);
    }
    public void RemoveOrb(OrbData orbData)
    {
        if (orbDictionary.TryGetValue(orbData, out OrbInventory orb))
        {
            orb.RemoveFromInventory();
            orbDictionary.Remove(orbData);
        }
    }
}
