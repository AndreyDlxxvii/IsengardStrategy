using Controllers.OutPost;
using UnityEngine;
using UnityEngine.AI;
using ResurseSystem;
using BuildingSystem;
using Views.BaseUnit.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private RightUI _rightUI;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private Transform _canvas;
    [SerializeField] private LeftUI _leftUI;
    [SerializeField] private LayerMask _layerMaskTiles;
    [SerializeField] private UnitUISpawnerTest _unitUISpawnerTest;   
    [SerializeField] private BuildingsUI buildingsUI;
    [SerializeField] private GlobalResurseStock GlobalResStock;
    [SerializeField] private TopResUiVew TopResUI;
    [SerializeField] private GlobalBuildingsModels _GlobalBuildingModel;
    private Controller _controllers;

    private void Start()
    {
        GlobalResStock.ResetGlobalRes();
        _controllers = new Controller();
        new GameInit(_controllers, _gameConfig, _rightUI, _navMeshSurface, _canvas, _leftUI, _layerMaskTiles,_unitUISpawnerTest, buildingsUI, GlobalResStock, TopResUI, _GlobalBuildingModel);
        _controllers.OnStart();
    }

    private void Update()
    {
        _controllers.OnUpdate(Time.deltaTime);
    }

    private void LateUpdate()
    {
        _controllers.OnLateUpdate(Time.deltaTime);
    }    
}