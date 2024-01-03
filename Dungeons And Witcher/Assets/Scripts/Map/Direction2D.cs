using System.Collections.Generic;
using UnityEngine;

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
