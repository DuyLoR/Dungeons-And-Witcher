using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    /// <summary>
    /// for each loop in walkLength, add position in the variable: path
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="walkLength"></param>
    /// <returns>HashSet.Vector2Int: path</returns>
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinal8Direction();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
    /// <summary>
    /// Create corridors, the last on our path to get the next start position --> no need HashSet
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="corridorLength"></param>
    /// <returns></returns>
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinal4Direction();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}
