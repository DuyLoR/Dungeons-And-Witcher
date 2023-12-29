using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/ Enemy State/ Base Data")]
public class EnemyBaseData : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public LayerMask whatIsWall;
}
