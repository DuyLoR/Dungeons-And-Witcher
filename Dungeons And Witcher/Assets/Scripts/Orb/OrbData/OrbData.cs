using UnityEngine;

[CreateAssetMenu(fileName = "NewOrdData", menuName = "Data/ Ord Data/ Orb Data")]
public class OrbData : ScriptableObject
{
    public GameObject orbPrefab;
    public string orbName;
    public string orbDescription;
    [Header("Stats")]
    public orbType type;
    public int manaUse = 5;
    public int damage = 5;
    public int orbSpeed = 250;
    public float castDelay = .1f;
    public float timeDestroy = 2f;


    public enum orbType
    {
        projectile,
        multicast,
        modifier
    }
}
