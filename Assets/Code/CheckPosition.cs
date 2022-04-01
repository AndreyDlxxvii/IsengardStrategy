using UnityEngine;

public static class CheckPosition
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
}