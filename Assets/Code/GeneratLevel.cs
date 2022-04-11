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
    [SerializeField] private GameObject MainTower;
    
    [SerializeField] private Transform _canvas;
    [SerializeField] private RightUI _rightUI;

    public int MapSizeX = 200;
    public int MapSizeY = 200;
    
    private VoxelTile[,] _spawnedTiles;
    private int _offsetInstanceTiles;
    private VoxelTile _firstTile;

    private Dictionary<Button, Vector3> _spawnedButtons = new Dictionary<Button, Vector3>();

    private void Awake()
    {
        _rightUI.FirstBtnSprite = TilePrefabs[0].IconTile;
        _rightUI.SecondBtnSprite = TilePrefabs[1].IconTile;
        _rightUI.ThirdBtnSprite = TilePrefabs[2].IconTile;
    }

    private void Start()
    {
        _rightUI.TileSelected += SelectFirstTile;
    }
    
    private void SelectFirstTile(int numTile)
    {
        switch (numTile)
        {
            case 0:
                _firstTile = TilePrefabs[numTile];
                break;
            case 1:
                _firstTile = TilePrefabs[numTile];
                break;
            case 2:
                _firstTile = TilePrefabs[numTile];
                break;
        }
        PlaceFirstTile(_firstTile);
        _rightUI.TileSelected -= SelectFirstTile;
        _rightUI.gameObject.SetActive(false);
    }
    
    private void PlaceFirstTile(VoxelTile tile)
    {
        _spawnedTiles = new VoxelTile[MapSizeX, MapSizeY];
        _offsetInstanceTiles = TilePrefabs[0].SizeTile;
        int x = MapSizeX / 2;
        int y = MapSizeY / 2;
        if (_spawnedTiles[x, y] == null)
        {
            _spawnedTiles[x, y] = Instantiate(tile, new Vector3(x, 0, y), 
                Quaternion.identity, _parentForTilesObject.transform);
            CreateButton(_spawnedTiles[x, y]);
        }
        Instantiate(MainTower,new Vector3(x, 0, y), Quaternion.identity);
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
                    if (i == 0 && Extensions.CheckEmptyPosition(tile, 0, -_offsetInstanceTiles, _spawnedTiles))
                    {
                        Vector3 posToSpawnBtn = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z - _offsetInstanceTiles);
                        InstansButton(posToSpawnBtn, Vector3.back, tile, i);
                    }
                    else if (i == 1 && Extensions.CheckEmptyPosition(tile, -_offsetInstanceTiles, 0, _spawnedTiles))
                    {
                        Vector3 posToSpawnBtn = new Vector3(tile.transform.position.x - _offsetInstanceTiles, tile.transform.position.y, tile.transform.position.z);
                        InstansButton(posToSpawnBtn, Vector3.left, tile, i);
                    }
                    else if (i == 2 && Extensions.CheckEmptyPosition(tile, 0, _offsetInstanceTiles, _spawnedTiles))
                    {
                        Vector3 posToSpawnBtn = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z + _offsetInstanceTiles);
                        InstansButton(posToSpawnBtn, Vector3.forward, tile, i);
                    }
                    else if (i == 3 && Extensions.CheckEmptyPosition(tile, _offsetInstanceTiles, 0, _spawnedTiles))
                    {
                        Vector3 posToSpawnBtn = new Vector3(tile.transform.position.x + _offsetInstanceTiles, tile.transform.position.y, tile.transform.position.z);
                        InstansButton(posToSpawnBtn, Vector3.right, tile, i);
                    } 
                    break;
            }
            i++;
        }
        _navMesh.BuildNavMesh();
    }

    private void InstansButton(Vector3 posForButton, Vector3 direction, VoxelTile tile, int numOfGroupAvailableTiles)
    {
        if (!_spawnedButtons.ContainsValue(posForButton))
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(posForButton);
            Button btn = Instantiate(buttonRespawn, pos, Quaternion.identity, _canvas);
            _spawnedButtons.Add(btn, posForButton);
            btn.onClick.AddListener(delegate
            {
                _spawnedButtons.Remove(btn);
                CreateTile(tile, direction * _offsetInstanceTiles, numOfGroupAvailableTiles);
                btn.onClick.RemoveAllListeners();
                Destroy(btn.gameObject);
            });
        }
    }

    private void CreateTile(VoxelTile voxelTile, Vector3 spawnPos, int i)
    {
        var _availableTiles = Extensions.TilesCanBeSet(i, TilePrefabs);
        var pos = new Vector3(voxelTile.transform.position.x + spawnPos.x, 0 , voxelTile.transform.position.z + spawnPos.z);
        var tile = Instantiate(_availableTiles[Random.Range(0, _availableTiles.Count-1)], pos, Quaternion.identity, _parentForTilesObject.transform);
        _availableTiles.Clear();
        _spawnedTiles[(int) pos.x, (int) pos.z] = tile;
        CreateButton(tile);
    }

    private void FixedUpdate()
    {
        if (_spawnedButtons.Count != 0)
        {
            foreach (var ell in _spawnedButtons)
            {
                ell.Key.transform.position = Camera.main.WorldToScreenPoint(ell.Value);
            }
        }
    }
}