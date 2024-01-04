using System.Collections.Generic;
using UnityEngine;

// The algorithms will be available for any class that wants to access
/// <summary>
/// This is the procedural of generation algorithms
/// </summary>
public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, Vector2Int roomSize)
    {
        var half = roomSize / 2;
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        for (int i = -half.x; i < half.x; i++)
        {
            for (int j = -half.y; j < half.y; j++)
            {
                var newPosition = startPosition + new Vector2Int(i, j);
                path.Add(newPosition);
            }
        }
        return path;
    }
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

/// <summary>
/// this class will allow us to get a random direction
/// </summary>
public static class Direction2D
{
    /// <summary> 
    ///danh sach cac huong chinh<cardinaldirections>
    /// </summary>
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),   //UP
        new Vector2Int(1, 0),   //RIGHT
        new Vector2Int(0, -1),  //DOWN
        new Vector2Int(-1, 0),  //LEFT
    };

    public static List<Vector2Int> cardinal8DirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),   //UP
        new Vector2Int(1, 1),   //UP-RIGHT
        new Vector2Int(1, 0),   //RIGHT
        new Vector2Int(1, -1),   //RIGHT-DOWN
        new Vector2Int(0, -1),  //DOWN
        new Vector2Int(-1, -1),  //DOWN-LEFT
        new Vector2Int(-1, 0),  //LEFT
        new Vector2Int(-1, 1),  //LEFT-UP
    };

    public static Vector2Int GetRandomCardinal4Direction()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
    public static Vector2Int GetRandomCardinal8Direction()
    {
        return cardinal8DirectionsList[Random.Range(0, cardinal8DirectionsList.Count)];
    }
}
