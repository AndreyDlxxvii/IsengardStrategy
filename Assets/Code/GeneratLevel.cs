using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GeneratLevel : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Button btn;
    [SerializeField] private VoxelTile[] TilePrefabs;
    [SerializeField] private NavMeshSurface _navMesh;
    
    public int MapSizeX = 200;
    public int MapSizeY = 200;
    
    [Range(1, 10)] public int NumberOfTilesSpawn;

    private VoxelTile[,] _spawnedTiles;
    private int _offset;
    
    private List<VoxelTile> _tilesHasFreeEdge = new List<VoxelTile>();
    private List<VoxelTile> _availableTilesDown = new List<VoxelTile>();
    private List<VoxelTile> _availableTilesLeft = new List<VoxelTile>();
    private List<VoxelTile> _availableTilesUp = new List<VoxelTile>();
    private List<VoxelTile> _availableTilesRight = new List<VoxelTile>();

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
            _spawnedTiles[x, y] = Instantiate(TilePrefabs[Random.Range(0, TilePrefabs.Length)], new Vector3(x, 0, y), Quaternion.identity, _parent.transform);
            StandNewTile(_spawnedTiles[x, y]);
        }
        else
        {
            foreach (var tile in _spawnedTiles)
            {
                if (tile != null)
                {
                    if (CheckEmptyPosition(tile,_offset, 0) && tile.TablePassAccess[3] == 1 || 
                        CheckEmptyPosition(tile,-_offset, 0) && tile.TablePassAccess[1] == 1 ||
                        CheckEmptyPosition(tile, 0,_offset) && tile.TablePassAccess[2] == 1 ||
                        CheckEmptyPosition(tile,0, -_offset) && tile.TablePassAccess[0] == 1)
                    {
                        _tilesHasFreeEdge.Add(tile);
                    }
                }
            }

            for (int i = 1; i <= NumberOfTilesSpawn; i++)
            {
                var tile = _tilesHasFreeEdge[Random.Range(0, _tilesHasFreeEdge.Count - 1)];
                StandNewTile(tile);
            }
            _tilesHasFreeEdge.Clear();
        }
        _navMesh.BuildNavMesh();
    }

    private void StandNewTile(VoxelTile standTile)
    {
        TilesCanBeSet(standTile);
        var tileTransform = standTile.transform;
        
        if (_availableTilesDown.Count != 0)
        {
            var pos = tileTransform.position + Vector3.back*_offset;
            var tile = _availableTilesDown[Random.Range(0, _availableTilesDown.Count - 1)];
            CreateTile(pos,tile);
        }
    
        if (_availableTilesLeft.Count != 0)
        {
            var pos = tileTransform.position + Vector3.left*_offset;
            var tile = _availableTilesLeft[Random.Range(0, _availableTilesLeft.Count - 1)];
            CreateTile(pos,tile);
        }        
    
        if (_availableTilesUp.Count != 0)
        {
            var pos = tileTransform.position + Vector3.forward*_offset;
            var tile = _availableTilesUp[Random.Range(0, _availableTilesUp.Count - 1)];
            CreateTile(pos,tile);
        }        
    
        if (_availableTilesRight.Count != 0 )
        {
            var pos = tileTransform.position + Vector3.right*_offset;
            var tile = _availableTilesRight[Random.Range(0, _availableTilesRight.Count - 1)];
            CreateTile(pos,tile);
        }
        _availableTilesDown.Clear();
        _availableTilesLeft.Clear();
        _availableTilesUp.Clear();
        _availableTilesRight.Clear();
    }
    
    private void TilesCanBeSet(VoxelTile standTile)
    {
        int i = 0;
        foreach (var ell in standTile.TablePassAccess)
        {
            if (ell == 1)
            {
                foreach (var tile in TilePrefabs)
                {
                    if (i == 0 && tile.TablePassAccess[2] == 1 && CheckEmptyPosition(standTile, 0, -_offset) && !_availableTilesDown.Contains(tile))
                        _availableTilesDown.Add(tile);
                    if (i == 1 && tile.TablePassAccess[3] == 1 && CheckEmptyPosition(standTile, -_offset, 0) && !_availableTilesLeft.Contains(tile))
                        _availableTilesLeft.Add(tile);    
                    if (i == 2 && tile.TablePassAccess[0] == 1 && CheckEmptyPosition(standTile, 0, _offset) && !_availableTilesUp.Contains(tile))
                        _availableTilesUp.Add(tile);
                    if (i == 3 && tile.TablePassAccess[1] == 1 && CheckEmptyPosition(standTile, _offset, 0) && !_availableTilesRight.Contains(tile))
                        _availableTilesRight.Add(tile);
                }
            }
            i++;
        }
    }
    private void CreateTile(Vector3 pos, VoxelTile voxelTile)
    {
        if (CheckEmptyPosition(null, (int) pos.x, (int) pos.z))
        {
            _spawnedTiles[(int) pos.x, (int) pos.z] = Instantiate(voxelTile, pos, Quaternion.identity, _parent.transform);
        }
    }

    private bool CheckEmptyPosition(VoxelTile tile, int xOfset, int yOfset)
    {
        var tilePosition = Vector3.zero;
        if (tile != null)
            tilePosition = tile.transform.position;
        var xPos = (int)tilePosition.x;
        var yPos = (int)tilePosition.z;
        return _spawnedTiles[xPos + xOfset, yPos + yOfset] == null;
    }
}