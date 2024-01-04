using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameter_", menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10;
    [Header("X")]
    public int minRoomSizeX = 10;
    public int maxRoomSizeX = 10;
    [Header("Y")]
    public int minRoomSizeY = 10;
    public int maxRoomSizeY = 10;
    public bool startRandomlyEachIteration = true;

}
