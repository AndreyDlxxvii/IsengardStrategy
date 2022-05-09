using System;
using System.Linq;
using Code.Models.WorkersPlace;
using Controllers.BaseUnit;
using Controllers.ResouresesPlace;
using Controllers.Worker;
using UnityEngine;
using Views.BaseUnit.UI;

namespace Controllers.WorkersPlace
{
    public class WorkersPlaceController: IOnController, IOnStart, IDisposable
    {
        private readonly WorkerSpawner _workerSpawner;
        private readonly WorkersCommandSender _workersCommandSender;
        private readonly ResourcesPlaceSpawner _resourcesPlaceSpawner;
        private readonly BuyUnitUI _buyUnitUI;
        private WorkersPlaceModel _poolOfWorkers;

        public WorkersPlaceController(WorkerSpawner workerSpawner, WorkersCommandSender workersCommandSender,
            ResourcesPlaceSpawner resourcesPlaceSpawner,BuyUnitUI buyUnitUI)
        {
            _workerSpawner = workerSpawner;
            _workersCommandSender = workersCommandSender;
            _resourcesPlaceSpawner = resourcesPlaceSpawner;
            _buyUnitUI = buyUnitUI;
        }
        
        public void OnStart()
        {
            _poolOfWorkers = new WorkersPlaceModel(0,_workerSpawner);
            _buyUnitUI.buyUnit += AddNewWorker;
            _resourcesPlaceSpawner.NewResourceLogicWasSpawned += ListenNewMine;
        }
        
        public void Dispose()
        {
            
        }

        public void ListenNewMine()
        {
            var action = _resourcesPlaceSpawner.ResourcesPlaceControllers.Last();
            action.AddUnitToMine += SendWorker;
            action.LessUnitFromMine += BackUnit;
        }
        
        public void AddNewWorker()
        {
            _poolOfWorkers.AddAndGetWorker();
        }
        
        public void SendWorker(Vector3 whereToSend,ResourcesPlaceController resourcesPlaceController)
        {
            //вызываем рабочего из пула
            //ставим в стартовую позицию
            //даем команду
            //включаем
            var worker = _poolOfWorkers.GetWorker("Worker");
            worker.WorkerView.gameObject.SetActive(true);
            worker.ResourcesPlaceController = resourcesPlaceController;
            resourcesPlaceController.AddNewUnit(worker);
            _workersCommandSender.SendMiningCommand(worker,resourcesPlaceController);
        }

        public void BackUnit(ResourcesPlaceController resourcesPlaceController)
        {
            var worker = resourcesPlaceController.GetLastUnit();
            _workersCommandSender.StopCommand(worker);
            _poolOfWorkers.ReturnToPool(worker.WorkerView.transform);
            resourcesPlaceController.DeleteLastUnitFromList();
        }
    }
}