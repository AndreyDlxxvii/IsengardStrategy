using System;
using System.Collections.Generic;
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
        [SerializeField] private UnitUISpawnerTest _unitUISpawnerTest;
        private BaseUnitFactory _baseUnitFactory;
        private List<OutPostUnitController> _outPostUnitControllers;
        public Action<int,Vector3> unitWasSpawned = delegate {  };
        
        #endregion


        #region UnityMethods

        private void Awake()
        {
            _baseUnitFactory = new BaseUnitFactory();
            _unitController.BaseUnitSpawner = this;
            _outPostUnitControllers = new List<OutPostUnitController>();
            _outPostUnitControllers.Add(new OutPostUnitController());
            _outPostUnitControllers[0].UiSpawnerTest = _unitUISpawnerTest;
            
            _outPostUnitControllers[0].Initialize();
            _outPostUnitControllers[0].Transaction += Spawn;
        }

        private void OnDestroy()
        {
            _outPostUnitControllers[0].Transaction -= Spawn;
        }

        #endregion


        #region Methods
        
        public void ShowMenu(OutpostUnitView outpostUnitView)
        {
            _outPostUnitControllers[0].OutpostUnitView = outpostUnitView;
            _outPostUnitControllers[0].UiSpawnerTest.gameObject.SetActive(true);
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
                _unitUISpawnerTest.Model,movementHolder,
                animHolder));
            unitWasSpawned.Invoke(_unitController.GetBaseUnitController().Count-1,endPos);
        }

        #endregion
    }
}