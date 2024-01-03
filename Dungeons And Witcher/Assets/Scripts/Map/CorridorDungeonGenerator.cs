using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorDungeonGenerator : RandomWalkDungeonGenerator
{

    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;           //decrease the room count

    [SerializeField]
    private int corriderSize = 3;
    protected override void RunProceduralGeneration()
    {
        CoorridorGeneration();
    }

    private void CoorridorGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        //because we had roomPercent so we need create room in the deadend direction.
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSize(corridors[i]);
            floorPositions.UnionWith(corridors[i]);

        }

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private List<Vector2Int> IncreaseCorridorSize(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            //handle corner
            for (int x = -corriderSize; x < corriderSize; x++)
            {
                for (int y = -corriderSize; y < corriderSize; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }
    /// <summary>
    /// this funciton help the cooridor no emd
    /// </summary>
    /// <param name="deadEnds"></param>
    /// <param name="roomPositions"></param>
    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var position in deadEnds)
        {
            if (roomPositions.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomPositions.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }
            if (neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    /// <summary>
    /// Return roomPositions with potential room positions <vi-tri-phong>
    /// </summary>
    /// <param name="potentialRoomPositions"></param>
    /// <returns></returns>
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        //orderby nếu không khai báo thì nó sẽ là ngẫu nhiên
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }
}
