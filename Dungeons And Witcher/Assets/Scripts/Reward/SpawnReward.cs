using UnityEngine;

public class SpawnReward : MonoBehaviour
{
    public DungeonData dungeonData;
    [SerializeField]
    private GameObject chestPref;
    private void Awake()
    {
        dungeonData = FindObjectOfType<DungeonData>();
        if (dungeonData == null)
            dungeonData = gameObject.AddComponent<DungeonData>();
    }
    private void Update()
    {
        foreach (var room in dungeonData.Rooms)
        {
            //TODO: Boss room

            if (room.EnemiesInTheRoom.Count == 0 && !room.isClearEnemies && room.type == Room.roomType.normal)
            {
                var chest = Instantiate(chestPref);
                chest.transform.position = room.RoomCenterPos;
                room.isClearEnemies = true;
            }
        }
    }
}
