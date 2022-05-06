using System;
using System.Collections.Generic;
using Code.View.ResourcesPlace;
using Controllers.OutPost;
using Controllers.ResouresesPlace;
using Controllers.Worker;
using Data;
using Interfaces;
using Models.BaseUnit;
using UnityEngine;
using Views.BaseUnit;
using Views.Outpost;

namespace Controllers.BaseUnit
{
    public class BaseUnitSpawner: IOnController, IOnStart, IDisposable
    {
        #region Fields
        
        private GameObject _unitPrefab;
        private Vector3 _whereToSpawn;
        private UnitController _unitController;
        private OutpostSpawner _outpostSpawner;
        private readonly ResourcesPlaceSpawner _resourcesPlaceSpawner;
        private BaseUnitFactory _baseUnitFactory;
        private bool _flag;
        public Action<int,List<Vector3>,List<float>> unitWasSpawned;
        public int SpawnIsActiveIndex = -1;

        #endregion


        #region UnityMethods

        public BaseUnitSpawner(GameConfig gameConfig,UnitController unitController, OutpostSpawner outpostSpawner, ResourcesPlaceSpawner resourcesPlaceSpawner,
            GameObject unitPrefab)
        {
            _whereToSpawn = new Vector3(gameConfig.MapSizeX / 2.0f,0,gameConfig.MapSizeY / 2.0f);
            _unitController = unitController;
            _unitController.BaseUnitSpawner = this;
            _outpostSpawner = outpostSpawner;
            _resourcesPlaceSpawner = resourcesPlaceSpawner;
            _unitPrefab = unitPrefab;
        }
        
        public void OnStart()
        {
            _baseUnitFactory = new BaseUnitFactory();
        }

        public void Dispose()
        {
            foreach (var outpost in _outpostSpawner.OutPostUnitControllers)
            {
                outpost.Transaction -= Spawn;
            }
        }

        #endregion


        #region Methods
        
        public void ShowMenu(OutpostUnitView outpostUnitView)
        {
            SpawnIsActiveIndex = outpostUnitView.IndexInArray;
            _outpostSpawner.OutPostUnitControllers[outpostUnitView.IndexInArray].Transaction += Spawn;
            _outpostSpawner.OutPostUnitControllers[outpostUnitView.IndexInArray].UiSpawnerTest.currentController =
                _outpostSpawner.OutPostUnitControllers[outpostUnitView.IndexInArray];
            _outpostSpawner.OutPostUnitControllers[outpostUnitView.IndexInArray].UiSpawnerTest.gameObject.SetActive(true);
            _flag = true;
        }
        public void ShowMenu(ResourcesPlaceView resourcesPlaceView)
        {
            SpawnIsActiveIndex = resourcesPlaceView.IndexInArray;
            _resourcesPlaceSpawner.ResourcesPlaceControllers[resourcesPlaceView.IndexInArray].Transaction += Spawn;
            _resourcesPlaceSpawner.ResourcesPlaceControllers[resourcesPlaceView.IndexInArray].UiSpawnerTest.currentController =
                _resourcesPlaceSpawner.ResourcesPlaceControllers[resourcesPlaceView.IndexInArray];
            _resourcesPlaceSpawner.ResourcesPlaceControllers[resourcesPlaceView.IndexInArray].UiSpawnerTest.gameObject.SetActive(true);
            _flag = true;
        }

        public void UnShowMenu(ISpawnerLogicView spawnerLogicView)
        {
            if (_flag)
            {
                switch (spawnerLogicView)
                {
                    case OutpostUnitView outpostUnitView:
                        Debug.Log("outpostUnitView");
                        _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].Transaction -= Spawn;
                        _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].UiSpawnerTest.gameObject.SetActive(false);
                        /*_resourcesPlaceSpawner.ResourcesPlaceControllers[SpawnIsActiveIndex].UiSpawnerTest
                            .currentController = null;*/
                        // переработать очищение выделенных зданий для данного класса
                        break;
                    case ResourcesPlaceView resourcesPlaceView:
                        Debug.Log("resourcesPlaceView");
                        _resourcesPlaceSpawner.ResourcesPlaceControllers[SpawnIsActiveIndex].Transaction -= Spawn;
                        _resourcesPlaceSpawner.ResourcesPlaceControllers[SpawnIsActiveIndex].UiSpawnerTest.gameObject.SetActive(false);
                        /*_outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].UiSpawnerTest
                            .currentController = null;*/
                        break;
                    default:
                        _resourcesPlaceSpawner.ResourcesPlaceControllers[0].UiSpawnerTest.gameObject.SetActive(false);
                        break;
                }
                SpawnIsActiveIndex = -1;
                _flag = false;
            }
        }

        private void Spawn(Vector3 endPos, IUnitSpawner type)
        {
            var go = _baseUnitFactory.CreateUnit(_unitPrefab,_whereToSpawn);
            SendInfoToGroupController(go,endPos,type);
        }
        
        private void SendInfoToGroupController(GameObject gameObject,Vector3 endPos,IUnitSpawner type)
        {
            var movementHolder = gameObject.GetComponent<UnitMovement>();
            var animHolder = gameObject.GetComponent<UnitAnimation>();
            var listOfUnitC = _unitController.GetBaseUnitController();
            List<float> timers = new List<float>();
            List<Vector3> whereToGo = new List<Vector3>() {endPos};
            switch (type)
            {
                case ResourcesPlaceController resourcesPlaceController:
                    var workerView = gameObject.GetComponent<WorkerView>();
                    listOfUnitC.Add(new WorkerController(
                        _resourcesPlaceSpawner.ResourcesPlaceControllers[SpawnIsActiveIndex].UiSpawnerTest.Model,movementHolder,
                        animHolder,resourcesPlaceController,workerView));
                    whereToGo.Add(resourcesPlaceController.Warehouse.transform.position);
                    timers.Add(resourcesPlaceController.ResurseMine.ExtractionTime);
                    break;
                case OutPostUnitController outPostUnitController:
                    // listOfUnitC.Add(new WorkerController(
                    //     _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].UiSpawnerTest.Model,movementHolder,
                    //     animHolder,_workersBackpack));
                    break;
            }
            listOfUnitC[listOfUnitC.Count-1].OnStart();
            unitWasSpawned.Invoke(listOfUnitC.Count-1,
                whereToGo,
                timers);
        }

        #endregion
    }
}