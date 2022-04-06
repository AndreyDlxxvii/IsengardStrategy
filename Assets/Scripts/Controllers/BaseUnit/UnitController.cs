using System;
using System.Collections.Generic;
using System.Linq;
using Enums.BaseUnit;
using Models.BaseUnit;
using UnityEngine;
using Views.BaseUnit;
using Random = UnityEngine.Random;

namespace Controllers.BaseUnit
{
    public class UnitController: MonoBehaviour
    {

        #region Fields

        private List<BaseUnitController> _baseUnitControllers;
        private List<UnitMovement> _baseUnitMoves;
        private List<UnitAnimation> _baseUnitAnimations;
        [NonSerialized]
        public BaseUnitSpawner BaseUnitSpawner;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _baseUnitControllers = new List<BaseUnitController>();
            _baseUnitMoves = new List<UnitMovement>();
            _baseUnitAnimations = new List<UnitAnimation>();
        }

        private void Start()
        {
            BaseUnitSpawner.unitWasSpawned += SetCommandLooking;
        }

        private void OnDestroy()
        {
            BaseUnitSpawner.unitWasSpawned -= SetCommandLooking;
        }
        
        #endregion
        
        private void SetCommandLooking()
        {
            _baseUnitControllers.Last().UnitModel.UnitType = UnitType.Worker;
        }

        public List<BaseUnitController> GetBaseUnitController()
        {
            return _baseUnitControllers;
        }
        
        public List<UnitMovement> GetUnitMovementHolder()
        {
            return _baseUnitMoves;
        }
        
        public List<UnitAnimation> GetUnitAnimationsHolder()
        {
            return _baseUnitAnimations;
        }

        private void SetEndPosition()
        {
            view.pointWhereToGo = new Vector3(Random.Range(190f, 210f),0,Random.Range(190f, 210f));
            view.SetThePointWhereToGo();
        }
    }
}