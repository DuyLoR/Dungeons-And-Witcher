﻿using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    /// <summary>
    /// Create walls with positions and tilemap, and what is wall type
    /// </summary>
    /// <param name="floorPositions"></param>
    /// <param name="tilemapVisualizer"></param>
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinal8DirectionsList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    /// <summary>
    /// This function return HashSet<Vector2Int> contains position what is had 1 neighbourPosition not inside floorPosition <4 derection>
    /// </summary>
    /// <param name="floorPositions"></param>
    /// <param name="cardinalDirectionsList"></param>
    /// <returns></returns>
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> cardinalDirectionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(position);
                }
            }
        }
        return wallPositions;
    }
}
// note: xử lý vấn để ruletile của walls: tạo 1 method duyệt lại wallPositions kiểm tra từng position,
// xét các hướng có nằm trong floorPositions?
// dựa vào các hướng đó mà xác định được sprite của wall nào được tạo ở đó. VD: position có direction.up thuộc floorPositions
// --> wall đó ở bên dưới floor thực hiện lấy sprite wall ở dưới gán vào việc vẽ wall.

// Tạo 1 class để định nghĩa các hướng với sprite của wall tương ứng.
// VD: direction(0,-1) --> up so với floorPositions --> sprite_wall_up

// C2: vẽ floorPositions có ruletile chứa Wall, khi vẽ wall ở walltilemap thực hiện đẩy nó ở dưới floorPositions --> vẫn còn collider.