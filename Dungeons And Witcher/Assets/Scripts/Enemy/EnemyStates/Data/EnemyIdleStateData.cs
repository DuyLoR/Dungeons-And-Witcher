using UnityEngine;

[CreateAssetMenu(fileName = "NewIdleStateData", menuName = "Data/ Enemy State/ Idle State")]
public class EnemyIdleStateData : ScriptableObject
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;
}
