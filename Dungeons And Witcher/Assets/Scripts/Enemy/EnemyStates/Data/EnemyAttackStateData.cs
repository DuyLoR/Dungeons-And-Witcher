using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackStateData", menuName = "Data/ Enemy State/ Attack State")]
public class EnemyAttackStateData : ScriptableObject
{
    [Header("Movement")]
    public float attackSpeed = 400f;

    [Header("Time")]
    public float timeToAttack = 2f;

}
