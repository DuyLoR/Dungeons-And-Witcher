using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject popupPrefabs;

    [Header("Move State")]
    public float movementVelocity = 4f;

    [Header("Dash State")]
    public float dashCooldown = 1f;
    public float dashVelocity = 20f;
    public float dashTime = 0.4f;


    [Header("Collect")]
    public float collectRadius = 1f;
    public LayerMask canCollectLayer;

    [Header("Stats")]
    public int maxHp = 100;
}
