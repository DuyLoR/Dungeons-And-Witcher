using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveStateData", menuName = "Data/ Enemy State/ Move State")]
public class EnemyMoveStateData : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float attackMovementSpeed = 10f;

    [Header("Time")]
    public float minMoveTime = 5f;
    public float maxMoveTime = 10f;
}
