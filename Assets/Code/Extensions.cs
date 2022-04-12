using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    
    public static bool  CheckEmptyPosition(VoxelTile standTile, int xOfset, int yOfset, VoxelTile[,] _spawnedTiles)
    {
        
        var tilePosition = Vector3.zero;
        if (standTile != null)
            tilePosition = standTile.transform.position;
        var xPos = (int)tilePosition.x;
        var yPos = (int)tilePosition.z;
        return _spawnedTiles[xPos + xOfset, yPos + yOfset] == null;
    }
    
    public static List<VoxelTile> TilesCanBeSet(int side, List<VoxelTile> tilePrefabs)
    {
        List<VoxelTile> availableTiles = new List<VoxelTile>();
        foreach (var tile in tilePrefabs)
        {
            if (side == 0 && tile.TablePassAccess[2] == 1 && !availableTiles.Contains(tile))
                availableTiles.Add(tile);
            if (side == 1 && tile.TablePassAccess[3] == 1 && !availableTiles.Contains(tile))
                availableTiles.Add(tile);    
            if (side == 2 && tile.TablePassAccess[0] == 1 && !availableTiles.Contains(tile))
                availableTiles.Add(tile);
            if (side == 3 && tile.TablePassAccess[1] == 1 && !availableTiles.Contains(tile))
                availableTiles.Add(tile);
        }
        return availableTiles;
    }
}