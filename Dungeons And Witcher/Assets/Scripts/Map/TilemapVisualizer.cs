using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap, pathTileMap;
    [SerializeField] private TileBase ruleFloorTile, ruleWallTile, rulepathTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, ruleFloorTile);
    }
    public void PaintPathTiles(IEnumerable<Vector2Int> pathPositions)
    {
        PaintTiles(pathPositions, pathTileMap, rulepathTile);
    }
    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        PaintTiles(wallPositions, wallTilemap, ruleWallTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }
    private void PaintSingleTile(Tilemap positions, TileBase tile, Vector2Int position)
    {
        var tilePosition = positions.WorldToCell((Vector3Int)position);
        positions.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

}
