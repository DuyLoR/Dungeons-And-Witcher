using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkDungeonGenerator : DungeonGenerator
{
    [SerializeField] protected RandomWalkData randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    /// <summary>
    /// Return random walk positions, HashSet is not contain the duplicate position
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkData parameters, Vector2Int position)
    {
        var currentPositions = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.RandomWalk(currentPositions, parameters.walkLength);
            //Add all path to the floorPosition, UnionWith(): add no duplicate
            floorPositions.UnionWith(path);
            //Get new position to loop, it is random
            if (parameters.startRandomPosEachIteration)
            {
                currentPositions = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
