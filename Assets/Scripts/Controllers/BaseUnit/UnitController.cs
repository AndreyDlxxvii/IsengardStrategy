using System;
using System.Collections.Generic;
using Enums.BaseUnit;
using UnityEngine;

namespace Controllers.BaseUnit
{
    public class UnitController: MonoBehaviour
    {

        #region Fields

        private List<BaseUnitController> _baseUnitControllers;
        [NonSerialized] public BaseUnitSpawner BaseUnitSpawner;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _baseUnitControllers = new List<BaseUnitController>();
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
        
        private void SetCommandLooking(int id, Vector3 endPos)
        {
            SetEndPosition(id,endPos);
        }

        public List<BaseUnitController> GetBaseUnitController()
        {
            return _baseUnitControllers;
        }

        private void SetEndPosition(int id, Vector3 endpos)
        {
            _baseUnitControllers[id].SetStateMachine(UnitStates.MOVING);
            _baseUnitControllers[id].UnitMovementView.pointWhereToGo = endpos;
            _baseUnitControllers[id].UnitMovementView.SetThePointWhereToGo();
        }
    }
}