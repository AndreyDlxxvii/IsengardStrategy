using System;
using System.Collections.Generic;
using Data;
using Enums.BaseUnit;
using Models.BaseUnit;
using UnityEngine;
using UnityEngine.Serialization;
using Views.BaseUnit;
using Views.BaseUnit.UI;

namespace Controllers.BaseUnit
{
    public class BaseUnitSpawner: MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<UnitUISpawnerTest> _uiSpawnerTest;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private UnitController _unitController;
        private BaseUnitFactory _baseUnitFactory;
        public Action unitWasSpawned = delegate {  };

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _baseUnitFactory = new BaseUnitFactory();
            _unitController.BaseUnitSpawner = this;
            foreach (var button in _uiSpawnerTest)
            {
                button.spawnUnit += Spawn;
            }
        }

        private void Spawn(Vector3 endPos)
        {
            var go = _baseUnitFactory.CreateUnit(_gameObject,_whereToSpawn);
            SendInfoToGroupController(go,endPos);
        }

        #endregion


        #region Methods

        private void SendInfoToGroupController(GameObject gameObject,Vector3 endPos)
        {
            var movementHolder = _unitController.GetUnitMovementHolder();
            var animHolder = _unitController.GetUnitAnimationsHolder();
            movementHolder.Add(gameObject.GetComponent<UnitMovement>());
            animHolder.Add(gameObject.GetComponent<UnitAnimation>());
            _unitController.GetBaseUnitController().Add(new BaseUnitController());
            unitWasSpawned.Invoke();
        }

        #endregion
        
    }
}