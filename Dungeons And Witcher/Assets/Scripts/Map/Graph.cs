using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public List<Vector2Int> graph;
    public Graph(IEnumerable<Vector2Int> vetices)
    {
        graph = new List<Vector2Int>(vetices);
    }
    public List<Vector2Int> GetNeighbours4Direction(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, Direction2D.cardinal4DirectionsList);
    }
    public List<Vector2Int> GetNeighbours8Direction(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, Direction2D.cardinal8DirectionsList);
    }
    private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighboursOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        foreach (var neighbourDirection in neighboursOffsetList)
        {
            Vector2Int potentialNeighbor = startPosition + neighbourDirection;
            if (graph.Contains(potentialNeighbor))
            {
                neighbours.Add(potentialNeighbor);
            }
        }
        return neighbours;
    }
}