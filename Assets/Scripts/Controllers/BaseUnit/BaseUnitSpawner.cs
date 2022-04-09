using System;
using System.Collections.Generic;
using System.Linq;
using Controllers.OutPost;
using Models.BaseUnit;
using UnityEngine;
using Views.BaseUnit;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers.BaseUnit
{
    public class BaseUnitSpawner: MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private UnitController _unitController;
        [SerializeField] private OutpostSpawner _outpostSpawner;
        private BaseUnitFactory _baseUnitFactory;
        public Action<int,Vector3> unitWasSpawned = delegate {  };
        [NonSerialized]
        public int SpawnIsActiveIndex;
        
        #endregion


        #region UnityMethods

        private void Awake()
        {
            _baseUnitFactory = new BaseUnitFactory();
            _unitController.BaseUnitSpawner = this;
        }

        private void OnDestroy()
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
        }

        public void UnShowMenu()
        {
            _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].Transaction -= Spawn;
            _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].UiSpawnerTest.gameObject.SetActive(false);
            SpawnIsActiveIndex = -1;
        }

        private void Spawn(Vector3 endPos)
        {
            var go = _baseUnitFactory.CreateUnit(_gameObject,_whereToSpawn);
            SendInfoToGroupController(go,endPos);
        }
        
        private void SendInfoToGroupController(GameObject gameObject,Vector3 endPos)
        {
            var movementHolder = gameObject.GetComponent<UnitMovement>();
            var animHolder = gameObject.GetComponent<UnitAnimation>();
            _unitController.GetBaseUnitController().Add(new BaseUnitController(
                _outpostSpawner.OutPostUnitControllers[SpawnIsActiveIndex].UiSpawnerTest.Model,movementHolder,
                animHolder));
            unitWasSpawned.Invoke(_unitController.GetBaseUnitController().Count-1,endPos);
        }

        #endregion
    }
}