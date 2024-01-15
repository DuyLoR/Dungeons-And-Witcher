using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyPrefab;


    [Space(10)]
    [SerializeField]
    private int minRoomEnemyCount = 5;
    [SerializeField]
    private int maxRoomEnemyCount = 10;
    [SerializeField]
    private Transform parent;

    DungeonData dungeonData;

    [SerializeField]
    private bool showGizmo = false;

    private GameObject player;

    private void Awake()
    {
        dungeonData = FindObjectOfType<DungeonData>();
        player = GameObject.FindWithTag("Player");
    }

    public void PlaceAgents()
    {
        if (dungeonData == null)
            return;
        //Loop for each room
        for (int i = 0; i < dungeonData.Rooms.Count; i++)
        {
            if (dungeonData.Rooms[i].type == Room.roomType.start)
                player.transform.position = dungeonData.Rooms[i].RoomCenterPos;

            // Place enemies in all room types except 'start' and 'exit'
            if (dungeonData.Rooms[i].type != Room.roomType.start && dungeonData.Rooms[i].type != Room.roomType.exit)
            {
                //TO place eneies we need to analyze the room tiles to find those accesible from the path
                Room room = dungeonData.Rooms[i];
                RoomGraph roomGraph = new RoomGraph(room.FloorTiles);

                //Find the Path inside this specific room
                HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>(room.FloorTiles);
                //Find the tiles belonging to both the path and the room
                roomFloor.IntersectWith(dungeonData.Path);

                //Run the BFS to find all the tiles in the room accessible from the path
                Dictionary<Vector2Int, Vector2Int> roomMap = roomGraph.RunBFS(roomFloor.First(), room.PropPositions);

                //Positions that we can reach + path == positions where we can place enemies
                room.PositionsAccessibleFromPath = roomMap.Keys.OrderBy(x => Guid.NewGuid()).ToList();
                //did we add this room to the roomEnemiesCount list?
                PlaceEnemies(room, UnityEngine.Random.Range(minRoomEnemyCount, maxRoomEnemyCount));
            }
        }
    }

    /// <summary>
    /// Places enemies in the positions accessible from the path
    /// </summary>
    /// <param name="room"></param>
    /// <param name="enemysCount"></param>
    private void PlaceEnemies(Room room, int enemysCount)
    {
        for (int i = 0; i < enemysCount; i++)
        {
            if (room.PositionsAccessibleFromPath.Count <= i)
            {
                return;
            }
            GameObject enemy = Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Count)]);
            enemy.transform.SetParent(parent);
            enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[i] + Vector2.one * 0.5f;
            enemy.GetComponent<EnemyBase>().SetRoom(room);
            room.EnemiesInTheRoom.Add(enemy);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (dungeonData == null || showGizmo == false)
            return;
        foreach (Room room in dungeonData.Rooms)
        {
            Color color = Color.green;
            color.a = 0.3f;
            Gizmos.color = color;

            foreach (Vector2Int pos in room.PositionsAccessibleFromPath)
            {
                Gizmos.DrawCube((Vector2)pos + Vector2.one * 0.5f, Vector2.one);
            }
        }
    }
}

public class RoomGraph
{
    Dictionary<Vector2Int, List<Vector2Int>> graph = new Dictionary<Vector2Int, List<Vector2Int>>();

    public RoomGraph(HashSet<Vector2Int> roomFloor)
    {
        foreach (Vector2Int pos in roomFloor)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();
            foreach (Vector2Int direction in Direction2D.cardinal4DirectionsList)
            {
                Vector2Int newPos = pos + direction;
                if (roomFloor.Contains(newPos))
                {
                    neighbours.Add(newPos);
                }
            }
            graph.Add(pos, neighbours);
        }
    }

    /// <summary>
    /// Creates a map of reachable tiles in our dungeon.
    /// </summary>
    /// <param name="startPos">Door position or tile position on the path between rooms inside this room</param>
    /// <param name="occupiedNodes">This position has containt prop</param>
    /// <returns></returns>
    public Dictionary<Vector2Int, Vector2Int> RunBFS(Vector2Int startPos, HashSet<Vector2Int> occupiedNodes)
    {
        //BFS related variuables
        Queue<Vector2Int> nodesToVisit = new Queue<Vector2Int>();
        nodesToVisit.Enqueue(startPos);

        HashSet<Vector2Int> visitedNodes = new HashSet<Vector2Int>();
        visitedNodes.Add(startPos);

        //The dictionary that we will return 
        Dictionary<Vector2Int, Vector2Int> map = new Dictionary<Vector2Int, Vector2Int>();
        map.Add(startPos, startPos);

        while (nodesToVisit.Count > 0)
        {
            //get the data about specific position
            Vector2Int node = nodesToVisit.Dequeue();
            List<Vector2Int> neighbours = graph[node];

            //loop through each neighbour position
            foreach (Vector2Int neighbourPosition in neighbours)
            {
                //add the neighbour position to our map if it is valid
                if (visitedNodes.Contains(neighbourPosition) == false &&
                    occupiedNodes.Contains(neighbourPosition) == false)
                {
                    nodesToVisit.Enqueue(neighbourPosition);
                    visitedNodes.Add(neighbourPosition);
                    map[neighbourPosition] = node;
                }
            }
        }

        return map;
    }
}
