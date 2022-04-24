using Controllers.OutPost;
using UnityEngine;
using ResurseSystem;
using UnityEngine.AI;

public class Main : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private RightUI _rightUI;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private Transform _canvas;
    [SerializeField] private LeftUI _leftUI;
    [SerializeField] private LayerMask _layerMaskTiles;
    [SerializeField] private OutpostSpawner _outpostSpawner;
    [SerializeField] private TopResUiVew TopResUI;
    [SerializeField] private GlobalResurseStock GlobalResStock;
    private BuildingsUI buildingResursesUI;
    [SerializeField] private BuildingResursesUIController _buildingResController;
    private Controller _controllers;
   
    private void Start()
    {
        GlobalResStock.ResetGlobalRes();
        _controllers = new Controller();
        new GameInit(_controllers, _gameConfig, _rightUI, _navMeshSurface, _canvas, _leftUI, _layerMaskTiles, _outpostSpawner);
        _buildingResController=new BuildingResursesUIController(buildingResursesUI);
        _controllers.Add(_buildingResController);
        _controllers.Add(new MainResursesController(GlobalResStock, TopResUI));
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