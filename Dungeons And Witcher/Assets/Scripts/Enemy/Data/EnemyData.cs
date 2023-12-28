using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/ Enemy State/ Base Data")]
public class EnemyData : ScriptableObject
{
    public LayerMask whatIsPlayer;
}
