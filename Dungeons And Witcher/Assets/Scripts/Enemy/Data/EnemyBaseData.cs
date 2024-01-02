using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/ Enemy State/ Base Data")]
public class EnemyBaseData : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject popupPrefabs;

    public float attackRange = 4f;
    public float targetRange = 7f;
    [Header("Stats")]
    public int maxHeal = 100;
    public int damage = 10;
    public float attackDelay = .3f;
}
