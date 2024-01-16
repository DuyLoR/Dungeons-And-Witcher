using UnityEngine;

[CreateAssetMenu(fileName = "NewRelic", menuName = "Relic/Relic Data")]
public class RelicData : ScriptableObject
{
    public Buff buffType = Buff.heal;
    public string Name;
    public int value;

    public enum Buff
    {
        heal,
        movementSpeed,
    }
}
