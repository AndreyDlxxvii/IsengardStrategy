using System;
using Controllers.OutPost;
using UnityEngine;
using Views.Outpost;

public class BuildGenerator : IOnController, IOnUpdate, IDisposable
{
    //возможно переполнение массива при большом количестве зданий и ресурсов
    public BaseBuildAndResources[,] Buildings => _buildings;

    private LeftUI _leftUI;
    private Camera _mainCamera;
    private BaseBuildAndResources[,] _buildings;
    private Building _flyingBuilding;
    private LayerMask _layerMask;
    

    private OutpostSpawner _outpostSpawner;
    //привязать к ширине тайла
    private float _offsetY = 0.1f;
    public BuildGenerator(GameConfig gameConfig, LeftUI leftUI, LayerMask layerMask, OutpostSpawner outpostSpawner)
    {
        _leftUI = leftUI;
       _buildings = new BaseBuildAndResources[gameConfig.MapSizeX,gameConfig.MapSizeY];
       _leftUI.BuildFirstButton.onClick.AddListener(() => StartPlacingBuild(gameConfig.BuildFirst));
       _leftUI.BuildSecondButton.onClick.AddListener(() => StartPlacingBuild(gameConfig.BuildSecond));
       _layerMask = layerMask;
       _mainCamera = Camera.main;
       _outpostSpawner = outpostSpawner;
    }
    
    public void StartPlacingBuild(Building build)
    {
        if (_flyingBuilding != null)
        {
            GameObject.Destroy(_flyingBuilding.gameObject);
        }
        _flyingBuilding = GameObject.Instantiate(build);
    }

    public void OnUpdate(float deltaTime)
    {
        if (_flyingBuilding != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var position, 100f, _layerMask))
            {
                var t = position.collider.transform.position;
                Vector3 worldPosition = position.point;
                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);
                _flyingBuilding.transform.position = new Vector3(x, _offsetY, y);
                _flyingBuilding.SetAvailableToInstant(false);
                if (position.point.y > _offsetY && _buildings[x, y] == null)
                {
                    _flyingBuilding.SetAvailableToInstant(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        Vector3 pointDestination = new Vector3(position.transform.parent.position.x - _flyingBuilding.transform.position.x, 
                            0f, position.transform.parent.position.z - _flyingBuilding.transform.position.z);
                        _flyingBuilding.SetPointDestination(pointDestination);
                        _buildings[x, y] = _flyingBuilding;
                        _flyingBuilding.SetNormalColor();
                        var outpost = _flyingBuilding.gameObject.GetComponentInChildren<OutpostUnitView>();
                        _flyingBuilding = null;
                        if (outpost)
                        {
                            _outpostSpawner.SpawnLogic(outpost);
                        }
                    }
                }
            }
        }
    }
    
    public bool IsFlyingBuildingTrue()
    {
        if (_flyingBuilding != null)
        {
            return true;
        }
        return false;
    }

    public void Dispose()
    {
        _leftUI.BuildFirstButton.onClick.RemoveAllListeners();
        _leftUI.BuildSecondButton.onClick.RemoveAllListeners();
    }
}