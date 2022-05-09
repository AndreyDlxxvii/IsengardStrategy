using System;
using System.Linq;
using Code.Models.WorkersPlace;
using Controllers.BaseUnit;
using Controllers.ResouresesPlace;
using Controllers.Worker;
using Data;
using ResurseSystem;
using UnityEngine;
using Views.BaseUnit.UI;

namespace Controllers.WorkersPlace
{
    public class WorkersPlaceController: IOnController, IOnStart, IOnUpdate ,IDisposable
    {
        private readonly WorkerSpawner _workerSpawner;
        private readonly WorkersCommandSender _workersCommandSender;
        private readonly ResourcesPlaceSpawner _resourcesPlaceSpawner;
        private readonly BuyUnitUI _buyUnitUI;
        private readonly GameConfig _gameConfig;
        private readonly WorkersPoolOptions _workersPoolOptions;
        private WorkersPlaceModel _poolOfWorkers;

        public WorkersPlaceController(WorkerSpawner workerSpawner, WorkersCommandSender workersCommandSender,
            ResourcesPlaceSpawner resourcesPlaceSpawner,BuyUnitUI buyUnitUI, GameConfig gameConfig)
        {
            _workerSpawner = workerSpawner;
            _workersCommandSender = workersCommandSender;
            _resourcesPlaceSpawner = resourcesPlaceSpawner;
            _buyUnitUI = buyUnitUI;
            _gameConfig = gameConfig;
            _workersPoolOptions =gameConfig.WorkersPoolOptions;
        }
        
        public void OnStart()
        {
            _poolOfWorkers = new WorkersPlaceModel(_workerSpawner,_workersPoolOptions,
                new Vector3(_gameConfig.MapSizeX / 2.0f,0,_gameConfig.MapSizeY / 2.0f));
            _poolOfWorkers.InitPool();
            _buyUnitUI.buyUnit += AddNewWorker;
            _resourcesPlaceSpawner.NewResourceLogicWasSpawned += ListenNewMine;
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var pool in _poolOfWorkers.WorkersPool)
            {
                if (pool.Value)
                {
                    pool.Key.OnUpdate(deltaTime);
                }
            }
            
        }
        
        public void Dispose()
        {
            _resourcesPlaceSpawner.NewResourceLogicWasSpawned -= ListenNewMine;
        }

        public void ListenNewMine()
        {
            Debug.Log("new was added");
            var action = _resourcesPlaceSpawner.ResourcesPlaceControllers.Last();
            action.AddUnitToMine += SendWorker;
            action.LessUnitFromMine += BackUnit;
        }
        
        public void AddNewWorker()
        {
            _poolOfWorkers.Spawn();
        }
        
        public void SendWorker(Vector3 whereToSend,ResourcesPlaceController resourcesPlaceController)
        {
            Debug.Log(resourcesPlaceController.PlaceView.GetInstanceID());
            if (!resourcesPlaceController.PlaceView.IsActive)
            {
                return;
            }
            var worker = _poolOfWorkers.GetWorker();
            worker.WorkerView.gameObject.SetActive(true);
            worker.WorkerView.
                AddResource(new ResurseHolder(resourcesPlaceController.ResurseMine.ResurseHolderMine.ResurseInHolder,
                    20,0));
            worker.ResourcesPlaceController = resourcesPlaceController;
            resourcesPlaceController.AddNewUnit(worker);
            _workersCommandSender.SendMiningCommand(worker,resourcesPlaceController);
        }

        public void BackUnit(ResourcesPlaceController resourcesPlaceController)
        {
            Debug.Log(resourcesPlaceController.PlaceView.GetInstanceID());
            if (!resourcesPlaceController.PlaceView.IsActive)
            {
                return;
            }
            var worker = resourcesPlaceController.GetLastUnit();
            worker.WorkerView.GetResurseOutOfHolder();
            _workersCommandSender.StopCommand(worker);
            _poolOfWorkers.ReturnToPool(worker);
            resourcesPlaceController.DeleteLastUnitFromList();
        }
    }
}