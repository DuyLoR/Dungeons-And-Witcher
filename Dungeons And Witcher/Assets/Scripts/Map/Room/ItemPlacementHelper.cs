using System.Collections.Generic;
using UnityEngine;

public class ItemPlacementHelper : MonoBehaviour
{
    Dictionary<PlacementType, HashSet<Vector2Int>> tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();
    HashSet<Vector2Int> roomFloorNoCorridor;

    public ItemPlacementHelper(HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridor)
    {
        Graph graph = new Graph(roomFloor);
        this.roomFloorNoCorridor = roomFloorNoCorridor;

        foreach (var position in roomFloorNoCorridor)
        {
            int neighbours8Dir = graph.GetNeighbours8Direction(position).Count;
            PlacementType type = neighbours8Dir < 8 ? PlacementType.nearWall : PlacementType.Openspace;

            if (tileByType.ContainsKey(type) == false)
            {
                tileByType[type] = new HashSet<Vector2Int>();
            }
            if (type == PlacementType.nearWall && graph.GetNeighbours4Direction(position).Count < 4)
            {
                continue;
            }
            tileByType[type].Add(position);
        }
    }

    public enum PlacementType
    {
        Openspace,
        nearWall
    }
}
