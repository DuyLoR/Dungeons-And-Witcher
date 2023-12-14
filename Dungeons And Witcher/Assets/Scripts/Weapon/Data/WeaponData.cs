using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/ Weapon/ Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    public string WeaponName;
    public string WeaponDescription;

}
