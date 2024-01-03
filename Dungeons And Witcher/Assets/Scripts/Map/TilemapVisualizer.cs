using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private TileBase ruleFloorTile, ruleWallTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, ruleFloorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> floorPositions, Tilemap floorTilemap, TileBase floorTile)
    {
        foreach (var position in floorPositions)
        {
            PaintSingleTile(floorTilemap, floorTile, position);
        }
    }

    private void PaintSingleTile(Tilemap floorTilemap, TileBase floorTile, Vector2Int position)
    {
        var tilePosition = floorTilemap.WorldToCell((Vector3Int)position);
        floorTilemap.SetTile(tilePosition, floorTile);
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, ruleWallTile, position);
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

}
