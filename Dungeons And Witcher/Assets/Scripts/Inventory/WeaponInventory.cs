using System;

[Serializable]
public class WeaponInventory
{
    public WeaponData weaponData;
    public bool isExisted;
    public WeaponInventory(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        AddToInventory();
    }
    public void AddToInventory()
    {
        isExisted = true;
    }
    public void RemoveFromInventory()
    {
        isExisted = false;
    }
}
