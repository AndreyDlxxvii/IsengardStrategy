using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GeneratLevel : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Button btn;
    [SerializeField] private VoxelTile[] TilePrefabs;
    
    public int MapSizeX = 10;
    public int MapSizeY = 10;

    private VoxelTile[,] _spawnedTiles;
    private List<VoxelTile> _voxelTiles = new List<VoxelTile>();
    private int _offset;

    private void Start()
    {
        btn.onClick.AddListener(PlaceTile);
        _spawnedTiles = new VoxelTile[MapSizeX, MapSizeY];
        _offset = TilePrefabs[0].SizeTile;
        PlaceTile();
    }

    private void PlaceTile()
    {
        var x = MapSizeX / 2;
        var y = MapSizeY / 2;
        if (_spawnedTiles[x, y] == null)
        {
            var t = Instantiate(TilePrefabs[Random.Range(0, TilePrefabs.Length)], new Vector3(x, 0, y), Quaternion.identity, _parent.transform);
            _spawnedTiles[x, y] = t;
            PossibleToInstallTile(t);
        }
        else
        {
            foreach (var tile in _spawnedTiles)
            {
                if (tile != null)
                {
                    var xPos = (int)tile.transform.position.x;
                    var yPos = (int)tile.transform.position.z;
                    if ((_spawnedTiles[xPos+_offset, yPos] == null || _spawnedTiles[xPos-_offset, yPos] == null) &&
                        (_spawnedTiles[xPos, yPos+4] == null || _spawnedTiles[xPos-_offset, yPos-_offset] == null))
                    {
                        _voxelTiles.Add(tile);
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                PossibleToInstallTile(_voxelTiles[Random.Range(0,_voxelTiles.Count-1)]);
            }
            _voxelTiles.Clear();
        }
        
    }

    private void PossibleToInstallTile(VoxelTile standTile)
    {
        List<VoxelTile> availableTilesDown = new List<VoxelTile>();
        List<VoxelTile> availableTilesLeft = new List<VoxelTile>();
        List<VoxelTile> availableTilesUp = new List<VoxelTile>();
        List<VoxelTile> availableTilesRight = new List<VoxelTile>();
        int i = 0;
        foreach (var ell in standTile.Table)
        {
            if (ell)
            {
                foreach (var tile in TilePrefabs)
                {
                    if (i == 0 && tile.Table[2] && !availableTilesDown.Contains(tile))
                        availableTilesDown.Add(tile);
                    if (i == 1 && tile.Table[3] && !availableTilesLeft.Contains(tile))
                        availableTilesLeft.Add(tile);    
                    if (i == 2 && tile.Table[0] && !availableTilesUp.Contains(tile))
                        availableTilesUp.Add(tile);
                    if (i == 3 && tile.Table[1] && !availableTilesRight.Contains(tile))
                        availableTilesRight.Add(tile);
                }
            }

            i++;
        }

        if (availableTilesDown.Count != 0)
        {
            var pos = standTile.transform.position + Vector3.back*_offset;
            if (_spawnedTiles[(int)pos.x, (int)pos.z] == null)
            {
                //var t = Instantiate(availableTilesDown[Random.Range(0, availableTilesDown.Count - 1)],
                //    pos, Quaternion.identity, _parent.transform);
                _spawnedTiles[(int)pos.x, (int)pos.z] = Instantiate(availableTilesDown[Random.Range(0, availableTilesDown.Count-1)], 
                    pos, Quaternion.identity, _parent.transform);
            }
        }
        
        if (availableTilesLeft.Count != 0)
        {
            var pos = standTile.transform.position + Vector3.left*_offset;
            if (_spawnedTiles[(int) pos.x, (int) pos.z] == null)
            {
                _spawnedTiles[(int)pos.x, (int)pos.z] = Instantiate(availableTilesLeft[Random.Range(0, availableTilesDown.Count-1)], 
                    pos, Quaternion.identity, _parent.transform);
            }
        }        
        
        if (availableTilesUp.Count != 0)
        {
            var pos = standTile.transform.position + Vector3.forward*_offset;
            if (_spawnedTiles[(int) pos.x, (int) pos.z] == null)
            {
                _spawnedTiles[(int)pos.x, (int)pos.z] = Instantiate(availableTilesUp[Random.Range(0, availableTilesDown.Count-1)], 
                    pos, Quaternion.identity, _parent.transform);
            }
        }        
        
        if (availableTilesRight.Count != 0)
        {
            var pos = standTile.transform.position + Vector3.right*_offset;
            if (_spawnedTiles[(int) pos.x, (int) pos.z] == null)
            {
                _spawnedTiles[(int)pos.x, (int)pos.z] = Instantiate(availableTilesRight[Random.Range(0, availableTilesDown.Count-1)], 
                    pos, Quaternion.identity, _parent.transform);
            }
        }
        availableTilesDown.Clear();
        availableTilesLeft.Clear();
        availableTilesUp.Clear();
        availableTilesRight.Clear();
    }
}