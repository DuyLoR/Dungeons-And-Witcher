using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/ Enemy State/ Base Data")]
public class EnemyBaseData : ScriptableObject
{
    public float attackRange = 4f;
    public float targetRange = 7f;
    [Header("Stats")]
    public int maxHeal = 100;
    public int damege = 10;
    public float attackDelay = .3f;
}
