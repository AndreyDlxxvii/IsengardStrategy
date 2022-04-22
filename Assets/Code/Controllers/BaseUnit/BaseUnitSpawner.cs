using System;
using Controllers.OutPost;
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
        private BaseUnitFactory _baseUnitFactory;
        private bool _flag;
        public Action<int,Vector3> unitWasSpawned;
        public int SpawnIsActiveIndex;

        #endregion


        #region UnityMethods

        public BaseUnitSpawner(GameConfig gameConfig,UnitController unitController, OutpostSpawner outpostSpawner,GameObject unitPrefab)
        {
            _whereToSpawn = new Vector3(gameConfig.MapSizeX / 2.0f,0,gameConfig.MapSizeY / 2.0f);
            _unitController = unitController;
            _unitController.BaseUnitSpawner = this;
            _outpostSpawner = outpostSpawner;
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

        public void UnShowMenu()
        {
            if (_flag)
            {
                _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].Transaction -= Spawn;
                _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].UiSpawnerTest.gameObject.SetActive(false);
                SpawnIsActiveIndex = -1;
                _flag = false;
            }
        }

        private void Spawn(Vector3 endPos)
        {
            var go = _baseUnitFactory.CreateUnit(_unitPrefab,_whereToSpawn);
            SendInfoToGroupController(go,endPos);
        }
        
        private void SendInfoToGroupController(GameObject gameObject,Vector3 endPos)
        {
            var movementHolder = gameObject.GetComponent<UnitMovement>();
            var animHolder = gameObject.GetComponent<UnitAnimation>();
            var listOfUnitC = _unitController.GetBaseUnitController();
            listOfUnitC.Add(new BaseUnitController(
                _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].UiSpawnerTest.Model,movementHolder,
                animHolder));
            unitWasSpawned.Invoke(listOfUnitC.Count-1,endPos);
        }

        #endregion
    }
}