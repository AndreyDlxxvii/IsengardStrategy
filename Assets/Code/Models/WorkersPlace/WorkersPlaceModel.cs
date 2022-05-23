using System;
using System.Collections.Generic;
using System.Linq;
using Code.Utils;
using Controllers.BaseUnit;
using Controllers.Worker;
using Data;
using Models.BaseUnit;
using UnityEngine;
using Views.BaseUnit;
using Object = UnityEngine.Object;

namespace Code.Models.WorkersPlace
{
    internal sealed class WorkersPlaceModel
    {
        private readonly int _capacityPool;
        private readonly WorkerSpawner _workerSpawner;
        private readonly Vector3 _whereIsABase;
        private Dictionary<WorkerController,bool> _workersPool; // workerController + IsHeBusy
        private Transform _rootPool;

        public Dictionary<WorkerController, bool> WorkersPool => _workersPool;

        public WorkersPlaceModel(WorkerSpawner workerSpawner, WorkersPoolOptions workersPoolOptions,
            Vector3 whereIsABase)
        {
            _workersPool = new Dictionary<WorkerController,bool>();
            _capacityPool = workersPoolOptions.countOfStartWorkers;
            _workerSpawner = workerSpawner;
            _whereIsABase = whereIsABase;
            if (!_rootPool)
            {
                _rootPool = new
                    GameObject(NameManager.POOL_OF_WORKERS).transform;
            }
        }

        public WorkerController GetWorker()
        {
            return ChooseAndGetWorker();
        }

        public void InitPool()
        {
            for (var i = 0; i < _capacityPool; i++)
            {
                Spawn();
            }
        }
        
        private WorkerController ChooseAndGetWorker()
        {
            var enemy = _workersPool.FirstOrDefault(a => 
                a.Value == false);
            _workersPool[enemy.Key] = true;
            return enemy.Key;
        }

        public void Spawn()
        {
            var instantiate = _workerSpawner.Spawn();
            var view = instantiate.GetComponent<WorkerView>();
            var movement = instantiate.GetComponent<UnitMovement>();
            var animator = instantiate.GetComponent<UnitAnimation>();
            _workersPool.Add(new WorkerController(new BaseUnitModel(),movement,animator,view),false);
            ReturnToPool(_workersPool.Last().Key);
            SetVisible(_workersPool.Last().Key.WorkerView.transform.gameObject,false);
        }
        
        public void ReturnToPool(WorkerController controller)
        {
            //can place navmeshagent
            _workersPool[controller] = false;
            controller.WorkerView.transform.localPosition = _whereIsABase;
            controller.WorkerView.transform.SetParent(_rootPool);
        }

        public void SetVisible(GameObject gameObject,bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
        
        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }

    }
}