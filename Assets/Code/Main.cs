using Controllers.OutPost;
using UnityEngine;
using UnityEngine.AI;
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
    private Controller _controllers;

    private void Start()
    {
        _controllers = new Controller();
        new GameInit(_controllers, _gameConfig, _rightUI, _navMeshSurface, _canvas, _leftUI, _layerMaskTiles,_unitUISpawnerTest);
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