using System;
using UnityEngine;

public class WeaponItem : MonoBehaviour, ICollectible
{
    public static event Action<WeaponData> OnWeaponCollected;
    public WeaponData weaponData;
    public void Collect()
    {
        Destroy(gameObject);
        OnWeaponCollected?.Invoke(weaponData);
    }
}
