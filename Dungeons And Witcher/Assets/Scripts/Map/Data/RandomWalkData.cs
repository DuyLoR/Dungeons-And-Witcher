using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkParameter_", menuName = "Data/Map/RandomWalkData")]
public class RandomWalkData : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomPosEachIteration = true;

}
