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
    {//Нужно переписать под свои нужды и все будет ок
        
        private readonly Dictionary<string, HashSet<WorkerController>> _workersPool;
        private readonly int _capacityPool;
        private readonly WorkerSpawner _workerSpawner;
        private readonly WorkersPoolOptions _workersPoolOptions;
        private Transform _rootPool;

        public WorkersPlaceModel(int capacityPool, WorkerSpawner workerSpawner, WorkersPoolOptions workersPoolOptions)
        {
            _workersPool = new Dictionary<string, HashSet<WorkerController>>();
            _capacityPool = capacityPool;
            _workerSpawner = workerSpawner;
            _workersPoolOptions = workersPoolOptions;
            if (!_rootPool)
            {
                _rootPool = new
                    GameObject(NameManager.POOL_OF_WORKERS).transform;
            }
        }
        
        public WorkerController GetWorker(string type)
        {
            WorkerController result;
            switch (type)
            {
                case "Worker":
                    result = GetWorker(GetListWorkers(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type,
                        "Не предусмотрен в программе");
            }
            return result;
        }

        public WorkerController AddAndGetWorker()
        {
            WorkerController result;
            result = AddNewWorker(GetListWorkers("Worker"));
            return result;
        }
        
        private HashSet<WorkerController> GetListWorkers(string type)
        {
            return _workersPool.ContainsKey(type) ? _workersPool[type] :
                _workersPool[type] = new HashSet<WorkerController>();
        }

        private WorkerController AddNewWorker(HashSet<WorkerController> enemies)
        {
            enemies = Spawn(enemies);
            WorkerController enemy = enemies.FirstOrDefault(a => !a.WorkerView.gameObject.activeSelf);
            return enemy;
        }
        
        private WorkerController GetWorker(HashSet<WorkerController> enemies)
        {
            var enemy = enemies.FirstOrDefault(a => !a.WorkerView.gameObject.activeSelf);
            if (enemy == null )
            {
                
                //var laser = Resources.Load<Asteroid>("Enemy/Asteroid");
                for (var i = 0; i < _capacityPool; i++)
                {
                    enemies = Spawn(enemies);
                }
                GetWorker(enemies);
            }
            enemy = enemies.FirstOrDefault(a => !a.WorkerView.gameObject.activeSelf);
            return enemy;
        }

        private HashSet<WorkerController> Spawn(HashSet<WorkerController> enemies)
        {
            var instantiate = _workerSpawner.Spawn();
            var view = instantiate.GetComponent<WorkerView>();
            var movement = instantiate.GetComponent<UnitMovement>();
            var animator = instantiate.GetComponent<UnitAnimation>();
            ReturnToPool(instantiate.transform);
            enemies.Add(new WorkerController(new BaseUnitModel(),movement,animator,view));
            return enemies;
        }
        
        public void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_rootPool);
        }
        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }

    }
}