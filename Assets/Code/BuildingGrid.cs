using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingGrid : MonoBehaviour
{
    //привязать к размеру поля
    private Vector2Int _gridSize = new Vector2Int(400, 400);
    [SerializeField] private Button _buildFirstButton;
    [SerializeField] private Button _buildSecondButton;
    [SerializeField] private Building _towerOne;
    [SerializeField] private Building _towerTwo;

    private Camera _mainCamera;
    private Building[,] _grid;
    private Building _flyingBuilding;

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
                _flyingBuilding.transform.position = new Vector3(x, 0, y);
                _flyingBuilding.SetAvailableToInstanr(false);
                if (position.point.y > 0.1f && _grid[x, y] == null)
                {
                    _flyingBuilding.SetAvailableToInstanr(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        _grid[x, y] = _flyingBuilding;
                        _flyingBuilding.SetNormalColor();
                        _flyingBuilding = null;
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
}
