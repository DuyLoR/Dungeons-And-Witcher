using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/ Weapon/ Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    public GameObject weaponPrefab;
    public string WeaponName;
    public string WeaponDescription;

    [Header("Stats")]
    public float castDelay = .1f;
    public float rechargeTime = .2f;
    public int maxMana = 100;
    public int manaCharge = 10;
    public int capacity = 3;
    public OrbData[] orbDatas;

}
