using System;
using Controllers.OutPost;
using UnityEngine;
using UnityEngine.UI;
using Views.Outpost;

public class BuildingGrid : MonoBehaviour
{
    //привязать к размеру поля
    private Vector2Int _gridSize = new Vector2Int(400, 400);
    [SerializeField] private Button _buildFirstButton;
    [SerializeField] private Button _buildSecondButton;
    [SerializeField] private Building _towerOne;
    [SerializeField] private Building _towerTwo;
    [SerializeField] private OutpostSpawner _outpostSpawner;
    
    private Camera _mainCamera;
    private Building[,] _grid;
    private Building _flyingBuilding;
    // сделать привязку к толщине тайла
    private float _offsetY = 0.1f;

    private void Awake()
    {
        _grid = new Building[_gridSize.x,_gridSize.y];
        _mainCamera = Camera.main;
        _buildFirstButton.onClick.AddListener(() => StartPlacingBuild(_towerOne));
        _buildSecondButton.onClick.AddListener(() => StartPlacingBuild(_towerTwo));
    }

    public void StartPlacingBuild(Building build)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }
        _flyingBuilding = Instantiate(build);
    }

    private void Update()
    {
        if (_flyingBuilding != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var position))
            {
                Vector3 worldPosition = position.point;
                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);
                _flyingBuilding.transform.position = new Vector3(x, _offsetY, y);
                _flyingBuilding.SetAvailableToInstant(false);
                if (position.point.y > _offsetY && _grid[x, y] == null)
                {
                    _flyingBuilding.SetAvailableToInstant(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        Vector3 pointDestination = new Vector3(position.transform.parent.position.x - _flyingBuilding.transform.position.x, 
                            0f, position.transform.parent.position.z - _flyingBuilding.transform.position.z);
                        _flyingBuilding.SetPointDestination(pointDestination);
                        _grid[x, y] = _flyingBuilding;
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

    private void OnDestroy()
    {
        _buildFirstButton.onClick.RemoveAllListeners();
        _buildSecondButton.onClick.RemoveAllListeners();
    }

    public bool IsFlyingBuildingTrue()
    {
        if (_flyingBuilding != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
