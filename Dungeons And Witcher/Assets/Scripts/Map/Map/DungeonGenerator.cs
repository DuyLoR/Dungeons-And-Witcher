using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DungeonGenerator : SimpleRandomWalkDungeonGenerator
{

    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    [SerializeField]
    private int corriderSize = 3;

    private DungeonData dungeonData;
    public UnityEvent OnFinishedRoomGeneration;
    private void Awake()
    {
        dungeonData = FindObjectOfType<DungeonData>();
        if (dungeonData == null)
            dungeonData = gameObject.AddComponent<DungeonData>();
    }
    private void Start()
    {
        GenerateDungeon();
    }

    protected override void RunProceduralGeneration()
    {
        dungeonData.Reset();
        CoorridorFirstGeneration();
        OnFinishedRoomGeneration?.Invoke();
    }

    private void CoorridorFirstGeneration()
    {
        tilemapVisualizer.Clear();

        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(potentialRoomPositions);
        CreateRooms(potentialRoomPositions);
        CreateWalls();
    }

    private HashSet<Vector2Int> IncreaseCorridorSize(HashSet<Vector2Int> corridor)
    {
        HashSet<Vector2Int> newCorridor = new HashSet<Vector2Int>();
        foreach (var positon in corridor)
        {
            for (int x = -corriderSize; x < corriderSize; x++)
            {
                for (int y = -corriderSize; y < corriderSize; y++)
                {
                    newCorridor.Add(positon + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    /// <summary>
    /// Return roomPositions with potential room positions <vi-tri-phong>
    /// </summary>
    /// <param name="potentialRoomPositions"></param>
    /// <returns></returns>
    private void CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(potentialRoomPositions.Count).ToList();
        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            dungeonData.Rooms.Add(new Room(roomPosition, roomFloor));
            tilemapVisualizer.PaintFloorTiles(roomFloor);
        }
    }

    private void CreateCorridors(HashSet<Vector2Int> potentialRoomPositions)
    {
        potentialRoomPositions.Add(startPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            HashSet<Vector2Int> corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(potentialRoomPositions.LastOrDefault(), corridorLength);
            dungeonData.Path.UnionWith(corridor);
            potentialRoomPositions.Add(corridor.LastOrDefault());
        }
        dungeonData.Path.UnionWith(IncreaseCorridorSize(dungeonData.Path));
        tilemapVisualizer.PaintPathTiles(dungeonData.Path);
    }
    public void CreateWalls()
    {
        HashSet<Vector2Int> dungeonTiles = new HashSet<Vector2Int>();
        foreach (var room in dungeonData.Rooms)
        {
            dungeonTiles.UnionWith(room.FloorTiles);
        }
        dungeonTiles.UnionWith(dungeonData.Path);
        var wallPositions = FindWallsInDirections(dungeonTiles, Direction2D.cardinal8DirectionsList);
        tilemapVisualizer.PaintWallTiles(wallPositions);
    }
    public HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> Positions, List<Vector2Int> cardinalDirectionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in Positions)
        {
            foreach (var direction in cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (Positions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                    continue;
                }
            }
        }
        return wallPositions;
    }
}
