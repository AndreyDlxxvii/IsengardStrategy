using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GeneratLevel : MonoBehaviour
{
    [SerializeField] private Transform _parentForTilesObject;
    [SerializeField] private VoxelTile[] TilePrefabs;
    [SerializeField] private NavMeshSurface _navMesh;
    [SerializeField] private Button buttonRespawn;
    [SerializeField] private Transform _canvas;
    
    public int MapSizeX = 200;
    public int MapSizeY = 200;
    
    private VoxelTile[,] _spawnedTiles;
    private int _offsetInstanceTiles;

    private List<VoxelTile> _availableTiles = new List<VoxelTile>();

    private void Start()
    {
        _spawnedTiles = new VoxelTile[MapSizeX, MapSizeY];
        _offsetInstanceTiles = TilePrefabs[0].SizeTile;
        PlaceTile();
    }
    private void PlaceTile()
    {
        var x = MapSizeX / 2;
        var y = MapSizeY / 2;
        if (_spawnedTiles[x, y] == null)
        {
            _spawnedTiles[x, y] = Instantiate(TilePrefabs[Random.Range(0, TilePrefabs.Length)], new Vector3(x, 0, y), 
                Quaternion.identity, _parentForTilesObject.transform);
            CreateButton(_spawnedTiles[x, y]);
        }
    }
        
    private void CreateButton(VoxelTile tile)
    {
        int i = 0;
        foreach (var ell in tile.TablePassAccess)
        {
            switch (ell)
            {
                case 0:
                    break;
                case 1:
                    if (i == 0 && CheckPosition.CheckEmptyPosition(tile, 0, -_offsetInstanceTiles, _spawnedTiles))
                    {
                        var posToSpawnBtn = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z - _offsetInstanceTiles);
                        Vector2 pos = Camera.main.WorldToScreenPoint(posToSpawnBtn);
                        CreateButton(pos, Vector3.back, tile, i);
                    }
                    else if (i == 1 && CheckPosition.CheckEmptyPosition(tile, -_offsetInstanceTiles, 0, _spawnedTiles))
                    {
                        var posToSpawnBtn = new Vector3(tile.transform.position.x - _offsetInstanceTiles, tile.transform.position.y, tile.transform.position.z);
                        Vector2 pos = Camera.main.WorldToScreenPoint(posToSpawnBtn);
                        CreateButton(pos, Vector3.left, tile, i);
                    }
                    else if (i == 2 && CheckPosition.CheckEmptyPosition(tile, 0, _offsetInstanceTiles, _spawnedTiles))
                    {
                        var posToSpawnBtn = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z + _offsetInstanceTiles);
                        Vector2 pos = Camera.main.WorldToScreenPoint(posToSpawnBtn);
                        CreateButton(pos, Vector3.forward, tile, i);
                    }
                    else if (i == 3 && CheckPosition.CheckEmptyPosition(tile, _offsetInstanceTiles, 0, _spawnedTiles))
                    {
                        var posToSpawnBtn = new Vector3(tile.transform.position.x + _offsetInstanceTiles, tile.transform.position.y, tile.transform.position.z);
                        Vector2 pos = Camera.main.WorldToScreenPoint(posToSpawnBtn);
                        CreateButton(pos, Vector3.right, tile, i);
                    } 
                    break;
            }
            i++;
        }
        _navMesh.BuildNavMesh();
    }

    private void CreateButton(Vector2 posForButton, Vector3 direction, VoxelTile tile, int numOfGroupAvailableTiles)
    {
        var btn = Instantiate(buttonRespawn, posForButton, Quaternion.identity, _canvas);
        btn.onClick.AddListener(delegate
        {
            CreateTile(tile, direction * _offsetInstanceTiles, numOfGroupAvailableTiles);
            btn.onClick.RemoveAllListeners();
            Destroy(btn.gameObject);
        });
    }

    private void CreateTile(VoxelTile voxelTile, Vector3 spawnPos, int i)
    {
        var _availableTiles = TilesCanBeSet(i);
        var pos = new Vector3(voxelTile.transform.position.x + spawnPos.x, 0 , voxelTile.transform.position.z + spawnPos.z);
        var tile = Instantiate(_availableTiles[Random.Range(0, _availableTiles.Count-1)], pos, Quaternion.identity, _parentForTilesObject.transform);
        _availableTiles.Clear();
        _spawnedTiles[(int) pos.x, (int) pos.z] = tile;
        CreateButton(tile);
    }
    private List<VoxelTile> TilesCanBeSet(int side)
    {
        foreach (var tile in TilePrefabs)
        {
            if (side == 0 && tile.TablePassAccess[2] == 1 && !_availableTiles.Contains(tile))
                _availableTiles.Add(tile);
            if (side == 1 && tile.TablePassAccess[3] == 1 && !_availableTiles.Contains(tile))
                _availableTiles.Add(tile);    
            if (side == 2 && tile.TablePassAccess[0] == 1 && !_availableTiles.Contains(tile))
                _availableTiles.Add(tile);
            if (side == 3 && tile.TablePassAccess[1] == 1 && !_availableTiles.Contains(tile))
                _availableTiles.Add(tile);
        }
        return _availableTiles;
    }
}