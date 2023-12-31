using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/ Enemy State/ Base Data")]
public class EnemyBaseData : ScriptableObject
{
    public float attackRange = 2f;
    public float targetRange = 5f;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsWall;
}
