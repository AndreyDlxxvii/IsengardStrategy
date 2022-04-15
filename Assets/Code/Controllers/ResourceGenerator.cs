using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : IOnController
{
    private List<VoxelTile> _spawnedTilesPosition;
    private BaseBuildAndResources[,] _installedBuildings;
    private List<Vector2Int> _possiblePlaceResource = new List<Vector2Int>();
    private List<Vector2Int> _spawnedResources = new List<Vector2Int>();
    private GameConfig _gameConfig;
    private Mineral _mineral;
    public ResourceGenerator(List<VoxelTile> spawnedTilesPosition, BaseBuildAndResources[,] installedBuildings,
        LeftUI leftUI, GameConfig gameConfig)
    {
        _spawnedTilesPosition = spawnedTilesPosition;
        _installedBuildings = installedBuildings;
        leftUI.BuildResources.onClick.AddListener(SpawnResources);
        _gameConfig = gameConfig;
    }
    
 
    private void SpawnResources()
    {
        var i = Random.Range(0, _spawnedTilesPosition.Count); 
        var tile = _spawnedTilesPosition[i];
        GetPossiblePlace(tile);
        PlaceResources();
    }

    private void GetPossiblePlace(VoxelTile tile)
    {
        int count = 0;
        int x = (int) tile.transform.position.x - 1;
        int y = (int) tile.transform.position.z - 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _possiblePlaceResource.Add(new Vector2Int(x + i, y + j));
            }
        }

        _possiblePlaceResource.Remove(new Vector2Int((int) tile.transform.position.x,
            (int) tile.transform.position.z));

        foreach (var byteAccess in tile.TablePassAccess)
        {
            if (byteAccess == 1)
            {
                switch (count)
                {
                    case 0:
                        _possiblePlaceResource.Remove(new Vector2Int((int) tile.transform.position.x,
                            (int) tile.transform.position.z - 1));
                        break;
                    case 1:
                        _possiblePlaceResource.Remove(new Vector2Int((int) tile.transform.position.x - 1,
                            (int) tile.transform.position.z));
                        break;
                    case 2:
                        _possiblePlaceResource.Remove(new Vector2Int((int) tile.transform.position.x,
                            (int) tile.transform.position.z + 1));
                        break;
                    case 3:
                        _possiblePlaceResource.Remove(new Vector2Int((int) tile.transform.position.x + 1,
                            (int) tile.transform.position.z));
                        break;
                }
            }
            count++;
        }

        foreach (var building in _installedBuildings)
        {
            if (building != null)
            {
                _possiblePlaceResource.Remove(new Vector2Int((int) building.transform.position.x,
                    (int) building.transform.position.z));
            }
        }
    }

    private void PlaceResources()
    {
        if (_possiblePlaceResource.Count != 0)
        {
            var pos = _possiblePlaceResource[Random.Range(0, _possiblePlaceResource.Count - 1)];
            _mineral = GameObject.Instantiate(_gameConfig.Mineral, new Vector3(pos.x, 0.1f, pos.y), Quaternion.identity);
            _installedBuildings[pos.x, pos.y] = _mineral;
            _possiblePlaceResource.Clear();
        }
    }
}